using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Brutal_Force_Crasher
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Sign> signs = new List<Sign>();
            string path = "MarciejB.7z";
            string password;
            string tmp;
            bool passwordSearch = true;
            bool addSign = false;
            int signCount = 0;
            int tryPassword = 0;
            Stopwatch stopWatch = new Stopwatch();

            signs.Add(new Sign());
            password = signs[0].sign;

            stopWatch.Start();

            while (passwordSearch)
            {
                signCount = signs.Count() - 1;

                ////  CREATE PASSWORD  /////
                tmp = "";
                foreach (var item in signs)
                {
                    tmp += item.sign;
                }
                password = tmp;
                ////////////////////////////

                try
                {
                    using (var archive = SharpCompress.Archives.SevenZip.SevenZipArchive.Open(path, new SharpCompress.Readers.ReaderOptions() { Password = password, LookForHeader = false }))
                    {
                        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                        {
                            entry.WriteToDirectory("temp", new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }
                    }
                    //Success
                    passwordSearch = false;
                    stopWatch.Stop();
                }
                catch (SharpCompress.Common.InvalidFormatException ex)
                {
                    Console.WriteLine("Something wrong: " + password);
                }
                catch (SharpCompress.Common.PasswordProtectedException ex)
                {
                    Console.WriteLine("Password wrong: " + password);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Password wrong: " + password);
                }

                signs[signCount].nextSign();

                if (signs[signCount].ifIsTheLastSign())
                {
                    if (signCount == 0)
                    {
                        signs[signCount].resetSign();
                        signs.Insert(0, new Sign());
                    }
                    else
                    {
                        signs[signCount].resetSign();
                        addSign = true;
                    }
                }

                for (int i = signCount - 1; i >= 0; i--)
                {
                    if (addSign)
                    {
                        if (signs[i].ifIsTheLastSign())
                        {
                            signs[i].resetSign();
                        }
                        else
                        {
                            signs[i].nextSign();
                            addSign = false;
                        }
                    }
                    if (addSign && i == 0)
                    {
                        signs.Insert(0, new Sign());
                        addSign = false;
                    }
                }

                tryPassword++;

            }

            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine($"{tryPassword} - password guesses");
            Console.WriteLine($"Time the password was cracked:{ts}");
            Console.WriteLine($"Hasło to: {password}");
        }
    }
}
