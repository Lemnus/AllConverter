using System;

namespace TheAllConverter
{
    class Program
    {

        static char prefSeparator = '.';
        static bool isPeriodic = false;
        static int periodStart = -1;
        static bool valid = true;

        static void Main(string[] args)
        {
            string[] splits;
            string number= "0";
            string decimals="2";
            int startingBase=10;
            int endingBase=2;
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
                ConsolePrinter(" (any whole number between 1 and 17) \n \n", 0);
                ConsolePrinter(" 3) The " ,0);
                ConsolePrinter("ending base" ,3);
                ConsolePrinter(" (any whole number between 1 and 17) \n \n", 0);
                ConsolePrinter(" The application keeps running until the user inputs a 0 \n \n" +
                               " Should the user input a whitespace (Enter, Spacebar), the last \n" +
                               " value for that specific field will be reused. \n \n", 0); 
            } // User usage instructions

                                                                            // NI : Check if either base input is in correct format                              
            do
            {
                {
                    ConsolePrinter("\nPlease write a ", 4);
                    ConsolePrinter("number ", 1); 
                    Console.Write("\n \n");
                }
                input = Console.ReadLine();
                input=input.ToLower();

                valid = true;

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
                        decimals = "";
                        prefSeparator = ' ';
                    }

                    isPeriodic = false;
                    periodStart = -1;
                }
                           
                if (number == "0" && decimals=="")
                        return;
                if(number[0]=='-')
                    number = number.Remove(0, 1);
                if(isNegative)
                ConsolePrinter("Number set to -" + number + prefSeparator + decimals, 0);
                else
                    ConsolePrinter("Number set to " + number + prefSeparator + decimals, 0);

                {
                    ConsolePrinter("\nPlease write a ", 4);
                    ConsolePrinter("starting base ", 2);
                    Console.Write("\n \n");
                }
                ReadStartingBase(ref startingBase);              
                    if (startingBase == 0)
                        return;
                ConsolePrinter("Starting base set to " + startingBase, 0);

                {
                    ConsolePrinter("\nPlease write an ", 4);
                    ConsolePrinter("ending base ", 3);          
                    Console.Write("\n \n");
                }
                ReadEndingBase(ref endingBase);
                    if (endingBase == 0)
                        return;
                ConsolePrinter("Ending base set to " + endingBase, 0);

                string nb = number;
                string dc = decimals;

                if (isNegative)
                    number = "-" + number;

                if (startingBase != 10)
                    (nb,dc)=NToTen(number, decimals,startingBase);
                if(endingBase !=10)
                {
                    nb = TenToNNumber(nb, endingBase);
                    dc = TenToNDecimals(dc, endingBase);
                }
                if (dc == "0")
                {
                    dc = "";
                    prefSeparator = ' ';
                }

                if (nb == "")
                    nb = "0";

                ConsolePrinter("\n \n \nThe number after base changes: \n \n", 0);
                if (valid == true)
                {
                    if (isNegative)
                        Console.WriteLine("-" + nb + prefSeparator + dc);
                    else
                        Console.WriteLine(nb + prefSeparator + dc);
                    Console.WriteLine("\n \n - - - - - - - - - - - - - - - - - - - - - \n \n");
                }
                else
                    ConsolePrinter("\n \n \n Invalid number input \n \n \n", 2);

            } while (goOn); 
  
        }

        static void ReadStartingBase(ref int startingBase)
        {
            string input = Console.ReadLine();

            input = input.ToLower();
            if (!String.IsNullOrWhiteSpace(input))
                try
                {
                    startingBase = int.Parse(input);
                    if (startingBase > 16 || startingBase < 2)
                        throw new Exception();
                }
                catch
                {
                    ConsolePrinter("\n Invalid number as starting base \n", 0);
                    ReadStartingBase(ref startingBase);
                }
        }

        static void ReadEndingBase(ref int endingBase)
        {
            string input = Console.ReadLine();
            input = input.ToLower();
            if (!String.IsNullOrWhiteSpace(input))
                try
                {
                    endingBase = int.Parse(input);
                    if (endingBase > 16 || endingBase < 2)
                        throw new Exception();
                }                                        // Exception handling not actually implemented
                catch
                {
                    ConsolePrinter("\n Invalid number as ending base \n", 0);
                    ReadEndingBase(ref endingBase);
                }
        }

        static string TenToNDecimals(string decimals, int endBase)
        {
            // take decimals and add string 0.
            // keep multiplying with base til right side of "." is =0
            // place each digit/letter from the left side in answerValues and then remove both the value and the dot
            // repeat till decimals ==0 or they start repeating

            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            char[] answerValues = new char[50];// [] of numbers left of separator
            string[] answerHistory= new string[50];
            bool goOn = true;
            int counter = 0;

            string numberToMultiply = "0." + decimals;
            Decimal decNumberToMuliply = decimal.Parse(numberToMultiply);

            do
            {
                counter++;
                answerHistory[counter] = "" + decNumberToMuliply;
                for(int i=1;i<counter;i++)
                    if(answerHistory[i]==answerHistory[counter])
                    {
                        isPeriodic = true;
                        goOn = false;
                        periodStart = i;                          
                    }             

                decNumberToMuliply *= endBase;

                if (decNumberToMuliply == 0)                                    
                    break;
                if (counter == 49)
                    goOn = false;

                numberToMultiply = "" + decNumberToMuliply;
                string[] splits = new string[2];
                splits = numberToMultiply.Split('.');
                answerValues[counter] = digits[int.Parse(splits[0])];
                numberToMultiply = "0."+splits[1];
                decNumberToMuliply = decimal.Parse(numberToMultiply);
                
                                
            } while (goOn);

            string answer = "";

            if(isPeriodic)
            {
                for (int i = 1; i <= counter; i++)
                {
                    if (i == periodStart)
                        answer += "(";
                    answer += answerValues[i];
                }
                answer += ")";
            }
            else
            for (int i = 1; i <=counter; i++)
                answer += answerValues[i];

            return answer;
        }

        static string TenToNNumber(string number, int endBase)
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
                ConsolePrinter("\n \n \n \n Error: Invalid number input",2);
                return "";
            }
            while(changedN>0)

            {
                i++;
                rest[i] = changedN % endBase;           
                changedN /= endBase;
            }            
            int index = i;
            while(i>=0)
            {
                answer[index - i] = digits[rest[i]];
                i--;
            }
            string answerS = new string(answer);
            answerS = answerS.Remove(index+1);
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
                        default: valid = false; break;
                    }
                if (coNumber[i] > startBase)
                    valid = false;
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
            //Console.WriteLine(sumaNb); // remove separator from Dec
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
