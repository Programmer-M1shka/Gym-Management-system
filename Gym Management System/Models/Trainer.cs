using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Management_System.Models
{
    internal class Trainer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public int Experience { get; set; }
        public decimal HourlyRate { get; set; }
        public bool IsAvailable { get; set; }

      
        public List<TrainingProgram> TrainingPrograms { get; set; }

        public Trainer()
        {
            IsAvailable = true;
            TrainingPrograms = new List<TrainingProgram>();
        }
    }
}
