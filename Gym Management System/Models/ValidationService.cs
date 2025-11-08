using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Management_System.Models
{
    internal class ValidationService
    {
        public bool ValidatePersonalNumber(string personalNumber)
        {
            if (string.IsNullOrWhiteSpace(personalNumber))
                return false;

            
            return personalNumber.Length == 11 && personalNumber.All(char.IsDigit);
        }

        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            
            return email.Contains("@") && email.Contains(".");
        }

        public bool ValidateDateRange(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate;
        }

        public bool ValidatePositiveNumber(decimal value)
        {
            return value > 0;
        }
    }
}

