using CarApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarApp.Infrastructure.Data.SeedDb
{
    public class DrivetrainConfiguration : IEntityTypeConfiguration<CarDrivetrain>
    {
        public void Configure(EntityTypeBuilder<CarDrivetrain> builder)
        {

            builder.HasData(SeedDrivetrain());
        }
        private IEnumerable<CarDrivetrain> SeedDrivetrain()
        {
            IEnumerable<CarDrivetrain> drivetrains = new List<CarDrivetrain>()
            {
                new CarDrivetrain()
                {
                    DrivetrainId = 1,
                    DrivetrainName = "Rear-Wheel Drive (RWD)"
                },
                new CarDrivetrain()
                {
                    DrivetrainId = 2,
                    DrivetrainName = "Front-Wheel Drive (FWD)"
                },
                new CarDrivetrain()
                {
                    DrivetrainId = 3,
                    DrivetrainName = "All-Wheel Drive (AWD)"
                },
                new CarDrivetrain()
                {
                    DrivetrainId = 4,
                    DrivetrainName = "Four-Wheel Drive (4x4)"
                }

            };

            return drivetrains;
        }
    }
}
