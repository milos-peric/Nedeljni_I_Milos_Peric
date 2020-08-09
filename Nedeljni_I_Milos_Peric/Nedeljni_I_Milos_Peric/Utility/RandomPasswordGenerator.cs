using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_I_Milos_Peric.Utility
{
    class RandomPasswordGenerator
    {
        private const string pathManagerPassword = @"..\..\ManagerAccess.txt";
        private static Random random = new Random();
        
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void WriteRandomPasswordToFile()
        {
            {
                try
                {
                    using (StreamWriter streamWriter = new StreamWriter(pathManagerPassword))
                    {
                        streamWriter.Write(RandomString(8));
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Not possible to write to file. Path: ", pathManagerPassword);
                    Debug.WriteLine(e.Message);
                }
            }
        }

        public static string ReadManagerPassword()
        {
            string line = string.Empty;
            try
            {
                using (StreamReader streamReader = new StreamReader(pathManagerPassword))
                {
                    line = streamReader.ReadLine();
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine("Not possible to read from file {0} or file doesn't exist.", pathManagerPassword);
                Console.WriteLine(e.Message);
            }
            return line;
        }

    }
}
