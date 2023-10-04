using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.IO;

namespace PRELIM
{
    internal class Program
    {
        List<char> EncryptKey = new List<char>();

        private void run()
        {
            do
            {
                Console.Write("Would you like to encrypt or decrypt a message? [E / D] : ");
                string input = Console.ReadLine();
                char choice = ' ';

                if (!(input == string.Empty))
                {
                    choice = char.ToUpper(input[0]);
                }
                
                if (choice != 'E' && choice != 'D')
                {
                    Console.WriteLine("Invalid Setting please try again. Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                Console.WriteLine("Machine Mode has been set.");
                Console.ReadKey();
                Console.Clear();

                if (choice == 'E')
                {
                    encryptMode();
                    break;
                }
                if (choice == 'D')
                {
                    decryptMode();
                    break;
                }
            } while (true);
            Console.WriteLine("Press any key to close the program");
            Console.ReadKey();
        }

        private void encryptMode()
        {
            EnterKey();

            Console.WriteLine("Please input the message you want to encrypt:");
            string message = Console.ReadLine();

            encryptMessage(message);
        }

        private void EnterKey()
        {
            Console.Write("What is the key you want to set? : ");
            string key = Console.ReadLine();
            setKey(key.ToUpper());

            Console.WriteLine("Cypher has been set.");
            Console.ReadKey();
            Console.Clear();
        }

        private void setKey(string key)
        {
            for (int i = 0; i < key.Length; i++)
            {
                if (!EncryptKey.Contains(key[i]) && (key[i] >= 65 && key[i] <= 90))
                {
                    EncryptKey.Add(key[i]);
                }
            }

            for (int i = 65; i < 91; i++)
            {
                if (EncryptKey.Contains((char)i))
                {
                    continue;
                }
                EncryptKey.Add((char)i);
            }
        }

        private void encryptMessage(string message)
        {
            string newMessage = string.Empty;
            char letter;

            int univIndex = 0;

            for (int i = 0; i < message.Length; i++)
            {
                letter = Char.ToUpper(message[i]);

                if (!(letter >= 65 && letter <= 90))
                {
                    newMessage += letter;
                }
                if (letter - 65 < EncryptKey.Count)
                {
                    int index = (letter - 65);
                    newMessage += (char)EncryptKey[index];
                }
                if (letter - 65 >= EncryptKey.Count)
                {
                    char newChar = (char)(letter - EncryptKey.Count - univIndex);

                    while (EncryptKey.Contains(newChar))
                    {
                        univIndex--;
                        newChar++;
                    }
                    newMessage += newChar;
                }
            }
            writeFile(newMessage);
        }

        private void decryptMode()
        {
            EnterKey();
            
            Console.WriteLine("Reading eMessage.txt and decrypting using the provided key.\nThe decrypted message is:");
            string decryptedMessage = decryptMessage(readFile());

            Console.WriteLine($"{decryptedMessage}\nMessage has been successfully decrypted.");
        }

        private string decryptMessage(string message)
        {
            string newMessage = string.Empty;
            for (int i = 0; i < message.Length; i++)
            {
                if (EncryptKey.Contains(message[i]))
                {
                    newMessage += (char)(EncryptKey.IndexOf(message[i]) + 65);
                    continue;
                }
                else
                {
                    newMessage += message[i];
                }
            }
            return newMessage;
        }

        private void writeFile(string message)
        {
            string filename = "eMessage.txt";
            File.WriteAllText(filename, message);

            Console.WriteLine("Message has been successfully encrypted and written to eMessage.txt");
        }

        private string readFile()
        {
            string filename = "eMessage.txt";
            return File.ReadAllText(filename);
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.run();
        }
    }
}
