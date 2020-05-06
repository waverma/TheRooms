using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TheRooms.MFUGE
{
    public class PathFinder
    {
        public static SinglyLinkedList<Vector> GetOrdinaryPath(Area area, Vector start, Vector end)
        {
            if (!Area.InBounds(area, start) || !Area.InBounds(area, end))
                return null;

            var queue = new Queue<SinglyLinkedList<Vector>>();
            var visited = new HashSet<Vector>();
            var result = default(SinglyLinkedList<Vector>);

            queue.Enqueue(new SinglyLinkedList<Vector>(start));
            while (queue.Count != 0)
            {
                var currentPoint = queue.Dequeue();
                if (visited.Contains(currentPoint.Value))
                    continue;
                visited.Add(currentPoint.Value);

                if (currentPoint.Value == end)
                {
                    result = currentPoint;
                    break;
                }

                foreach (var nextPoint in GetIncidentPoint(area, currentPoint.Value))
                    queue.Enqueue(new SinglyLinkedList<Vector>(nextPoint, currentPoint));
            }

            return result;
        }

        public static IReadOnlyList<Vector> GetFlattenedPath(Area area, IReadOnlyList<Vector> ordinaryPath)
        {
            var list = ordinaryPath.ToList();
            //list.Reverse();

            var currentPosition = ordinaryPath.FirstOrDefault();
            var result = new List<Vector>
            {
                currentPosition
            };

            while (true)
            {
                foreach (var vector in list.Skip(1).Where(vector => !IsIntersect(area, currentPosition, vector)))
                {
                    result.Add(vector);
                    currentPosition = vector;
                    break;
                }

                if (list.FirstOrDefault() == currentPosition)
                    return result;
            }
        }

        private static bool IsIntersect(Area area, Vector start, Vector end)
        {
            throw new NotImplementedException();
        }

        private bool IsIntersect(Vector fs, Vector fe, Vector ss, Vector se)
        { // TODO HELP ME
            var firstLine = new StraightLineEquation(fs, fe);
            var secondLine = new StraightLineEquation(ss, se);

            if (firstLine.A == 0 && secondLine.A == 0 || firstLine.B == 0 && secondLine.B == 0) 
                return firstLine.IsCoincidesWith(secondLine);
            var point = GetIntersectPoint(firstLine, secondLine);

            return point.IsBetween(fs.ToPoint(), fe.ToPoint()) 
                   && point.IsBetween(ss.ToPoint(), se.ToPoint());
        }

        private Point GetIntersectPoint(StraightLineEquation f, StraightLineEquation s)
        { // TODO HELP ME
            var x = (f.B * s.C - s.B * f.C) / (f.A * s.B - s.A * f.B);
            var y = (s.A * f.C - f.A * s.C) / (f.A * s.B - s.A * f.B);

            return new Point(x, y);
        }

        private static IEnumerable<Vector> GetIncidentPoint(Area area, Vector point)
        { // TODO Fix me
            for (var i = -1; i <= 1; i++)
                for (var j = -1; j <= 1; j++)
                {
                    if ((i != 0 || j == 0) && (i == 0 || j != 0))
                        continue;

                    var newPoint = new Vector(point.X + i, point.Y + j);
                    if (area.InBounds(point) && area.Map[point.X, point.Y].IsEmpty())
                        yield return newPoint;
                }
        }



        private class StraightLineEquation
        {
            public readonly int A;
            public readonly int B;
            public readonly int C;

            public StraightLineEquation(Vector first, Vector second)
            {
                A = first.Y - second.Y;
                B = second.X - first.X;
                C = first.X * second.Y - first.Y * second.X;

                if (A == B && A == 0) throw new ArgumentException();
            }

            public bool IsCoincidesWith(StraightLineEquation other)
            {
                var fb = -(A / B);
                var sb = -(other.A / other.B);

                return fb * sb > 0 && C * other.B == other.C * B;
            }
        }
    }
}
