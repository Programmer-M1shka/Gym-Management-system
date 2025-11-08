using Gym_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Management_System.Models
{
    internal class ProgramRepository
    {
        private readonly DataContex _context;

        public ProgramRepository(DataContex context)
        {
            _context = context;
        }

        public List<TrainingProgram> GetAll()
        {
            return _context.TrainingPrograms
                .Include(tp => tp.Member)
                .Include(tp => tp.Trainer)
                .ToList();
        }

        public TrainingProgram GetById(int id)
        {
            return _context.TrainingPrograms
                .Include(tp => tp.Member)
                .Include(tp => tp.Trainer)
                .Include(tp => tp.TrainingSessions)
                .FirstOrDefault(tp => tp.Id == id);
        }

        public List<TrainingProgram> GetByMember(int memberId)
        {
            return _context.TrainingPrograms
                .Where(tp => tp.MemberId == memberId)
                .Include(tp => tp.Trainer)
                .ToList();
        }

        public List<TrainingProgram> GetByTrainer(int trainerId)
        {
            return _context.TrainingPrograms
                .Where(tp => tp.TrainerId == trainerId)
                .Include(tp => tp.Member)
                .ToList();
        }

        public void Add(TrainingProgram program)
        {
            _context.TrainingPrograms.Add(program);
            _context.SaveChanges();
        }

        public void Update(TrainingProgram program)
        {
            _context.TrainingPrograms.Update(program);
            _context.SaveChanges();
        }
    }
}

