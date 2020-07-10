using RockyHockey.Common;
using RockyHockey.MotionCaptureFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    public static class DirectionForStrategy
    {
        /// <summary>
        /// calculates movement direction the puck needs to follow wanted path
        /// </summary>
        /// <param name="start">position where bat hits the puck</param>
        /// <param name="target">position where the puck should be played to</param>
        /// <param name="bankHitCounter">how often the puck soulh hit the bank on its way</param>
        /// <param name="toXMax">necessary if bankHitCounter > 0; on false: puck hits bank first at x=0; on true: puck hits bank first at x=height</param>
        /// <returns>direction the puck needs</returns>
        public static Vector getPuckDirection(Coordinate start, Coordinate target, int bankHitCounter = 0, bool toXMax = false)
        {
            if (bankHitCounter < 0)
                bankHitCounter = 0;

            Vector direction;

            if (bankHitCounter == 0)
                direction = new Vector(start, target);
            else
            {
                Coordinate pointOnIntersectionLine = getPointOnIntersectionLine(start.Y, target.Y, Math.Abs(start.X - target.X), bankHitCounter, toXMax);

                if (start.X <= target.X)
                    pointOnIntersectionLine.X += start.X;
                else
                    pointOnIntersectionLine.X = start.X - pointOnIntersectionLine.X;

                direction = new Vector(start, pointOnIntersectionLine);
            }

            return direction;
        }

        /* 
         * bsp to fill function call
         * 
         * intendet path:
         * 
         *   x=0   -------------------
         *         |   /\    /\    /\| Target
         *         |  /  \  /  \  /  |
         * Source  |\/    \/    \/   |
         *  x=max  -------------------
         *  
         *  Source and Target are Coordinate objects
         *  
         *  sourceY = Source.Y
         *  targetY = Target.Y
         *  width = |Source.X - Target.X|
         *  
         *      2     4     6 --> bankHitCount=6
         * -------------------
         * |   /\    /\    /\|
         * |  /  \  /  \  /  |
         * |\/    \/    \/   |
         * -------------------
         *   1     3     5    
         *   
         *   |
         *   V
         * 
         * first hit in bank at x=max => toTop=true
         * 
         * 
         * base calculation logic:
         * 
         *       S ---------------- C       [SR] ∩ [TT'] = X
         *         |\             |	
         *         | \            |	    1.  ∠X'XR = ∠RTB = ∠ASR
         *         |  \           |	
         *         |   \          |		    |[X'R]| = |[RB]|
         *         |    \ X       |	
         *      T' |--------------| T	2.	|[AR]|  = |[AX']| +  |[RB]|
         *         |     |\      /|	
         *         |     | \    / |		    |[AB]|  = |[AR]|  +  |[RB]|
         *         |     |  \  /  |	
         *         |     |   \/   |		    |[AB]|  = |[AX']| + 2|[RB]|
         *       A ---------------- B	3.	|[AX']| = |[AB]|  - 2|[RB]|
         *               X'   R
         *
         *       tan(∠RTB) = |[TB]| / |[RB]|
         *
         *       tan(∠ASR) = |[SA]| / |[AR]|
         *      with 1.:
         *       tan(∠RTB) = |[SA]| / |[AR]|
         *
         *       |[TB]| / |[RB]| = |[SA]| / |[AR]|
         *       |[TB]| * |[AR]| = |[SA]| * |[RB]|
         *      with 2.:
         *       |[TB]| * (|[AX']| + |[RB]|) = |[SA]| * |[RB]|
         *      with 3.:
         *       |[TB]| * (|[AB]| - 2|[RB]| + |[RB]|) = |[SA]| * |[RB]|
         *       |[TB]| * (|[AB]| - |[RB]|) = |[SA]| * |[RB]|
         *       |[TB]| * |[AB]| - |[TB]| * |[RB]| = |[SA]| * |[RB]|
         *       |[TB]| * |[AB]| = |[SA]| * |[RB]| + |[TB]| * |[RB]|
         *       |[TB]| * |[AB]| = |[RB]| * (|[SA]| + |[TB]|)
         *      4.
         *       |[RB]| = (|[TB]| * |[AB]|) / (|[SA]| + |[TB]|)
         *       
         * to calculate result for any other bankHitCount => create something similar to the situation above:
         * 
         * -------------------
         * |   /\    /\    /\|
         * |  /  \  /  \  /  |
         * |\/    \/    \/   |
         * -------------------
         *          ||
         *         \||/
         *          \/
         *                                                  S
         * -----------------/|          -----------------/| 
         * |               / |          |             X / |
         * |              /  |        T |-----------------| T'
         * |\            /   |          |\            /|  |
         * --\----------/-----    =>    --\----------/-|---
         *    \        /                   \        /  |
         *     \      /                     \      /   |
         *      \    /                       \    /    |
         *       \  /                         \  /     |
         *        \/                           \/      |
         *                                      R      X'
         */
        private static Coordinate getPointOnIntersectionLine(double sourceY, double targetY, double width, int bankHitCount, bool toTop = false)
        {
            Coordinate retval = new Coordinate();

            double stretchFactor = Math.Ceiling((double)bankHitCount / 2.0f) - 1;
            double reachableHeight = Config.Instance.GameFieldSize.Height - 2 * Config.Instance.PuckRadius;
            double stretchDistance = stretchFactor * reachableHeight;

            if (toTop)
            {
                targetY = Config.Instance.GameFieldSize.Height - targetY;
                sourceY = Config.Instance.GameFieldSize.Height - sourceY;
                retval.Y = stretchDistance + reachableHeight;
            }
            else
                retval.Y = -stretchDistance;

            sourceY += stretchDistance;

            if (bankHitCount % 2 == 0)
                targetY = (stretchFactor + 2) * reachableHeight - targetY;
            else
                targetY += stretchDistance;

            if (sourceY <= targetY)
                retval.X = calcIntersectionPointX(sourceY, targetY, width);
            else
                retval.X = width - calcIntersectionPointX(targetY, sourceY, width);

            return retval;
        }

        private static double calcIntersectionPointX(double shortSide, double longSide, double width)
        {
            return (shortSide * width) / (shortSide + longSide);
        }
    }
}
