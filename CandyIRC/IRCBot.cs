namespace CandyIRC
{
    using System;
    using System.IO;
    using System.Net.Sockets;

    class IRCBot
    {
        private readonly string userNick;

        private readonly string userInfo;

        private readonly string server;

        private readonly string channel;

        private readonly int port;

        public IRCBot(string userNick, string userInfo, string server, string channel, int port = 6667)
        {
            this.userNick = userNick;
            this.userInfo = userInfo;
            this.server = server;
            this.channel = channel;
            this.port = port;
        }

        public void Start(string receiverNick)
        {
            var irc = new TcpClient(server, port);
            var stream = irc.GetStream();
            var reader = new StreamReader(stream);
            var writer = new StreamWriter(stream);

            writer.WriteLine("PING :" + server);
            writer.Flush();

            writer.WriteLine(userInfo);
            writer.Flush();

            writer.WriteLine("NICK " + userNick);
            writer.Flush();

            writer.WriteLine("JOIN " + channel);
            writer.Flush();


            bool done = false;
            string inputLine = null;

            while (true)
            {
                while ((inputLine = reader.ReadLine()) != null)
                {
                    Console.WriteLine(inputLine);

                    if (done) continue;

                    if (inputLine.Contains($"MODE {userNick} +x"))
                    {
                        writer.WriteLine($"PRIVMSG {receiverNick} :!ep1");
                        writer.Flush();
                    }
                    if (inputLine.Contains($"PRIVMSG {userNick} :"))
                    {
                        string[] parts = inputLine.Split(':');
                        parts = parts[2].Split('/');
                        double num1 = double.Parse(parts[0].Trim());
                        double num2 = double.Parse(parts[1].Trim());

                        double original = Math.Sqrt(num1) * num2;

                        double result = Math.Round(original, 2, MidpointRounding.AwayFromZero);

                        Console.WriteLine($"PRIVMSG {receiverNick} :!ep1 -rep {result} (original: {original})");
                        writer.WriteLine($"PRIVMSG {receiverNick} :!ep1 -rep {result}");
                        writer.Flush();
                        done = true;
                    }
                }

                writer.Close();
                reader.Close();
                irc.Close();
            }
        }
    }
}