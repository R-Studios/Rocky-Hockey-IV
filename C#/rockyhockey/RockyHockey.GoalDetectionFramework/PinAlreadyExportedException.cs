using System;

namespace RockyHockey.GoalDetectionFramework
{
    class PinAlreadyExportedException : Exception
    {
        public PinAlreadyExportedException(): base()
        {

        }

        public PinAlreadyExportedException(string message): base(message)
        {

        }
    }
}