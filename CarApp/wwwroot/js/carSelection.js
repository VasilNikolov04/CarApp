if (typeof carModels === 'undefined') {
    var carModels = {};  // Using var for broader scope
}

// Function to populate the carModels object
function populateCarModels(models) {
    models.forEach(({ BrandId, ModelId, ModelName }) => {
        if (!carModels[BrandId]) {
            carModels[BrandId] = [];
        }
        carModels[BrandId].push({ ModelId: ModelId, ModelName: ModelName });
    });

    console.log('Populated carModels:', carModels);
}

function updateModels() {
    const brandSelect = document.getElementById("carBrand");
    const modelSelect = document.getElementById("carModel");

    modelSelect.innerHTML = '<option value="">Select a Model</option>';

    const selectedBrandId = parseInt(brandSelect.value, 10);  

    console.log('Selected Brand ID:', selectedBrandId);

    const models = carModels[selectedBrandId];

    console.log('Models found for this brand:', models);

    if (models && models.length > 0) {
        models.forEach(model => {
            const option = new Option(model.ModelName, model.ModelId);
            modelSelect.add(option);
        });
        console.log('Models successfully added to dropdown.');
    } else {
        console.log('No models found for this brand.');
    }
}