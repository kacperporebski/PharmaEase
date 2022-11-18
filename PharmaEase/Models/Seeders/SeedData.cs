namespace PharmaEase.Models.Seeders
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            MedicationSeeder.Initialize(serviceProvider);
            PrescriptionSeeder.Initialize(serviceProvider);
            //put rest of seeders here
            CourierSeeder.Initialize(serviceProvider);
        }

    }
}
