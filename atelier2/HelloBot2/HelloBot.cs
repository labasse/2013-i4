using System;

namespace HelloBot2
{
    public class HelloBot
    {
        private string prefix;

        public HelloBot(string prefix)
        {
            this.prefix = prefix;
        }
        public void SayHello(string name)
        {
            Console.WriteLine("{0} : Hello {1} !", this.prefix, name);
        }
        public void SayGoodBye(string name)
        { 
#if DEMO
            Console.WriteLine("Sorry : Feature not available in DEMO version !");
#else
            Console.WriteLine("{0} : Bye {1} !", this.prefix, name); 
#endif
        }
    }
}
