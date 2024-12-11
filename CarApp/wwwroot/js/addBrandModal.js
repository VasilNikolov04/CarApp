document.addEventListener('DOMContentLoaded', function () {
        const addModelButton = document.getElementById('addModelButton');
        const modelNameInput = document.getElementById('modelName');
        const modelList = document.getElementById('modelList');
        const saveBrandButton = document.getElementById('saveBrandButton');
        const errorMessage = document.createElement('div'); 
        errorMessage.className = 'alert alert-danger d-none';
        document.querySelector('.modal-body').prepend(errorMessage);

        let models = [];

        addModelButton.addEventListener('click', function () {
            const modelName = modelNameInput.value.trim();
            if (modelName) {
                models.push(modelName);

                const listItem = document.createElement('li');
                listItem.className = 'list-group-item d-flex justify-content-between align-items-center';
                listItem.textContent = modelName;

                const removeButton = document.createElement('button');
                removeButton.className = 'btn btn-danger btn-sm';
                removeButton.textContent = 'Remove';
                removeButton.onclick = function () {
                    models = models.filter(model => model !== modelName);
                    modelList.removeChild(listItem);
                };

                listItem.appendChild(removeButton);
                modelList.appendChild(listItem);

                modelNameInput.value = '';
            }
        });

        saveBrandButton.addEventListener('click', function () {
            const brandName = document.getElementById('brandName').value.trim();
            if (!brandName) {
                alert('Brand name is required.');
                return;
            }

            fetch('/Admin/DataManagement/AddBrandWithModels', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'

                },
                body: JSON.stringify({ BrandName: brandName, Models: models })
            }).then(async response => {
                if (response.ok) {
                    alert('Brand and models saved successfully!');
                    location.reload();
                } else if (response.status === 400) {
                    return response.text().then(error => {
                        displayError(error);
                    });
                } else if (response.status === 422) {
                    return response.text().then(error => {
                        displayError(error);
                    });
                } else {
                    displayError('An unexpected error occurred. Please try again.');
                }
            });
        });
        function displayError(message) {
            errorMessage.textContent = message;
            errorMessage.classList.remove('d-none');
        }
    });