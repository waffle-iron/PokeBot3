using System;
using System.Threading.Tasks;
using PokemonGo.RocketAPI.Exceptions;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using PokemonGo.RocketAPI.GeneratedCode;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace PokemonGo.RocketAPI.Console
{
    class Program
    {
        private static readonly Version CurrentVersion = new Version("3.1.1");
        public static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs");
        public static string account = Path.Combine(path, "Config.txt");
        public static string items = Path.Combine(path, "Items.txt");
        public static string keep = Path.Combine(path, "noTransfer.txt");
        public static string ignore = Path.Combine(path, "Unwanted.txt");
        public static string evolve = Path.Combine(path, "Evolve.txt");
        private static string data;
        public static Pokemons PokemonList;
        [STAThread]
        static void Main(string[] args)
        {
            System.Console.WriteLine("Current CurrentVersion: " + CurrentVersion);
            string urlAddress = "http://pastebin.com/raw/5xi0UDAv";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                var newVersion = new Version(data);
                var versionResult = CurrentVersion.CompareTo(newVersion);
                if (versionResult >= 0)
                {
                    System.Console.WriteLine("Newest version: " + data);
                    System.Console.WriteLine("You are already using the newest version.");
                }
                else
                {
                    System.Console.WriteLine("Newest version: " + data);
                    System.Console.WriteLine("There is a newer version avaliable on GitHub. Opening link in 5 Seconds.");
                    Thread.Sleep(5000);
                    Process.Start("http://github.com/shiftcodeYT/PokeBot3/releases/latest");
                    System.Environment.Exit(1);
                }

            }
            else
            {
                System.Console.WriteLine("Couldn't check for Updates. Is Pastebin down?");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI());
            Task.Run(() =>
            {
                Stats stats = new Stats();
                stats.ShowDialog();
            });

            Logger.SetLogger(new Logging.ConsoleLogger(LogLevel.Info));

            Mutex mutex = new Mutex(false, Globals.username);
            try
            {
                if (mutex.WaitOne(0, false))
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            new Logic.Logic(new Settings()).Execute().Wait();
                        }
                        catch (PtcOfflineException)
                        {
                            Logger.ColoredConsoleWrite(ConsoleColor.Red,
                                "PTC Servers are probably down OR you credentials are wrong.", LogLevel.Error);
                            Logger.ColoredConsoleWrite(ConsoleColor.Red, "Trying again in 20 seconds...");
                            Thread.Sleep(20000);
                            new Logic.Logic(new Settings()).Execute().Wait();
                        }
                        catch (AccountNotVerifiedException)
                        {
                            Logger.ColoredConsoleWrite(ConsoleColor.Red,
                                "Your PTC Account is not activated. Exiting in 10 Seconds.");
                            Thread.Sleep(10000);
                            Environment.Exit(0);
                        }

                        catch (Exception ex)
                        {
                            Logger.ColoredConsoleWrite(ConsoleColor.Red, $"Unhandled exception: {ex}", LogLevel.Error);
                            Logger.Error("Restarting in 20 Seconds.");
                            Thread.Sleep(200000);
                            new Logic.Logic(new Settings()).Execute().Wait();
                        }
                    });
                    ReadCommands();
                }
                else
                {
                    Logger.ColoredConsoleWrite(ConsoleColor.Red, "You already have an instance of this bot running with this account. Exiting in 10 seconds!", LogLevel.Error);
                    Logger.ColoredConsoleWrite(ConsoleColor.Red, "Please close other instances and try again!", LogLevel.Error);
                    Thread.Sleep(10000);
                }
            }
            finally
            {
                mutex?.Close();
            }
        }

        private static void ReadCommands()
        {
            while (true)
            {
                var input = System.Console.ReadLine();
                if (input == "exit")
                {
                    Environment.Exit(1);
                }

                if (input == "GUI")
                {
                    if (Globals.pokeList)
                    {
                        Task.Run(() =>
                        {
                            PokemonList.ShowDialog();
                        });
                    }
                    else
                    {
                        Task.Run(() =>
                        {
                            PokemonList = new Pokemons();
                            PokemonList.ShowDialog();
                        });
                    }
                }

                if (input == "items")
                {
                    new Logic.Logic(new Settings()).ListItems();
                }
            }
        }


    }
    public static class Globals
    {
        public static Enums.AuthType acc = Enums.AuthType.Google;
        public static bool defLoc = true;
        public static string username = "empty";
        public static string password = "empty";
        public static double latitute = 40.764883;
        public static double longitude = -73.972967;
        public static double altitude = 15.173855;
        public static double speed = 50;
        public static int radius = 5000;
        public static bool transfer = true;
        public static bool transferUnwanted = true;
        public static int duplicate = 3;
        public static bool evolve = true;
        public static int maxCp = 999;
        public static int pokeball = 20;
        public static int greatball = 50;
        public static int ultraball = 100;
        public static int masterball = 200;
        public static int revive = 20;
        public static int potion = 0;
        public static int superpotion = 0;
        public static int hyperpotion = 50;
        public static int toppotion = 100;
        public static int toprevive = 50;
        public static int berry = 50;
        public static int ivmaxpercent = 0;
        public static List<PokemonId> noTransfer = new List<PokemonId>();
        public static List<PokemonId> Unwanted = new List<PokemonId>();
        public static List<PokemonId> doEvolve = new List<PokemonId>();
        public static string telAPI = string.Empty;
        public static string telName = string.Empty;
        public static int telDelay = 5000;

        public static int navigation_option = 1;
        public static bool useluckyegg = true;
        public static bool useincense = true;
        public static bool gerNames = false;
        public static bool pokeList = true;
        public static bool keepPokemonsThatCanEvolve = true;
        public static bool pokevision = false;
    }
}
