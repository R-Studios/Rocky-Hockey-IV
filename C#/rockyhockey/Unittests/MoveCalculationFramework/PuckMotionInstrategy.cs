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
            Config config = Config.Instance;
            Config.Instance.GameFieldSize = new System.Drawing.Size(22, 5);

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
            Coordinate start = new Coordinate(2, 1);
            Coordinate end = new Coordinate(20, 3);
            Vector testVec = DirectionForStrategy.getPuckDirection(start, end, 4);
            Assert.AreEqual(testVec.End, new Coordinate(8, -5));
            testVec = DirectionForStrategy.getPuckDirection(end, start, 4, true);
            Assert.AreEqual(testVec.End, new Coordinate(13, 10));

            /* 
             *      first & second result
             *    
             * -5             /\              
             * -4            /  \             
             * -3           /    \            
             * -2          /      \           
             * -1 ------------------------
             *  1 |      /\        /\    |
             *  2 |     /  \      /  \   |
             *  3 |    /    \    /    \  |
             *  4 |   /      \  /        |
             *  5 |  /        \/         |
             *  6 ------------------------
             */
            start = new Coordinate(2, 5);
            end = new Coordinate(20, 3);
            testVec = DirectionForStrategy.getPuckDirection(start, end, 3);
            Assert.AreEqual(testVec.End, new Coordinate(12, -5));
            testVec = DirectionForStrategy.getPuckDirection(end, start, 3);
            Assert.AreEqual(testVec.End, new Coordinate(12, -5));

            /*        
             * -1 ------------------------
             *  1 |          /\          |
             *  2 |  \      /  \      /  |
             *  3 |   \    /    \    /   |
             *  4 |    \  /      \  /    |
             *  5 |     \/        \/     |
             *  6 ------------------------
             *  7         \      /      
             *  8          \    /       
             *  9           \  /        
             * 10            \/         
             * 
             *      first & second result
             */
            start = new Coordinate(2, 1);
            end = new Coordinate(20, 1);
            testVec = DirectionForStrategy.getPuckDirection(start, end, 3, true);
            Assert.AreEqual(testVec.End, new Coordinate(11, 10));
            testVec = DirectionForStrategy.getPuckDirection(end, start, 3, true);
            Assert.AreEqual(testVec.End, new Coordinate(11, 10));
        }
    }
}
