using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    /// <summary>
    /// Uygulama başladığında araç fotoğraflarını DB'ye ekler.
    /// Zaten veri varsa atlar (idempotent).
    /// </summary>
    public static class VehiclePhotoDataSeeder
    {
        // Her araç için 2-3 güzel dummy Unsplash fotoğrafı (nötr, genel araç görselleri)
        private static readonly string[] CarPhotos = new[]
        {
            "https://images.unsplash.com/photo-1492144534655-ae79c964c9d7?w=800&q=80",  // Genel araç - ön
            "https://images.unsplash.com/photo-1503376780353-7e6692767b70?w=800&q=80",  // Genel araç - yan
            "https://images.unsplash.com/photo-1544636331-e26879cd4d9b?w=800&q=80",     // Genel araç - arka
            "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800&q=80",  // Araç 4
            "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800&q=80",  // Araç 5
            "https://images.unsplash.com/photo-1548013146-72479768bada?w=800&q=80",     // Araç 6
            "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?w=800&q=80",  // Araç 7
            "https://images.unsplash.com/photo-1552519507-da3b142c6e3d?w=800&q=80",     // Araç 8
            "https://images.unsplash.com/photo-1590362891991-f776e747a588?w=800&q=80",  // Araç 9
            "https://images.unsplash.com/photo-1541899481282-d53bffe3c35d?w=800&q=80",  // Araç 10
            "https://images.unsplash.com/photo-1559416523-140ddc3d238c?w=800&q=80",     // Araç 11
        };

        private static readonly string[] SidePhotos = new[]
        {
            "https://images.unsplash.com/photo-1503376780353-7e6692767b70?w=800&q=80",
            "https://images.unsplash.com/photo-1492144534655-ae79c964c9d7?w=800&q=80",
            "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?w=800&q=80",
            "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800&q=80",
            "https://images.unsplash.com/photo-1544636331-e26879cd4d9b?w=800&q=80",
            "https://images.unsplash.com/photo-1548013146-72479768bada?w=800&q=80",
            "https://images.unsplash.com/photo-1541899481282-d53bffe3c35d?w=800&q=80",
            "https://images.unsplash.com/photo-1552519507-da3b142c6e3d?w=800&q=80",
            "https://images.unsplash.com/photo-1590362891991-f776e747a588?w=800&q=80",
            "https://images.unsplash.com/photo-1559416523-140ddc3d238c?w=800&q=80",
            "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800&q=80",
        };

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<BaseDbContext>>();

            try
            {
                // Tüm mevcut araç ID'lerini çek
                var vehicleIds = await db.Vehicles
                    .Select(v => v.Id)
                    .OrderBy(id => id)
                    .ToListAsync();

                if (!vehicleIds.Any())
                {
                    logger.LogInformation("VehiclePhotoDataSeeder: Hiç araç bulunamadı, seed atlanıyor.");
                    return;
                }

                // Zaten fotoğrafı olan araçları çek
                var vehiclesWithPhotos = await db.VehiclePhotos
                    .Select(p => p.VehicleId)
                    .Distinct()
                    .ToListAsync();

                // Fotoğrafı olmayan araçları bul
                var vehiclesNeedingPhotos = vehicleIds
                    .Where(id => !vehiclesWithPhotos.Contains(id))
                    .ToList();

                if (!vehiclesNeedingPhotos.Any())
                {
                    logger.LogInformation("VehiclePhotoDataSeeder: Tüm araçların fotoğrafı mevcut, seed atlanıyor.");
                    return;
                }

                logger.LogInformation("VehiclePhotoDataSeeder: {Count} araç için dummy fotoğraf ekleniyor...", vehiclesNeedingPhotos.Count);

                var now = DateTime.UtcNow;
                var photosToAdd = new List<VehiclePhoto>();

                // Her aracın ClientId'sini de çek
                var vehicleClientMap = await db.Vehicles
                    .Where(v => vehiclesNeedingPhotos.Contains(v.Id))
                    .Select(v => new { v.Id, v.ClientId })
                    .ToDictionaryAsync(v => v.Id, v => v.ClientId);

                for (int i = 0; i < vehiclesNeedingPhotos.Count; i++)
                {
                    var vehicleId = vehiclesNeedingPhotos[i];
                    var clientId = vehicleClientMap.TryGetValue(vehicleId, out var cid) ? cid : 1;

                    // Ön fotoğraf
                    photosToAdd.Add(new VehiclePhoto
                    {
                        VehicleId = vehicleId,
                        FilePath = CarPhotos[i % CarPhotos.Length],
                        PhotoType = "Ön",
                        Description = "Araç ön görünüm",
                        DisplayOrder = 1,
                        UploadDate = now.AddMonths(-(vehiclesNeedingPhotos.Count - i)),
                        ClientId = clientId,
                        CreatedDate = now.AddMonths(-(vehiclesNeedingPhotos.Count - i)),
                        CreatedBy = 1,
                    });

                    // Yan fotoğraf
                    photosToAdd.Add(new VehiclePhoto
                    {
                        VehicleId = vehicleId,
                        FilePath = SidePhotos[i % SidePhotos.Length],
                        PhotoType = "Yan",
                        Description = "Araç yan görünüm",
                        DisplayOrder = 2,
                        UploadDate = now.AddMonths(-(vehiclesNeedingPhotos.Count - i)),
                        ClientId = clientId,
                        CreatedDate = now.AddMonths(-(vehiclesNeedingPhotos.Count - i)),
                        CreatedBy = 1,
                    });
                }

                await db.VehiclePhotos.AddRangeAsync(photosToAdd);
                await db.SaveChangesAsync();

                logger.LogInformation("VehiclePhotoDataSeeder: {Count} araç için {Photos} fotoğraf başarıyla eklendi.",
                    vehiclesNeedingPhotos.Count, photosToAdd.Count);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "VehiclePhotoDataSeeder: Fotoğraf seed işlemi sırasında hata oluştu.");
            }
        }
    }
}
