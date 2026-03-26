using System;
using System.Collections.Generic;
using System.Text;

namespace GridMap.Shapes
{
    internal abstract class BaseShape
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public List<PointF> Vertices { get; } = new List<PointF>();

        protected BaseShape() { }
        public BaseShape(List<PointF> vertices)
        {
            Vertices = vertices;
        }
    }
}
