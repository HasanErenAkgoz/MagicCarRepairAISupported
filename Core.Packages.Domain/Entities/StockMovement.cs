using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Stok hareketi entity'si (Giriş/Çıkış kayıtları)
    /// </summary>
    public class StockMovement : BaseEntity<int>, IClientEntity
    {
        public int PartId { get; set; }
        public virtual Part Part { get; set; }
        
        public StockMovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
        
        // İşlem yapan personel
        public int? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
        
        // İşlem açıklaması
        public string? Description { get; set; }
        public string? ReferenceNumber { get; set; } // Referans numarası (Fatura No, İş Emri No, vb.)
        public string? ReferenceType { get; set; } // Referans tipi (Invoice, WorkOrder, Purchase, vb.)
        
        // Fiyat bilgisi (işlem anındaki fiyat)
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        
        // Transfer için (başka depoya transfer)
        public int? TargetLocationId { get; set; } // Hedef konum ID (gelecekte Location entity eklendiğinde)
        public string? TargetLocation { get; set; } // Hedef konum (string olarak)
        
        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        
        /// <summary>
        /// Stok hareketi oluşturur
        /// </summary>
        public static StockMovement Create(
            int partId,
            StockMovementType movementType,
            int quantity,
            int? employeeId = null,
            string? description = null,
            string? referenceNumber = null,
            string? referenceType = null,
            decimal? unitPrice = null,
            int clientId = 0)
        {
            var movement = new StockMovement
            {
                PartId = partId,
                MovementType = movementType,
                Quantity = quantity,
                EmployeeId = employeeId,
                Description = description,
                ReferenceNumber = referenceNumber,
                ReferenceType = referenceType,
                UnitPrice = unitPrice,
                TotalPrice = unitPrice.HasValue ? unitPrice.Value * quantity : null,
                MovementDate = DateTime.UtcNow,
                ClientId = clientId,
                Status = Status.Active
            };
            
            return movement;
        }
    }
}

