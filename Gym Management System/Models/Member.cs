using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gym_Management_System.Enums.GymEnums;

namespace Gym_Management_System.Models
{
    internal class Member
    {
        public int Id { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

     
        public string PersonalNumber { get; set; }

        public DateTime JoinDate { get; set; }

        public MemberStatus Status { get; set; }

        public MemberDetails MemberDetails { get; set; }
        public List<TrainingProgram> TrainingPrograms { get; set; }

        public Member()
        {
            JoinDate = DateTime.Now;
            Status = MemberStatus.Active;
            TrainingPrograms = new List<TrainingProgram>();
        }
    }
}
