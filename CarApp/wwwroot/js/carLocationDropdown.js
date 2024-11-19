let carCities = {};

function populateCarCities(models) {
    models.forEach(({ LocationId, CityName }) => {
        if (!carCities[LocationId]) {  
            carCities[LocationId] = [];
        }
        carCities[LocationId].push({ CityName: CityName });
    });
}

function updateLocations() {
    const locationSelect = document.getElementById("carLocation");
    const citySelect = document.getElementById("carCity");

    citySelect.innerHTML = '<option value="">Select a City</option>';

    const selectedLocationId = locationSelect.value;

    const cities = carCities[selectedLocationId];
    if (cities) {
        cities.forEach((city) => {
            const option = document.createElement('option');
            option.value = city.CityName;
            option.textContent = city.CityName;
            citySelect.appendChild(option);
        });
    }
}

