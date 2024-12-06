using CarApp.Infrastructure.Data.Models;
using CarApp.Infrastructure.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarApp.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static CarApp.Core.Services.DataSeedService;
using CarApp.Core.Services.Contracts;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;

namespace CarApp.Core.Services
{
    public class DataSeedService : IDataSeedService
    {
        private readonly IRepository<CarModel, int> modelRepository;
        private readonly IRepository<Car, int> carRepository;
        private readonly IRepository<CarListing, int> carListingRepository;
        private readonly IRepository<CarBrand, int> brandRepository;
        private readonly IRepository<CarBodyType, int> bodyTypeRepository;
        private readonly IRepository<CarGear, int> gearRepository;
        private readonly IRepository<CarLocationRegion, int> locationRepository;
        private readonly IRepository<CarLocationCity, int> cityRepository;
        private readonly IRepository<CarDrivetrain, int> drivetrainRepository;
        private readonly IRepository<CarFuelType, int> fuelRepository;
        private readonly HttpClient client;

        private readonly UserManager<ApplicationUser> userManager;
        public DataSeedService(IRepository<CarModel, int> _modelRepository,
            IRepository<CarBrand, int> _brandRepository,
            IRepository<CarLocationRegion, int> _locationRepository,
            IRepository<CarLocationCity, int> _cityRepository,
            HttpClient _client, IRepository<Car, int> _carRepository,
            IRepository<CarListing, int> _carListingRepository,
            UserManager<ApplicationUser> _userManager, IRepository<CarBodyType, int> bodyTypeRepository,
            IRepository<CarGear, int> gearRepository, IRepository<CarDrivetrain, int> drivetrainRepository, 
            IRepository<CarFuelType, int> fuelRepository)
        {
            modelRepository = _modelRepository;
            brandRepository = _brandRepository;
            locationRepository = _locationRepository;
            cityRepository = _cityRepository;
            client = _client;
            carRepository = _carRepository;
            carListingRepository = _carListingRepository;
            userManager = _userManager;
            this.bodyTypeRepository = bodyTypeRepository;
            this.gearRepository = gearRepository;
            this.drivetrainRepository = drivetrainRepository;
            this.fuelRepository = fuelRepository;
        }

        public List<CarBrandDto> GetCarBrandData()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\..\..\CarApp.Infrastructure\Data\SeedData\BrandSeed.json");
            string jsonData = File.ReadAllText(jsonFilePath);
            var carBrandList = JsonConvert.DeserializeObject<List<CarBrandDto>>(jsonData);


            return carBrandList;
        }

        public async Task SeedBrandsAndModelsFromJson()
        {
            var carBrandData = GetCarBrandData();
            if (carBrandData == null || !carBrandData.Any())
                return;

            // Seed CarBrands with IDs explicitly
            var existingBrands = await brandRepository.GetAllAttached().ToListAsync();
            var newBrands = new List<CarBrand>();

            for (int i = 1; i <= carBrandData.Count; i++)
            {
                var brandDto = carBrandData[i - 1];

                if (!existingBrands.Any(b => b.BrandName
                .Equals(brandDto.Brand, StringComparison.OrdinalIgnoreCase)))
                {
                    var carBrand = new CarBrand
                    {
                        BrandName = brandDto.Brand
                    };
                    newBrands.Add(carBrand);
                }
            }

            if (newBrands.Any())
            {
                var newBrandsArray = newBrands.ToArray(); // Convert List<CarBrand> to CarBrand[]
                brandRepository.AddRange(newBrandsArray);
                existingBrands.AddRange(newBrands);
            }

            // Seed CarModels
            var existingModels = await modelRepository.GetAllAttached().ToListAsync();
            var modelsToSeed = new List<CarModel>();

            foreach (var brandDto in carBrandData)
            {
                var brand = existingBrands
                    .FirstOrDefault(b => b.BrandName
                    .Equals(brandDto.Brand, StringComparison.OrdinalIgnoreCase));
                if (brand != null && brandDto.Models != null)
                {
                    var models = brandDto.Models
                        .Where(modelName => !existingModels
                        .Any(m => m.ModelName
                        .Equals(modelName, StringComparison.OrdinalIgnoreCase) && m.BrandId == brand.Id))
                        .Select(modelName => new CarModel { ModelName = modelName, BrandId = brand.Id });

                    modelsToSeed.AddRange(models);
                }
            }

            if (modelsToSeed.Any())
            {
                var modelsToSeedArray = modelsToSeed.ToArray(); // Convert List<CarModel> to CarModel[]
                modelRepository.AddRange(modelsToSeedArray);
            }
        }

