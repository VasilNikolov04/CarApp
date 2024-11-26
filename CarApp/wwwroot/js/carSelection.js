var carModels = {};
var carCities = {};

function populateCarModels(models) {
    models.forEach(({ BrandId, ModelId, ModelName }) => {
        if (!carModels[BrandId]) {
            carModels[BrandId] = [];
        }
        carModels[BrandId].push({ ModelId, ModelName });
    });
}

function updateModels() {
    const brandSelect = document.getElementById("carBrand");
    const modelSelect = document.getElementById("carModel");

    modelSelect.innerHTML = '<option value="">Select a Model</option>';

    const selectedBrandId = parseInt(brandSelect.value, 10);

    const models = carModels[selectedBrandId];
    if (models) {
        models.forEach(model => {
            const option = document.createElement('option');
            option.value = model.ModelId;
            option.textContent = model.ModelName;
            modelSelect.appendChild(option);
        });
    }
}

function populateCarCities(cities) {
    cities.forEach(({ LocationId, CityId, CityName }) => {
        if (!carCities[LocationId]) {
            carCities[LocationId] = [];
        }
        carCities[LocationId].push({ LocationId: LocationId, CityId: CityId, CityName: CityName });
    });
}

function updateCities() {
    const locationSelect = document.getElementById("carLocation");
    const citySelect = document.getElementById("carCity");

    citySelect.innerHTML = '<option value="">Select a City</option>';

    const selectedLocationId = parseInt(locationSelect.value, 10);

    const cities = carCities[selectedLocationId];
    if (cities) {
        cities.forEach(city => {
            const option = document.createElement('option');
            option.value = city.CityId;
            option.textContent = city.CityName;
            citySelect.appendChild(option);

        });
    }
}