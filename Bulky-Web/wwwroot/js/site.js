document.addEventListener('DOMContentLoaded', () => {
    const buttons = document.querySelectorAll('[data-dropdown-toggle]');
    const dropdowns = document.querySelectorAll('[id^="dropdown"]');

    buttons.forEach(button => {
        const dropdownId = button.getAttribute('data-dropdown-toggle');
        const dropdown = document.getElementById(dropdownId);
        const delay = parseInt(button.getAttribute('data-dropdown-delay'), 10) || 0;
        const trigger = button.getAttribute('data-dropdown-trigger') || 'click';

        let timeout;

        if (!dropdown) {
            console.warn(`Dropdown element with ID "${dropdownId}" not found.`);
            return;
        }

        if (trigger === 'hover') {
            // Handle hover functionality
            button.addEventListener('mouseenter', () => {
                timeout = setTimeout(() => {
                    dropdown.classList.remove('hidden');
                }, delay);
            });

            button.addEventListener('mouseleave', () => {
                clearTimeout(timeout);
                dropdown.classList.add('hidden');
            });

            dropdown.addEventListener('mouseenter', () => {
                clearTimeout(timeout);
            });

            dropdown.addEventListener('mouseleave', () => {
                dropdown.classList.add('hidden');
            });
        } else {
            // Handle click functionality
            button.addEventListener('click', (event) => {
                dropdown.classList.toggle('hidden');
                event.stopPropagation(); // Prevent click from propagating to the document
            });
        }

        // Close the dropdown when clicking outside the button or the dropdown
        document.addEventListener('click', (event) => {
            if (!button.contains(event.target) && !dropdown.contains(event.target)) {
                dropdown.classList.add('hidden');
            }
        });
    });
});
