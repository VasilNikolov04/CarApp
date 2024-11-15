if (typeof carModels === 'undefined') {
    var carModels = {};
}

// Function to populate the carModels object
function populateCarModels(models) {
    models.forEach(({ BrandId, ModelId, ModelName }) => {
        if (!carModels[BrandId]) {
            carModels[BrandId] = [];
        }
        carModels[BrandId].push({ ModelId: ModelId, ModelName: ModelName });
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

    //if (models && models.length > 0) {
    //    models.forEach(model => {
    //        const option = new Option(model.ModelName, model.ModelId);
    //        modelSelect.add(option);
    //    });
    //    console.log('Models successfully added to dropdown.');
    //} else {
    //    console.log('No models found for this brand.');
    //}
    
}

function updateModelsIndex() {
    const brandSelect = document.getElementById("carBrand");
    const modelSelect = document.getElementById("carModel");

    modelSelect.innerHTML = '<option value="">Select a Model</option>';

    const selectedBrandId = parseInt(brandSelect.value, 10);

    const models = carModels[selectedBrandId];
    if (models) {
        models.forEach(model => {
            const option = document.createElement("option");
            option.value = model.ModelId; 
            option.textContent = model.ModelName;
            modelSelect.appendChild(option);
        });
    }
}


