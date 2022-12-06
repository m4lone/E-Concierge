using Newtonsoft.Json;
using qodeless.messaging.Worker.Enum;
using System;
using System.Net.Sockets;
using System.Threading;

namespace qodeless.messaging.Worker.Commands
{
    public abstract class BaseCommand<TRequest, TResponse> where TResponse : class where TRequest : class
    {
        public abstract TResponse Run(TcpClient client, byte[] data);

        public TResponse Execute(TcpClient client, byte[] data)
        {
            var response = Run(client, data);            
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(response));
            var stream = client.GetStream();
            stream.Write(msg, 0, msg.Length);
            stream.Flush();
            return response;
        }

        public TRequest Validate(byte[] payload)
        {
            if (payload[(int)EFramePosition.START_MESSAGE] == (byte)EFrameType.STX)
            {
                var messageSize = BitConverter.ToInt32(payload,(int)EFramePosition.BODY_SIZE);
                var objData = System.Text.Encoding.ASCII.GetString(payload,(int)EFramePosition.START_BODY,messageSize);
                return JsonConvert.DeserializeObject<TRequest>(objData);
            }
            return null;            
        }

        public static void PrintLine(string message, ConsoleColor color, ConsoleColor backColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = backColor;
            Console.WriteLine($"THREAD ID: {Thread.CurrentThread.ManagedThreadId} - {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
