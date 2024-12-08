document.addEventListener("DOMContentLoaded", function () {
    // Get the hamburger icon and the mobile menu
    const hamburgerIcon = document.getElementById('hamburger-icon');
    const mobileMenu = document.getElementById('mobile-menu');

    // Make sure both elements exist before adding event listeners
    if (hamburgerIcon && mobileMenu) {
        hamburgerIcon.addEventListener('click', function () {
            if (mobileMenu.classList.contains('hidden')) {
                mobileMenu.classList.remove('hidden');
                mobileMenu.classList.add('flex');
            } else if (mobileMenu.classList.contains('flex')) {
                mobileMenu.classList.remove('flex');
                mobileMenu.classList.add('hidden');
            }
        });

        // Optional: Close the mobile menu if a link inside it is clicked
        const menuLinks = mobileMenu.querySelectorAll('a');
        menuLinks.forEach(link => {
            link.addEventListener('click', function () {
                mobileMenu.classList.add('hidden'); // Hide the menu after link click
            });
        });
    } else {
        console.error("Hamburger icon or mobile menu not found.");
    }
});