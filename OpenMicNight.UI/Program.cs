using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using OpenMicNight.Data;
using OpenMicNight.Domain;

namespace OpenMicNight.UI
{
    class Program
    {
      static public void WelcomeBanner()
        {
            Console.WriteLine(@"
                 __        __   _                            _                        
                 \ \      / /__| | ___ ___  _ __ ___   ___  | |_ ___                  
                  \ \ /\ / / _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \                 
                   \ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |                
                   _\_/\_/ \___|_|\___\___/|_| |_|_|_|\___|_ \__\___/    _     _   _  
                  / _ \ _ __   ___ _ __   |  \/  (_) ___  | \ | (_) __ _| |__ | |_| | 
                 | | | | '_ \ / _ \ '_ \  | |\/| | |/ __| |  \| | |/ _` | '_ \| __| | 
                 | |_| | |_) |  __/ | | | | |  | | | (__  | |\  | | (_| | | | | |_|_| 
                  \___/| .__/ \___|_| |_| |_|  |_|_|\___| |_| \_|_|\__, |_| |_|\__(_) 
                       |_|                                         |___/              
                             ");
            static string GetUserInput()
            {
                Console.Write("-> ");
                string userInput = Console.ReadLine();
                Console.WriteLine();
                return userInput;
            }

        }
    }
}

//private static PerformanceContext _context = new PerformanceContext();

//private static void Main(string[] args)
//{
//    _context.Database.EnsureCreated();
//    GetPerformance("Before Add:");
//    AddPerformance();
//    GetPerformance("After Add:");
//    Console.Write("Press any key...");
//    Console.ReadKey();
//}

//private static void AddPerformance()
//{
//    var performance = new Performance { PerformerName = "Two For One", PerformerType = "Music", Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss") };
//    _context.Performance.Add(performance);
//    _context.SaveChanges();
//}

//private static void GetPerformance(string text)
//{
//    var performances = _context.Performance.ToList();
//    Console.WriteLine($"{text}: Performer count is {performances.Count}");
//    foreach (var performance in performances)
//    {
//        Console.WriteLine(performance.PerformerName);
//    }