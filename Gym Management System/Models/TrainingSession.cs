using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gym_Management_System.Enums.GymEnums;

namespace Gym_Management_System.Models
{
    internal class TrainingSession
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public SessionStatus Status { get; set; }
        public string Notes { get; set; }
        public int CaloriesBurned { get; set; }

     
        public TrainingProgram Program { get; set; }

        public TrainingSession()
        {
            Date = DateTime.Now;
            Status = SessionStatus.Scheduled;
        }
    }
}
