document.getElementById('prod-img-inp-update').addEventListener('change', function (event) {
    const file = event.target.files[0]; // Get the selected file
    const preview = document.getElementById('prod-update-img-preview'); // Get the preview element

    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            preview.src = e.target.result; // Set the preview src to the file data
            preview.style.display = "block"; // Show the preview
        };
        reader.readAsDataURL(file); // Read the file as a data URL
    } else {
        preview.src = "#";
        preview.style.display = "none"; // Hide the preview if no file is selected
    }
});
