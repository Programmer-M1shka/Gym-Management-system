using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym_Management_System.Data;
using Gym_Management_System.Models;
using static Gym_Management_System.Enums.GymEnums;
using Microsoft.Extensions.DependencyInjection;

namespace Gym_Management_System.Models
{
    internal class GymService
    {

        private static DataContex _context;
        private static MemberRepository _memberRepo;
        private static TrainerRepository _trainerRepo;
        private static ProgramRepository _programRepo;
        private static SessionRepository _sessionRepo;
        private static LoggingService _loggingService;
        private static ValidationService _validationService;

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║                                        ║");
            Console.WriteLine("║          Gym Management System         ║");
            Console.WriteLine("║                                        ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.ResetColor(); 
            Initialize();

            bool exit = false;
            while (!exit)
            {
                ShowMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        MemberManagement();
                        break;
                    case "2":
                        TrainerManagement();
                        break;
                    case "3":
                        ProgramManagement();
                        break;
                    case "4":
                        SessionManagement();
                        break;
                    case "5":
                        Analytics();
                        break;
                    case "6":
                        FileManagement();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong Choise!");
                        break;
                }
            }
        }

        static void Initialize()
        {
            _context = new DataContex();
            _context.Database.EnsureCreated();

            _memberRepo = new MemberRepository(_context);
            _trainerRepo = new TrainerRepository(_context);
            _programRepo = new ProgramRepository(_context);
            _sessionRepo = new SessionRepository(_context);
            _loggingService = new LoggingService();
            _validationService = new ValidationService();
        }

