using Emgu.CV;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RockyHockey.Common;
using RockyHockey.MoveCalculationFramework;
using RockyHockeyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            ImageReader reader = new ImageReader("P:\\testImages\\", -2, 12);
            ImagePositionCollector collector = new ImagePositionCollector(reader);

            List<TimedCoordinate> posList = collector.GetPuckPositions();

            Assert.True(posList.Count == 10);

            Assert.True(posList[2] == new Coordinate(368.5f, 168.5f));

            Assert.True(posList[4] == new Coordinate(330.5f, 206.5f));

            Assert.True(posList[6] == new Coordinate(292.5f, 244.5f));

            Assert.True(posList[8] == new Coordinate(254.5f, 282.5f));
        }

        [Test]
        public void direct_pathPrediction()
        {
            new PathPrediction(new ImageReader("P:\\testImages\\", -2, 12)).init();
        }

        [Test]
        public void reflected_pathPrediction()
        {
            ImageReader reader = new ImageReader("P:\\testImages\\", -2, 12);
            reader.setFrameIndex(0);
            new PathPrediction(reader).init();
        }

        [Test]
        public void testPathSimulation()
        {
            SimulationPositionCollector test = new SimulationPositionCollector(new Coordinate(407, 131), new Coordinate(406, 132), 0.03125, 20);
            PathPrediction predictionTest = new PathPrediction(test);
            predictionTest.init();
        }

        [Test]
        public void testZeug()
        {
            SimulationPositionCollector test = new SimulationPositionCollector(new Coordinate(407, 131), new Coordinate(406, 132), 1000);

            TimedCoordinate first = test.GetPuckPosition();
            TimedCoordinate second = test.GetPuckPosition();

            TimedVector testVec = new TimedVector(first, second);
        }
    }
}
