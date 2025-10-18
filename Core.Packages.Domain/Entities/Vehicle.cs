using Core.Packages.Domain.Comman;
using Core.Packages.Domain.Enums;
using Core.Packages.Domain.Exceptions;

namespace Core.Packages.Domain.Entities
{
    public class Vehicle : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public long Kilometers { get; private set; }
        public VehicleStatus Status { get; set; }
        public Customer Customer { get; set; }

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
