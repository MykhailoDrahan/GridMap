using System;
using System.Collections.Generic;
using System.Text;

namespace GridMap.Shapes
{
    internal class HexagonShape : BaseShape
    {
        public HexagonShape() { }
        public HexagonShape(List<PointF> vertices) : base(ValidateVerticesNumber(vertices))
        {

        }

        public static HexagonShape FromVertex(PointF vertex, double side, double rotation = 0)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(side);
            List<PointF> vertices = GenerateFromVertex(vertex, side, rotation);
            return new HexagonShape(vertices);
        }

        public static HexagonShape FromCenter(PointF center, double radius, double rotation = 0)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(radius);
            PointF vertex = CalculateStartVertex(center, radius, rotation);
            List<PointF> vertices = GenerateFromVertex(vertex, radius, rotation);
            return new HexagonShape(vertices);
        }

        private static List<PointF> ValidateVerticesNumber(List<PointF> vertices)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }
            if (vertices.Count != 6)
            {
                throw new ArgumentException("Hexagon must have exactly 6 vertices");
            }

            return vertices;

        }

        private static List<PointF> GenerateFromVertex(PointF vertex, double side, double rotation)
        {
            List<PointF> vertices = new List<PointF>(6);
            double vectorAngle = rotation;
            vertices.Add(vertex);
            for (int i = 0; i < 5; i++)
            {
                vertex = new PointF(vertex.X + (float)(side * Math.Cos(vectorAngle)), vertex.Y + (float)(side * Math.Sin(vectorAngle)));
                vectorAngle += Math.PI / 3;
                vertices.Add(vertex);
            }
            return vertices;
        }

        private static PointF CalculateStartVertex(PointF center, double radius, double rotation)
        {
            double radiusAngle = -2 * Math.PI / 3 + rotation;
            PointF vertex = new PointF(center.X + (float)(radius * Math.Cos(radiusAngle)), center.Y + (float)(radius * Math.Sin(radiusAngle)));
            return vertex;
        }

    }
}
