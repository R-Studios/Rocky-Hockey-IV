using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MoveCalculationFramework
{
    interface StrategyCommunicationInterface
    {
        void setXCoordinate(double x);

        Task startCalculation();

        bool enoughTimeLeft();

        void execute();
    }
}
