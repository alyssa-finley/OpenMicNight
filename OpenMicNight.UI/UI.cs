using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using OpenMicNight.Data;
using OpenMicNight.Domain;
using OpenMicNight.Logic;
using System.Text.Json;
using Microsoft.VisualBasic;
using System.Diagnostics.Eventing.Reader;

namespace OpenMicNight.UI
{
    static public class UI
    {
        //Welcome banner to greet the user
        static public void WelcomeBanner()
        {
            Console.WriteLine(@"
                 __        __   _                            _                        
                 \ \      / /__| | ___ ___  _ __ ___   ___  | |_ ___                  
                  \ \ /\ / / _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \                 
                   \ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |                
                   _\_/\_/ \___|_|\___\___/|_| |_|_|_|\___|_ \__\___/   _     _   _  
                  / _ \ _ __   ___ _ __   |  \/  (_) ___  | \ | (_) __ _| |__ | |_| | 
                 | | | | '_ \ / _ \ '_ \  | |\/| | |/ __| |  \| | |/ _` | '_ \| __| | 
                 | |_| | |_) |  __/ | | | | |  | | | (__  | |\  | | (_| | | | | |_|_| 
                  \___/| .__/ \___|_| |_| |_|  |_|_|\___| |_| \_|_|\__, |_| |_|\__(_) 
                       |_|                                         |___/              
                             ");
        }

        //Creating our user input method to prompt user input and return it as a string
        static string GetUserInput()
        {
            Console.Write("-> ");
            string userInput = Console.ReadLine();
            Console.WriteLine();
            return userInput;
        }

        //Create main menu
        static public void MainMenu(ISignUpLogic tonightsPerformanceLogic)
        {
            bool exitCondition = false;
            while (!exitCondition)
            {
                DisplayMainMenu();
                string choice = GetUserInput().ToLower();
                switch (choice)
                {
                    case "1":
                        PerformerMenu(tonightsPerformanceLogic);
                        break;
                    case "2":
                        DisplaySignUpMenu(tonightsPerformanceLogic);
                        break;
                    case "exit":
                        exitCondition = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection");
                        break;
                }
            }
        }
        static void DisplayMainMenu()
        {
            Console.WriteLine("Press 1 to view, add, remove or update the performers");
            Console.WriteLine("Press 2 to view, add or remove performances from tonight's sign up list");
            Console.WriteLine("Type 'exit' to quit");
        }
        static void PerformerMenu(ISignUpLogic signUpLogic)
        {
            bool exitCondition = false;
            while (!exitCondition)
            {
                DisplayPerformerMenu();
                string choice = GetUserInput().ToLower();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("The following is a list of all performers: ");
                        Console.WriteLine();
                        var allPerformers = signUpLogic.GetAllPerformers();
                        foreach (var performer in allPerformers)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(performer));
                        }
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine("What is the name of the performer you would like to add?");
                        var performerToAddName = GetUserInput();
                        Console.WriteLine("Is this a music, comedy or poetry type performer?");
                        var performerToAddType = GetUserInput();
                        using (var dbContext = new PerformerContext())
                        {
                            var existingPerformer = dbContext.Performer.FirstOrDefault(p => p.PerformerName == performerToAddName);
                            if (existingPerformer == null)
                            {
                                var newPerformer = new Performer { PerformerName = performerToAddName , PerformerType = performerToAddType , Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss") };
                                signUpLogic.AddPerformer(newPerformer);
                                Console.WriteLine($"Added {performerToAddName} to the performers list.");
                            }
                            else
                            {
                                Console.WriteLine($"Performer '{performerToAddName}' already exists.");
                            }
                        }
                            break;
                    case "3":
                        Console.WriteLine("What type of performer would you like to see?");
                        var performerType = GetUserInput().ToLower();
                        var selectedType = signUpLogic.GetPerformersByType(performerType);
                        foreach (var performer in selectedType)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(performer));
                        }
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine("What is the name of the performer you would like to update?");
                        var performerToUpdateName = GetUserInput();
                        var existingPerformersToUpdate = signUpLogic.GetPerformersByName(performerToUpdateName);
                        if (existingPerformersToUpdate.Count == 0)
                            Console.WriteLine("There is not a performer by that name.");
                        else if (existingPerformersToUpdate.Count == 1)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(existingPerformersToUpdate[0]));
                            Console.WriteLine();
                            Console.WriteLine("Please enter the updated performer name: ");
                            existingPerformersToUpdate[0].PerformerName = Console.ReadLine();
                            Console.Write("Enter the updated type of performer (music, comedy or poetry): ");
                            existingPerformersToUpdate[0].PerformerType = Console.ReadLine();
                        }
                        else
                        {
                            foreach (var performance in existingPerformersToUpdate)
                            {
                                Console.WriteLine(JsonSerializer.Serialize(performance));
                            }
                            Console.WriteLine();
                            Console.WriteLine($"Enter the ID of the {performerToUpdateName} you would like to update.");
                            var performerId = int.Parse(GetUserInput());
                            var existingPerformerIds = existingPerformersToUpdate.Select(x => x.PerformerId);
                            if (existingPerformerIds.Contains(performerId))
                            {
                                var performerToUpdate = signUpLogic.GetPerformersById(performerId);
                                Console.WriteLine(JsonSerializer.Serialize(performerToUpdate));
                                Console.WriteLine();
                                Console.Write("Enter the updated name of the performer: ");
                                performerToUpdate.PerformerName = Console.ReadLine();
                                Console.Write("Enter the updated type of performer: ");
                                performerToUpdate.PerformerType = Console.ReadLine();
                            }
                            else Console.WriteLine($"There is no {existingPerformersToUpdate} with ID {performerId}.");
                        }
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.WriteLine("What is the name of the performer you would like to remove?");
                        var performerToRemoveName = GetUserInput();
                        var existingPerformersToRemove = signUpLogic.GetPerformersByName(performerToRemoveName);
                        if (existingPerformersToRemove.Count == 0)
                            Console.WriteLine("There is not a performer by that name.");
                        else if (existingPerformersToRemove.Count == 1) signUpLogic.RemovePerformer(existingPerformersToRemove[0]);
                        else
                        {
                            foreach (var performer in existingPerformersToRemove)
                            {
                                Console.WriteLine(JsonSerializer.Serialize(performer));
                            }
                            Console.WriteLine();
                            Console.WriteLine($"Enter the Id of the {performerToRemoveName} you would like to remove.");
                            var performerId = int.Parse(GetUserInput());
                            var existingPerformerIds = existingPerformersToRemove.Select(x => x.PerformerId);
                            if (existingPerformerIds.Contains(performerId))
                            {
                                var performerToRemove = signUpLogic.GetPerformersById(performerId);
                                signUpLogic.RemovePerformer(performerToRemove);
                            }
                            else Console.WriteLine($"There is no {performerToRemoveName} with Id {performerId}.");
                        }
                        Console.WriteLine();
                        break;
                    //case "5":
                    //    Console.WriteLine("What is the name of the performer you would like to add?");
                    //    var performerToAddName = GetUserInput();
                    //    var newPerformer = new Peformer();
                    //    var existingPerformers = signUpLogic.GetPerformersByName(performerToAddName);
                    //    if (existingPerformers.Any()) 
                    //    { 
                    //        bool exists = existingPerformers
                    //            .Where(x => x.PerformerName.ToUpper() == performerToAddName.ToUpper()).Any();
                    //        if (!exists)
                    //        {
                    //            Console.WriteLine("What type of performer would you like to add?");
                    //            var performerToAddType = GetUserInput();
                    //            newPerformer.PerformerName = performerToAddName;
                    //            newPerformer.PerformerType = performerToAddType;
                                
                    //            bool success = signUpLogic.AddPerformer(newPerformer);
                                
                    //        }
                    //    }
                        
                    //    //Console.WriteLine("There is already a performer by this name.");
                       
                    //    break;
                    case "back":
                        exitCondition = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
        }
        static void DisplayPerformerMenu()
        {
            Console.WriteLine("Press 1 to View all Performers");
            Console.WriteLine("Press 2 to Add a Performer");
            Console.WriteLine("Press 3 to View Performers by Performance Type");
            Console.WriteLine("Press 4 to Update a Performer");
            Console.WriteLine("Press 5 to Remove a Performer");
            Console.WriteLine("Type 'back' to return to the Main Menu");
            Console.Write("Choice: ");
        }
        static void DisplaySignUpMenu(ISignUpLogic signUpLogic)
        {
            bool exitCondition = false;
            while (!exitCondition)
            {
                DisplaySignUpMenu();
                string choice = GetUserInput().ToLower();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Sign Up List:");

                        // Remove this line: var signUpLogic = new SignUpLogic();

                        var signUpList = signUpLogic.GetSignUpList();

                        foreach (var performer in signUpList.Performances)
                        {
                            Console.WriteLine($"Performer: {performer.PerformerName}");
                            Console.WriteLine($"Type of Performance: {performer.PerformerType}");

                            // Check if the performer has any songs
                            if (performer.Songs != null && performer.Songs.Any())
                            {
                                Console.WriteLine("Songs:");
                                foreach (var song in performer.Songs)
                                {
                                    Console.WriteLine($"- {song.SongName} (Original: {song.IsOriginal})");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No songs added for this performer.");
                            }

                            Console.WriteLine(); // Add a line break between performers
                        }

                        Console.WriteLine();
                        break;
                    case "2":
                        if (!signUpLogic.MaxPerformances())
                        {
                            Console.WriteLine("The maximum number of sign-ups has been reached tonight, sorry!");
                            Console.WriteLine(" ");
                            Console.WriteLine(" ");
                            break;
                        }
                        Console.WriteLine("What is the name of the performer you would like to add?");
                        var performerAddToSignUpName = GetUserInput();
                        using (var dbContext = new PerformerContext())
                        {
                            var existingPerformerToAdd = dbContext.Performer.FirstOrDefault(p => p.PerformerName == performerAddToSignUpName);
                            if (existingPerformerToAdd != null)
                            {
                                string performerType = existingPerformerToAdd.PerformerType;

                                var songs = new List<Song>();
                                if (performerType.ToLower() == "music")
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        Console.WriteLine($"Enter the name of song {i + 1}:");
                                        string songName = GetUserInput();

                                        Console.WriteLine($"Is '{songName}' an original song? (yes/no)");
                                        string isOriginalInput = GetUserInput().ToLower();
                                        bool isOriginal = isOriginalInput == "yes";
                                    
                                    var newSong = new Song
                                    {
                                        PerformerId = existingPerformerToAdd.PerformerId,
                                        SongName = songName,
                                        IsOriginal = isOriginal
                                    };
                                        songs.Add(newSong); // Adding new song to the list
                                    }

                                    // Adding all songs to the database outside the loop
                                    dbContext.Songs.AddRange(songs);
                                }
                                else
                            { 
                                Console.WriteLine($"Performer '{performerAddToSignUpName}' exists with type '{performerType}'. No songs added.");
                            }
                            // Add songs to the database
                            
                            dbContext.SaveChanges();
                            Console.WriteLine($"Added songs to performer '{performerAddToSignUpName}'.");
                        }
                      else
                        {
                            Console.WriteLine("Performer not found in the database. Return to the main menu and add a new performer.");
                        }
                }
                break;
                        //this is not adding to the db :( 
                                  

                                    //if (existingPerformerToAdd.Count == 0) Console.WriteLine("There is no performer by that name. Please try again.");
                                    //else if (existingPerformerToAdd.Count == 1)
                                    //{
                                    //    tonightsPerformance.AddPerformanceToSignUpList(existingPerformerToAdd[0]);
                                    //    Console.WriteLine($"Added {performerAddToSignUpName} to tonight's open mic performance list.");
                                    //}
                                    //else
                                    //{
                                    //    foreach (var performance in existingPerformerToAdd)
                                    //    {
                                    //        Console.WriteLine(JsonSerializer.Serialize(performerAddToSignUpName));
                                    //    }
                                    //    Console.WriteLine();
                                    //    Console.WriteLine($"Enter the Id of the {performerAddToSignUpName} you would like to add.");
                                    //    var performerId = int.Parse(GetUserInput());
                                    //    var existingPerformerIds = existingPerformerToAdd.Select(x => x.PerformerId);
                                    //    if (existingPerformerIds.Contains(performerId))
                                    //    {
                                    //        tonightsPerformance.AddPerformanceToSignUpListById(performerId);
                                    //        Console.WriteLine($"Added {performerAddToSignUpName} to tonight's open mic performance list.");
                                    //    }
                                    //    else Console.WriteLine($"There is no {performerAddToSignUpName} with ID {performerId}. Please try again.");
                                    //}
                            //
                            //                      using (var dbContext = new PerformerContext())
                            //                      {
                            //                          var existingPerformer = dbContext.Performer.FirstOrDefault(p => p.PerformerName == performerToAddName);
                            //                          if (existingPerformer == null)
                            //                          {
                            //                              var newPerformer = new Performer { PerformerName = performerToAddName, PerformerType = performerToAddType, Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss") };
                            //                              signUpLogic.AddPerformer(newPerformer);
                            //                              Console.WriteLine($"Added {performerToAddName} to the performers list.");
                            //                          }
                            //                          else
                            //                          {
                            //                              Console.WriteLine($"Performer '{performerToAddName}' already exists.");
                            //                          }
                            //                      }

                    case "3":
                        Console.WriteLine("What is the name of the performer you would like to remove?");
                        var performerToRemoveName = GetUserInput();
                        var existingPerformerToRemove = signUpLogic.GetPerformersByName(performerToRemoveName);
                        if (existingPerformerToRemove.Count == 0)
                            Console.WriteLine("There is no performer with this name.");
                        else if (existingPerformerToRemove.Count == 1)
                        {
                            bool wasRemoved = signUpLogic.RemovePerformanceFromSignUpListById(existingPerformerToRemove[0].PerformerId);
                            if (wasRemoved)
                                Console.WriteLine($"{performerToRemoveName} was removed from the list.");
                            else
                                Console.WriteLine($"{performerToRemoveName} was not already on the list.");
                        }
                        else
                        {
                            foreach(var performer in existingPerformerToRemove)
                            {
                                Console.WriteLine(JsonSerializer.Serialize(performer) );
                            }
                            Console.WriteLine();
                            Console.WriteLine($"Enter the Id of the {performerToRemoveName} you would like to remove.");
                            var performerId = int.Parse(GetUserInput());
                            var existingPerformerIds = existingPerformerToRemove.Select(x => x.PerformerId);
                            if (existingPerformerIds.Contains(performerId))
                            {
                                bool wasRemoved = signUpLogic.RemovePerformanceFromSignUpListById(performerId);
                                if (wasRemoved)
                                    Console.WriteLine($"{performerToRemoveName} was removed from the sign up list for tonight's performances.");
                                else
                                    Console.WriteLine($"There is no {performerToRemoveName} with ID {performerId}.");
                            }
                        }
                        Console.WriteLine();
                        break;
                    case "back":
                                exitCondition = true;
                                break;
                            default:
                                Console.WriteLine("Invalid selection. Please try again.");
                                break;
                            }
                        }
            }
        
        static void DisplaySignUpMenu()
        {
        Console.WriteLine("Press 1 to view the list of performers for tonight");
        Console.WriteLine("Press 2 to add a performer to the list tonight");
        Console.WriteLine("Press 3 to remove a performer from the list tonight");
        Console.WriteLine("Type 'back' to return to the Main Menu");
        Console.WriteLine("Choice:");
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