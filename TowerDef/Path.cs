using System.Collections.Generic;
using OpenTK;

namespace TowerDef
{
    struct Point
    {
        public Vector2d Coords;
        public double Course;
        public Point(Vector2d Coords, double Course)
        {
            this.Coords = Coords;
            this.Course = Course;
        }
    }

    class Path
    {
        public List<Point> Points;

        public Path()
        {
            Points = new List<Point>();
        }

        public void AddPoint(Vector2d PT)
        {
            int c = Points.Count;
            if (c == 0)
            {
                Points.Add(new Point(PT, 0));
            }
            else
            {
                Point P = Points[c - 1];
                double dist = (PT - P.Coords).Length;
                Points.Add(new Point(PT, P.Course + dist));
            }
        }

        public Vector2d GetPosition(double time, double speed)
        {
            double ElapsedLength = time * speed;
            double segm = 0;
            int index = 0;
            Vector2d result = Vector2d.Zero;
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Course > ElapsedLength)
                    break;
                segm = (ElapsedLength - Points[i].Course);
                index++;
            }
            if (index == Points.Count)//last point, out of the track
                return result;
            if (index == Points.Count - 1)
            {
                ;
            }
            Vector2d vec = Points[index].Coords - Points[index - 1].Coords;
            Vector2d start = Points[index - 1].Coords;
            vec.Normalize();
            vec = Vector2d.Multiply(vec, segm);
            result = Vector2d.Add(start, vec);
            return result;
        }
    }
}
