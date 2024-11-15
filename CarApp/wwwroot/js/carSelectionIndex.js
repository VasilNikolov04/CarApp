if (typeof carModels === 'undefined') {
    var carModels = {};
}
function populateCarModelsIndex(models) {
    models.forEach(({ BrandName, ModelName }) => {
        if (!carModels[BrandName]) {
            carModels[BrandName] = [];
        }
        carModels[BrandName].push({ ModelName });
    });
}
function updateModelsIndex() {
    const brandSelect = document.getElementById("carBrand");
    const modelSelect = document.getElementById("carModel");

    const selectedModelName = modelSelect.value;

    modelSelect.innerHTML = '<option value="">Select a Model</option>';

    const selectedBrandName = brandSelect.value;

    const models = carModels[selectedBrandName];
    if (models) {
        models.forEach(model => {
            const option = document.createElement("option");
            option.value = model.ModelName;
            option.textContent = model.ModelName;

            if (model.ModelName === selectedModelName) {
                option.selected = true;
            }

            modelSelect.appendChild(option);
        });
    }
}

