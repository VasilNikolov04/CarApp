if (typeof carModels === 'undefined') {
    var carModels = {};
}
function populateCarModels(models) {
    models.forEach(({ BrandName, ModelName }) => {
        if (!carModels[BrandName]) {
            carModels[BrandName] = [];
        }
        carModels[BrandName].push({ ModelName: ModelName });
    });
}
function updateModels() {
    const brandSelect = document.getElementById("carBrand");
    const modelSelect = document.getElementById("carModel");

    modelSelect.innerHTML = '<option value="">Select a Model</option>';

    const selectedBrandName = brandSelect.value;

    const models = carModels[selectedBrandName];
    if (models) {
        models.forEach((model) => {
            const option = document.createElement('option');
            option.value = model.ModelName;
            option.textContent = model.ModelName;
            modelSelect.appendChild(option);
        });
    }
}


