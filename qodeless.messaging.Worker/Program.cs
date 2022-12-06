using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using NLog;
using qodeless.application.ViewModels;
using qodeless.messaging.Worker.Commands;
using qodeless.messaging.Worker.Enum;
using qodeless.messaging.Worker.ViewModels.Request;
using qodeless.messaging.Worker.ViewModels.Response;
using RestSharp;

namespace qodeless.messaging.Worker
{
    class Program
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Logger.Info("================== TCP HAS BEEN STARTED ================== ");
            TcpServerConfig.Init();
            new Thread(RunTCPServer).Start();
            //new Thread(RunClient2TestToken).Start();
            //new Thread(RunClient1TestLogin).Start();
            //new Thread(RunPlayTest).Start();
            //new Thread(RunPaymentTest).Start();
        }

        static void RunTCPServer()
        {
            Logger.Info("Wait while it's connecting.");
            TcpListener server = null;
            try
            {
                server = new TcpListener(IPAddress.Parse(TcpServerConfig.Host), TcpServerConfig.Port);
                server.Start();
                var rawdata = new Byte[1024];
                while (true)
                {

                    Console.Write("Waiting for a connection... ");
                    var client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    var stream = client.GetStream();
                    int i;
                    try
                    {
                        while ((i = stream.Read(rawdata, 0, rawdata.Length)) != 0)
                            if (rawdata[(int)EFramePosition.START_MESSAGE] == (byte)EFrameType.STX)
                            {
                                var topic = (ETopic)rawdata[(int)EFramePosition.TOPIC];
                                switch (topic)
                                {
                                    case ETopic.Token:
                                        new TokenCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.Join:
                                        new JoinCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.Login:
                                        new LoginCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.DeviceStatus:
                                        new DeviceStatusCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.DeviceIn:
                                        new DeviceInCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.DeviceOut:
                                        new DeviceOutCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.Play:
                                        new PlayCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.QrCode:
                                        new QrCodeCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.Charge:
                                        new ChargeCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.Payment:
                                        new PaymentCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.PixPaidConfirmation:
                                        new PixPaidConfirmationCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.Register:
                                        new RegisterCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.CompleteRegister:
                                        new CompleteRegisterCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.GetUser:
                                        new GetUserCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.Voucher:
                                        new VoucherCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.CreditPlayer:
                                        new CreditPlayerCommand().Execute(client, rawdata);
                                        break;
                                    case ETopic.GameCoinBet:
                                        new GameCoinBetCommand().Execute(client, rawdata);
                                        break;
                                    default:
                                        break;
                                }
                            }

                            else
                            {
                                byte[] msg = { 0x0E };
                                stream.Write(msg, 0, msg.Length);
                                client.Close();
                                break;
                            }

                        // Shutdown and end connection
                        client.Close();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (SocketException e)
            {
                Logger.Error("Error by running socket", e);
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
        }

        static void RunRGBSocketServer()
        {
            using (var server = new ResponseSocket())
            {
                server.Bind("tcp://*:5555");
                while (true)
                {
                    bool more = true;
                    var topic = server.ReceiveFrameBytes();
                    topic = topic;
                    //switch (topic)
                    //{
                    //    case ETopic.LOGIN:
                    //        new LoginCommand().Execute(server);
                    //        break;
                    //    case ETopic.TOKEN:
                    //        new TokenCommand().Execute(server);
                    //        break;
                    //    default:
                    //        break;
                    //}

                    Thread.Sleep(1);
                }
            }
        }

        #region EMULADOR
        static void RunClient1TestLogin()
        {
            //var playerNumber = "5511987940993";
            //var msgToServer = "AA:00:00:00:00:00:12";
            //using (var client = new RequestSocket())
            //{
            //    client.Connect("tcp://localhost:5555");
            //    for (int i = 0; i < 5; i++)
            //    {
            //        var loginMessage = new LoginRequest(playerNumber, msgToServer);
            //        client
            //            .SendMoreFrame(ETopic.Login)
            //            .SendFrame(JsonConvert.SerializeObject(loginMessage));

            //        var serverResponse = client.ReceiveFrameString();
            //        PrintLine($"Server <> {playerNumber} : {serverResponse}", ConsoleColor.Blue, ConsoleColor.Yellow);
            //        PrintLine($"{playerNumber} <> Server: {msgToServer}", ConsoleColor.White);
            //    }
            //}
        }
        static void RunClient2TestToken()
        {
            //Thread.Sleep(5000);

            //using (var client = new RequestSocket())
            //{
            //    client.Connect("tcp://localhost:5556");

            //    var siteBaseURL = "http://localhost:5000/api/Site/ByCode/RG157BD";

            //    var playerNumber = GetRandomPhoneNumber();
            //    var msg1 = new GameMsg(ETopic.Token).AddPayload(new TokenRequest(playerNumber));

            //    var response1 = new GameClient().Send<TokenResponse>(msg1);
            //    if(response1.Status == ECommandStatus.ACK)
            //    {
            //        var msg2 = new GameMsg(ETopic.Join).AddPayload(new JoinRequest(playerNumber));
            //        var response2 = new GameClient().Send<JoinResponse>(msg2);
            //        if (response2.Status == ECommandStatus.ACK)
            //        {
            //            var session = QRCodeReadByApi(siteBaseURL, response2.AspNetUserId);
            //            var device1 = session.Devices.FirstOrDefault();
            //            var msg3 = new GameMsg(ETopic.DeviceIn).AddPayload(new DeviceInRequest(device1.Id,response2.AspNetUserId));
            //            var response3 = new GameClient().Send<DeviceInResponse>(msg3);
            //            if (response3.Status == ECommandStatus.ACK)
            //            {
            //                Console.WriteLine($"Device In....{device1.SerialNumber}");

            //                var msgPlay = new GameMsg(ETopic.Play)
            //                    .AddPayload(new PlayRequest(
            //                            Guid.NewGuid(),
            //                            response3.SessionDeviceId,
            //                            123,
            //                            123,
            //                            device1.Id,
            //                            device1.GameId,
            //                            session.AccountId,
            //                            response2.AspNetUserId,
            //                            "leiel@eliel.com",
            //                            "255.255.255.255",
            //                            device1.SerialNumber,
            //                            device1.GameName,
            //                            "asdfasdfasdf"
            //                            ));

            //                var responsePlay = new GameClient().Send<PlayResponse>(msgPlay);
            //                if(responsePlay.Status == ECommandStatus.ACK)
            //                {
            //                    Console.WriteLine($"Play From Device....{device1.SerialNumber}");
            //                }

            //                var msg4 = new GameMsg(ETopic.DeviceOut).AddPayload(new DeviceOutRequest(response3.SessionDeviceId,device1.Id, response2.AspNetUserId));
            //                var response4 = new GameClient().Send<DeviceOutResponse>(msg4);
            //                if(response4.Status == ECommandStatus.ACK)
            //                {
            //                    Console.WriteLine($"Device Out....{device1.SerialNumber}");
            //                }
            //            }
            //        }
            //    }                
            //}
        }
        static string GetRandomPhoneNumber()
        {
            const int DIGITS = 4;
            var partialNumber = "";
            for (int i = 0; i < DIGITS; ++i)
                partialNumber += new Random().Next(0, 9).ToString();

            return $"551197777{partialNumber}";
        }
        static void RunClient2TestUnity()
        {
            for (int i = 0; i < 5; i++)
            {
                var playerNumber = $"551198794099{i}";
                SendSmsByTCP(playerNumber);
            }
        }
        static void PrintLine(string message, ConsoleColor color, ConsoleColor backColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = backColor;
            Console.WriteLine($"THREAD ID: {Thread.CurrentThread.ManagedThreadId} - {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void SendSmsByTCP(string to)
        {
            var msg = new GameMsg(ETopic.Token).AddPayload<TokenRequest>(new TokenRequest(to));
            var tcpClient = new GameClient();
            var response = tcpClient.Send<TokenResponse>(msg);
            Console.WriteLine("asdf");
        }
        //static void RunPlayTest()
        //{
        //    Thread.Sleep(10000);
        //    var msg = new GameMsg(ETopic.Play).AddPayload<PlayRequest>(new PlayRequest(
        //      Guid.Parse("86ac088a-35aa-484b-8cf7-d84840e8f912"),
        //      20,//TODO: Mudar para valor em banco
        //      5,//apenas para teste TODO: MUDAR PARA UNIVERSAL
        //      Guid.Parse("2fb500a6-5f1e-46e1-9f43-dbd6b8c1133c"),
        //      Guid.Parse("326f09cc-c24e-4d62-8985-98672034a2e0"), //SiteId
        //      "c4f41668-4b55-4b3b-8df5-137a5996573a"  //PlayerId
        //      ));

        //    var tcpClient = new GameClient();
        //    var response = tcpClient.Send<PlayResponse>(msg);
        //}
        static void RunPaymentTest()
        {
            Thread.Sleep(10000);
            var msg = new GameMsg(ETopic.Payment).AddPayload<PaymentRequest>(new PaymentRequest() 
            {
                SiteId = Guid.Parse("cd562c68-348b-4886-ba9a-718832267296"),
                UserPlayId = "a52734a4-dccc-4b63-865f-2f58381c8479",
                Amount = 1
            });

            var tcpClient = new GameClient();
            var response = tcpClient.Send<PaymentResponse>(msg);
        }
        #endregion
    }

    #region GAME_SERVER_CLIENT

    public class GameMsg
    {
        List<byte> buffer = new List<byte>();
        public GameMsg(ETopic topic)
        {
            buffer.Clear();
            buffer.Insert((byte)EFramePosition.START_MESSAGE, (byte)EFrameType.STX);
            buffer.Insert((byte)EFramePosition.TOPIC, (byte)topic);
        }

        public GameMsg AddPayload<TPlayload>(TPlayload playload) where TPlayload : class
        {
            var binaryData = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(playload));
            var size = BitConverter.GetBytes(binaryData.Length);
            buffer.InsertRange((byte)EFramePosition.BODY_SIZE, size);
            buffer.InsertRange((byte)EFramePosition.START_BODY, binaryData);
            return this;
        }

        public byte[] GetMsg()
        {
            return buffer.ToArray();
        }
    }

    public class GameClient
    {
        private TcpClient client;
        public GameClient()
        {
            client = new TcpClient(TcpServerConfig.Host, TcpServerConfig.Port);
        }

        public TResponse Send<TResponse>(GameMsg msg) where TResponse : class
        {
            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = msg.GetMsg();

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0} bytes", data.Length);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[1024];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();

                return JsonConvert.DeserializeObject<TResponse>(responseData);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

            return null;
        }
    }
    #endregion //GAME_SERVER_CLIENT

    public class TcpServerConfig
    {
        public static void Init()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();
            Host = config.GetSection("TcpServerConfig")["Host"];
            Port = Convert.ToInt32(config.GetSection("TcpServerConfig")["Port"]);
        }
        public static string Host { get; private set; }
        public static int Port { get; private set; }
    }
}
