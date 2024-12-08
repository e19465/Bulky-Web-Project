document.addEventListener("DOMContentLoaded", function () {
    // Hamburger menu logic (mobile menu)
    const hamburgerIcon = document.getElementById('hamburger-icon');
    const mobileMenu = document.getElementById('mobile-menu');
    const dropdownButton = document.getElementById('dropdownDefaultButton');
    const dropdownMenu = document.getElementById('dropdown');

    // Hamburger menu toggle
    if (hamburgerIcon && mobileMenu) {
        hamburgerIcon.addEventListener('click', function () {
            mobileMenu.classList.toggle('hidden');
            mobileMenu.classList.toggle('flex');
        });

        const menuLinks = mobileMenu.querySelectorAll('a');
        menuLinks.forEach(link => {
            link.addEventListener('click', function () {
                mobileMenu.classList.add('hidden'); // Hide the menu after link click
            });
        });
    } else {
        console.error("Hamburger icon or mobile menu not found.");
    }

    // Dropdown toggle logic
    if (dropdownButton && dropdownMenu) {
        dropdownButton.addEventListener('click', function () {
            dropdownMenu.classList.toggle('hidden'); // Toggle visibility of dropdown
        });

        // Optional: Close the dropdown if a link inside it is clicked
        const dropDownLinks = dropdownMenu.querySelectorAll('a');
        dropDownLinks.forEach(link => {
            link.addEventListener('click', function () {
                dropdownMenu.classList.add('hidden'); // Hide dropdown after link click
            });
        });
    } else {
        console.error("Dropdown button or dropdown menu not found.");
    }
});
