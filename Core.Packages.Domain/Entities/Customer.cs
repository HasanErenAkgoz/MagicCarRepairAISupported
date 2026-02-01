using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCarRepairAISupported.Domain.Entities
{
    public class Customer : BaseEntity<int>, IClientEntity
    {
        public string IdentityNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public string Address { get; set; }
        public DateTime DateTimeOfBirth { get; set; }
        public string Language { get; set; } = "tr";
        public bool IsVip { get; set; } = false;
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public int GetAge() { var today = DateTime.Today; var age = today.Year - DateTimeOfBirth.Year; if (DateTimeOfBirth.Date > today.AddYears(-age)) age--; return age; }
        public bool IsBirthdayToday() { return DateTimeOfBirth.Month == DateTime.Today.Month && DateTimeOfBirth.Day == DateTime.Today.Day; }
    }
}
