using NUnit.Framework;
using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    class PuckMotionInstrategy
    {

        [Test]
        public void testDirectionForStrategy()
        {
            /* 
             *    first result
             *    
             * -5         /\              
             * -4        /  \             
             * -3       /    \            
             * -2      /      \           
             * -1 ------------------------
             *  1 |  /\        /\        |
             *  2 |    \      /  \       |
             *  3 |     \    /    \      |
             *  4 |      \  /      \  /  |
             *  5 |       \/        \/   |
             *  6 ------------------------
             *  7           \      /      
             *  8            \    /       
             *  9             \  /        
             * 10              \/         
             * 
             *         second result
             */
            Config config = Config.Instance;
            Config.Instance.GameFieldSize = new System.Drawing.Size(22, 5);
            Coordinate start = new Coordinate(2, 1);
            Coordinate end = new Coordinate(20, 3);
            Vector testVec = DirectionForStrategy.getPuckDirection(start, end, 4);
            Assert.AreEqual(testVec.End, new Coordinate(8, -5));
            testVec = DirectionForStrategy.getPuckDirection(end, start, 4, true);
            Assert.AreEqual(testVec.End, new Coordinate(13, 10));
        }
    }
}
