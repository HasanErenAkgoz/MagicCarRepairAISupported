using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Parça stok bilgisi entity'si
    /// </summary>
    public class PartStock : BaseEntity<int>, IClientEntity
    {
        public int PartId { get; set; }
        public virtual Part Part { get; set; }
        
        public int Quantity { get; set; } = 0; // Mevcut miktar
        public string? Location { get; set; } // Konum (Raf No, Depo, vb.)
        public DateTime? LastUpdatedDate { get; set; }
        public int? LastUpdatedByEmployeeId { get; set; }
        public virtual Employee? LastUpdatedByEmployee { get; set; }
        
        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        
        /// <summary>
        /// Stok miktarını günceller
        /// </summary>
        public void UpdateQuantity(int newQuantity, int? employeeId = null)
        {
            Quantity = newQuantity;
            LastUpdatedDate = DateTime.UtcNow;
            LastUpdatedByEmployeeId = employeeId;
        }
        
        /// <summary>
        /// Stok miktarına ekler
        /// </summary>
        public void AddQuantity(int amount, int? employeeId = null)
        {
            Quantity += amount;
            LastUpdatedDate = DateTime.UtcNow;
            LastUpdatedByEmployeeId = employeeId;
        }
        
        /// <summary>
        /// Stok miktarından çıkarır
        /// </summary>
        public void SubtractQuantity(int amount, int? employeeId = null)
        {
            if (Quantity < amount)
            {
                throw new InvalidOperationException($"Yetersiz stok! Mevcut: {Quantity}, İstenen: {amount}");
            }
            
            Quantity -= amount;
            LastUpdatedDate = DateTime.UtcNow;
            LastUpdatedByEmployeeId = employeeId;
        }
    }
}

