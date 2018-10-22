using System;

namespace TheAllConverter
{
    class Program
    {
        //string msg;
        //System.Diagnostics.Process.Start(new ProcessStartInfo("cmd", $"/c start https://www.google.com/search?q={msg}"));

        //Console.Beep(300, 500);

        static void Main(string[] args)
        {
            string[] splits;
            string number= "1010";
            string decimals="01";
            int startingBase=2;
            int endingBase=10;
            bool goOn = true;
            string input;
            char prefSeparator = '.';
            int separatorPoz = 4;
            bool isNegative = false;

            {
                ConsolePrinter("This application takes 3 inputs: \n \n", 0);
                ConsolePrinter(" 1) The ", 0);
                ConsolePrinter("number", 1);
                ConsolePrinter(" to be rewritten, in any base up to 16, with or \n " ,0);
                ConsolePrinter("without  decimals, negative or positive \n \n", 0);
                ConsolePrinter(" 2) The " ,0);
                ConsolePrinter("starting base" ,2);
                ConsolePrinter(" (any whole number between 0 and 17) \n \n", 0);
                ConsolePrinter(" 3) The " ,0);
                ConsolePrinter("ending base" ,3);
                ConsolePrinter(" (any whole number between 0 and 17) \n \n", 0);
                ConsolePrinter(" The application keeps running until the user inputs a 0 \n \n" +
                               " Should the user input a whitespace (Enter, Spacebar), the last \n" +
                               " value for that specific field will be reused. \n \n", 0); 
            } // User usage instructions

                                                                        // TODO: Add support for numbers in bases bigger than 10

            do
            {
                {
                    ConsolePrinter("Please write a ", 4);
                    ConsolePrinter("number ", 1); 
                    Console.Write("\n \n");
                }
                input = Console.ReadLine();
                                                        
                if (!String.IsNullOrWhiteSpace(input))
                {
                    if (input[0] == '-')
                    {
                        isNegative = true;
                        input = input.Remove(0, 1);
                    }
                    else
                        isNegative = false;
                    
                    if (input.Contains(','))
                    {
                        prefSeparator = ',';
                        separatorPoz = input.IndexOf(',');
                    }
                    else
                        if (input.Contains('.'))
                    {
                        prefSeparator = '.';
                        separatorPoz = input.IndexOf('.');
                    }
                    else separatorPoz = 0;

                    splits = input.Split(prefSeparator);

                    number = splits[0];
                    if (separatorPoz > 0)
                        decimals = splits[1];
                    else
                    {
                        decimals = string.Empty;
                        prefSeparator = ' ';
                    }
                }

                
                Console.WriteLine("ToN: "+TenToN(number,2));           // TODO: Add Exception handling

                if (number[0] == '0' && decimals[0]=='0')
                        return;
                {
                    ConsolePrinter("Please write a ", 4);
                    ConsolePrinter("starting base ", 2);
                    Console.Write("\n \n");
                }
                input = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(input))
                    startingBase = int.Parse(input);                    // TODO: Add Exception handling
                    if (startingBase == 0)
                        return;
                 {
                    ConsolePrinter("Please write an ", 4);
                    ConsolePrinter("ending base ", 3);          
                    Console.Write("\n \n");
                }
                input = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(input))
                    endingBase = int.Parse(input);                      // TODO: Add Exception handling
                    if (endingBase == 0)
                        return;

                


            } while (goOn); 
  
        }

        static string TenToN(string number, int endBase)
        {
            char[] digits = { '0','1','2','3', '4', '5', '6', '7', '8', '9','a', 'b', 'c', 'd', 'e', 'f' };
            int[] rest = new int[50];
            int i = -1;
            char[] answer= new char[50];
            int changedN = int.Parse(number);
            while(changedN>0)
            {
                i++;
                Console.WriteLine("At i=" + i + " performing " + changedN + "%" + endBase);
                rest[i] = changedN % endBase;
                Console.WriteLine("Giving the rest" + rest[i]);            
                changedN /= endBase;
            }            
            int index = i;
            while(i>=0)
            {
               // Console.WriteLine("rest[i]: "+rest[i]);
                answer[index - i] = digits[rest[i]];
               // Console.WriteLine("answer[i]: "+answer[index - i]);
                i--;
            }
            string answerS = new string(answer);
            return answerS;
        }


        static int PowerUp(int a,int b)
        {
            int k = a;
            for (int i = 1; i < b; i++)
                k *= a;
            return k;
        }

        static void ConsolePrinter(string msg, int color)
        {
            Console.ResetColor();
            switch (color)
            {
                case 1: Console.ForegroundColor = ConsoleColor.Green; break;
                case 2: Console.ForegroundColor = ConsoleColor.Red; break;
                case 3: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case 0: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case 4: break;
            }

            Console.Write(msg);
            Console.ResetColor();
            

        }
    }
}
