using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarApp.Core.Services.BrandAndModelSeedService;

namespace CarApp.Core.Services.Contracts
{
    public interface IBrandAndModelSeedService
    {
        public List<CarBrandDto> GetCarBrandData();
        public Task SeedBrandsAndModelsFromJson();

    }
}