        public async Task SeedCarsAndListingsAsync()
        {
            if (carRepository.GetAllAttached().Any() &&
                carListingRepository.GetAllAttached().Any()) return;

            var user1 = await userManager.FindByEmailAsync("ivan79@abv.bg");
            var user2 = await userManager.FindByEmailAsync("gosho.G@gmail.com");
            var user3 = await userManager.FindByEmailAsync("bdinov@abv.bg");
            var user4 = await userManager.FindByEmailAsync("b_kostadinov@gmail.com");

            if (user1 == null || user2 == null || user3 == null || user4 == null)
            {
                throw new Exception("Users with specified emails not found");
            }
            if (!carRepository.GetAllAttached().Any())
            {
                var cars = new List<Car>
        {
            new Car
            {
                ModelId = await modelRepository
                .GetAllAttached()
                .Where(m => m.ModelName == "Corolla")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                CarBodyId = 4,
                Trim = "XSE",
                EngineDisplacement = 1800,
                Whp = 169,
                Mileage = 45000,
                Year = 2020,
                FuelId = 3,
                GearId = 2,
                DrivetrainId = 2
            },
            new Car
            {
                ModelId = await modelRepository
                .GetAllAttached()
                .Where(m => m.ModelName == "Civic")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                CarBodyId = 4,
                Trim = "Sport Touring",
                EngineDisplacement = 1500,
                Whp = 174,
                Mileage = 35000,
                Year = 2021,
                FuelId = 3,
                GearId = 2,
                DrivetrainId = 2
            },
            new Car
            {
                ModelId = await modelRepository
                .GetAllAttached()
                .Where(m => m.ModelName == "Mustang")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                CarBodyId = 2,
                Trim = "GT",
                EngineDisplacement = 5000,
                Whp = 450,
                Mileage = 25000,
                Year = 2019,
                FuelId = 3,
                GearId = 1,
                DrivetrainId = 1
            },
            new Car
            {
                ModelId = await modelRepository
                .GetAllAttached()
                .Where(m => m.ModelName == "340" && m.BrandId == 56)
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                CarBodyId = 4,
                Trim = "",
                EngineDisplacement = 4000,
                Whp = 382,
                Mileage = 30000,
                Year = 2020,
                FuelId = 3,
                GearId = 2,
                DrivetrainId = 3
            },
            new Car
            {
                ModelId = await modelRepository
                .GetAllAttached()
                .Where(m => m.ModelName == "Camaro")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                CarBodyId = 2,
                Trim = "SS",
                EngineDisplacement = 6200,
                Whp = 455,
                Mileage = 40000,
                Year = 2018,
                FuelId = 3,
                GearId = 2,
                DrivetrainId = 1
            },
            new Car
            {
                ModelId = await modelRepository
                .GetAllAttached()
                .Where(m => m.ModelName == "C 300")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                CarBodyId = 4,
                Trim = "",
                EngineDisplacement = 2000,
                Whp = 255,
                Mileage = 22000,
                Year = 2021,
                FuelId = 3,
                GearId = 2,
                DrivetrainId = 3
            },
            new Car
            {
                ModelId = await modelRepository
                .GetAllAttached()
                .Where(m => m.ModelName == "A4")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                CarBodyId = 4,
                Trim = "Premium Plus",
                EngineDisplacement = 2000,
                Whp = 248,
                Mileage = 28000,
                Year = 2020,
                FuelId = 3,
                GearId = 2,
                DrivetrainId = 3
            },
            new Car
            {
                ModelId = await modelRepository
                .GetAllAttached()
                .Where(m => m.ModelName == "F-Type")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                CarBodyId = 2,
                Trim = "P300",
                EngineDisplacement = 2000,
                Whp = 296,
                Mileage = 15000,
                Year = 2021,
                FuelId = 3,
                GearId = 2,
                DrivetrainId = 1
            },
            new Car
            {
                ModelId = await modelRepository
                .GetAllAttached()
                .Where(m => m.ModelName == "370 Z")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                CarBodyId = 2,
                Trim = "Nismo",
                EngineDisplacement = 3700,
                Whp = 350,
                Mileage = 75000,
                Year = 2019,
                FuelId = 3,
                GearId = 1,
                DrivetrainId = 1
            },
            new Car
            {
                ModelId = await modelRepository
                .GetAllAttached()
                .Where(m => m.ModelName == "MX-5")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                CarBodyId = 2,
                Trim = "Miata",
                EngineDisplacement = 2000,
                Whp = 181,
                Mileage = 45000,
                Year = 2020,
                FuelId = 3,
                GearId = 1,
                DrivetrainId = 1
            }
        };

                await carRepository.AddRangeAsync(cars.ToArray());
            }
            if (!carListingRepository.GetAllAttached().Any())
            {
                var listings = new List<CarListing>
        {
            new CarListing
            {
                CarId = 1,
                Description = "I’m selling my 2020 Toyota Corolla XSE, a car I’ve owned for about a year. I’ve had such a good experience with this vehicle—it's been incredibly reliable, fuel-efficient, and smooth to drive, but I’ve recently upgraded to something a little larger to accommodate my growing family.\r\n\r\nThis Corolla is the XSE trim, which means it comes with a sportier appearance and all the tech you could ask for in a compact car. The 1.8L engine provides plenty of power for city driving, yet it’s still very fuel-efficient, making it perfect for daily commuting. The car has a sleek, modern design with LED headlights, a stunning black interior with leather-trimmed seats, and a user-friendly infotainment system with Apple CarPlay and Android Auto.\r\n\r\nI’ve kept the car in pristine condition and have regularly serviced it at Toyota dealerships. It’s never been involved in any accidents and is still under warranty. The car also comes with great features like lane assist, pre-collision system, and a rearview camera for added safety.\r\n\r\nI’m selling it only because I need a larger vehicle now that my family is expanding, but this Corolla has been a fantastic car that I’ve enjoyed driving. It's perfect for someone looking for an affordable, reliable, and safe vehicle.\r\n\r\nPrice is firm, no negotiations. I’ve priced it competitively for its age and condition.",
                Price = 22000,
                CityId = await cityRepository
                .GetAllAttached()
                .Where(m => m.CityName == "Nesebar")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                SellerId = user1.Id,
                //MainImageUrl = "corolla(1).webp",
                CarImages = new List<CarImage>
                {
                    new CarImage { ImageUrl = "corolla(1).webp", Order = 0 },
                    new CarImage { ImageUrl = "corolla(2).webp", Order = 1},
                }
            },
            new CarListing
            {
                CarId = 2,
                Description = "I’m putting up for sale my 2021 Honda Civic Sport Touring, a car that I’ve thoroughly enjoyed over the past year. I’ve had to make the tough decision to sell it due to an upcoming move, and I can’t take the car with me. It’s been a great daily driver, and it’s in excellent condition, having been kept in a garage and well-maintained.\r\n\r\nThis Civic is the Sport Touring trim, which means it comes with everything you’d want in a compact car. It features a 1.5L turbocharged engine that provides an exciting amount of power while still being incredibly fuel-efficient. The car is finished in a beautiful crystal black pearl exterior, and the interior is a comfortable mix of leather and high-quality materials.\r\n\r\nI’ve added a few custom touches, including a set of high-performance tires and a blacked-out grille that adds a sportier look to the car. The car comes with features like a large touchscreen display, Apple CarPlay, a premium sound system, and Honda Sensing, which includes adaptive cruise control, lane-keeping assist, and more.\r\n\r\nI’ve kept the car well-maintained with regular service, and it’s still under the original warranty. It’s an excellent choice for anyone looking for a reliable, fun-to-drive, and tech-packed compact car.\r\n\r\nI’m firm on the price—no negotiations. This is a well-priced, high-condition car, and I’m only selling it because of the move.",
                Price = 25000,
                CityId = await cityRepository
                .GetAllAttached()
                .Where(m => m.CityName == "Nesebar")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                SellerId = user1.Id,
                //MainImageUrl = "civic(1).webp",
                CarImages = new List<CarImage>
                {
                    new CarImage { ImageUrl = "civic(1).webp", Order = 0 },
                    new CarImage { ImageUrl = "civic(2).webp", Order = 1 },
                    new CarImage { ImageUrl = "civic(3).webp", Order = 2 },
                }
            },
            new CarListing
            {
                CarId = 3,
                Description = "Selling my personal 2018 Ford Mustang GT. I’ve had this car for almost 3 years now, and it’s been a blast to drive, but it’s time for me to part ways. I’m moving to a more family-oriented car, and sadly, this beauty can’t be my daily driver anymore. The car has been well-maintained with full service history and has never been involved in any accidents.\r\n\r\nThis Mustang is the GT model with the iconic 5.0L V8 engine that packs a punch with 450 horsepower, making every drive exhilarating. I’ve installed a few aftermarket parts to make it even more fun to drive, including a cold air intake and a performance exhaust system that gives it that unmistakable muscle car growl. The suspension has been upgraded with coilovers to give it a more aggressive stance and better handling.\r\n\r\nThe interior is in excellent condition with leather seats, a modern infotainment system, and a premium sound system. It also has some great tech like parking sensors and a rearview camera. I added a set of custom alloy wheels and high-performance tires to complete the sporty look and feel.\r\n\r\nI’m selling this car because it’s just not practical for my current needs, but trust me, it’s a joy to drive. You’ll love the power, the sound, and the attention it gets everywhere you go!\r\n\r\nPrice is firm—no negotiations. This is a fair price for such a great car in excellent condition. Feel free to contact me if you’re serious.",
                Price = 35000,
                CityId = await cityRepository
                .GetAllAttached()
                .Where(m => m.CityName == "Dupnitsa")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                SellerId = user2.Id,
                //MainImageUrl = "mustang(1).webp",
                CarImages = new List<CarImage>
                {
                    new CarImage { ImageUrl = "mustang(1).webp", Order = 0 },
                    new CarImage { ImageUrl = "mustang(2).webp", Order = 1 },
                    new CarImage { ImageUrl = "mustang(3).webp", Order = 2 },
                }
            },
            new CarListing
            {
                CarId = 4,
                Description = "I’m selling my 2020 BMW M340i after a year of driving it, and it’s been an absolute dream. This car offers a perfect combination of performance, luxury, and technology, and it’s honestly one of the best sedans I’ve ever driven. I’m only parting with it because of a lifestyle change, but I know the next owner will be just as impressed as I’ve been.\r\n\r\nThe M340i is equipped with a 3.0L turbocharged inline-6 engine that produces 382 horsepower, making it quick and powerful. It has the xDrive all-wheel-drive system, which gives it incredible handling in all weather conditions. The car can go from 0 to 60 mph in just 4.1 seconds, and it handles corners with precision. The 8-speed automatic transmission is smooth, and the car has multiple driving modes to suit your mood—whether you’re cruising on the highway or pushing it to the limit.\r\n\r\nInside, the cabin is luxurious, with high-quality materials, leather seats, a large infotainment screen with BMW’s iDrive system, Apple CarPlay, and a premium sound system. It also features a panoramic sunroof, heated seats, and a wide array of safety features like adaptive cruise control, lane-departure warning, and blind-spot monitoring.\r\n\r\nThe car has been well-maintained, always garage-kept, and has had regular service. It’s still under warranty and has never been in any accidents.",
                Price = 35000,
                CityId = await cityRepository
                .GetAllAttached()
                .Where(m => m.CityName == "Dupnitsa")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                SellerId = user2.Id,
                //MainImageUrl = "340(1).webp",
                CarImages = new List<CarImage>
                {
                    new CarImage { ImageUrl = "340(1).webp", Order = 0 },
                    new CarImage { ImageUrl = "340(2).webp", Order = 1 },
                }
            },
            new CarListing
            {
                CarId = 5,
                Description = "I'm reluctantly selling my 2018 Chevrolet Camaro SS, a car that has been an absolute thrill to drive. I’ve decided to part ways with it because I’m looking to get something a bit more practical, but I’ve enjoyed every minute of owning this high-performance muscle car. The Camaro SS is an absolute beast, and it has provided all the power and excitement I could ever ask for.\r\n\r\nUnder the hood is a 6.2L V8 engine that cranks out 455 horsepower, making it incredibly fast and powerful. The sound of the exhaust is like nothing else—deep, throaty, and exhilarating. This car can go from 0 to 60 mph in just 4.0 seconds, and with the magnetic ride control suspension, it handles every twist and turn of the road with precision. The 8-speed automatic transmission is smooth, but I’ve also enjoyed using the paddle shifters for an even more engaging driving experience.\r\n\r\nThe interior is just as impressive, featuring leather-trimmed seats, an 8-inch touchscreen, Apple CarPlay, and a premium sound system. The car also has heated and ventilated seats, making it comfortable no matter the season.\r\n\r\nThis Camaro has been very well-maintained, regularly serviced, and always stored in a garage. It’s a true American muscle car with incredible performance that’s hard to beat.",
                Price = 33000,
                CityId = await cityRepository
                .GetAllAttached()
                .Where(m => m.CityName == "Dupnitsa")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                SellerId = user2.Id,
                //MainImageUrl = "camaro(1).webp",
                CarImages = new List<CarImage>
                {
                    new CarImage { ImageUrl = "camaro(1).webp", Order = 0 },
                    new CarImage { ImageUrl = "camaro(2).webp", Order = 1 },
                }
            },
            new CarListing
            {
                CarId = 6,
                Description = "I’m selling my 2021 Mercedes-Benz C 300 after a year of driving it, and it’s honestly been one of the best experiences I’ve had with a luxury sedan. The car is as comfortable as it is stylish, and it offers everything I was looking for in a car—performance, technology, and top-tier comfort. I’m selling it only because I’ve recently upgraded to an SUV, and I no longer need a sedan.\r\n\r\nThe C 300 is powered by a 2.0L turbocharged engine that provides a perfect balance of power and fuel efficiency. It has 255 horsepower and can go from 0 to 60 mph in just 5.9 seconds, making it fun to drive while maintaining great fuel economy for its class. The 9-speed automatic transmission ensures smooth shifting, and the car’s handling is precise, offering a truly luxurious driving experience.\r\n\r\nInside, the car is equipped with a high-end interior, featuring leather seats, a massive touchscreen with Mercedes’ COMAND system, Apple CarPlay, and a Harman Kardon sound system that sounds amazing. It also includes features like a panoramic sunroof, dual-zone climate control, and a full suite of safety features, including adaptive cruise control and lane-keeping assist.\r\n\r\nI’ve kept the car in pristine condition, always garaged, and regularly serviced. It’s still under warranty and has never been in any accidents.",
                Price = 38000,
                CityId = await cityRepository
                .GetAllAttached()
                .Where(m => m.CityName == "Pleven")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                SellerId = user3.Id,
                //MainImageUrl = "c300(1).webp",
                CarImages = new List<CarImage>
                {
                    new CarImage { ImageUrl = "c300(1).webp", Order = 0 },
                    new CarImage { ImageUrl = "c300(2).webp", Order = 1 },
                    new CarImage { ImageUrl = "c300(3).webp", Order = 2 }
                }
            },
            new CarListing
            {
                CarId = 7,
                Description = "I’m selling my 2020 Audi A4 Premium Plus after having it for a little over a year. It’s been a fantastic car, combining luxury, comfort, and performance in one sleek package. I’m reluctantly parting with it due to a change in lifestyle, but I’m confident that the next owner will love it just as much as I have.\r\n\r\nThis A4 comes with a 2.0L turbocharged engine, offering a perfect balance of power and fuel efficiency. The 7-speed dual-clutch transmission makes driving smooth, whether you’re cruising on the highway or navigating through the city. The all-wheel-drive system ensures excellent handling, no matter the weather conditions.\r\n\r\nThe interior is a true standout—premium leather seats, a panoramic sunroof, a large touchscreen with MMI navigation, and a Bang & Olufsen sound system. It also comes with a suite of driver-assistance features like lane-keeping assist, adaptive cruise control, and a rearview camera. I’ve always kept it well-maintained with regular servicing and garage parking.\r\n\r\nThis car is perfect for anyone looking for a luxury sedan with modern technology, exceptional comfort, and a responsive driving experience. I’m only selling because I need something larger, but this car has been an absolute pleasure to drive.",
                Price = 35000,
                CityId = await cityRepository
                .GetAllAttached()
                .Where(m => m.CityName == "Pleven")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                SellerId = user3.Id,
                //MainImageUrl = "a4(1).webp",
                CarImages = new List<CarImage>
                {
                    new CarImage { ImageUrl = "a4(1).webp", Order = 0 },
                    new CarImage { ImageUrl = "a4(2).webp", Order = 1 },
                }
            },
            new CarListing
            {
                CarId = 8,
                Description = "I’m selling my 2020 Jaguar F-Type P300 after an amazing year of ownership. This car is a true head-turner, and I’ve loved every moment spent behind the wheel. It combines luxury and performance in a way that few cars can match. However, with a change in lifestyle, I need to let it go, and it’s time for someone else to experience this beautiful machine.\r\n\r\nThe F-Type P300 features a 2.0L turbocharged engine producing 296 horsepower, which is more than enough to make this car thrilling to drive. The exhaust note is incredible, and it sounds like a true performance car should. The handling is sharp, with adaptive suspension that offers a smooth ride while still being responsive when you push it through corners.\r\n\r\nInside, the cabin is filled with luxurious materials, including leather upholstery and premium finishes. The infotainment system is intuitive and includes Apple CarPlay and a Meridian sound system. This car has been treated with care—always garaged and meticulously maintained. It has never been in any accidents and is still under warranty.\r\n\r\nThis car is perfect for someone who wants a sports car with a touch of luxury. It’s got the looks, the sound, and the performance to back it up. I’m only selling because my priorities have changed, but I’m confident the next owner will enjoy it just as much as I have.",
                Price = 55000,
                CityId = await cityRepository
                .GetAllAttached()
                .Where(m => m.CityName == "Blagoevgrad")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                SellerId = user4.Id,
                //MainImageUrl = "f-type(1).webp",
                CarImages = new List<CarImage>
                {
                    new CarImage { ImageUrl = "f-type(1).webp", Order = 0 },
                    new CarImage { ImageUrl = "f-type(2).webp", Order = 1 },
                }
            },
            new CarListing
            {
                CarId = 10,
                Description = "I’m selling my 2020 Mazda MX-5 Miata RF, which has been an absolute joy to own. Unfortunately, due to a change in circumstances, I’m no longer able to keep this fun little roadster. If you’ve ever driven an MX-5, you know it’s not just a car; it’s an experience, and I’ve loved every minute of it. This car is in perfect condition and has been babied throughout my ownership.\r\n\r\nThe car is the RF model, so it comes with the retractable hardtop that gives you the flexibility of both a convertible and a coupe experience. The 2.0L engine is lively and responsive, providing an engaging drive with every twist and turn of the road. The car’s lightweight design makes it incredibly nimble and fun to drive, and the manual transmission puts you in full control of the driving experience.\r\n\r\nInside, the car has premium leather seats, a Bose sound system, and a modern infotainment system with Apple CarPlay. I’ve kept it in pristine condition and have always parked it in a garage. It’s never been in any accidents, and I’ve had regular servicing done to make sure it stays in top shape.\r\n\r\nIf you’re looking for a car that’s pure driving fun, this is the one. I’ll definitely miss driving it, but it’s time for me to move on to something else.",
                Price = 28000,
                CityId = await cityRepository
                .GetAllAttached()
                .Where(m => m.CityName == "Blagoevgrad")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                SellerId = user4.Id,
                //MainImageUrl = "mx-5(1).webp",
                CarImages = new List<CarImage>
                {
                    new CarImage { ImageUrl = "mx-5(1).webp", Order = 0 },
                    new CarImage { ImageUrl = "mx-5(2).webp", Order = 1 },
                    new CarImage { ImageUrl = "mx-5(3).webp", Order = 2 }
                }
            },
            new CarListing
            {
                CarId = 9,
                Description = "I’m selling my 2021 Nissan 370Z Sport after enjoying every moment behind the wheel. This car has been a pure joy to drive, but with a change in my lifestyle, I’ve decided it’s time to part ways with this iconic sports car. It’s an absolute head-turner, and I’ve loved every second of owning it.\r\n\r\nThe 370Z comes with a powerful 3.7L V6 engine producing 332 horsepower, making it an exciting car to drive. The exhaust note is pure magic—something every enthusiast will appreciate. I’ve done a few performance upgrades to this car to make it even more fun, including a cold air intake and a performance exhaust system that adds to the already amazing sound. The handling has been improved with upgraded sway bars, giving the car a more responsive and planted feel when cornering.\r\n\r\nThe interior is a mix of sport and comfort with leather seats, a modern infotainment system, and a premium sound system. It’s perfect for someone who wants a car that looks fast and drives even faster. I’ve kept the car in excellent condition, always garage-kept and regularly serviced.\r\n\r\nThis car is a true sports car for enthusiasts. If you’ve been looking for a fun, powerful ride, this is the one. I’m only selling it because of lifestyle changes, and I hope it goes to someone who will enjoy it as much as I have.",
                Price = 25000,
                CityId = await cityRepository
                .GetAllAttached()
                .Where(m => m.CityName == "Blagoevgrad")
                .Select(m => m.Id)
                .FirstOrDefaultAsync(),
                SellerId = user4.Id,
                //MainImageUrl = "370z(1).jpg",
                CarImages = new List<CarImage>
                {
                    new CarImage { ImageUrl = "370z(1).jpg", Order = 0 },
                    new CarImage { ImageUrl = "370z(2).jpg", Order = 1 },
                    new CarImage { ImageUrl = "370z(3).jpg", Order = 2 }
                }
            }
        };

                await carListingRepository.AddRangeAsync(listings.ToArray());
            }
        }

