using System;

namespace TheAllConverter
{
    class Program
    {
        //string msg;
        //System.Diagnostics.Process.Start(new ProcessStartInfo("cmd", $"/c start https://www.google.com/search?q={msg}"));

        //Console.Beep(300, 500);

        static char prefSeparator = '.';

        static void Main(string[] args)
        {
            string[] splits;
            string number= "1010";
            string decimals="01";
            int startingBase=2;
            int endingBase=10;
            bool goOn = true;
            string input;
            
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

                                                                            // NI : Check if either base input is in correct format                              
                                                                            // NI : Printing negative numbers
            do
            {
                {
                    ConsolePrinter("Please write a ", 4);
                    ConsolePrinter("number ", 1); 
                    Console.Write("\n \n");
                }
                input = Console.ReadLine();
                input=input.ToLower();
                                                        
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
                           
                if (number[0] == '0' && decimals[0]=='0')
                        return;

                Console.WriteLine("NToTen() prints: "+NToTen(number,decimals,2));



                {
                    ConsolePrinter("Please write a ", 4);
                    ConsolePrinter("starting base ", 2);
                    Console.Write("\n \n");
                }
                input = Console.ReadLine();
                input=input.ToLower();
                if (!String.IsNullOrWhiteSpace(input))
                try{
                        startingBase = int.Parse(input);
                   }
                    catch
                    {
                        ConsolePrinter("Invalid number as starting base", 0);
                    }
                    if (startingBase == 0)
                        return;
                 {
                    ConsolePrinter("Please write an ", 4);
                    ConsolePrinter("ending base ", 3);          
                    Console.Write("\n \n");
                }
                input = Console.ReadLine();
                input=input.ToLower();
                if (!String.IsNullOrWhiteSpace(input))
                    try
                    {                       
                        endingBase = int.Parse(input);
                        //if (endingBase > 16)
                        //    throw Exception e;
                    }                                        // Exception handling not actually implemented
                    catch
                    {
                        ConsolePrinter("Invalid number as ending base", 0);
                    }
                    if (endingBase == 0)
                        return;
            } while (goOn); 
  
        }

        static string TenToN(string number, int endBase)  // Partial Ten to N: still not handling decimals
        {
            char[] digits = { '0','1','2','3', '4', '5', '6', '7', '8', '9','a', 'b', 'c', 'd', 'e', 'f' };
            int[] rest = new int[50];
            int i = -1;
            char[] answer= new char[50];
            int changedN;
            try
            {
                 changedN = int.Parse(number);
            }
            catch
            {
                ConsolePrinter("Error: Invalid number input",0);
                return "";
            }
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
                answer[index - i] = digits[rest[i]];
                i--;
            }
            string answerS = new string(answer);
            return answerS;
        }

        static Tuple<string, string> NToTen(string number, string decimals, int startBase)
        {
            int i = 0;
            int[] coNumber = new int[50];
            int[] coDecimal = new int[50];
            while (i<number.Length)
            {
                if (number[i] <= '9' && number[i] >= '0')
                    coNumber[i] = int.Parse(number[i].ToString());
                else
                    switch(number[i])
                    {
                        case 'a': coNumber[i] = 10; break;
                        case 'b': coNumber[i] = 11; break;
                        case 'c': coNumber[i] = 12; break;
                        case 'd': coNumber[i] = 13; break;
                        case 'e': coNumber[i] = 14; break;
                        case 'f': coNumber[i] = 15; break;
                    }
                i++;
                
            }
            i = 0;
            while (i < decimals.Length)
            {
                if (decimals[i] <= '9' && decimals[i] >= '0')
                    coDecimal[i] = int.Parse(decimals[i].ToString());
                else
                    switch (decimals[i])
                    {
                        case 'a': coDecimal[i] = 10; break;
                        case 'b': coDecimal[i] = 11; break;
                        case 'c': coDecimal[i] = 12; break;
                        case 'd': coDecimal[i] = 13; break;
                        case 'e': coDecimal[i] = 14; break;
                        case 'f': coDecimal[i] = 15; break;
                    }
                //Console.WriteLine(decimals[i]);
                //Console.WriteLine(coDecimal[i]);


                i++;
            }

            Decimal sumaDec = 0;           
            i = 0;
            while(i<decimals.Length)
            {
                sumaDec += (decimal)coDecimal[i] / (decimal)PowerUp(startBase, i + 1);
                i++;
            }
            Decimal sumaNb = sumaDec;
            i = 0;
            while (i<number.Length)
            {
                sumaNb += (decimal)coNumber[i] * (decimal)PowerUp(startBase, number.Length-i-1);
                i++;
            }
            Console.WriteLine(sumaNb); // remove separator from Dec
            string answer = "" + sumaNb;
            string nbAnswer="0";
            string dcAnswer="0";
            string[] splits = new string[2];
            if (sumaDec > 0)
            {
                splits = answer.Split('.');
                dcAnswer = "" + splits[1];
                nbAnswer = "" + splits[0];
            }
            else
                nbAnswer = "" + sumaNb;            

            return Tuple.Create(nbAnswer,dcAnswer);
        }

        static int PowerUp(int baza,int putere)
        {
            int k = 1;
            int i = 0;
            while(i<putere)
            {
                k *= baza;
                i++;
            }
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
