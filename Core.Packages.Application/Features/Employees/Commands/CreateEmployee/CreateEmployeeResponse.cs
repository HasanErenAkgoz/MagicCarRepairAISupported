using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeResponse
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public EmployeePosition Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public DateTime CreatedDate { get; set; }

        /// <summary>Identity kullanıcı kaydı açıldı mı (davet maili sadece buna bağlı)</summary>
        public bool UserAccountCreated { get; set; }

        /// <summary>Şifre belirleme davet e-postası gönderildi mi</summary>
        public bool InviteEmailSent { get; set; }

        /// <summary>İnsan tarafından okunabilir uyarı satırları (SMTP, duplicate email, vb.)</summary>
        public List<string> Warnings { get; set; } = new();
    }
}