        public async Task SeedCitiesAndRegionsFromApi()
        {
            var regions = locationRepository.GetAll();
            if (!regions.Any())
            {
                var regionsRequest = new HttpRequestMessage(HttpMethod.Post, "https://countriesnow.space/api/v0.1/countries/states");
                var regionsContent = new StringContent("{\"country\": \"Bulgaria\"}", Encoding.UTF8, "application/json");
                regionsRequest.Content = regionsContent;

                try
                {
                    var regionsResponse = await client.SendAsync(regionsRequest);
                    regionsResponse.EnsureSuccessStatusCode();

                    var regionsResponseContent = await regionsResponse.Content.ReadAsStringAsync();
                    var regionsJson = JsonDocument.Parse(regionsResponseContent);

                    if (regionsJson.RootElement.TryGetProperty("data", out var data) && data.ValueKind == JsonValueKind.Object)
                    {
                        var states = data.GetProperty("states").EnumerateArray();

                        foreach (var state in states)
                        {
                            string regionName = state.GetProperty("name").GetString();
                            string formattedRegion = regionName.Replace(" Province", "");

                            var existRegion = await locationRepository
                                .GetAllAttached()
                                .FirstOrDefaultAsync(r => r.RegionName == formattedRegion);
                            var region = new CarLocationRegion { RegionName = formattedRegion };
                            if (existRegion == null)
                            {
                                await locationRepository.AddAsync(region);
                            }



                            var citiesRequest = new HttpRequestMessage(HttpMethod.Post, "https://countriesnow.space/api/v0.1/countries/state/cities");
                            var citiesContent = new StringContent($"{{\"country\": \"Bulgaria\",\"state\": \"{regionName}\"}}", Encoding.UTF8, "application/json");
                            citiesRequest.Content = citiesContent;

                            var citiesResponse = await client.SendAsync(citiesRequest);
                            citiesResponse.EnsureSuccessStatusCode();

                            var citiesResponseContent = await citiesResponse.Content.ReadAsStringAsync();
                            var citiesJson = JsonDocument.Parse(citiesResponseContent);

                            if (citiesJson.RootElement.GetProperty("error").GetBoolean() == false)
                            {
                                var cities = citiesJson.RootElement.GetProperty("data").EnumerateArray();

                                foreach (var city in cities)
                                {
                                    if (!city.ToString().Contains("Obshtina"))
                                    {
                                        string cityName = city.GetString();

                                        var existCity = await cityRepository
                                            .GetAllAttached()
                                            .FirstOrDefaultAsync(r => r.CityName == cityName);
                                        if (existCity == null)
                                        {
                                            var cityEntity = new CarLocationCity { CityName = cityName, LocationId = region.Id };
                                            await cityRepository.AddAsync(cityEntity);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                throw new ArgumentNullException("No cities found or error occurred.");
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Error: Could not find 'data' as an object with 'states' in the regions response.");
                    }
                }
                catch (HttpRequestException e)
                {
                    throw new ArgumentException($"Request error: {e.Message}");
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"An error occurred: {e.Message}");
                }
            }
        }

        public class CarBrandDto
        {
            public string Brand { get; set; }
            public List<string> Models { get; set; }
        }
    }
}
