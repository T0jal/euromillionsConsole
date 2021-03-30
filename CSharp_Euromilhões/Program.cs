using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Data.SqlClient;
using System.Threading;
using System.ComponentModel;
using System.IO;
using Chave;
using System.Globalization;

namespace Chave
{
    class Program
    {
        #region Msc

        static List<Chave> listFavouriteKeys = new List<Chave>();
        static List<Chave> listRandomKeys = new List<Chave>();
        static List<Chave> listBets = new List<Chave>();
        public static void readFiles()
        {
            StreamReader rd1 = new StreamReader(@"FAVOURITEKEYS.txt");
            while (!rd1.EndOfStream)
            {
                string linha = rd1.ReadLine();
                string[] chaves = linha.Split(',');
                Chave c = new Chave(int.Parse(chaves[0]), int.Parse(chaves[1]), int.Parse(chaves[2]), int.Parse(chaves[3]), 
                    int.Parse(chaves[4]), int.Parse(chaves[5]), int.Parse(chaves[6]));
                listFavouriteKeys.Add(new Chave(c));
            }
            rd1.Close();

            StreamReader rd2 = new StreamReader(@"RANDOMKEYS.txt");
            while (!rd2.EndOfStream)
            {
                string linha = rd2.ReadLine();
                string[] chaves = linha.Split(',');
                Chave c = new Chave(int.Parse(chaves[0]), int.Parse(chaves[1]), int.Parse(chaves[2]), int.Parse(chaves[3]),
                    int.Parse(chaves[4]), int.Parse(chaves[5]), int.Parse(chaves[6]));
                listRandomKeys.Add(new Chave(c));
            }
            rd2.Close();

            StreamReader rd3 = new StreamReader(@"BETS.txt");
            while (!rd3.EndOfStream)
            {
                string linha = rd3.ReadLine();
                string[] chaves = linha.Split(',');
                Chave c = new Chave(int.Parse(chaves[0]), int.Parse(chaves[1]), int.Parse(chaves[2]), int.Parse(chaves[3]),
                    int.Parse(chaves[4]), int.Parse(chaves[5]), int.Parse(chaves[6]));
                listBets.Add(new Chave(c));
            }
            rd3.Close();
        }

        static int readValue()
        {
            int val = 0;
            bool flag = false;
            do
            {
                try
                {
                    val = int.Parse(Console.ReadLine());
                    flag = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect value, try again.");
                    flag = false;
                }
            } while (!flag);
            return val;
        }
        #endregion

        #region Main Menu
        static int mainMenu()
        {
            Console.Clear();
            Console.WriteLine("*********WELCOME TO EUROMILLIONS********");
            Console.WriteLine();
            Console.WriteLine("*****************MENU*******************");
            Console.WriteLine(" Which option would you like to choose?");
            Console.WriteLine(" 1 - Add/Delete/Show favourite keys.");
            Console.WriteLine(" 2 - Add/Delete/Show random keys.");
            Console.WriteLine(" 3 - Bet.");
            Console.WriteLine(" 4 - Run the draw!");
            Console.WriteLine(" 9 - Exit.");
            Console.WriteLine();
            Console.Write("Option: ");
            return readValue();
        }
        #endregion

