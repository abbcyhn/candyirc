namespace CandyIRC
{
    class Program
    {
        static void Main()
        {
            var ircBot = new IRCBot(
                userNick: "OsminoqBot",
                userInfo: "USER CSharpBot 8 *:I'm a C# irc bot",
                channel: "#root-me_challenge",
                server: "irc.root-me.org",
                port: 6667
            );

            ircBot.Start(receiverNick: "CANDY");
        }
    }
}
