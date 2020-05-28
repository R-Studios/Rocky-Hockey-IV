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
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    class DetectionTest
    {
        [Test]
        public async Task checkPosition()
        {
            TestImageReader reader = new TestImageReader();
            TimedCoordinate pos;

            pos = await PositionCalculator.ProcessImage(reader.getTimedImage(0), false);
            Assert.True(pos == new Coordinate(368.5f, 168.5f));

            pos = await PositionCalculator.ProcessImage(reader.getTimedImage(2), false);
            Assert.True(pos == new Coordinate(330.5f, 206.5f));

            pos = await PositionCalculator.ProcessImage(reader.getTimedImage(4), false);
            Assert.True(pos == new Coordinate(292.5f, 244.5f));

            pos = await PositionCalculator.ProcessImage(reader.getTimedImage(6), false);
            Assert.True(pos == new Coordinate(254.5f, 282.5f));
        }

        [Test]
        public void checkPositionList()
        {
            ImagePositionCollector collector = new ImagePositionCollector(new TestImageReader());

            List<TimedCoordinate> posList = collector.GetPuckPositions();

            Assert.True(posList.Count == 10);
        }

        [Test]
        public void checkVector()
        {
            VectorCalculationProvider test = new VectorCalculationProvider(new ImagePositionCollector(new TestImageReader()));

            Task<VelocityVector> vecTask = test.CalculatePuckVector();
            vecTask.Wait();

            VelocityVector vec = vecTask.Result;
            int a = 0;
            a++;
        }
    }
}