        #region Favourite Keys
        static void menuFavouriteKeys()
        {
            Console.Clear();

            int flag = 0;
            do
            {
                Console.WriteLine(" Which option would you like to choose?");
                Console.WriteLine(" 1 - Add favourite keys.");
                Console.WriteLine(" 2 - Delete favourite key.");
                Console.WriteLine(" 3 - Delete all favourite keys.");
                Console.WriteLine(" 4 - Show favourite keys.");
                Console.WriteLine();
                Console.Write("Option: ");

                int op = int.Parse(Console.ReadLine());
                switch (op)
                {
                        case 1:
                            addFavouriteKey();
                            flag = 1;
                            break;
                        case 2:
                            deleteFavouriteKey();
                            flag = 1;
                            break;
                        case 3:
                            deleteAllFavouriteKeys();
                            flag = 1;
                            break;
                        case 4:
                            showFavouriteKeys();
                            flag = 1;
                            break;
                        default:
                            invalidOption();
                            break;
                }
            } while (flag == 0);
        }
        static void addFavouriteKey()
        {
            int flag = 0;
            do {
                Console.Clear();
                Console.WriteLine("Please insert the key: ");
                Console.Write("Num 1: ");
                int num1 = int.Parse(Console.ReadLine());
                Console.Write("Num 2: ");
                int num2 = int.Parse(Console.ReadLine());
                Console.Write("Num 3: ");
                int num3 = int.Parse(Console.ReadLine());
                Console.Write("Num 4: ");
                int num4 = int.Parse(Console.ReadLine());
                Console.Write("Num 5: ");
                int num5 = int.Parse(Console.ReadLine());
                Console.Write("Star 1: ");
                int star1 = int.Parse(Console.ReadLine());
                Console.Write("Star 2: ");
                int star2 = int.Parse(Console.ReadLine());

                Chave c = new Chave(num1, num2, num3, num4, num5, star1, star2);
                c.orderKey();

                if (c.validateKey() == true)
                {
                    flag = 1;
                    listFavouriteKeys.Add(new Chave(c));
                    Console.WriteLine($"\nThe following Key was added to the favourite list:\n{c.printToConsole()}");
                }
                else
                {
                    Console.WriteLine("\nChave inválida.");
                    System.Threading.Thread.Sleep(3000);
                }
            } while (flag == 0);

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        static void deleteFavouriteKey()
        {
            Console.Clear();
            int counter = 0;
            Console.WriteLine("Keys in the list:");
            Console.WriteLine();
            foreach (Chave key in listFavouriteKeys)
                Console.WriteLine(++counter + ")\n" + key.printToConsole());
            
            Console.WriteLine("Which key would you like to delete?");
            int favKey = int.Parse(Console.ReadLine());
            if (favKey <= 0 || favKey > listFavouriteKeys.Count())
            {
                Console.WriteLine("That key does not exist.");
            }
            else
            {
                listFavouriteKeys.RemoveAt(favKey - 1);
                Console.WriteLine("Key removed.");
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void deleteAllFavouriteKeys()
        {
            Console.WriteLine("Are you sure you want to delete all the favourite keys?");
            string answer = Console.ReadLine();
            if (answer == "yes" || answer == "y") {
                listFavouriteKeys.Clear();
                Console.WriteLine("All your favourite keys were deleted.");
                System.Threading.Thread.Sleep(3000);
            }
            else { 
                Console.WriteLine("Your favourite keys were not deleted.");
                System.Threading.Thread.Sleep(3000);
            };
        }

        static void showFavouriteKeys()
        {
            Console.Clear();
            int counter = 0;
            Console.WriteLine("Keys in the list:");
            Console.WriteLine();
            foreach (Chave key in listFavouriteKeys)
                Console.WriteLine(++counter + ")\n" + key.printToConsole());
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        #endregion

        #region Random Keys
        static void menuRandomKeys()
        {
            Console.Clear();

            int flag = 0;
            do
            {
                Console.WriteLine("  Which option would you like to choose?");
                Console.WriteLine("1 - Add random key.");
                Console.WriteLine("2 - Delete a random key.");
                Console.WriteLine("3 - Delete all random keys");
                Console.WriteLine("4 - Show random keys.");
                Console.WriteLine();
                Console.Write("Option: ");


                int op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        addRandomKey();
                        flag = 1;
                        break;
                    case 2:
                        deleteRandomKey();
                        flag = 1;
                        break;
                    case 3:
                        deleteAllRandomKeys();
                        break;
                    case 4:
                        showRandomKeys();
                        flag = 1;
                        break;
                    default:
                        invalidOption();
                        break;
                }
            } while (flag == 0);
        }

        static void addRandomKey()
        {
            Chave rndKey = new Chave();
            rndKey.orderKey();
            listRandomKeys.Add(new Chave(rndKey));
            Console.WriteLine($"\nThis is your random key:\n{rndKey.printToConsole()}");
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        static void deleteRandomKey()
        {
            Console.Clear();
            int counter = 0;
            Console.WriteLine("Keys in the list:");
            Console.WriteLine();
            foreach (Chave key in listRandomKeys)
                Console.WriteLine(++counter + ")\n" + key.printToConsole());

            Console.Write("Which key would you like to delete?");
            int rndKey = int.Parse(Console.ReadLine());
            if (rndKey <= 0 || rndKey > listRandomKeys.Count())
            {
                Console.WriteLine("That key does not exist.");
            }
            else
            {
                listRandomKeys.RemoveAt(rndKey - 1);
                Console.WriteLine("Key removed.");
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void deleteAllRandomKeys()
        {
            Console.WriteLine("Are you sure you want to delete all random keys?");
            string answer = Console.ReadLine();
            if (answer == "yes" || answer == "y") { 
                listRandomKeys.Clear();
                Console.WriteLine("All your random keys were deleted.");
                System.Threading.Thread.Sleep(3000);

            }
            else { 
                Console.WriteLine("Your random keys were not deleted.");
                System.Threading.Thread.Sleep(3000);
            };
        }

        static void showRandomKeys()
        {
            Console.Clear();
            int counter = 0;
            Console.WriteLine("Keys in the list:");
            Console.WriteLine();
            foreach (Chave key in listRandomKeys)
                Console.WriteLine(++counter + ")\n" + key.printToConsole());
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        #endregion

        #region Bet
        static void bet()
        {
            Console.Clear();
            int flag = 0;
            do
            {
                Console.WriteLine("  Which option would you like to choose?");
                Console.WriteLine("1 - Bet with my favourite keys.");
                Console.WriteLine("2 - Bet with my random keys.");
                Console.WriteLine("3 - Manually insert the keys to bet.");
                Console.WriteLine("4 - Show my bets.");
                Console.WriteLine();
                Console.Write("Option: ");

                int op = int.Parse(Console.ReadLine());
                switch (op)
                {
                        case 1:
                            betWFavouriteKeys();
                            flag = 1;
                            break;
                        case 2:
                            betWRandomKeys();
                            flag = 1;
                            break;
                        case 3:
                            betWNewKeys();
                            flag = 1;
                            break; ;
                        case 4:
                            showBets();
                            flag = 1;
                            break;
                        default:
                            invalidOption();
                            break;
                }
            } while (flag != 1);
        }

        static void betWFavouriteKeys()
        {
            Console.Clear();
            listBets.Clear();
            foreach (Chave key in listFavouriteKeys)
                listBets.Add(new Chave(key));

            Console.WriteLine("\nYou've just bet!");
            System.Threading.Thread.Sleep(3000);
        }

        static void betWRandomKeys()
        {
            Console.Clear();
            listBets.Clear();
            foreach (Chave key in listRandomKeys)
                listBets.Add(new Chave(key));

            Console.WriteLine("\nYou've just bet!");
            System.Threading.Thread.Sleep(3000);
        }

        static void betWNewKeys()
        {
            listBets.Clear();
            int flag = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Please insert the key: ");
                Console.Write("Num 1: ");
                int num1 = int.Parse(Console.ReadLine());
                Console.Write("Num 2: ");
                int num2 = int.Parse(Console.ReadLine());
                Console.Write("Num 3: ");
                int num3 = int.Parse(Console.ReadLine());
                Console.Write("Num 4: ");
                int num4 = int.Parse(Console.ReadLine());
                Console.Write("Num 5: ");
                int num5 = int.Parse(Console.ReadLine());
                Console.Write("Star 1: ");
                int star1 = int.Parse(Console.ReadLine());
                Console.Write("Star 2: ");
                int star2 = int.Parse(Console.ReadLine());

                Chave c = new Chave(num1, num2, num3, num4, num5, star1, star2);
                c.orderKey();

                if (c.validateKey() == true)
                {
                    flag = 1;
                    listBets.Add(new Chave(c));
                    Console.WriteLine($"\nThe following Key was added to the bets list:\n{c.printToConsole()}");
                }
                else
                {
                    Console.WriteLine("\nInvalid Key.");
                    System.Threading.Thread.Sleep(3000);
                }
            } while (flag == 0);

            Console.WriteLine("\nYou've just bet!");
            System.Threading.Thread.Sleep(3000);
        }

        static void showBets()
        {
            Console.Clear();
            int counter = 0;
            Console.WriteLine("Keys in the list:");
            Console.WriteLine();
            foreach (Chave key in listBets)
                Console.WriteLine(++counter + ")\n" + key.printToConsole());
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        #endregion

        #region Run the Draw!
        static void runDraw()
        {   // Draws the winning key
            Console.Clear();
            Chave rndKey = new Chave();
            rndKey.orderKey();
            Console.WriteLine($"\nThis is the winning key:\n{rndKey.printToConsole()}");
            Console.WriteLine();
            
            //Checks if there's a prize
            Console.WriteLine($"\nThese are the results of this draw:\n");

            foreach (Chave betKeys in listBets)
            {
                Console.WriteLine(betKeys.printToConsole());
                int matches = betKeys.play(rndKey);
                switch (matches)
                {
                    case 1:
                        Console.WriteLine("You got 1 right! Unfortunately there's no prize this time.\n");
                        break;
                    case 2:
                        Console.WriteLine("You got 2 right! You just won 3 euros! Congratulations!\n");
                        break;
                    case 3:
                        Console.WriteLine("You got 3 right! You just won 6,50 euros! Congratulations!\n");
                        break;
                    case 4:
                        Console.WriteLine("You got 4 right! You just won 25 euros! Congratulations!\n");
                        break;
                    case 5:
                        Console.WriteLine("You got 5 right! You just won 7000 euros! Congratulations!\n");
                        break;
                    case 6:
                        Console.WriteLine("You got 6 right! You just won 260.000 euros! Congratulations!\n");
                        break;
                    case 7:
                        Console.WriteLine("You got 7 right! You just won 100.000.000 euros! Congratulations, you are a Millionaire!!!\n");
                        break;
                    default:
                        Console.WriteLine("This key didn't match the winning key. Better luck next time!\n");
                        break;
                }
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
        #endregion

        static void exit()
        {
            Console.Clear();

            StreamWriter wr = new StreamWriter(@"FAVOURITEKEYS.txt", false); // false = rewrites the whole file
            foreach (Chave key in listFavouriteKeys)
                wr.WriteLine(key);
            wr.Close();

            StreamWriter wr1 = new StreamWriter(@"RANDOMKEYS.txt", false);
            foreach (Chave key1 in listRandomKeys)
                wr1.WriteLine(key1);
            wr1.Close();

            StreamWriter wr2 = new StreamWriter(@"BETS.txt", false);
            foreach (Chave key2 in listBets)
                wr2.WriteLine(key2);
            wr2.Close();

            Console.WriteLine("See you later alligator!");
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(0);
        }

        static void invalidOption()
        {
            Console.WriteLine("Invalid option!");
        }

        #region Code that didn't make the final cut
        /*string key = rndKey.ToString();

        string[] rKey = key.Split(',');
        int[] rKeyInt = Array.ConvertAll(rKey, int.Parse); // TOP -> Converte um Array de strings para ints!

        int[] numbers = new int[5];
        int[] stars = new int[2];

        for (int i = 0; i < 5; i++)
        {
            numbers[i] = rKeyInt[i];
        }

        stars[0] = rKeyInt[5];
        stars[1] = rKeyInt[6];

        order(numbers);
        order(stars);

        Chave aleatoria = new Chave(numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], stars[0], stars[1]);*/

        /*static void order(int[] values)
        {
        Array.Sort(values);
        }*/

        /*int flag = 0;
        do
        {
            Console.Clear();
            Console.Write("Please insert the 5 numbers to add to the list.\nPlease separate the numbers inserted by the following symbol: ,\nYour Numbers: ");
            string linha1 = Console.ReadLine();
        string[] letras1 = linha1.Split(',');
        int[] numeros = Array.ConvertAll(letras1, int.Parse); // TOP -> Converte um Array de strings para ints!
        order(numeros);

        Console.Write("Please insert the 2 stars to add to the list.\nPlease separate the stars inserted by the following symbol: ,\nYour Stars: ");
            string linha2 = Console.ReadLine();
        string[] letras2 = linha2.Split(',');
        int[] estrelas = Array.ConvertAll(letras2, int.Parse);
        order(estrelas);

        Chave c = new Chave(numeros[0], numeros[1], numeros[2], numeros[3], numeros[4], estrelas[0], estrelas[1]);

            if (c.validateKey() == true)
            {
                flag = 1;
                listBets.Add(new Chave(c));
                Console.WriteLine($"\nThe following Key was added to the favourite list:\n{c.printToConsole()}");
            }
            else
            {
                Console.WriteLine("\nChave inválida.");
                System.Threading.Thread.Sleep(3000);
            }
        } while (flag == 0) ;*/

        /*Console.Clear();
            Chave c = new Chave();
        string key = c.gerarChaveAleatoria();

        string[] rKey = key.Split(',');
        int[] rKeyInt = Array.ConvertAll(rKey, int.Parse); // TOP -> Converte um Array de strings para ints!

        int[] numbers = new int[5];
        int[] stars = new int[2];

            for (int i = 0; i< 5; i++)
            {
                numbers[i] = rKeyInt[i];
            }

    stars[0] = rKeyInt[5];
            stars[1] = rKeyInt[6];

            order(numbers);
    order(stars);

    Chave chAle = new Chave(numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], stars[0], stars[1]);
    listDraw.Add(new Chave(chAle));

            Console.WriteLine($"\nThis is the winning key:\n{chAle.printToConsole()}");*/
        #endregion

        static void Main(string[] args)
        {
            readFiles();

            int op;
            do
            {
                op = mainMenu();
                switch (op)
                {
                    case 1:
                        menuFavouriteKeys();
                        break;
                    case 2:
                        menuRandomKeys();
                        break;
                    case 3:
                        bet();
                        break;
                    case 4:
                        runDraw();
                        break;
                    case 9:
                        exit();
                        break;
                    default:
                        invalidOption();
                        break;
                }
            } while (op != 9);
        }
    }
}