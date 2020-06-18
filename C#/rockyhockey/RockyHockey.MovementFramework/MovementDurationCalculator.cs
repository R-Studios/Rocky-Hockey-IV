using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MovementFramework
{
    static class MovementDurationCalculator
    {
        public static long getMovementDuration(Coordinate start, Coordinate end)
        {
            return getMovementDuration(new Vector(start, end));
        }

        public static long getMovementDuration(Vector movement)
        {
            double length = movement.Length * Config.Instance.SizeRatio;

            return (long)(length / Config.Instance.MaxBatVelocity);
        }

        public static long getMovementDuration(IEnumerable<Coordinate> positions)
        {
            return getMovementDuration(positions.ToArray());
        }

        public static long getMovementDuration(Coordinate[] positions)
        {
            long duration = 0;

            for (int a = 1; a < positions.Length; a++)
            {
                duration += getMovementDuration(positions[a - 1], positions[a]);
            }

            return duration;
        }
    }
}
