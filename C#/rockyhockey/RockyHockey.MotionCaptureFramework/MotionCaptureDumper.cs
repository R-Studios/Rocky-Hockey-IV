using RockyHockey.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.MotionCaptureFramework
{
    /// <inheritdoc />
    /// <summary>
    /// Implementierung des MotionCaptureProvider
    /// </summary>
    public class MotionCaptureDumper
    {
        static Queue<string> strings = new Queue<string>();
        static StreamWriter writer = new StreamWriter(File.OpenRead("positiondump.txt"), Encoding.UTF8);

        public static void AddCapture(string str)
        {
            lock (strings)
            {
                strings.Enqueue(str);
            }
        }

        public static void Run()
        {
            while (true)
            {
                lock (strings)
                {
                    while (strings.Count > 0)
                    {
                        writer.WriteLine(strings.Dequeue());
                    }
                }
            }
        }
    }
}
