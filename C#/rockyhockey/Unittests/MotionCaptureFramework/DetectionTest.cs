using Emgu.CV;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RockyHockey.Common;
using RockyHockey.MoveCalculationFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    class DetectionTest
    {
        [Test]
        public void aConfigInitialisationTest()
        {
            Config config = Config.Instance;
        }


        [Test]
        public void checkPosition()
        {
            TestImageReader reader = new TestImageReader();
            ImagePositionCollector collector = new ImagePositionCollector(reader);

            List<TimedCoordinate> posList = collector.GetPuckPositions();

            Assert.True(posList.Count == 10);

            Assert.True(posList[2] == new Coordinate(368.5f, 168.5f));

            Assert.True(posList[4] == new Coordinate(330.5f, 206.5f));

            Assert.True(posList[6] == new Coordinate(292.5f, 244.5f));

            Assert.True(posList[8] == new Coordinate(254.5f, 282.5f));
        }

        [Test]
        public void checkVector()
        {
            PositionCollector collector = new ImagePositionCollector(new TestImageReader());
            VectorCalculationProvider test = new VectorCalculationProvider(collector);

            VelocityVector vec = test.CalculatePuckVector();
        }

        [Test]
        public void direct_pathPrediction()
        {
            new PathPrediction(new TestImageReader()).init();
        }

        [Test]
        public void reflected_pathPrediction()
        {
            new PathPrediction(new TestImageReader().setcounter(0)).init();
        }

        [Test]
        public void testPathSimulation()
        {
            SimulationPositionCollector test = new SimulationPositionCollector(new Coordinate(407, 131), new Coordinate(406, 132), 0.03125, 20);
            PathPrediction predictionTest = new PathPrediction(test);
            predictionTest.init();

            //allows some validation cycles
            Thread.Sleep(100);

            predictionTest.finalize();
        }
    }
}
