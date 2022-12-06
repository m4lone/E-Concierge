using System;

namespace qodeless.DataManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+------------------------------------------------------------------------+");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  QODELESS BEGIN");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("+------------------------------------------------------------------------+");

            try
            {
                
                Run(new CoreDataManager());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  QODELESS END");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("+------------------------------------------------------------------------+");
            Console.ReadKey();
        }

        public static void Run(DataManager dt)
        {
            Console.WriteLine("> Gerenciador " + dt.GetType().Name + " finalizado.\n");
        }
    }
}
