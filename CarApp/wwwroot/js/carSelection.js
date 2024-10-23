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

    // Debugging: Log the populated carModels object to confirm it's filled correctly
    console.log('Populated carModels:', carModels);
}

// Function to update the car model dropdown based on selected brand
function updateModels() {
    const brandSelect = document.getElementById("carBrand");
    const modelSelect = document.getElementById("carModel");

    // Clear existing options in the model dropdown
    modelSelect.innerHTML = '<option value="">Select a Model</option>';

    const selectedBrandId = parseInt(brandSelect.value, 10);  // Convert to integer

    // Debugging: Log the selected BrandId
    console.log('Selected Brand ID:', selectedBrandId);

    // Check if models exist for the selected brand
    const models = carModels[selectedBrandId];

    // Debugging: Log the models array for this BrandId
    console.log('Models found for this brand:', models);

    if (models && models.length > 0) {
        models.forEach(model => {
            const option = new Option(model.ModelName, model.Id);
            modelSelect.add(option);
        });
        console.log('Models successfully added to dropdown.');
    } else {
        console.log('No models found for this brand.');
    }
}