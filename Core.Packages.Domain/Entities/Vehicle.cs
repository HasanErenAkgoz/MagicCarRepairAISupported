using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    public class Vehicle : BaseEntity<int>, IClientEntity
    {
        public int CustomerId { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string? Vin { get; set; }
        public string? ModelVariant { get; set; }
        public string? Trim { get; set; }
        public long Kilometers { get; private set; }
        public new VehicleStatus Status { get; set; }
        
        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        
        public Customer Customer { get; set; }
        
        /// <summary>
        /// Araç fotoğrafları
        /// </summary>
        public virtual ICollection<VehiclePhoto> Photos { get; set; } = new List<VehiclePhoto>();

        public void UpdateKilometers(long newKilometers)
        {
            if (newKilometers < this.Kilometers)
            {
                throw new DomainException(
                    "VEHICLE_KM_LOWER_THAN_CURRENT",
                    new { 
                        CurrentKilometers = this.Kilometers, 
                        NewKilometers = newKilometers,
                        LicensePlate = this.LicensePlate
                    })
                {
                    Details = new { 
                        CurrentKilometers = this.Kilometers, 
                        NewKilometers = newKilometers,
                        LicensePlate = this.LicensePlate
                    }
                };
            }

            if (newKilometers < 0)
            {
                throw new DomainException(
                    "VEHICLE_KM_NEGATIVE",
                    new { 
                        NewKilometers = newKilometers,
                        LicensePlate = this.LicensePlate
                    })
                {
                    Details = new { 
                        NewKilometers = newKilometers,
                        LicensePlate = this.LicensePlate
                    }
                };
            }

            this.Kilometers = newKilometers;
        }

        public Vehicle()
        {
            Status = VehicleStatus.Registered;
        }

    }


}
