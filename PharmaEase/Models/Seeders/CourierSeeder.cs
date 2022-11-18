using Microsoft.EntityFrameworkCore;
using PharmaEase.Data;
using PharmaEase.Models;

public static class CourierSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new PharmaEaseContext(
            serviceProvider.GetRequiredService<DbContextOptions<PharmaEaseContext>>()))
        {
            // Look for any movies.
            if (context.Courier.Any())
            {
                return;   // DB has been seeded
            }
            context.Courier.AddRange(
                new Courier
                {
                    OperatingRange = 7,
                    Name = "UPS"
                },
               new Courier
               {
                   OperatingRange = 700,
                   Name = "USPS"
               },
                new Courier
                {
                    OperatingRange = 700,
                    Name = "Canada Post"
                },
               new Courier
               {
                   OperatingRange = 700,
                   Name = "Purolator"
               }
            );
            context.SaveChanges();
        }
    }

}
