using Gym_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gym_Management_System.Enums.GymEnums;

namespace Gym_Management_System.Models
{
    internal class TrainerRepository
    {
        private readonly DataContex _context;

        public TrainerRepository(DataContex context)
        {
            _context = context;
        }

        public List<Trainer> GetAll()
        {
            return _context.Trainers.ToList();
        }

        public Trainer GetById(int id)
        {
            return _context.Trainers.Find(id);
        }

        public List<Trainer> GetAvailableTrainers()
        {
            return _context.Trainers.Where(t => t.IsAvailable).ToList();
        }

        public IEnumerable<IGrouping<string, Trainer>> GetTrainersBySpecialization()
        {
            return _context.Trainers.GroupBy(t => t.Specialization).ToList();
        }

        public void Add(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            _context.SaveChanges();
        }

        public void Update(Trainer trainer)
        {
            _context.Trainers.Update(trainer);
            _context.SaveChanges();
        }

        public void ExportTrainerSchedule(int trainerId)
        {
            var trainer = _context.Trainers
                .Include(t => t.TrainingPrograms)
                .ThenInclude(tp => tp.TrainingSessions)
                .Include(t => t.TrainingPrograms)
                .ThenInclude(tp => tp.Member)
                .FirstOrDefault(t => t.Id == trainerId);

            if (trainer == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Trainer not found.");
                Console.ResetColor();

                return;
            }

            string fileName = $"trainer_{trainerId}_schedule.txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
               
                writer.WriteLine($"Trainer: {trainer.FirstName} {trainer.LastName}");
                writer.WriteLine($"Specialization: {trainer.Specialization}");
                writer.WriteLine($"Experience: {trainer.Experience} years");
                writer.WriteLine($"Hourly Rate: {trainer.HourlyRate:C}"); 
                writer.WriteLine($"Available: {(trainer.IsAvailable ? "Yes" : "No")}");


                writer.WriteLine("\nToday Session:");
                var todaySessions = trainer.TrainingPrograms
                    .SelectMany(tp => tp.TrainingSessions)
                    .Where(s => s.Date.Date == DateTime.Today)
                    .OrderBy(s => s.Date)
                    .ToList();

                if (todaySessions.Any())
                {
                    foreach (var session in todaySessions)
                    {
                        var program = trainer.TrainingPrograms.First(p => p.Id == session.ProgramId);
                        
                        writer.WriteLine($"Time: {session.Date.ToString("HH:mm")} - {session.Date.AddMinutes(session.Duration).ToString("HH:mm")}");
                        writer.WriteLine($"Member: {program.Member.FirstName} {program.Member.LastName}");
                        writer.WriteLine($"Program Type: {program.ProgramType}");
                        writer.WriteLine($"Status: {session.Status}");
                        writer.WriteLine("---------------------------");

                    }
                }
                else
                {
                    writer.WriteLine("No sessions are scheduled for today.");

                }

                writer.WriteLine("\nActive Members:");

                var activePrograms = trainer.TrainingPrograms
                    .Where(p => p.Status == ProgramStatus.Active)
                    .ToList();

                if (activePrograms.Any())
                {
                    foreach (var program in activePrograms)
                    {
                        writer.WriteLine($"Member: {program.Member.FirstName} {program.Member.LastName}");
                        writer.WriteLine($"Program: {program.ProgramType}");
                        writer.WriteLine($"Start Date: {program.StartDate.ToShortDateString()}");
                        writer.WriteLine($"End Date: {program.EndDate.ToShortDateString()}");
                        writer.WriteLine("---------------------------");

                    }
                }
                else
                {
                    writer.WriteLine("There are no active members.");

                }
            }

            Console.WriteLine($"The trainer's schedule has been exported to the file: {fileName}");


        }
    }
}

