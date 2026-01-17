using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;
using System.Collections.Generic;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Personel entity'si
    /// </summary>
    public class Employee : BaseEntity<int>, IClientEntity
    {
    
        public string EmployeeNo { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? NationalId { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public EmployeePosition Position { get; set; }
        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }

        public string? Specializations { get; set; }
        public string? Address { get; set; }
        public string? BloodType { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? Notes { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        
        // Public profile fields (team showcase)
        /// <summary>
        /// Biyografi (public profilde gösterilecek)
        /// </summary>
        public string? Biography { get; set; }
        
        /// <summary>
        /// Profil fotoğrafı yolu
        /// </summary>
        public string? ProfilePhotoUrl { get; set; }
        
        /// <summary>
        /// Uzmanlık alanları (JSON formatında: ["Motor Tamiri", "Elektrik Sistemleri", ...])
        /// </summary>
        public string? SpecializationsJson { get; set; }
        
        /// <summary>
        /// Public profilde gösterilsin mi? (ekip tanıtımında)
        /// </summary>
        public bool IsPublic { get; set; } = false;
        
        /// <summary>
        /// Sıralama (ekip listesinde)
        /// </summary>
        public int DisplayOrder { get; set; } = 0;
    }
}

