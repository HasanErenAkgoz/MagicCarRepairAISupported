using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class InsuranceCompanySeedData
    {
        public static List<InsuranceCompany> GetInsuranceCompanies()
        {
            var companies = new List<InsuranceCompany>
            {
                new InsuranceCompany
                {
                    Id = 1,
                    CompanyName = "Allianz Sigorta",
                    CompanyCode = "ALLIANZ",
                    ContactPerson = "Genel Müdürlük",
                    Phone = "+90 850 222 0 100",
                    Email = "info@allianz.com.tr",
                    Address = "İstanbul, Türkiye",
                    IsActive = true,
                    ClientId = 0, // Global - tüm client'lar kullanabilir
                    SupportedInsuranceTypes = "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]",
                    CreatedDate = DateTime.UtcNow,
                    Status = Domain.Enums.Status.Active
                },
                new InsuranceCompany
                {
                    Id = 2,
                    CompanyName = "Anadolu Sigorta",
                    CompanyCode = "ANADOLU",
                    ContactPerson = "Genel Müdürlük",
                    Phone = "+90 850 222 0 200",
                    Email = "info@anadolusigorta.com.tr",
                    Address = "İstanbul, Türkiye",
                    IsActive = true,
                    ClientId = 0,
                    SupportedInsuranceTypes = "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]",
                    CreatedDate = DateTime.UtcNow,
                    Status = Domain.Enums.Status.Active
                },
                new InsuranceCompany
                {
                    Id = 3,
                    CompanyName = "Axa Sigorta",
                    CompanyCode = "AXA",
                    ContactPerson = "Genel Müdürlük",
                    Phone = "+90 850 222 0 300",
                    Email = "info@axa-sigorta.com.tr",
                    Address = "İstanbul, Türkiye",
                    IsActive = true,
                    ClientId = 0,
                    SupportedInsuranceTypes = "[\"Comprehensive\", \"TrafficInsurance\"]",
                    CreatedDate = DateTime.UtcNow,
                    Status = Domain.Enums.Status.Active
                },
                new InsuranceCompany
                {
                    Id = 4,
                    CompanyName = "Groupama Sigorta",
                    CompanyCode = "GROUPAMA",
                    ContactPerson = "Genel Müdürlük",
                    Phone = "+90 850 222 0 400",
                    Email = "info@groupama.com.tr",
                    Address = "İstanbul, Türkiye",
                    IsActive = true,
                    ClientId = 0,
                    SupportedInsuranceTypes = "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]",
                    CreatedDate = DateTime.UtcNow,
                    Status = Domain.Enums.Status.Active
                },
                new InsuranceCompany
                {
                    Id = 5,
                    CompanyName = "HDI Sigorta",
                    CompanyCode = "HDI",
                    ContactPerson = "Genel Müdürlük",
                    Phone = "+90 850 222 0 500",
                    Email = "info@hdi-sigorta.com.tr",
                    Address = "İstanbul, Türkiye",
                    IsActive = true,
                    ClientId = 0,
                    SupportedInsuranceTypes = "[\"Comprehensive\", \"TrafficInsurance\"]",
                    CreatedDate = DateTime.UtcNow,
                    Status = Domain.Enums.Status.Active
                },
                new InsuranceCompany
                {
                    Id = 6,
                    CompanyName = "Mapfre Sigorta",
                    CompanyCode = "MAPFRE",
                    ContactPerson = "Genel Müdürlük",
                    Phone = "+90 850 222 0 600",
                    Email = "info@mapfre.com.tr",
                    Address = "İstanbul, Türkiye",
                    IsActive = true,
                    ClientId = 0,
                    SupportedInsuranceTypes = "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]",
                    CreatedDate = DateTime.UtcNow,
                    Status = Domain.Enums.Status.Active
                },
                new InsuranceCompany
                {
                    Id = 7,
                    CompanyName = "Neova Sigorta",
                    CompanyCode = "NEOVA",
                    ContactPerson = "Genel Müdürlük",
                    Phone = "+90 850 222 0 700",
                    Email = "info@neova.com.tr",
                    Address = "Ankara, Türkiye",
                    IsActive = true,
                    ClientId = 0,
                    SupportedInsuranceTypes = "[\"Comprehensive\", \"TrafficInsurance\"]",
                    CreatedDate = DateTime.UtcNow,
                    Status = Domain.Enums.Status.Active
                },
                new InsuranceCompany
                {
                    Id = 8,
                    CompanyName = "Ray Sigorta",
                    CompanyCode = "RAY",
                    ContactPerson = "Genel Müdürlük",
                    Phone = "+90 850 222 0 800",
                    Email = "info@ray.com.tr",
                    Address = "İstanbul, Türkiye",
                    IsActive = true,
                    ClientId = 0,
                    SupportedInsuranceTypes = "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]",
                    CreatedDate = DateTime.UtcNow,
                    Status = Domain.Enums.Status.Active
                },
                new InsuranceCompany
                {
                    Id = 9,
                    CompanyName = "Unico Sigorta",
                    CompanyCode = "UNICO",
                    ContactPerson = "Genel Müdürlük",
                    Phone = "+90 850 222 0 900",
                    Email = "info@unico.com.tr",
                    Address = "İstanbul, Türkiye",
                    IsActive = true,
                    ClientId = 0,
                    SupportedInsuranceTypes = "[\"Comprehensive\", \"TrafficInsurance\"]",
                    CreatedDate = DateTime.UtcNow,
                    Status = Domain.Enums.Status.Active
                },
                new InsuranceCompany
                {
                    Id = 10,
                    CompanyName = "Ziraat Sigorta",
                    CompanyCode = "ZIRAAT",
                    ContactPerson = "Genel Müdürlük",
                    Phone = "+90 850 222 0 000",
                    Email = "info@ziraat.com.tr",
                    Address = "Ankara, Türkiye",
                    IsActive = true,
                    ClientId = 0,
                    SupportedInsuranceTypes = "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]",
                    CreatedDate = DateTime.UtcNow,
                    Status = Domain.Enums.Status.Active
                }
            };

            return companies;
        }
    }
}
