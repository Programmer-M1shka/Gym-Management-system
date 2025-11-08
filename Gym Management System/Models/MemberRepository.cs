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
    internal class MemberRepository
    {
        private readonly DataContex _context;

        public MemberRepository(DataContex context)
        {
            _context = context;
        }

        public List<Member> GetAll()
        {
            return _context.Members
                .Include(m => m.MemberDetails)
                .ToList();
        }
        public List<Member> GetActiveMembers()
        {
            return _context.Members.Where(m => m.Status == MemberStatus.Active).ToList();
        }
        public Member GetById(int id)
        {
            return _context.Members
                .Include(m => m.MemberDetails)
                .Include(m => m.TrainingPrograms)
                .FirstOrDefault(m => m.Id == id);
        }

        public List<Member> GetByStatus(MemberStatus status)
        {
            return _context.Members
                .Where(m => m.Status == status)
                .ToList();
        }

        public void Add(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public void Update(Member member)
        {
            _context.Members.Update(member);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var member = _context.Members.Find(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
            }
        }

        public void ExportMemberProgress(int memberId)
        {
            var member = _context.Members
                .Include(m => m.MemberDetails)
                .Include(m => m.TrainingPrograms)
                .ThenInclude(tp => tp.TrainingSessions)
                .FirstOrDefault(m => m.Id == memberId);

            if (member == null)
            {
                Console.WriteLine("Member Not Found!.");
                return;
            }

            string fileName = $"member_{memberId}_progress.txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine($"Member ID: {member.Id}");
                writer.WriteLine($"First Name: {member.FirstName} {member.LastName}");
                writer.WriteLine($"Personal Number: {member.PersonalNumber}");
                writer.WriteLine($"Join Date: {member.JoinDate}");
                writer.WriteLine($"Status: {member.Status}");

                if (member.MemberDetails != null)
                {
                    writer.WriteLine($"Date of Birth: {member.MemberDetails.DateOfBirth}");
                    writer.WriteLine($"Email: {member.MemberDetails.Email}");
                    writer.WriteLine($"Phone: {member.MemberDetails.PhoneNumber}");
                    writer.WriteLine($"Emergency Contact: {member.MemberDetails.EmergencyContact}");
                    writer.WriteLine($"Medical Notes: {member.MemberDetails.MedicalNotes}");
                    writer.WriteLine($"Height: {member.MemberDetails.Height}");
                    writer.WriteLine($"Weight: {member.MemberDetails.Weight}");

                }

                writer.WriteLine("\nTraining Programs:");

                foreach (var program in member.TrainingPrograms)
                {
                    writer.WriteLine($"Program ID: {program.Id}");
                    writer.WriteLine($"Type: {program.ProgramType}");
                    writer.WriteLine($"Status: {program.Status}");
                    writer.WriteLine($"Start Date: {program.StartDate}");
                    writer.WriteLine($"End Date: {program.EndDate}");

                    writer.WriteLine("\nTraining Sessions:");

                    foreach (var session in program.TrainingSessions)
                    {
                        writer.WriteLine($"Session ID: {session.Id}");
                        writer.WriteLine($"Date: {session.Date}");
                        writer.WriteLine($"Duration: {session.Duration} minutes");
                        writer.WriteLine($"Status: {session.Status}");
                        writer.WriteLine($"Calories Burned: {session.CaloriesBurned}");
                        writer.WriteLine($"Notes: {session.Notes}");
                        writer.WriteLine("---------------------------");

                    }
                }
            }

            Console.WriteLine($"Export File: {fileName}");
        }
    }
}


    

        
    
           

           