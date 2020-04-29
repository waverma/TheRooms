using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheRooms.MFUGE
{
    public class PathFinder
    {
        private static Vector[] NeigborVector = new[]
        {
            new Vector(-1, -1), 
            new Vector(-1, 0), 
            new Vector(-1, 1), 
            new Vector(1, -1), 
            new Vector(1, 0), 
            new Vector(1, 1), 
            new Vector(0, -1), 
            new Vector(0, 1)
        };
        public static SinglyLinkedList<Vector> GetOrdinaryPath(Area area, Vector start, Vector end)
        { // TEST ME
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
        { // TEST ME // Fix me
            var list = ordinaryPath.ToList();
            list.Reverse();

            var currentPosition = ordinaryPath.FirstOrDefault();
            var result = new List<Vector>
            {
                currentPosition
            };

            while(true)
            {
                foreach (var vector in list.Where(vector => !IsIntersect(area, currentPosition, vector)))
                {
                    result.Add(vector);
                    currentPosition = vector;
                    break;
                }

                if (list.FirstOrDefault() == currentPosition)
                    return list;
            }
        }

        private static bool IsIntersect(Area area, Vector start, Vector end)
        {
            var a = start.Y - end.Y;
            var b = end.X - start.X;
            var c = start.X * end.Y - end.X * start.Y;
            if (a == b && a == 0)
                throw new ArgumentException();
            var temp = start;
            while (temp == end)
            {
                if (area.Map[temp.X, temp.Y].Creature != null || area.Map[temp.X, temp.Y] == null)
                    return false;
                var isStep = true;
                foreach (var vectorAdd in NeigborVector)
                {
                    var tempTemp = vectorAdd + temp;
                    if (!area.InBounds(tempTemp) || tempTemp.X * a + tempTemp.Y * b + c != 0) 
                        continue;
                    temp = tempTemp;
                    isStep = false;

                }
                
                if (isStep) return false;
            }

            return true;
        }

        private static IEnumerable<Vector> GetIncidentPoint(Area area, Vector point)
        { // Fix me
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
    }
}
