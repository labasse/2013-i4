using System;

namespace HelloBot2
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloBot bot = new HelloBot("EPSI");

            // TODO : Ask name to user
            bot.SayHello("Sylvain");
            bot.SayGoodBye("Sylvain");
            Console.ReadLine();
        }
    }
}
