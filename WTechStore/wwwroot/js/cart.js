// Retrieve cart items from localStorage or initialize an empty array
let cartItems = JSON.parse(localStorage.getItem('cart')) || [];

// Cache DOM elements
const cartContainer = document.getElementById('cart-items').getElementsByTagName('tbody')[0];
const clearCartBtn = document.getElementById('clear-cart');
const totalPriceElement = document.getElementById('total-price-value');
const checkoutButton = document.getElementById('checkout-btn');
const checkoutSection = document.getElementById('checkout-section');
const checkoutForm = document.getElementById('checkout-form');

// Display cart items
function displayCart() {
    cartContainer.innerHTML = ''; // Clear current cart
    let totalPrice = 0;

    if (cartItems.length === 0) {
        cartContainer.innerHTML = "<tr><td colspan='6'>Your cart is empty.</td></tr>";
        totalPriceElement.textContent = '0.00';
        return;
    }

    cartItems.forEach(item => {
        const listItem = document.createElement('tr');
        const itemTotal = item.price * item.quantity;
        totalPrice += itemTotal;

        listItem.innerHTML = `
            <td><img src="${item.image}" alt="${item.name}" class="cart-image" /></td>
            <td>${item.name}</td>
            <td>$${item.price.toFixed(2)}</td>
            <td>
                <input type="number" value="${item.quantity}" min="1" data-id="${item.id}" class="quantity-input" />
            </td>
            <td>$${itemTotal.toFixed(2)}</td>
            <td><button class="remove-btn" data-id="${item.id}">Remove</button></td>
        `;
        cartContainer.appendChild(listItem);
    });

    totalPriceElement.textContent = totalPrice.toFixed(2);
}

// Remove item from cart
cartContainer.addEventListener('click', (event) => {
    if (event.target.classList.contains('remove-btn')) {
        const itemId = event.target.getAttribute('data-id');
        cartItems = cartItems.filter(item => item.id !== itemId);
        localStorage.setItem('cart', JSON.stringify(cartItems));
        displayCart();
    }
});

// Update item quantity
cartContainer.addEventListener('input', (event) => {
    if (event.target.classList.contains('quantity-input')) {
        const itemId = event.target.getAttribute('data-id');
        const newQuantity = parseInt(event.target.value);

        if (!isNaN(newQuantity) && newQuantity > 0) {
            const cartItem = cartItems.find(item => item.id === itemId);
            if (cartItem) {
                cartItem.quantity = newQuantity;
                localStorage.setItem('cart', JSON.stringify(cartItems));
                displayCart();
            }
        } else {
            event.target.value = 1; // Default to 1 if invalid
        }
    }
});

// Clear the entire cart
clearCartBtn.addEventListener('click', () => {
    Swal.fire({
        title: 'Are you sure?',
        text: "This will remove all items from your cart!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, clear it!',
        cancelButtonText: 'No, keep it'
    }).then((result) => {
        if (result.isConfirmed) {
            cartItems = [];
            localStorage.removeItem('cart');
            displayCart();
            Swal.fire('Cleared!', 'Your cart is now empty.', 'success');
        }
    });
});

// Checkout button functionality
checkoutButton.addEventListener('click', () => {
    if (cartItems.length > 0) {
        checkoutSection.style.display = 'block';
    } else {
        Swal.fire({
            title: 'Cart is empty!',
            text: 'Add items to proceed with checkout.',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    }
});

// Form submit event to send order to the server
checkoutForm.addEventListener('submit', (event) => {
    event.preventDefault();

    const formValid = checkoutForm.checkValidity();
    if (!formValid) {
        Swal.fire('Error', 'Please fill all the fields correctly.', 'error');
        return;
    }

    const formData = new FormData(checkoutForm);

    // Add cart items to FormData
    cartItems.forEach((item, index) => {
        formData.append(`items[${index}][id]`, item.id);
        formData.append(`items[${index}][name]`, item.name);
        formData.append(`items[${index}][price]`, item.price);
        formData.append(`items[${index}][quantity]`, item.quantity);
    });

    formData.append('totalAmount', totalPriceElement.textContent);

    // Send the form data to the server
    fetch('/Cart/SubmitOrder', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            Swal.fire('Order placed!', 'Thank you for your order.', 'success');
        })
        .catch(error => {
            Swal.fire('Order placed!', 'Thank you for your order.', 'success');
            console.error('Error:', error);
        })
        .finally(() => {
            // Clear the cart regardless of success or error
            localStorage.removeItem('cart'); // Clear the local storage cart
            cartItems = []; // Reset the cart items array
            displayCart(); // Re-render the cart display
            totalPriceElement.textContent = '0.00'; // Reset the total price
            checkoutSection.style.display = 'none'; // Hide checkout section
            checkoutForm.reset(); // Clear the form fields
        });
});

// Initialize display cart
displayCart();

document.addEventListener('DOMContentLoaded', function () {
    const creditCardRadio = document.getElementById('credit-card');
    const cashRadio = document.getElementById('cash');
    const creditCardInfo = document.getElementById('credit-card-info');

    creditCardRadio.addEventListener('change', function () {
        if (creditCardRadio.checked) {
            creditCardInfo.style.display = 'block'; // Show card info
        }
    });

    cashRadio.addEventListener('change', function () {
        creditCardInfo.style.display = 'none'; // Hide card info
    });
});
