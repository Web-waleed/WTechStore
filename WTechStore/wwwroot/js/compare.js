// Array to store selected product IDs for comparison
let compareProducts = [];

// When the compare button is clicked
document.querySelectorAll('.compare-btn').forEach(button => {
    button.addEventListener('click', function () {
        // Get the product ID from data-id attribute
        const productId = this.getAttribute('data-id');

        // Toggle product selection (add/remove from the comparison array)
        if (compareProducts.includes(productId)) {
            compareProducts = compareProducts.filter(id => id !== productId);
            alert(`Product ID ${productId} removed from comparison.`);
        } else {
            compareProducts.push(productId);
            alert(`Product ID ${productId} added to comparison.`);
        }
    });
});

// Function to handle comparison when "Compare Now" button is clicked
document.getElementById('compare-now-btn').addEventListener('click', function () {
    if (compareProducts.length === 0) {
        alert('No products selected for comparison.');
        return;
    }

    // Create the query string with selected product IDs
    const productIds = compareProducts.join(',');

    // Redirect to the Compare action with the product IDs
    window.location.href = `/Home/Compare?productIds=${productIds}`;
});
