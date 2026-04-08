using System;
using System.Collections.Generic;
using System.Text;

namespace GridMap.Shapes
{
    internal class GosperShape : BaseShape
    {
        public GosperShape() { }

        public GosperShape(List<PointF> vertices) : base(ValidateVerticesNumber(vertices)) { }

        public static GosperShape FromVertex(PointF vertex, double hexagonSide, int depth, double rotation = 0)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(depth);
            HexagonShape hexagon = HexagonShape.FromVertex(vertex, hexagonSide, rotation);
            List<PointF> vertices = RefineGosper(hexagon.Vertices, depth);
            return new GosperShape(vertices);
        }

        public static GosperShape FromCenter(PointF center, double hexagonSide, int depth, double rotation = 0)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(depth);
            HexagonShape hexagon = HexagonShape.FromCenter(center, hexagonSide, rotation);
            List<PointF> vertices = RefineGosper(hexagon.Vertices, depth);
            return new GosperShape(vertices);
        }

        private static List<PointF> ValidateVerticesNumber(List<PointF> vertices)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }
            if (!IsValidGosperPointCount(vertices.Count))
            {
                throw new ArgumentException("Points count must be 6 * 3^n");
            }

            return vertices;
        }

        private static bool IsValidGosperPointCount(int value)
        {
            if (value <= 0)
            {
                return false;
            }
            if (value % 6 != 0)
            {
                return false;
            }
            value /= 6;
            while (value % 3 == 0)
            {
                value /= 3;
            }
            return value == 1;
        }

        public static List<PointF> RefineGosper(List<PointF> vertices, int depth)
        {
            int verticesCount = vertices.Count;
            List<PointF> gosperVertices = new List<PointF>(verticesCount * 3);
            for (int i = 0; i < verticesCount; i++)
            {
                gosperVertices.Add(vertices[i]);
                (PointF first, PointF second) = GosperSegment(vertices[i], vertices[(i + 1) % verticesCount]);
                gosperVertices.Add(first);
                gosperVertices.Add(second);
            }
            if (--depth == 0)
            {
                return gosperVertices;
            }
            else
            {
                return RefineGosper(gosperVertices, depth);
            }
        }

        private static (PointF first, PointF second) GosperSegment(PointF start, PointF end)
        {
            double side = SegmentDistance(start, end);
            double vectorAngle = VectorAngle(start, end);

            vectorAngle += Math.Asin(Math.Sqrt(3) / (2 * Math.Sqrt(7)));
            PointF first = NextPoint(start, side, vectorAngle);

            vectorAngle -= Math.PI / 3;
            PointF second = NextPoint(first, side, vectorAngle);

            return (first, second);
        }

        private static double SegmentDistance(PointF start, PointF end)
        {
            double dx = start.X - end.X;
            double dy = start.Y - end.Y;

            return Math.Sqrt(dx * dx + dy * dy) / Math.Sqrt(7);
        }

        private static double VectorAngle(PointF start, PointF end)
        {
            double dx = end.X - start.X;
            double dy = end.Y - start.Y;

            return Math.Atan2(dy, dx);
        }

        private static PointF NextPoint(PointF previous, double side, double vectorAngle)
        {
            return new PointF((float)(previous.X + side * Math.Cos(vectorAngle)), (float)(previous.Y + side * Math.Sin(vectorAngle)));
        }
    }
}
