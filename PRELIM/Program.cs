using System;
using System.Collections.Generic;
using System.IO;

namespace PRELIM
{
    internal class Program
    {
        static List<char> EncryptKey = new List<char>();
        static string filename = "eMessage.txt";

        private static void Run()
        {
            char mode;

            mode = SetMachineMode();
            Console.WriteLine("Machine Mode has been set.");
            Console.ReadKey();
            Console.Clear();

            EnterKey();
            if (mode == 'E')
            {
                EncryptMode();
                return;
            }
            DecryptMode();
        }

        private static char SetMachineMode()
        {
            string input;
            do
            {
                Console.Write("Would you like to encrypt or decrypt a message? [E / D] : ");

                input = Console.ReadLine();
                if (input.Length != 1 || (char.ToUpper(input[0]) != 'E' && char.ToUpper(input[0]) != 'D'))
                {
                    Console.WriteLine("Invalid Setting please try again. Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                break;
            } while (true);

            return char.ToUpper(input[0]);
        }

        private static void EnterKey()
        {
            Console.Write("What is the key you want to set? : ");
            string key = Console.ReadLine();
            SetKey(key.ToUpper());

            Console.WriteLine("Cypher has been set.");
            Console.ReadKey();
            Console.Clear();
        }

        private static void SetKey(string key)
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

        private static void EncryptMode()
        {
            Console.WriteLine("Please input the message you want to encrypt:");
            string message = Console.ReadLine();

            WriteFile(EncryptMessage(message.ToUpper()));
            Console.WriteLine("Message has been successfully encrypted and written to eMessage.txt");
        }

        private static string EncryptMessage(string message)
        {
            string newMessage = string.Empty;

            for (int i = 0; i < message.Length; i++)
            {
                if (!(message[i] >= 65 && message[i] <= 90))
                {
                    newMessage += message[i];
                    continue;
                }
                newMessage += EncryptKey[message[i] - 65];
            }
            return newMessage;
        }

        private static void DecryptMode()
        {
            string decryptedMessage;

            Console.WriteLine("Reading eMessage.txt and decrypting using the provided key.\nThe decrypted message is:");
            decryptedMessage = DecryptMessage(ReadFile());

            Console.WriteLine($"{decryptedMessage}\nMessage has been successfully decrypted.");
        }

        private static string DecryptMessage(string message)
        {
            string newMessage = string.Empty;

            for (int i = 0; i < message.Length; i++)
            {
                if (EncryptKey.Contains(message[i]))
                {
                    newMessage += (char)(EncryptKey.IndexOf(message[i]) + 65);
                    continue;
                }
                newMessage += message[i];
            }
            return newMessage;
        }

        private static void WriteFile(string message)
        {
            File.WriteAllText(filename, message);            
        }

        private static string ReadFile()
        {
            return File.ReadAllText(filename);
        }

        static void Main(string[] args)
        {
            Run();
        }
    }
}
