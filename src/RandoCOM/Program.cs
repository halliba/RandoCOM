using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace RandoCOM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args?.Length <= 0)
                {
                    Console.WriteLine("No COM Port");
                    return;
                }

                var portName = args[0];
                var ports = SerialPort.GetPortNames();
                if (!ports.Contains(portName))
                {
                    Console.WriteLine("Can not find Port " + portName);
                    return;
                }

                var port = new SerialPort(portName);
                port.Open();

                var buffer = new byte[256];
                var random = new Random();
                var count = 0;

                while (true)
                {
                    random.NextBytes(buffer);
                    port.Write(buffer, 0, buffer.Length);
                    count += buffer.Length;
                    Console.CursorLeft = 0;
                    Console.CursorTop = 0;
                    Console.WriteLine($"Total Bytes Written: {count:n0}");
                    Thread.Sleep(300);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("An Exception occured:" + Environment.NewLine + e.GetType() + Environment.NewLine + e.Message);
            }
        }
    }
}