using Emgu.CV;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RockyHockey.Common;
using RockyHockey.MoveCalculationFramework;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Assert.True(pos == new Coordinate(334.5f, 203.5f));

            pos = await PositionCalculator.ProcessImage(reader.getTimedImage(4), false);
            Assert.True(pos == new Coordinate(292.5f, 244.5f));

            pos = await PositionCalculator.ProcessImage(reader.getTimedImage(6), false);
            Assert.True(pos == new Coordinate(249.5f, 286.5f));
        }

        [Test]
        public void checkPositionList()
        {
            PositionCollector collector = new PositionCollector(new TestImageReader());

            List<TimedCoordinate> posList = collector.GetPuckPositions();

            Assert.True(posList.Count == 8);
        }

        [Test]
        public void checkVector()
        {
            VectorCalculationProvider test = new VectorCalculationProvider(new PositionCollector(new TestImageReader()));

            Task<VelocityVector> vecTask = test.CalculatePuckVector();
            vecTask.Wait();

            VelocityVector vec = vecTask.Result;
            int a = 0;
            a++;
        }

        [Test]
        public async Task detectPosition()
        {
            TestTrajectoryCalculationFramework test = new TestTrajectoryCalculationFramework();
            await test.BeginCalculationLoop(null).ConfigureAwait(false);
        }
    }
}
