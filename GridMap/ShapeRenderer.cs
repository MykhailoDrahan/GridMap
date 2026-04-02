using GridMap.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GridMap
{
    internal class ShapeRenderer
    {
        public Brush Brush { get; set; } = Brushes.Black;
        public float Thickness { get; set; } = 1.0f;
        public ShapeRenderer() { }

        public void DrawShape(Graphics g, BaseShape shape)
        {
            using (Pen pen = new Pen(Brush, Thickness)) 
            {
                int count = shape.Vertices.Count;

                for (int i = 0; i < count; i++)
                {
                    g.DrawLine(pen, shape.Vertices[i], shape.Vertices[(i + 1) % count]);
                }                
            }
        }
    }
}
