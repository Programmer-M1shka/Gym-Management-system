using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gym_Management_System.Enums.GymEnums;

namespace Gym_Management_System.Models
{
    internal class TrainingProgram
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int TrainerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProgramType ProgramType { get; set; }
        public ProgramStatus Status { get; set; }

       
        public Member Member { get; set; }
        public Trainer Trainer { get; set; }
        public List<TrainingSession> TrainingSessions { get; set; }

        public TrainingProgram()
        {
            StartDate = DateTime.Now;
            Status = ProgramStatus.Active;
            TrainingSessions = new List<TrainingSession>();
        }
    }
}
