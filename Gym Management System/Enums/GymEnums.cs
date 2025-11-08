using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Management_System.Enums
{
    internal class GymEnums
    {
        public enum MemberStatus { Active, Frozen, Expired }
        public enum ProgramType { Personal, Group, Nutrition, CardioFitness , StrengthTraining , Rehabilitation }
        public enum ProgramStatus { Active, Completed, Cancelled, Paused }
        public enum SessionStatus { Scheduled, Completed, Cancelled }

    }
}
