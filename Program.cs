using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CbtcTcp
{
    class CbtcTcp
    {
        Thread thread1;
        Thread thread2;


        // Main Method
        static void Main(string[] args)
        {

            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);
            Socket listener = new Socket(ipAddr.AddressFamily,
                             SocketType.Stream, ProtocolType.Tcp);


            try
            {


                /* IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                 IPAddress ipAddr = ipHost.AddressList[0];
                 IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

                Socket listener = new Socket(ipAddr.AddressFamily,
                              SocketType.Stream, ProtocolType.Tcp);*/


                listener.Bind(localEndPoint);

                while (true)
                {

                    Console.WriteLine("Waiting connection ... ");
                    listener.Listen(10);
                    Socket clientSocket = listener.Accept();

                    Thread thread1 = new Thread(ExecuteServer);
                    Thread thread2 = new Thread(Responsive);

                    thread1.Start(clientSocket);
                    Thread.Sleep(1000);
                    thread2.Start(clientSocket);


                    //Console.WriteLine(thread1.ThreadState);



                }
            }
            catch (Exception e)
            {


                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("main finally girdi...");

                if (listener != null)
                {
                    listener.Close();



                }
            }




            //ExecuteServer();
        }


        public static void Responsive(object argument)
        {
            Socket clientSocket2 = (Socket)argument;
            string msj = "";

            try
            {
                while (true)
                {
                    if (clientSocket2.Connected == false)
                    {
                        break;
                    }

                    Console.WriteLine("mesajı giriniz...");

                    msj = Console.ReadLine();

                    if (msj.Equals("exit")||msj.Equals(""))
                    {
                        clientSocket2.Shutdown(SocketShutdown.Both);
                        clientSocket2.Close();
                        /* Console.WriteLine("Thread Durumu: " + t.ThreadState);
                         thread1.Interrupt();
                         Console.WriteLine("Thread Kapandı");
                         Console.WriteLine("Thread Durumu: " + t.ThreadState);*/
                        break;
                    }
                    byte[] message = Encoding.ASCII.GetBytes(msj);

                    if(message == null)
                    {
                        Console.WriteLine("Mesaj null");
                        break;
                    }
                    if (clientSocket2.Available != 0)
                    {
                        string reMessage = msj;
                        byte[] reMessageByte = Encoding.ASCII.GetBytes(reMessage);
                        clientSocket2.Send(reMessageByte);

                    }
                    clientSocket2.Send(message);
                    
                }
            }
            catch (Exception e)
            {
                //clientSocket2.EndAccept();
               // clientSocket2.Shutdown(SocketShutdown.Both);
               // clientSocket2.Close();
                Console.WriteLine(e.ToString());
            }
            /*finally
            {
                Console.WriteLine("ExecuteServer finally girdi...");

                if (clientSocket2 != null)
                {

                    clientSocket2.Shutdown(SocketShutdown.Both);
                    clientSocket2.Close();


                }
            }*/


        }

        public static void ExecuteServer(object argument)
        {
            Socket clientSocket1 = (Socket)argument;
            MessageEntity messageEntity = new MessageEntity();

            try
            {


                while (true)
                {
                    if (clientSocket1.Connected == false)
                    {
                        break;
                    }

                    Console.WriteLine("connected...  while girdi... ");



                    byte[] bytes = new Byte[1024];
                    string data = null;

                    //string msj = "";



                    int numByte = clientSocket1.Receive(bytes);

                    data += Encoding.ASCII.GetString(bytes,
                                               0, numByte);
                    messageEntity.message = data;
                    EndPoint localEndPoint = clientSocket1.LocalEndPoint;
                  //  messageEntity.ip = localEndPoint.AddressFamily;

                    
                    Console.WriteLine(localEndPoint);
                    Console.WriteLine(messageEntity.dateTime);
                    Console.WriteLine(messageEntity.Id + messageEntity.message);



                    /*Console.WriteLine("mesajı giriniz...");

                    msj = Console.ReadLine();

                 


                    byte[] message = Encoding.ASCII.GetBytes(msj);


                    clientSocket.Send(message);*/
                    if (numByte==0)
                    {

                        clientSocket1.Shutdown(SocketShutdown.Both);
                        clientSocket1.Close();
                        break;
                    }
                    if (clientSocket1.Available != 0)
                    {
                        string reMessage = data;
                        byte[] reMessageByte = Encoding.ASCII.GetBytes(reMessage);
                        clientSocket1.Send(reMessageByte);

                    }

                }

                //clientSocket.Shutdown(SocketShutdown.Both);
                //clientSocket.Close();

            }

            catch (Exception e)
            {

                clientSocket1.Shutdown(SocketShutdown.Both);
                clientSocket1.Close();
                Console.WriteLine(e.ToString());
            }
           /* finally
            {
                Console.WriteLine("ExecuteServer finally girdi...");

                if (clientSocket1 != null)
                {

                    clientSocket1.Shutdown(SocketShutdown.Both);
                    clientSocket1.Close();


                }
            }*/

        }

    }
}


