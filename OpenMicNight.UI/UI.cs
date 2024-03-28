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
                            Console.WriteLine($"ID:{performer.PerformerId}   -    Name: {performer.PerformerName}   -   Type: {performer.PerformerType}");
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
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine($"Performer '{performerToAddName}' already exists.");
                                Console.WriteLine();
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
            SignUpList signUpList = signUpLogic.GetSignUpList();
            while (!exitCondition)
            {
                DisplaySignUpMenu();
                string choice = GetUserInput().ToLower();
                switch (choice)
                {
                    case "1": //Display the sign up list
                        Console.WriteLine("Sign Up List:");
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");

                        if (signUpList.Performances.Count == 0)
                        {
                            Console.WriteLine("There are no performances yet signed up for this evening.");
                            Console.WriteLine(" ");
                            Console.WriteLine(" ");
                        }
                        else
                        {
                            foreach (var performer in signUpList.Performances)
                            {
                                Console.WriteLine(" ");
                                Console.WriteLine(" ");
                                Console.WriteLine($"Performer: {performer.PerformerName}");
                                Console.WriteLine($"Type of Performance: {performer.PerformerType}");

                                if (performer.PerformerType.ToLower() == "music")
                                {
                                    Console.WriteLine("Songs:");
                                    foreach (var song in performer.Songs)
                                    {
                                        string originalStatus = song.IsOriginal ? "yes" : "no";
                                        Console.WriteLine($"- {song.SongName} (Original: {originalStatus})");
                                    }
                                }
                            }
                            Console.WriteLine(" ");
                        }
                        break;

                    case "2": //Sign up a new performer 


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
                            var songRepository = new SongRepository(dbContext);
                            var existingPerformerToAdd = dbContext.Performer.FirstOrDefault(p => p.PerformerName == performerAddToSignUpName);
                            if (existingPerformerToAdd != null)
                            {
                                Console.WriteLine($"Performer '{performerAddToSignUpName}' found!");
                                Console.WriteLine(" ");
                                Console.WriteLine(" ");

                                string performerType = existingPerformerToAdd.PerformerType;
                                int performerId = existingPerformerToAdd.PerformerId;

                                if (performerType.ToLower() == "music")
                                {
                                    var performerSongs = songRepository.GetAllSongsByPerformerId(performerId);
                                    Console.WriteLine("Here are the existing songs for this performer: ");
                                    foreach (var song in performerSongs)
                                    {
                                        Console.WriteLine($" - {song.SongName}");
                                    }

                                    var newSongs = new List<Song>();

                                    for (int i = 0; i < 3; i++)
                                    {
                                        Console.WriteLine($"Enter the name of song {i + 1} you will be performing:");
                                        string songName = GetUserInput();

                                        //Checking if song already exists in the database
                                        var existingSong = dbContext.Songs.FirstOrDefault(s => s.PerformerId == existingPerformerToAdd.PerformerId && s.SongName.ToLower() == songName.ToLower());

                                        if (existingSong == null)
                                        {

                                            Console.WriteLine($"Is '{songName}' an original song? (yes/no)");
                                            string isOriginalInput = GetUserInput().ToLower();
                                            bool isOriginal = isOriginalInput == "yes";
                                            var newSong = new Song
                                            {
                                                PerformerId = existingPerformerToAdd.PerformerId,
                                                SongName = songName,
                                                IsOriginal = isOriginal
                                            };
                                            newSongs.Add(newSong); // Adding new song to the database
                                        }
                                    }
                                    //Adding performer to the sign up list 
                                    signUpList.Performances.Add(existingPerformerToAdd);
                                    Console.WriteLine($"Added '{performerAddToSignUpName}' to sign up list. Good luck and have fun! ");
                                    Console.WriteLine(" ");
                                    Console.WriteLine(" ");

                                    dbContext.Songs.AddRange(newSongs);
                                    dbContext.SaveChanges();
                                }
                                else
                                {
                                    signUpList.Performances.Add(existingPerformerToAdd);
                                    Console.WriteLine($"Performer '{performerAddToSignUpName}' has been added to the list tonight with type '{performerType}'. Good luck and have fun! ");
                                    Console.WriteLine(" ");
                                    Console.WriteLine(" ");
                                }

                                dbContext.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Performer not found in the database. Return to the main menu and add a new performer.");
                            }
                        }
                break;

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
