using Gym_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Management_System.Models
{
    internal class SessionRepository
    {
        private readonly DataContex _context;

        public SessionRepository(DataContex context)
        {
            _context = context;
        }

        public List<TrainingSession> GetAll()
        {
            return _context.TrainingSessions
                .Include(ts => ts.Program)
                .ToList();
        }

        public TrainingSession GetById(int id)
        {
            return _context.TrainingSessions
                .Include(ts => ts.Program)
                .FirstOrDefault(ts => ts.Id == id);
        }

        public List<TrainingSession> GetByProgram(int programId)
        {
            return _context.TrainingSessions
                .Where(ts => ts.ProgramId == programId)
                .OrderBy(ts => ts.Date)
                .ToList();
        }

        public List<TrainingSession> GetByDate(DateTime date)
        {
            return _context.TrainingSessions
                .Include(ts => ts.Program)
                .ThenInclude(p => p.Member)
                .Include(ts => ts.Program)
                .ThenInclude(p => p.Trainer)
                .Where(ts => ts.Date.Date == date.Date)
                .OrderBy(ts => ts.Date)
                .ToList();
        }

        public void Add(TrainingSession session)
        {
            _context.TrainingSessions.Add(session);
            _context.SaveChanges();
        }

        public void Update(TrainingSession session)
        {
            _context.TrainingSessions.Update(session);
            _context.SaveChanges();
        }
    }
}

