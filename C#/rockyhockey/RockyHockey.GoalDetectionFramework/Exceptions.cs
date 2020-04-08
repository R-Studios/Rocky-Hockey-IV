using System;

namespace RaspberryGPIOManager
{
    class PinAlreadyExportedException : Exception
    {
        public PinAlreadyExportedException(): base()
        { }

        public PinAlreadyExportedException(string message): base(message)
        { }
    }
}