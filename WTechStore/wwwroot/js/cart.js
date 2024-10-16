// Retrieve cart items from localStorage or initialize an empty array
let cartItems = JSON.parse(localStorage.getItem('cart')) || [];

// Cache DOM elements
const cartContainer = document.getElementById('cart-items').getElementsByTagName('tbody')[0];
const clearCartBtn = document.getElementById('clear-cart');
const totalPriceElement = document.getElementById('total-price-value');
const checkoutButton = document.getElementById('checkout-btn');
const checkoutSection = document.getElementById('checkout-section');
const checkoutForm = document.getElementById('checkout-form');
const creditCardInfo = document.getElementById('credit-card-info');

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

// Form submit event to send order to database
checkoutForm.addEventListener('submit', (event) => {
    event.preventDefault();

    const formValid = checkoutForm.checkValidity();
    if (!formValid) {
        Swal.fire('Error', 'Please fill all the fields correctly.', 'error');
        return;
    }

    const formData = new FormData(checkoutForm);
    const orderData = {
        fullName: formData.get('FullName'),
        email: formData.get('Email'),
        phoneNumber: formData.get('PhoneNumber'),
        address: formData.get('Address'),
        paymentMethod: formData.get('PaymentMethod'),
        cartItems: cartItems,
        totalAmount: totalPriceElement.textContent
    };

    // Send the order data to the server
    fetch('/Cart/SubmitOrder', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        },
        body: JSON.stringify(orderData)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                Swal.fire('Order placed successfully!', 'Thank you for your order.', 'success');
                localStorage.removeItem('cart');
                cartItems = [];
                displayCart();
                totalPriceElement.textContent = '0.00';
                checkoutSection.style.display = 'none';
            } else {
                Swal.fire('Error', 'Something went wrong with your order. Please try again.', 'error');
            }
        })
        .catch(error => {
            Swal.fire('Error', 'Unable to submit order. Please try again.', 'error');
            console.error('Error:', error);
        });
});

// Initialize display cart
displayCart();
