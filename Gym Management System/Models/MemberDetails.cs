using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Management_System.Models
{
    internal class MemberDetails
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string EmergencyContact { get; set; }
        public string MedicalNotes { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }

       
        public Member Member { get; set; }
    }
}
