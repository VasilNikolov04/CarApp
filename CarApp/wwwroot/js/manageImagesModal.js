document.addEventListener('DOMContentLoaded', function () {
    const addImagesButton = document.querySelector('.btn-success[data-toggle="modal"]');

    // Open modal on button click
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
                        updateImageOrder();
                        refreshModalImages(data.images);
                    } else {
                        alert('Failed to remove image: ' + data.message);
                    }
                })
                .catch(error => {
                    console.error('Error removing image:', error);
                });
        }
    };

    let draggedImage = null;

    document.querySelectorAll('.draggable-image-container').forEach(item => {
        item.addEventListener('dragstart', dragStart);
        item.addEventListener('dragover', dragOver);
        item.addEventListener('drop', drop);
    });

    function dragStart(event) {
        draggedImage = event.target.closest('.draggable-image-container'); // Ensure correct element
        event.dataTransfer.setData('text', draggedImage.dataset.id);
        event.dataTransfer.effectAllowed = 'move';
    }

    function dragOver(event) {
        event.preventDefault(); // Prevent default to allow drop
        const target = event.target.closest('.draggable-image-container');
        if (target && target !== draggedImage) {
            target.classList.add('drag-over');
        }
    }

    function drop(event) {
        event.preventDefault();
        const target = event.target.closest('.draggable-image-container');

        if (!target || target === draggedImage) return;

        target.classList.remove('drag-over');

        const draggedImageIndex = Array.from(target.parentElement.children).indexOf(draggedImage);
        const droppedImageIndex = Array.from(target.parentElement.children).indexOf(target);

        if (draggedImageIndex < droppedImageIndex) {
            target.parentElement.insertBefore(draggedImage, target.nextSibling);
        } else {
            target.parentElement.insertBefore(draggedImage, target);
        }
        updateImageOrder();
    }

    function updateImageOrder() {
        const imageOrder = [];

            document.querySelectorAll('.draggable-image-container').forEach((imageElement, index) => {
                const imageId = imageElement.dataset.id;
                imageOrder.push({ Id: parseInt(imageId), Order: index });
            });
            console.log('Updated Image Order:', imageOrder)
            document.getElementById('saveOrderButton').onclick = function () {
                saveImageOrder(imageOrder);
            };
    }

    function saveImageOrder(imageOrder)
    {
        const carId = document.getElementById('addImagesModal').getAttribute('data-car-id');

        const payload = {
            CarId: parseInt(carId),
            OrderedImages: imageOrder
        };


        fetch(`/User/UpdateImageOrder`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(payload),
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    alert('Image order saved successfully');
                    refreshModalImages(data.images);

                    const modal = bootstrap.Modal.getInstance(document.getElementById('addImagesModal'));
                    modal.hide();

                    setTimeout(() => {
                        location.reload();
                    }, 300);
                } else {
                    alert('Failed to save image order: ' + data.message);
                }
            })
            .catch(error => {
                console.error('Error saving image order:', error);
            });
    }

    function refreshModalImages(images) {

        images.sort((a, b) => a.Order - b.Order);

        const imageContainer = document.querySelector('#addImagesModal .modal-body .row');
        imageContainer.innerHTML = '';

        images.forEach((image, index) => {
            const colDiv = document.createElement('div');
            colDiv.classList.add('col-md-4', 'mb-3', 'draggable-image-container');
            colDiv.setAttribute('data-id', image.id);
            colDiv.draggable = true;

            const imageWrapperDiv = document.createElement('div');
            imageWrapperDiv.classList.add('image-wrapper');

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

            imageWrapperDiv.appendChild(imgElement);
            imageWrapperDiv.appendChild(removeButton);
            colDiv.appendChild(imageWrapperDiv);
            imageContainer.appendChild(colDiv);

            colDiv.addEventListener('dragstart', dragStart);
            colDiv.addEventListener('dragover', dragOver);
            colDiv.addEventListener('drop', drop);
        });
    }
});