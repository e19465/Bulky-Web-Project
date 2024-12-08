document.addEventListener("DOMContentLoaded", function () {
    // Get the count input and the total price display element by their IDs
    const countInput = document.getElementById('countInput');
    const totalPriceElement = document.getElementById('totalPrice');

    // Get the prices from the Razor model, passed as global JavaScript variables
    const price1to50 = parseFloat(priceData.price1to50);
    const price51to100 = parseFloat(priceData.price51to100);
    const price100Plus = parseFloat(priceData.price100Plus);

    // Function to calculate the price based on quantity
    function calculateTotalPrice() {
        let quantity = parseInt(countInput.value) || 0; // Default to 0 if input is not a number
        let totalPrice = 0;

        // Determine the price tier based on quantity
        if (quantity >= 1 && quantity <= 50) {
            totalPrice = price1to50 * quantity;
        } else if (quantity >= 51 && quantity <= 100) {
            totalPrice = price51to100 * quantity;
        } else if (quantity > 100) {
            totalPrice = price100Plus * quantity;
        }

        // Update the total price element
        totalPriceElement.textContent = `${totalPrice.toFixed(2)}`; // Format as currency
    }

    // Attach event listener to the input field to recalculate total price whenever quantity changes
    countInput.addEventListener('input', calculateTotalPrice);

    // Initial calculation when the page loads
    calculateTotalPrice();
});
