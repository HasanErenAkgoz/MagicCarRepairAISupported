namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Abonelik planı türleri
    /// </summary>
    public enum SubscriptionPlan
    {
        /// <summary>
        /// Ücretsiz plan - 10 iş emri/ay, temel özellikler, 1 kullanıcı
        /// </summary>
        Free = 0,

        /// <summary>
        /// Temel plan - 50 iş emri/ay, 2 kullanıcı, temel raporlar
        /// </summary>
        Basic = 1,

        /// <summary>
        /// Profesyonel plan - Sınırsız iş emri, 5 kullanıcı, AI analiz (10/ay), gelişmiş raporlar
        /// </summary>
        Professional = 2,

        /// <summary>
        /// Kurumsal plan - Sınırsız her şey, sınırsız kullanıcı, özel destek, API erişimi
        /// </summary>
        Enterprise = 3
    }
}
