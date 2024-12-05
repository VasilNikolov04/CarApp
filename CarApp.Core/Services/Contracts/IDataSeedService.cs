using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarApp.Core.Services.DataSeedService;

namespace CarApp.Core.Services.Contracts
{
    public interface IDataSeedService
    {
        public List<CarBrandDto> GetCarBrandData();
        public Task SeedBrandsAndModelsFromJson();

        public Task SeedCitiesAndRegionsFromApi();

    }
}
