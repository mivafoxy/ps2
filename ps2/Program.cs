using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ps2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TcpClient client = new TcpClient();

            string ipAddress;
            int port;
            int milliseconds;

            try
            {
                ipAddress = args[0];
                port = int.Parse(args[1]);
                milliseconds = int.Parse(args[2]);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Need 3 args: host, port and millis between spam iters.");
                return;
            }

            //client.Connect("127.0.0.1", 4063);

            client.Connect(ipAddress, port);


            var rand = new Random();

            for (var i = 0; i < 1_000_000; i++)
            {
                var package = new List<byte>()
                {
                    0x31, 0x31, 0x31, 0x31, 0x31, 0x32, 0x32, 0x32, 0x32, 0x32
                };

                var temperature = (float)rand.Next(0, 180) + (float)rand.NextDouble();
                Console.WriteLine(temperature);

                var humidity = (float)rand.Next(0, 100) + (float)rand.NextDouble();
                Console.WriteLine(humidity);

                var rain = (float)rand.Next(0, 100) + (float)rand.NextDouble();
                Console.WriteLine(rain);

                var propane = (float)rand.Next(0, 100) + (float)rand.NextDouble();
                Console.WriteLine(propane);

                var butane = (float)rand.Next(0, 100) + (float)rand.NextDouble();
                Console.WriteLine(butane);

                byte[] values = Encoding.UTF8.GetBytes($"T{temperature}H{humidity}RD{rain}MQ2P{propane}MQ2B{butane}");

                package.AddRange(values);

                var stream = client.GetStream();
                stream.Write(package.ToArray());

                Task.Delay(milliseconds).Wait();
            }
        }
    }
}
