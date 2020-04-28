using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRooms.MFUGE
{
    public class PathFinder
    {
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
            throw new NotImplementedException();
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
