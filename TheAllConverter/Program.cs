using System;

namespace TheAllConverter
{
    class Program
    {
        //string msg;
        //System.Diagnostics.Process.Start(new ProcessStartInfo("cmd", $"/c start https://www.google.com/search?q={msg}"));

        static void Main(string[] args)
        {

            int number=2;
            int decimals=0;
            int startingBase=10;
            int endingBase=2;
            bool goOn = true;
            string input;
            char[] digits = { 'a', 'b', 'c', 'd', 'e', 'f' };

           // Console.WriteLine("This application takes 3 inputs:");

            do
            {
                Console.WriteLine("Please write a number (positive, negative, integer or not; press Enter to reuse last value; 0 to stop running):"); // UI improvements possible
                input = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(input))
                    number = int.Parse(input); // TODO: Add Exception handling
                                               // TODO: Add support for non-integer numbers
                                               // TODO: Add support for numbers in bases bigger than 10
                    if (number == 0 && decimals==0)
                        return;
                Console.WriteLine("Please write starting base (positive integer not bigger than 16; press Enter to reuse last value; 0 to stop running):"); // UI improvements possible
                input = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(input))
                    startingBase = int.Parse(input); // TODO: Add Exception handling
                    if (startingBase == 0)
                        return;
                Console.WriteLine("Please write ending base (positive integer not bigger than 16; press Enter to reuse last value; 0 to stop running):"); // UI improvements possible
                input = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(input))
                    endingBase = int.Parse(input); // TODO: Add Exception handling
                    if (endingBase == 0)
                        return;

                


            } while (goOn);   //|| decimals!=0);

        }
    }
}
