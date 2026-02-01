using System;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class VehiclePhotoSeedData
    {
        public static List<VehiclePhoto> GetVehiclePhotos()
        {
            var now = DateTime.UtcNow;
            return new List<VehiclePhoto>
            {
                // Vehicle 1 Photos
                new VehiclePhoto
                {
                    Id = 1,
                    VehicleId = 1,
                    FilePath = "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800",
                    PhotoType = "Ön",
                    Description = "Araç ön görünüm",
                    DisplayOrder = 1,
                    UploadDate = now.AddMonths(-6),
                    ClientId = 1,
                    CreatedDate = now.AddMonths(-6),
                    CreatedBy = 1
                },
                new VehiclePhoto
                {
                    Id = 2,
                    VehicleId = 1,
                    FilePath = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800",
                    PhotoType = "Yan",
                    Description = "Araç yan görünüm",
                    DisplayOrder = 2,
                    UploadDate = now.AddMonths(-6),
                    ClientId = 1,
                    CreatedDate = now.AddMonths(-6),
                    CreatedBy = 1
                },
                // Vehicle 2 Photos
                new VehiclePhoto
                {
                    Id = 3,
                    VehicleId = 2,
                    FilePath = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800",
                    PhotoType = "Ön",
                    Description = "Araç ön görünüm",
                    DisplayOrder = 1,
                    UploadDate = now.AddMonths(-5),
                    ClientId = 1,
                    CreatedDate = now.AddMonths(-5),
                    CreatedBy = 1
                },
                // Vehicle 3 Photos
                new VehiclePhoto
                {
                    Id = 4,
                    VehicleId = 3,
                    FilePath = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800",
                    PhotoType = "Ön",
                    Description = "Araç ön görünüm",
                    DisplayOrder = 1,
                    UploadDate = now.AddMonths(-5),
                    ClientId = 1,
                    CreatedDate = now.AddMonths(-5),
                    CreatedBy = 1
                },
                // Vehicle 4 Photos
                new VehiclePhoto
                {
                    Id = 5,
                    VehicleId = 4,
                    FilePath = "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800",
                    PhotoType = "Ön",
                    Description = "Araç ön görünüm",
                    DisplayOrder = 1,
                    UploadDate = now.AddMonths(-4),
                    ClientId = 1,
                    CreatedDate = now.AddMonths(-4),
                    CreatedBy = 1
                },
                // Vehicle 5 Photos
                new VehiclePhoto
                {
                    Id = 6,
                    VehicleId = 5,
                    FilePath = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800",
                    PhotoType = "Ön",
                    Description = "Araç ön görünüm",
                    DisplayOrder = 1,
                    UploadDate = now.AddMonths(-3),
                    ClientId = 1,
                    CreatedDate = now.AddMonths(-3),
                    CreatedBy = 1
                },
                // Vehicle 6 Photos
                new VehiclePhoto
                {
                    Id = 7,
                    VehicleId = 6,
                    FilePath = "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800",
                    PhotoType = "Ön",
                    Description = "Araç ön görünüm",
                    DisplayOrder = 1,
                    UploadDate = now.AddMonths(-2),
                    ClientId = 1,
                    CreatedDate = now.AddMonths(-2),
                    CreatedBy = 1
                },
                // Vehicle 7 Photos
                new VehiclePhoto
                {
                    Id = 8,
                    VehicleId = 7,
                    FilePath = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800",
                    PhotoType = "Ön",
                    Description = "Araç ön görünüm",
                    DisplayOrder = 1,
                    UploadDate = now.AddMonths(-1),
                    ClientId = 1,
                    CreatedDate = now.AddMonths(-1),
                    CreatedBy = 1
                },
                // Vehicle 8 Photos
                new VehiclePhoto
                {
                    Id = 9,
                    VehicleId = 8,
                    FilePath = "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800",
                    PhotoType = "Ön",
                    Description = "Araç ön görünüm",
                    DisplayOrder = 1,
                    UploadDate = now.AddDays(-15),
                    ClientId = 1,
                    CreatedDate = now.AddDays(-15),
                    CreatedBy = 1
                },
                // Vehicle 9 Photos
                new VehiclePhoto
                {
                    Id = 10,
                    VehicleId = 9,
                    FilePath = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800",
                    PhotoType = "Ön",
                    Description = "Araç ön görünüm",
                    DisplayOrder = 1,
                    UploadDate = now.AddDays(-7),
                    ClientId = 1,
                    CreatedDate = now.AddDays(-7),
                    CreatedBy = 1
                },
                // Vehicle 10 Photos
                new VehiclePhoto
                {
                    Id = 11,
                    VehicleId = 10,
                    FilePath = "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800",
                    PhotoType = "Ön",
                    Description = "Araç ön görünüm",
                    DisplayOrder = 1,
                    UploadDate = now.AddMonths(-4),
                    ClientId = 1,
                    CreatedDate = now.AddMonths(-4),
                    CreatedBy = 1
                },
                // Vehicle 11 Photos
                new VehiclePhoto
                {
                    Id = 12,
                    VehicleId = 11,
                    FilePath = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800",
                    PhotoType = "Ön",
                    Description = "Araç ön görünüm",
                    DisplayOrder = 1,
                    UploadDate = now.AddMonths(-3),
                    ClientId = 1,
                    CreatedDate = now.AddMonths(-3),
                    CreatedBy = 1
                }
            };
        }

        public static void SeedVehiclePhotos(this Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            builder.Entity<VehiclePhoto>().HasData(GetVehiclePhotos());
        }
    }
}
