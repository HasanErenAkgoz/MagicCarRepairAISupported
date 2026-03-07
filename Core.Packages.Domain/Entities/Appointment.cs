using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Randevu entity'si
    /// </summary>
    public class Appointment : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Randevu numarası (Otomatik oluşturulur: APT-YYYYMMDD-XXXX)
        /// </summary>
        public string AppointmentNumber { get; set; }

        /// <summary>
        /// Müşteri ID
        /// </summary>
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Araç ID (opsiyonel)
        /// </summary>
        public int? VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

        /// <summary>
        /// Randevu Tarihi
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// Randevu Saati (Başlangıç)
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Randevu Saati (Bitiş - tahmini)
        /// </summary>
        public TimeSpan? EndTime { get; set; }

        /// <summary>
        /// Randevu Türü
        /// </summary>
        public AppointmentType AppointmentType { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Durum
        /// </summary>
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        /// <summary>
        /// Atanan Personel ID (opsiyonel)
        /// </summary>
        public int? AssignedEmployeeId { get; set; }
        public virtual Employee? AssignedEmployee { get; set; }

        /// <summary>
        /// Hatırlatma gönderildi mi?
        /// </summary>
        public bool ReminderSent { get; set; } = false;

        /// <summary>
        /// Hatırlatma gönderilme tarihi
        /// </summary>
        public DateTime? ReminderSentDate { get; set; }

        /// <summary>
        /// İptal nedeni
        /// </summary>
        public string? CancellationReason { get; set; }

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Randevu numarası oluşturur
        /// </summary>
        public static string GenerateAppointmentNumber()
        {
            var prefix = "APT";
            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            var random = new Random().Next(1000, 9999);
            return $"{prefix}-{date}-{random}";
        }

        /// <summary>
        /// Randevu tarihini kontrol eder
        /// </summary>
        public bool IsPast()
        {
            var appointmentDateTime = AppointmentDate.Date + StartTime;
            return appointmentDateTime < DateTime.Now;
        }

        /// <summary>
        /// Hatırlatma gönderilmeli mi? (24 saat öncesi)
        /// </summary>
        public bool ShouldSendReminder()
        {
            if (ReminderSent || Status != AppointmentStatus.Scheduled)
                return false;

            var appointmentDateTime = AppointmentDate.Date + StartTime;
            var timeUntilAppointment = appointmentDateTime - DateTime.Now;
            return timeUntilAppointment.TotalHours <= 24 && timeUntilAppointment.TotalHours > 0;
        }
    }
}