        static void ShowMainMenu()
        {
            Console.Clear();

            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== Fitness Center Management System =====");

          
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1.  ➤ Manage Members");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2.  ➤ Manage Trainers");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("3.  ➤ Manage Programs");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("4.  ➤ Manage Sessions");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("5.  ➤ Analytics (LINQ)");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("6.  ➤ Manage Files");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("0.  ➤ Exit");

          
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==========================================");

          
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Choice: ");
            Console.ResetColor();


        }

       
        static void MemberManagement()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== Members Management =====");

               
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1.  ➤ Register New Member");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("2.  ➤ Update Member Details");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("3.  ➤ Change Membership Status");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("4.  ➤ Member List");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("5.  ➤ Member Detailed Information");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("0.  ➤ Back");

               
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==========================================");

              
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Choice: ");
                Console.ResetColor(); 


                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewMember();
                        break;
                    case "2":
                        UpdateMember();
                        break;
                    case "3":
                        ChangeMemberStatus();
                        break;
                    case "4":
                        ListMembers();
                        break;
                    case "5":
                        ShowMemberDetails();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Wrong Choise!");
                        break;
                }
            }
        }

        static void AddNewMember()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan; 
            Console.WriteLine("===== New Member Registration =====");

            Member member = new Member();
            MemberDetails details = new MemberDetails();

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.Write("First Name: ");
            member.FirstName = Console.ReadLine();

            Console.Write("Last Name: ");
            member.LastName = Console.ReadLine();

            bool validPersonalNumber = false;
            while (!validPersonalNumber)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Personal Number (11 digits): ");
                member.PersonalNumber = Console.ReadLine();
                validPersonalNumber = _validationService.ValidatePersonalNumber(member.PersonalNumber);
                if (!validPersonalNumber)
                    Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine("Invalid personal number!");
            }

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.Write("Date of Birth (yyyy-MM-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dob))
                details.DateOfBirth = dob;
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine("Invalid date format, date of birth was not saved.");
            }

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.Write("Phone Number: ");
            details.PhoneNumber = Console.ReadLine();

            bool validEmail = false;
            while (!validEmail)
            {
                Console.ForegroundColor = ConsoleColor.Yellow; 
                Console.Write("Email: ");
                details.Email = Console.ReadLine();
                validEmail = _validationService.ValidateEmail(details.Email);
                if (!validEmail)
                {
                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine("Invalid email!");
                }
            }

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.Write("Emergency Contact: ");
            details.EmergencyContact = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.Write("Medical Notes: ");
            details.MedicalNotes = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Height (cm): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal height))
                details.Height = height;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Weight (kg): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal weight))
                details.Weight = weight;

            
            member.Status = MemberStatus.Active;
            member.MemberDetails = details;

            _memberRepo.Add(member);
            _loggingService.LogEvent("Member Registration", $"Registered member: {member.FirstName} {member.LastName}");

            Console.ForegroundColor = ConsoleColor.Cyan; 
            Console.WriteLine("Member successfully registered!");
            Console.ResetColor(); 
            Console.ReadKey();
        }

            static void UpdateMember()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan; 
            Console.WriteLine("===== Member Update =====");

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.Write("Enter Member ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var member = _memberRepo.GetById(id);
                if (member != null)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; 
                    Console.WriteLine($"Member: {member.FirstName} {member.LastName}");

                    Console.ForegroundColor = ConsoleColor.Yellow; 
                    Console.Write("New First Name (Press Enter to leave unchanged): ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                        member.FirstName = newName;

                    Console.ForegroundColor = ConsoleColor.Yellow; 
                    Console.Write("New Last Name: ");
                    string newLastName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newLastName))
                        member.LastName = newLastName;

                    if (member.MemberDetails == null)
                        member.MemberDetails = new MemberDetails { MemberId = member.Id };

                    Console.ForegroundColor = ConsoleColor.Yellow; 
                    Console.Write("New Phone Number: ");
                    string newPhone = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newPhone))
                        member.MemberDetails.PhoneNumber = newPhone;

                    Console.ForegroundColor = ConsoleColor.Yellow; 
                    Console.Write("New Email: ");
                    string newEmail = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newEmail))
                    {
                        if (_validationService.ValidateEmail(newEmail))
                            member.MemberDetails.Email = newEmail;
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red; 
                            Console.WriteLine("Invalid email! Email was not changed.");
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("New Height (cm): ");
                    string heightStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(heightStr) && decimal.TryParse(heightStr, out decimal height))
                        member.MemberDetails.Height = height;

                    Console.ForegroundColor = ConsoleColor.Yellow; 
                    Console.Write("New Weight (kg): ");
                    string weightStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(weightStr) && decimal.TryParse(weightStr, out decimal weight))
                        member.MemberDetails.Weight = weight;

                    _memberRepo.Update(member);
                    _loggingService.LogEvent("Member Update", $"Updated member: {member.FirstName} {member.LastName}");

                    Console.ForegroundColor = ConsoleColor.Green; 
                    Console.WriteLine("Member successfully updated!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine("Member not found!");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine("Invalid ID!");
            }

            Console.ResetColor(); 
            Console.ReadKey();

        }

        static void ChangeMemberStatus()
        {
            Console.Clear();

           
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("===== Membership Status Update =====");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.Write("Enter Member ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var member = _memberRepo.GetById(id);
                if (member != null)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; 
                    Console.WriteLine($"Member: {member.FirstName} {member.LastName}");
                    Console.WriteLine($"Current Status: {member.Status}");

              
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Select New Status:");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("1. Active");
                    Console.WriteLine("2. Frozen");
                    Console.WriteLine("3. Expired");
                    Console.WriteLine("4. Cancelled");

                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            member.Status = MemberStatus.Active;
                            break;
                        case "2":
                            member.Status = MemberStatus.Frozen;
                            break;
                        case "3":
                            member.Status = MemberStatus.Expired;
                            break;
                        case "4":
                            member.Status = MemberStatus.Cancelled;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red; 
                            Console.WriteLine("Invalid choice!");
                            Console.ReadKey();
                            return;
                    }

                    _memberRepo.Update(member);
                    _loggingService.LogEvent("Member Status Update", $"Member status changed: {member.FirstName} {member.LastName} -> {member.Status}");

                   
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Member status successfully updated!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Member not found!");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine("Invalid ID!");
            }

            Console.ResetColor();
            Console.ReadKey();

        }


        static void ListMembers()
        {
            Console.Clear();

           
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("===== Member List =====");
            Console.ResetColor();

            var members = _memberRepo.GetAll();
            if (members.Any())
            {
                
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("ID\tFull Name\t   -----    \tStatus\t");
                Console.ResetColor();

                Console.WriteLine("--------------------------------------------------");

               
                foreach (var member in members)
                {
                    Console.ForegroundColor = ConsoleColor.Green; 
                    Console.WriteLine($"{member.Id}\t{member.FirstName} {member.LastName}\t\t{member.Status}");
                }
            }
            else
            {
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No members are registered!");
            }

            Console.ResetColor(); 
            Console.ReadKey();

        }


        static void ShowMemberDetails()
        {
            Console.Clear();

            
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("===== Member Detailed Information =====");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.Write("Enter Member ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var member = _memberRepo.GetById(id);
                if (member != null)
                {
                    
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nMember Information:");
                    Console.ResetColor();

                  
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ID: {member.Id}");
                    Console.WriteLine($"Full Name: {member.FirstName} {member.LastName}");
                    Console.WriteLine($"Personal Number: {member.PersonalNumber}");
                    Console.WriteLine($"Status: {member.Status}");

                    if (member.MemberDetails != null)
                    {
                        
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nDetailed Information:");
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Date of Birth: {member.MemberDetails.DateOfBirth:yyyy-MM-dd}");
                        Console.WriteLine($"Phone Number: {member.MemberDetails.PhoneNumber}");
                        Console.WriteLine($"Email: {member.MemberDetails.Email}");
                        Console.WriteLine($"Emergency Contact: {member.MemberDetails.EmergencyContact}");
                        Console.WriteLine($"Medical Notes: {member.MemberDetails.MedicalNotes}");
                        Console.WriteLine($"Height: {member.MemberDetails.Height} cm");
                        Console.WriteLine($"Weight: {member.MemberDetails.Weight} kg");
                    }
                    else
                    {
                       
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nDetailed information not available.");
                    }
                }
                else
                {
                 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Member not found!");
                }
            }
            else
            {
               
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID!");
            }

            Console.ResetColor(); 
            Console.ReadKey();

        }


        static void TrainerManagement()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();

               
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("===== Coach Management =====");
                Console.ResetColor();

                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1.  ➤ Add New Coach");
                Console.WriteLine("2.  ➤ Update Coach Information");
                Console.WriteLine("3.  ➤ Coach List");
                Console.WriteLine("4.  ➤ Coach Detailed Information");
                Console.WriteLine("5.  ➤ Available Coaches List");
                Console.WriteLine("6.  ➤ Export Coach Schedule");
                Console.WriteLine("0.  ➤ Return");

                
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Choose an option: ");
                Console.ResetColor();


                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewTrainer();
                        break;
                    case "2":
                        UpdateTrainer();
                        break;
                    case "3":
                        ListTrainers();
                        break;
                    case "4":
                        ShowTrainerDetails();
                        break;
                    case "5":
                        ListAvailableTrainers();
                        break;
                    case "6":
                        ExportTrainerSchedule();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Wrong Choise!");
                        break;
                }
            }
        }

        static void AddNewTrainer()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=====================================");
            Console.WriteLine("          Add a New Trainer          ");
            Console.WriteLine("=====================================");
            Console.ResetColor();

            Trainer trainer = new Trainer();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nFirst Name: ");
            Console.ResetColor();
            trainer.FirstName = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nLast Name: ");
            Console.ResetColor();
            trainer.LastName = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nSpecialization: ");
            Console.ResetColor();
            trainer.Specialization = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nExperience (years): ");
            Console.ResetColor();
            if (int.TryParse(Console.ReadLine(), out int experience))
                trainer.Experience = experience;
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input, experience set to 0.");
                Console.ResetColor();
                trainer.Experience = 0;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nHourly Rate: ");
            Console.ResetColor();
            if (decimal.TryParse(Console.ReadLine(), out decimal rate))
                trainer.HourlyRate = rate;
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid input, rate set to 0.");
                Console.ResetColor();
                trainer.HourlyRate = 0;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nAvailable (y/n): ");
            Console.ResetColor();
            string available = Console.ReadLine().ToLower();
            trainer.IsAvailable = available == "y";

            _trainerRepo.Add(trainer);
            _loggingService.LogEvent("Trainer Added", $"Trainer: {trainer.FirstName} {trainer.LastName} added");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=====================================");
            Console.WriteLine("     Trainer added successfully!     ");
            Console.WriteLine("=====================================");
            Console.ResetColor();
            Console.ReadKey();


        }

        static void UpdateTrainer()
        {
            Console.Clear();

            
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("===== Update Coach =====");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter Coach ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var trainer = _trainerRepo.GetById(id);
                if (trainer != null)
                {
                
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Coach: {trainer.FirstName} {trainer.LastName}");
                    Console.ResetColor();

                 
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("New First Name (Press Enter to keep current): ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                        trainer.FirstName = newName;

                    
                    Console.Write("New Last Name: ");
                    string newLastName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newLastName))
                        trainer.LastName = newLastName;

                   
                    Console.Write("New Specialization: ");
                    string newSpecialization = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newSpecialization))
                        trainer.Specialization = newSpecialization;

                 
                    Console.Write("New Experience (years): ");
                    string expStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(expStr) && int.TryParse(expStr, out int experience))
                        trainer.Experience = experience;

              
                    Console.Write("New Hourly Rate: ");
                    string rateStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(rateStr) && decimal.TryParse(rateStr, out decimal rate))
                        trainer.HourlyRate = rate;

                
                    Console.Write("Available (y/n): ");
                    string available = Console.ReadLine().ToLower();
                    if (available == "y" || available == "n")
                        trainer.IsAvailable = available == "y";

               
                    _trainerRepo.Update(trainer);
                    _loggingService.LogEvent("Coach Update", $"Updated coach: {trainer.FirstName} {trainer.LastName}");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Coach successfully updated!");
                }
                else
                {
                   
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Coach not found!");
                }
            }
            else
            {
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID!");
            }

            Console.ResetColor(); 

        }

        static void ListTrainers()
        {
            Console.Clear();

       
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("===== Trainers List =====");
            Console.ResetColor();

   
            var trainers = _trainerRepo.GetAll();
            if (trainers.Any())
            {
        
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ID\tName\t\tSpecialization\t\tAvailable");
                Console.WriteLine("--------------------------------------------------------");
                Console.ResetColor();

              
                foreach (var trainer in trainers)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{trainer.Id}\t{trainer.FirstName} {trainer.LastName}\t\t{trainer.Specialization}\t\t{(trainer.IsAvailable ? "Yes" : "No")}");
                }
            }
            else
            {
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No trainers are registered!");
            }

            Console.ResetColor(); 
            Console.ReadKey();

        }

        static void ShowTrainerDetails()
        {
            Console.Clear();

            
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("===== Trainer Detailed Information =====");
            Console.ResetColor();

            
            Console.Write("Enter Trainer ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var trainer = _trainerRepo.GetById(id);
                if (trainer != null)
                {
                   
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"ID: {trainer.Id}");
                    Console.WriteLine($"Name: {trainer.FirstName} {trainer.LastName}");
                    Console.WriteLine($"Specialization: {trainer.Specialization}");
                    Console.WriteLine($"Experience: {trainer.Experience} years");
                    Console.WriteLine($"Hourly Rate: {trainer.HourlyRate:F2}");
                    Console.WriteLine($"Available: {(trainer.IsAvailable ? "Yes" : "No")}");
                    Console.ResetColor();

                    
                    if (trainer.TrainingPrograms != null && trainer.TrainingPrograms.Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nActive Training Programs:");
                        Console.ResetColor();
                        foreach (var program in trainer.TrainingPrograms.Where(p => p.Status == ProgramStatus.Active))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"- {program.ProgramType} (Member: {program.Member?.FirstName} {program.Member?.LastName})");
                        }
                    }
                    else
                    {
                        
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNo active programs available.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Trainer not found!");
                    Console.ResetColor();
                }
            }
            else
            {
               
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID format!");
                Console.ResetColor();
            }

            Console.ReadKey();

        }

        static void ListAvailableTrainers()
        {
            Console.Clear();

         
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("===== Available Trainers List =====");
            Console.ResetColor();

           
            var trainers = _trainerRepo.GetAvailableTrainers();
            if (trainers.Any())
            {
               
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0,-10} {1,-25} {2,-20} {3,-15}", "ID", "Name and Surname", "Specialization", "Experience");
                Console.WriteLine(new string('-', 70));  
                Console.ResetColor();

              
                foreach (var trainer in trainers)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0,-10} {1,-25} {2,-20} {3,-15}", trainer.Id, $"{trainer.FirstName} {trainer.LastName}", trainer.Specialization, $"{trainer.Experience} years");
                    Console.ResetColor();
                }
            }
            else
            {
              
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No available trainers at the moment!");
                Console.ResetColor();
            }

            Console.ReadKey();


        }

        static void ExportTrainerSchedule()
        {
            Console.Clear();

           
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=====  Coach Schedule Export  =====");
            Console.ResetColor();
            Console.WriteLine(); 

          
            Console.Write("Please enter the trainer's ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _trainerRepo.ExportTrainerSchedule(id);

                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nExport Completed Successfully.");
                Console.ResetColor();
            }
            else
            {
               
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid ID! Please try again with a valid numeric ID.");
                Console.ResetColor();
            }

            Console.ReadKey();

        }


        static void ProgramManagement()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();

             
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("*************************************");
                Console.WriteLine("         Program Management          ");
                Console.WriteLine("*************************************");

                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1.  ➤ Create New Program");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("2.  ➤ Update Program");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("3.  ➤ Change Program Status");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("4.  ➤ Program List");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("5.  ➤ Program Details");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("0.  ➤ Back");

       
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("*************************************");

            
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Choice: ");
                Console.ResetColor(); 


                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNewProgram();
                        break;
                    case "2":
                        UpdateProgram();
                        break;
                    case "3":
                        ChangeProgramStatus();
                        break;
                    case "4":
                        ListPrograms();
                        break;
                    case "5":
                        ShowProgramDetails();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Wrong Choise!");
                        break;
                }
            }
        }
         
        static void CreateNewProgram()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=====================================");
            Console.WriteLine("         Create New Program          ");
            Console.WriteLine("=====================================");
            Console.ResetColor();

            TrainingProgram program = new TrainingProgram();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== Select Member =====");
            Console.ResetColor();
            var members = _memberRepo.GetActiveMembers();
            if (!members.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No active members registered!");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ID\tName and Surname");
            Console.WriteLine("---------------------------");
            foreach (var member in members)
            {
                Console.WriteLine($"{member.Id}\t{member.FirstName} {member.LastName}");
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nSelect Member ID: ");
            Console.ResetColor();
            if (int.TryParse(Console.ReadLine(), out int memberId))
            {
                var member = _memberRepo.GetById(memberId);
                if (member == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Member not found!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }
                program.MemberId = memberId;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID!");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== Select Trainer =====");
            Console.ResetColor();
            var trainers = _trainerRepo.GetAvailableTrainers();
            if (!trainers.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No available trainers registered!");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ID\tName and Surname\t\tSpecialization");
            Console.WriteLine("--------------------------------------------------");
            foreach (var trainer in trainers)
            {
                Console.WriteLine($"{trainer.Id}\t{trainer.FirstName} {trainer.LastName}\t\t{trainer.Specialization}");
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nSelect Trainer ID: ");
            Console.ResetColor();
            if (int.TryParse(Console.ReadLine(), out int trainerId))
            {
                var trainer = _trainerRepo.GetById(trainerId);
                if (trainer == null || !trainer.IsAvailable)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Trainer not found or not available!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }
                program.TrainerId = trainerId;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID!");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=====================================");
            Console.WriteLine("     Program created successfully!   ");
            Console.WriteLine("=====================================");
            Console.ResetColor();
            Console.ReadKey();


            Console.Clear();

           
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n=====  Program Type Selection  =====");
            Console.ResetColor();  
            Console.WriteLine(); 

           
            Console.WriteLine("Please select the type of program:");
            Console.WriteLine("------------------------------------");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Personal");
            Console.WriteLine("2. Group");
            Console.WriteLine("3. CardioFitness");
            Console.WriteLine("4. StrengthTraining");
            Console.WriteLine("5. Rehabilitation");
            Console.ResetColor();

            Console.Write("\nEnter your choice: ");

            string typeChoice = Console.ReadLine();
            switch (typeChoice)
            {
                case "1":
                    program.ProgramType = ProgramType.Personal;
                    break;
                case "2":
                    program.ProgramType = ProgramType.Group;
                    break;
                case "3":
                    program.ProgramType = ProgramType.CardioFitness;
                    break;
                case "4":
                    program.ProgramType = ProgramType.StrengthTraining;
                    break;
                case "5":
                    program.ProgramType = ProgramType.Rehabilitation;
                    break;
                default:
                    program.ProgramType = ProgramType.Personal;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Invalid choice! Automatically selected 'Personal'.");
                    break;
            }


            Console.Clear();
            Console.WriteLine("===== Program Date Setup =====");

            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nStart Date (yyyy-MM-dd): ");
            Console.ResetColor();

            if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                program.StartDate = startDate;
            }

        
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("End Date (yyyy-MM-dd): ");
            Console.ResetColor();

            if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate) && endDate > program.StartDate)
            {
                program.EndDate = endDate;
            }
            else
            {
                program.EndDate = program.StartDate.AddMonths(1);

                
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\nInvalid date! The end date has been automatically set to: {program.EndDate.ToShortDateString()}");
                Console.ResetColor();  
            }

           
            _programRepo.Add(program);
            _loggingService.LogEvent("Program Creation", $"Created program: {program.ProgramType}, Member ID: {program.MemberId}, Trainer ID: {program.TrainerId}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nProgram successfully created!");
            Console.ResetColor();

            Console.ReadKey();

        }

        static void UpdateProgram()
        {
            Console.Clear();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=====================================");
            Console.WriteLine("           Update Program            ");
            Console.WriteLine("=====================================");
            Console.ResetColor();


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter program ID: ");
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var program = _programRepo.GetById(id);
                if (program != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Program: {program.ProgramType}");
                    Console.WriteLine($"Member: {program.Member?.FirstName} {program.Member?.LastName}");
                    Console.WriteLine($"Trainer: {program.Trainer?.FirstName} {program.Trainer?.LastName}");
                    Console.ResetColor();


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nWould you like to change the trainer? (y/n): ");
                    Console.ResetColor();

                    if (Console.ReadLine().ToLower() == "y")
                    {
                        var trainers = _trainerRepo.GetAvailableTrainers();
                        if (trainers.Any())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("ID\tName\t\tSpecialization");
                            Console.ResetColor();

                            Console.WriteLine("--------------------------------------------------");
                            foreach (var trainer in trainers)
                            {
                                Console.WriteLine($"{trainer.Id}\t{trainer.FirstName} {trainer.LastName}\t\t{trainer.Specialization}");
                            }

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("\nSelect new trainer ID: ");
                            Console.ResetColor();

                            if (int.TryParse(Console.ReadLine(), out int trainerId))
                            {
                                var trainer = _trainerRepo.GetById(trainerId);
                                if (trainer != null && trainer.IsAvailable)
                                {
                                    program.TrainerId = trainerId;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Trainer not found or not available!");
                                    Console.ResetColor();

                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Wrong ID!");
                                Console.ResetColor();

                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No available trainers!");
                            Console.ResetColor();

                        }
                    }


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nWould you like to change the program type? (y/n): ");
                    Console.ResetColor();

                    if (Console.ReadLine().ToLower() == "y")
                    {
                        Console.WriteLine("1. Personal");
                        Console.WriteLine("2. Group");
                        Console.WriteLine("3. CardioFitness");
                        Console.WriteLine("4. StrengthTraining");
                        Console.WriteLine("5. Rehabilitation");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Select new program type: ");
                        Console.ResetColor();


                        string typeChoice = Console.ReadLine();
                        switch (typeChoice)
                        {
                            case "1":
                                program.ProgramType = ProgramType.Personal;
                                break;
                            case "2":
                                program.ProgramType = ProgramType.Group;
                                break;
                            case "3":
                                program.ProgramType = ProgramType.CardioFitness;
                                break;
                            case "4":
                                program.ProgramType = ProgramType.StrengthTraining;
                                break;
                            case "5":
                                program.ProgramType = ProgramType.Rehabilitation;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Wrong choice! Type not changed.");
                                Console.ResetColor();

                                break;
                        }
                    }


                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nWould you like to change the dates? (y/n): ");
                    Console.ResetColor();

                    if (Console.ReadLine().ToLower() == "y")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("New start date (yyyy-mm-dd): ");
                        Console.ResetColor();

                        if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                        {
                            program.StartDate = startDate;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid date format! Start date not changed.");
                            Console.ResetColor();

                        }

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("New end date (yyyy-mm-dd): ");
                        Console.ResetColor();

                        if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate) && endDate > program.StartDate)
                        {
                            program.EndDate = endDate;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid date format or end date is before the start date! End date not changed.");
                            Console.ResetColor();

                        }
                    }

                    _programRepo.Update(program);
                    _loggingService.LogEvent("Program Update", $"Program ID: {program.Id} updated");


                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Program updated successfully!");
                    Console.ResetColor();

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Program not found!");
                    Console.ResetColor();

                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID!");
                Console.ResetColor();

            }

            Console.ReadKey();
        }

        static void ChangeProgramStatus()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== Change Program Status =====");
            Console.ResetColor();

            Console.Write("Enter Program ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var program = _programRepo.GetById(id);
                if (program != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nProgram: {program.ProgramType}");
                    Console.WriteLine($"Member: {program.Member?.FirstName} {program.Member?.LastName}");
                    Console.WriteLine($"Trainer: {program.Trainer?.FirstName} {program.Trainer?.LastName}");
                    Console.WriteLine($"Current Status: {program.Status}");
                    Console.ResetColor();

                    Console.WriteLine("\nSelect a new status:");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1. Active");
                    Console.WriteLine("2. Paused");
                    Console.WriteLine("3. Completed");
                    Console.WriteLine("4. Cancelled");
                    Console.ResetColor();

                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            program.Status = ProgramStatus.Active;
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case "2":
                            program.Status = ProgramStatus.Paused;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        case "3":
                            program.Status = ProgramStatus.Completed;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case "4":
                            program.Status = ProgramStatus.Cancelled;
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid choice!");
                            Console.ReadKey();
                            return;
                    }

                    _programRepo.Update(program);
                    _loggingService.LogEvent("Program Status Change", $"Program status changed ID: {program.Id} -> {program.Status}");

                    Console.ResetColor();
                    Console.WriteLine("\nProgram status successfully updated!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Program not found!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID!");
                Console.ResetColor();
            }

            Console.ReadKey();

        }

        static void ListPrograms()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== Program List =====");
            Console.ResetColor();

            var programs = _programRepo.GetAll();
            if (programs.Any())
            {
        
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0,-6} {1,-20} {2,-25} {3,-25} {4,-10}", "ID", "Program Type", "Member", "Trainer", "Status");
                Console.WriteLine("---------------------------------------------------------------------------------------");
                Console.ResetColor();

                foreach (var program in programs)
                {
                    string memberName = program.Member != null ? $"{program.Member.FirstName} {program.Member.LastName}" : "N/A";
                    string trainerName = program.Trainer != null ? $"{program.Trainer.FirstName} {program.Trainer.LastName}" : "N/A";

                   
                    Console.WriteLine("{0,-6} {1,-20} {2,-25} {3,-25} {4,-10}",
                        program.Id,
                        program.ProgramType,
                        memberName,
                        trainerName,
                        program.Status);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No programs are registered!");
                Console.ResetColor();
            }

            Console.ReadKey();



        }

        static void ShowProgramDetails()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== Program Details =====");
            Console.ResetColor();

            Console.Write("Enter Program ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var program = _programRepo.GetById(id);
                if (program != null)
                {
                    Console.WriteLine($"ID: {program.Id}");
                    Console.WriteLine($"Program Type: {program.ProgramType}");
                    Console.WriteLine($"Status: {program.Status}");
                    Console.WriteLine($"Start Date: {program.StartDate.ToShortDateString()}");
                    Console.WriteLine($"End Date: {program.EndDate.ToShortDateString()}");

                    if (program.Member != null)
                    {
                        Console.WriteLine($"\nMember: {program.Member.FirstName} {program.Member.LastName}");
                        Console.WriteLine($"Member Status: {program.Member.Status}");
                    }

                    if (program.Trainer != null)
                    {
                        Console.WriteLine($"\nTrainer: {program.Trainer.FirstName} {program.Trainer.LastName}");
                        Console.WriteLine($"Specialization: {program.Trainer.Specialization}");
                        Console.WriteLine($"Experience: {program.Trainer.Experience} years");
                    }

                    if (program.TrainingSessions != null && program.TrainingSessions.Any())
                    {
                        Console.WriteLine("\nSessions:");
                        foreach (var session in program.TrainingSessions.OrderBy(s => s.Date))
                        {
                            Console.WriteLine($"- {session.Date.ToString("yyyy-MM-dd HH:mm")} ({session.Duration} minutes) - {session.Status}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo sessions scheduled.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Program not found!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ID!");
                Console.ResetColor();
            }

            Console.ReadKey();

        }


        static void SessionManagement()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==== Session Management ====");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("4.1  Schedule Session");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("4.2  Complete Session");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("4.3  Cancel Session");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("4.4  View Daily Sessions");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("0.    Go Back");
                Console.ResetColor();

                Console.Write("\nSelect an operation: ");



                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "4.1":
                        PlanSession();
                        break;
                    case "4.2":
                        CompleteSession();
                        break;
                    case "4.3":
                        CancelSession();
                        break;
                    case "4.4":
                        ViewDailySessions();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong Choise!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void PlanSession()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==== Session Scheduling ====");
            Console.ResetColor();

            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter Program ID: ");
                Console.ResetColor();
                if (!int.TryParse(Console.ReadLine(), out int programId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ID format!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter Date (YYYY-MM-DD): ");
                Console.ResetColor();
                string dateStr = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter Time (HH:MM): ");
                Console.ResetColor();
                string timeStr = Console.ReadLine();

                if (!DateTime.TryParse($"{dateStr} {timeStr}", out DateTime sessionDateTime))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date or time format!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Session Duration (in minutes): ");
                Console.ResetColor();
                if (!int.TryParse(Console.ReadLine(), out int duration))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid duration format!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Additional Notes: ");
                Console.ResetColor();
                string notes = Console.ReadLine();

                var session = new TrainingSession
                {
                    ProgramId = programId,
                    Date = sessionDateTime,
                    Duration = duration,
                    Notes = notes
                };

                var sessionRepo = new SessionRepository(new DataContex());
                sessionRepo.Add(session);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Session successfully scheduled!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }

            Console.ReadKey();

        }

        static void CompleteSession()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==== Session Completion ====");
            Console.ResetColor();

            try
            {
               
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter Session ID: ");
                Console.ResetColor();
                if (!int.TryParse(Console.ReadLine(), out int sessionId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ID format!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

               
                var context = new DataContex();
                var sessionRepo = new SessionRepository(context);
                var session = sessionRepo.GetById(sessionId);

                if (session == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Session not found!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

               
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Completion Comments: ");
                Console.ResetColor();
                string completionNotes = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(completionNotes))
                {
                    session.Notes = string.IsNullOrWhiteSpace(session.Notes)
                        ? $"Completed: {completionNotes}"
                        : $"{session.Notes}\n\nCompleted: {completionNotes}";
                }
                else
                {
                    session.Notes = string.IsNullOrWhiteSpace(session.Notes)
                        ? "Completed"
                        : $"{session.Notes}\n\nCompleted";
                }

                
                sessionRepo.Update(session);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Session successfully completed!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }

            Console.ReadKey();

        }

        static void CancelSession()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==== Session Cancellation ====");
            Console.ResetColor();

            try
            {
               
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter Session ID: ");
                Console.ResetColor();
                if (!int.TryParse(Console.ReadLine(), out int sessionId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ID format!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

             
                var context = new DataContex();
                var sessionRepo = new SessionRepository(context);
                var session = sessionRepo.GetById(sessionId);

                if (session == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Session not found!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

            
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Are you sure you want to delete session (ID: {sessionId}, Date: {session.Date:yyyy-MM-dd HH:mm})? (y/n)");
                Console.ResetColor();
                string confirmation = Console.ReadLine()?.ToLower();

                if (confirmation != "y" && confirmation != "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Session cancellation has been aborted.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                
                context.TrainingSessions.Remove(session);
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Session successfully deleted!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }

            Console.ReadKey();

        }

        static void ViewDailySessions()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==== Daily Session View ====");
            Console.ResetColor();

            try
            {
               
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter a date (YYYY-MM-DD), or leave blank for today: ");
                Console.ResetColor();
                string dateStr = Console.ReadLine();

                DateTime date = string.IsNullOrWhiteSpace(dateStr) ?
                    DateTime.Today :
                    DateTime.Parse(dateStr);

                var sessionRepo = new SessionRepository(new DataContex());
                var sessions = sessionRepo.GetByDate(date);

            
                if (sessions.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"No sessions found for {date:yyyy-MM-dd}!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Sessions for {date:yyyy-MM-dd}:");
                Console.ResetColor();
                Console.WriteLine("ID | Time | Member | Trainer | Duration");
                Console.WriteLine("------------------------------------------");

                foreach (var session in sessions)
                {
                    string memberName = session.Program?.Member?.FirstName ?? "N/A";
                    string trainerName = session.Program?.Trainer?.FirstName ?? "N/A";

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{session.Id} | {session.Date:HH:mm} | {memberName} | {trainerName} | {session.Duration} min");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }

            Console.ReadKey();

        }



        static void Analytics()
        {
            while (true)
            {
                Console.Clear();

               
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("══════════════════════════════");
                Console.WriteLine("=====   (LINQ)   =====");
                Console.WriteLine("══════════════════════════════");

                Console.ResetColor();
                Console.WriteLine();

           
                Console.ForegroundColor = ConsoleColor.Green;
                
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();


                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===============================================");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. 5.1 Active Member Statistics");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("2. 5.2 Coaches' Workload");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("3. 5.3 Popular Programs");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("4. 5.4 Session Analysis");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===============================================");



                Console.ResetColor();


                Console.ResetColor();
                Console.WriteLine("══════════════════════════════");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("0. Back page");
                Console.WriteLine("══════════════════════════════");

          
                Console.ResetColor();


                Console.Write("\nPlease choose an option: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string choice = Console.ReadLine();
                Console.ResetColor();
                switch (choice)
                {
                    case "5.1":
                        ActiveMembersStatistics(_context);
                        break;
                    case "5.2":
                        TrainerWorkloadAnalysis(_context);
                        break;
                    case "5.3":
                        PopularProgramsAnalysis(_context);
                        break;
                    case "5.4":
                        SessionsAnalysis(_context);
                        break;
                    case "0":
                        return;
                    default:
                      
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");

                      
                        Console.ResetColor();

                        break;
                }

            
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPress any key to continue...");

                
                Console.ResetColor();

                Console.ReadKey();
            }
        }

        static void ActiveMembersStatistics(DataContex context)
        {
            Console.Clear();
  
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== Active Member Statistics =====\n");



            Console.ResetColor();


        
            var activeMembers = context.Members
                .Where(m => m.Status == MemberStatus.Active)
                .ToList();

          
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Active Members Count: ");



            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(activeMembers.Count);

           
            Console.ResetColor();


         
            var ageGroups = activeMembers
                .Where(m => m.MemberDetails != null)
                .GroupBy(m => GetAgeGroup(m.MemberDetails.DateOfBirth))
                .OrderBy(g => g.Key)
                .Select(g => new { AgeGroup = g.Key, Count = g.Count() })
                .ToList();
            Console.WriteLine("\nDistribution of members by age groups:");

            foreach (var group in ageGroups)
            {
                Console.WriteLine($"{group.AgeGroup}: {group.Count} წევრი ({(double)group.Count / activeMembers.Count * 100:F1}%)");
            }

          
            var averageStats = activeMembers
                .Where(m => m.MemberDetails != null)
                .Select(m => new { m.MemberDetails.Weight, m.MemberDetails.Height })
                .ToList();

            if (averageStats.Any())
            {
                Console.WriteLine($"{averageStats.Average(s => s.Weight):F1} კგ");
                Console.WriteLine($"{averageStats.Average(s => s.Height):F1} სმ");
            }

          
            var oneMonthAgo = DateTime.Now.AddMonths(-1);
            var lastMonthMembers = context.Members
                .Where(m => m.JoinDate >= oneMonthAgo)
                .ToList()
                .Count;

            Console.WriteLine($"\nNumber of members joined last month: {lastMonthMembers}");

         
            var membersWithProgramCount = activeMembers
                .Select(m => new {
                    Member = m,
                    ProgramCount = m.TrainingPrograms.Count(p => p.Status == ProgramStatus.Active)
                })
                .OrderByDescending(m => m.ProgramCount)
                .Take(5)
                .ToList();

            Console.WriteLine("\nMost Active Members (by active programs):");
            foreach (var item in membersWithProgramCount)
            {
                Console.WriteLine($"{item.Member.FirstName} {item.Member.LastName}: {item.ProgramCount}  ACTIVE PROGRAM");
            }
        }

        static void TrainerWorkloadAnalysis(DataContex context)
        {
            Console.Clear();
            Console.WriteLine("===== Coaches' Workload =====\n");


           
            var trainers = context.Trainers
                .Include(t => t.TrainingPrograms)
                    .ThenInclude(p => p.TrainingSessions)
                .ToList();

            var trainerWorkload = trainers
                .Select(t => new {
                    Trainer = t,
                    ActivePrograms = t.TrainingPrograms.Count(p => p.Status == ProgramStatus.Active),
                    TotalSessions = t.TrainingPrograms.SelectMany(p => p.TrainingSessions).Count(),
                    TotalHours = t.TrainingPrograms.SelectMany(p => p.TrainingSessions).Sum(s => s.Duration) / 60.0,
                    ScheduledSessions = t.TrainingPrograms.SelectMany(p => p.TrainingSessions)
                        .Count(s => s.Date > DateTime.Now && s.Status == SessionStatus.Scheduled)
                })
                .OrderByDescending(t => t.ActivePrograms)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine("===== Coaches' Workload (By Active Programs) =====");
            Console.ResetColor(); 
            foreach (var trainer in trainerWorkload)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{trainer.Trainer.FirstName} {trainer.Trainer.LastName} ({trainer.Trainer.Specialization}):");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  - Active Programs: {trainer.ActivePrograms}");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  - Total Sessions: {trainer.TotalSessions}");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"  - Total Hours: {trainer.TotalHours:F1} hrs");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  - Scheduled Sessions: {trainer.ScheduledSessions}");

                Console.ResetColor(); 
                Console.WriteLine();

            }

           
            var specializations = context.Trainers
                .GroupBy(t => t.Specialization)
                .Select(g => new { Specialization = g.Key, Count = g.Count() })
                .ToList()
                .OrderByDescending(g => g.Count)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Cyan; 
            Console.WriteLine("\nSpecialization Distribution:");
            Console.ResetColor(); 

            foreach (var spec in specializations)
            {
                Console.WriteLine($"{spec.Specialization}: {spec.Count} Trainer");
            }

           
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            var sessions = context.TrainingSessions
                .Where(s => s.Date > thirtyDaysAgo && s.Date <= DateTime.Now)
                .ToList();

            var sessionsByDay = sessions
                .GroupBy(s => s.Date.DayOfWeek)
                .Select(g => new { DayOfWeek = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Magenta; 
            Console.WriteLine("\nMost Busy Days (Last 30 Days):");
            Console.ResetColor();

            foreach (var day in sessionsByDay)
            {
                Console.WriteLine($"{GetGeorgianDayName(day.DayOfWeek)}: {day.Count} Session");
            }
        }

        static void PopularProgramsAnalysis(DataContex context)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("===== Popular Programs =====\n");
            Console.ResetColor(); 


         
            var programsByType = context.TrainingPrograms
                .GroupBy(p => p.ProgramType)
                .Select(g => new { ProgramType = g.Key, Count = g.Count() })
                .ToList()
                .OrderByDescending(g => g.Count)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine("Program Distribution by Types:");
            Console.ResetColor(); 

            foreach (var program in programsByType)
            {
                Console.WriteLine($"{program.ProgramType}: {program.Count} Program");
            }

           
            var programs = context.TrainingPrograms.ToList();
            var programDurationByType = programs
                .GroupBy(p => p.ProgramType)
                .Select(g => new {
                    ProgramType = g.Key,
                    AvgDuration = g.Average(p => (p.EndDate - p.StartDate).TotalDays)
                })
                .OrderByDescending(g => g.AvgDuration)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nAverage Program Duration (in Days):");
            Console.ResetColor(); 

            foreach (var program in programDurationByType)
            {
                Console.WriteLine($"{program.ProgramType}: {program.AvgDuration:F1} Day");
            }

        
            var programsWithMembers = context.TrainingPrograms
                .Include(p => p.Member)
                    .ThenInclude(m => m.MemberDetails)
                .ToList();

            var programsByAgeGroup = programsWithMembers
                .Where(p => p.Member?.MemberDetails != null)
                .GroupBy(p => new {
                    AgeGroup = GetAgeGroup(p.Member.MemberDetails.DateOfBirth),
                    p.ProgramType
                })
                .Select(g => new {
                    g.Key.AgeGroup,
                    g.Key.ProgramType,
                    Count = g.Count()
                })
                .OrderBy(x => x.AgeGroup)
                .ThenByDescending(x => x.Count)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Blue; 
            Console.WriteLine("\nProgram Popularity by Age Groups:");
            Console.ResetColor(); 

            string currentAgeGroup = "";
            foreach (var item in programsByAgeGroup)
            {
                if (currentAgeGroup != item.AgeGroup)
                {
                    currentAgeGroup = item.AgeGroup;
                    Console.WriteLine($"\n{currentAgeGroup}:");
                }
                Console.WriteLine($"  - {item.ProgramType}: {item.Count} Program");
            }
        }

        static void SessionsAnalysis(DataContex context)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow; 
            Console.WriteLine("===== Session Analysis =====\n");
            Console.ResetColor(); 


         
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            var recentSessions = context.TrainingSessions
                .Where(s => s.Date > thirtyDaysAgo && s.Date <= DateTime.Now)
                .ToList();

            var sessionsByStatus = recentSessions
                .GroupBy(s => s.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Session Statistics by Status (Last 30 Days):");
            Console.ResetColor(); 

            foreach (var status in sessionsByStatus)
            {
                Console.WriteLine($"{status.Status}: {status.Count} Session");
            }

         
            var completedSessions = context.TrainingSessions
                .Where(s => s.Status == SessionStatus.Completed)
                .ToList();

            var avgSessionDuration = completedSessions.Any()
                ? completedSessions.Average(s => s.Duration)
                : 0;

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.WriteLine($"\nAverage Session Duration: {avgSessionDuration:F1} minutes");
            Console.ResetColor(); 



            var ninetyDaysAgo = DateTime.Now.AddDays(-90);
            var lastNinetyDaysSessions = context.TrainingSessions
                .Where(s => s.Date > ninetyDaysAgo && s.Date <= DateTime.Now)
                .ToList();

            var sessionsByDayOfWeek = lastNinetyDaysSessions
                .GroupBy(s => s.Date.DayOfWeek)
                .Select(g => new { DayOfWeek = g.Key, Count = g.Count() })
                .OrderBy(g => g.DayOfWeek)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSession Count by Day of the Week (Last 90 Days):");
            Console.ResetColor();

            foreach (var day in sessionsByDayOfWeek)
            {
                Console.WriteLine($"{GetGeorgianDayName(day.DayOfWeek)}: {day.Count} Session");
            }

            
            var sessionsByHour = recentSessions
                .GroupBy(s => s.Date.Hour)
                .Select(g => new { Hour = g.Key, Count = g.Count() })
                .OrderBy(g => g.Hour)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("\nSession Distribution by Hours (Last 30 Days):");
            Console.ResetColor(); 

            foreach (var hour in sessionsByHour)
            {
                Console.WriteLine($"{hour.Hour}:00 - {hour.Hour}:59: {hour.Count} Session");
            }

            var sessionsWithPrograms = context.TrainingSessions
                .Where(s => s.Status == SessionStatus.Completed && s.CaloriesBurned > 0)
                .Include(s => s.Program)
                .ToList();

            var caloriesByProgramType = sessionsWithPrograms
                .GroupBy(s => s.Program.ProgramType)
                .Select(g => new {
                    ProgramType = g.Key,
                    AvgCaloriesBurned = g.Average(x => x.CaloriesBurned),
                    TotalSessions = g.Count()
                })
                .OrderByDescending(g => g.AvgCaloriesBurned)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAverage Calories Burned by Program Type:");
            Console.ResetColor(); 

            foreach (var item in caloriesByProgramType)
            {
                Console.WriteLine($"{item.ProgramType}: {item.AvgCaloriesBurned:F1} Callories ({item.TotalSessions} Session)");
            }
        }

        
        static string GetAgeGroup(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Now.AddYears(-age)) age--;

            if (age < 18) return "Under 18";
            else if (age < 25) return "18-24 years";
            else if (age < 35) return "25-34 years";
            else if (age < 45) return "35-44 years";
            else if (age < 55) return "45-54 years";
            else return "55+ years";
        }

        static string GetGeorgianDayName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return "Monday";
                case DayOfWeek.Tuesday: return "Tuesday";
                case DayOfWeek.Wednesday: return "Wednesday";
                case DayOfWeek.Thursday: return "Thursday";
                case DayOfWeek.Friday: return "Friday";
                case DayOfWeek.Saturday: return "Saturday";
                case DayOfWeek.Sunday: return "Sunday";
                default: return "";
            }
        }

        

        static void FileManagement()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== File Management ===");
                Console.WriteLine("1. Export Member Progress");
                Console.WriteLine("2. Export Trainer Schedule");
                Console.WriteLine("3. View System Log");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("\nEnter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ExportMemberProgressUI();
                            break;
                        case 2:
                            ExportTrainerScheduleUI();
                            break;
                        case 3:
                            ViewSystemLog();
                            break;
                    
                        case 0:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number. Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void ExportMemberProgressUI()
        {
            Console.Clear();
            Console.WriteLine("=== Export Member Progress ===");

            using (var context = new DataContex())
            {
                var memberRepo = new MemberRepository(context);

                Console.WriteLine("Active Members:");
                var members = memberRepo.GetActiveMembers();

                if (members.Count == 0)
                {
                    Console.WriteLine("No active members found.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                foreach (var member in members)
                {
                    Console.WriteLine($"{member.Id}. {member.FirstName} {member.LastName}");
                }

                Console.Write("\nEnter member ID to export progress (0 to cancel): ");
                if (int.TryParse(Console.ReadLine(), out int memberId) && memberId > 0)
                {
                    try
                    {
                        memberRepo.ExportMemberProgress(memberId);
                        LogEvent($"Exported progress for member ID: {memberId}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error exporting member progress: {ex.Message}");
                        Console.ResetColor();
                        LogEvent($"Error exporting progress for member ID {memberId}: {ex.Message}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
            }
        }

        private static void ExportTrainerScheduleUI()
        {
            Console.Clear();
            Console.WriteLine("=== Export Trainer Schedule ===");

            using (var context = new DataContex())
            {
                var trainerRepo = new TrainerRepository(context);

                Console.WriteLine("Available Trainers:");
                var trainers = trainerRepo.GetAll();

                if (trainers.Count == 0)
                {
                    Console.WriteLine("No trainers found.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                foreach (var trainer in trainers)
                {
                    Console.WriteLine($"{trainer.Id}. {trainer.FirstName} {trainer.LastName} - {trainer.Specialization}");
                }

                Console.Write("\nEnter trainer ID to export schedule (0 to cancel): ");
                if (int.TryParse(Console.ReadLine(), out int trainerId) && trainerId > 0)
                {
                    try
                    {
                       
                        trainerRepo.ExportTrainerSchedule(trainerId);
                        LogEvent($"Exported schedule for trainer ID: {trainerId}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error exporting trainer schedule: {ex.Message}");
                        Console.ResetColor();
                        LogEvent($"Error exporting schedule for trainer ID {trainerId}: {ex.Message}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
            }
        }

        private static void ViewSystemLog()
        {
            Console.Clear();
            Console.WriteLine("=== System Log ===");

            string logFilePath = "system_log.txt";

            if (!File.Exists(logFilePath))
            {
                Console.WriteLine("Log file does not exist yet.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            try
            {
                string[] logLines = File.ReadAllLines(logFilePath);

                if (logLines.Length == 0)
                {
                    Console.WriteLine("Log file is empty.");
                }
                else
                {
                    int currentPage = 0;
                    int pageSize = 15; 
                    int totalPages = (int)Math.Ceiling(logLines.Length / (double)pageSize);

                    bool viewingLogs = true;

                    while (viewingLogs)
                    {
                        Console.Clear();
                        Console.WriteLine("=== System Log ===");
                        Console.WriteLine($"Page {currentPage + 1} of {totalPages}");
                        Console.WriteLine("===========================================");

                     
                        int startIndex = currentPage * pageSize;
                        int endIndex = Math.Min(startIndex + pageSize, logLines.Length);

                        for (int i = startIndex; i < endIndex; i++)
                        {
                            Console.WriteLine(logLines[i]);
                        }

                        Console.WriteLine("\n===========================================");
                        Console.WriteLine("| E - Export Log | Q - Quit");
                        Console.Write("Enter choice: ");

                        var key = Console.ReadKey().Key;

                        switch (key)
                        {
                            case ConsoleKey.E:
                                ExportLogFile(logLines);
                                break;
                            case ConsoleKey.Q:
                                viewingLogs = false;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error reading log file: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void ExportLogFile(string[] logLines)
        {
            try
            {
                string exportPath = $"system_log_export_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                File.WriteAllLines(exportPath, logLines);

                Console.WriteLine($"\nLog exported to: {exportPath}");
                LogEvent($"System log exported to: {exportPath}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError exporting log: {ex.Message}");
                Console.ResetColor();
                LogEvent($"Error exporting system log: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

       
        public static void LogEvent(string message)
        {
            string logFilePath = "system_log.txt";
            string logEntry = $"[{DateTime.Now}] {message}";

            try
            {
               
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error writing to log: {ex.Message}");
                Console.ResetColor();
            }
        }



    }
    }



    public enum MemberStatus
    {
        Active,
        Frozen,
        Expired,
        Cancelled
    }


