document.addEventListener('DOMContentLoaded', function () {
    const addImagesButton = document.querySelector('.btn-success[data-toggle="modal"]');

    addImagesButton.addEventListener('click', function () {
        const modal = new bootstrap.Modal(document.getElementById('addImagesModal'));
        modal.show();
    });

    window.removeImage = function (imageId) {
        const carId = document.getElementById('addImagesModal').getAttribute('data-car-id');

        if (confirm('Are you sure you want to remove this image?')) {
            fetch(`/User/RemoveImage?carId=${carId}&imageId=${imageId}`, {
                method: 'DELETE'
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        alert('Image removed successfully');
                        refreshModalImages(data.images);
                    } else {
                        alert('Failed to remove image: ' + data.message);
                    }
                })
                .catch(error => {
                    console.error('Error removing image:', error);
                });
        }
    }

    function refreshModalImages(images) {
        const imageContainer = document.querySelector('#addImagesModal .modal-body .row');
        imageContainer.innerHTML = '';

        images.forEach((image, index) => {

            const colDiv = document.createElement('div');
            colDiv.classList.add('col-md-4', 'mb-3');
            const imageContainerDiv = document.createElement('div');
            imageContainerDiv.classList.add('image-container');

            const imgElement = document.createElement('img');
            imgElement.src = `/images/${image.imageUrl}`;
            imgElement.classList.add('img-fluid', 'img-thumbnail');
            imgElement.alt = `Car Image ${index + 1}`;

            const removeButton = document.createElement('button');
            removeButton.type = 'button';
            removeButton.classList.add('btn', 'btn-danger', 'btn-sm', 'mt-2');
            removeButton.textContent = 'Remove';
            removeButton.onclick = function () {
                removeImage(image.id);
            };

            imageContainerDiv.appendChild(imgElement);
            imageContainerDiv.appendChild(removeButton);
            colDiv.appendChild(imageContainerDiv);
            imageContainer.appendChild(colDiv);
        });
    }
});