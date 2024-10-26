let cartItems = JSON.parse(localStorage.getItem('cart')) || [];

// DOM elements
const cartContainer = document.getElementById('cart-items').getElementsByTagName('tbody')[0];
const totalPriceElement = document.getElementById('total-price-value');
const clearCartBtn = document.getElementById('clear-cart');
const checkoutButton = document.getElementById('checkout-btn');
const checkoutSection = document.getElementById('checkout-section');
const checkoutForm = document.getElementById('checkout-form');

function displayCart() {
    cartContainer.innerHTML = '';
    let totalPrice = 0;

    if (cartItems.length === 0) {
        cartContainer.innerHTML = "<tr><td colspan='6'>Your cart is empty.</td></tr>";
        totalPriceElement.textContent = '0.00';
        return;
    }

    cartItems.forEach(item => {
        // Ensure quantity is initialized correctly
        item.quantity = item.quantity || 1;

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

// Event listener for quantity change
cartContainer.addEventListener('input', (event) => {
    if (event.target.classList.contains('quantity-input')) {
        const itemId = event.target.getAttribute('data-id');
        const newQuantity = parseInt(event.target.value, 10);

        if (!isNaN(newQuantity) && newQuantity > 0) {
            const cartItem = cartItems.find(item => item.id === itemId);
            if (cartItem) {
                cartItem.quantity = newQuantity;
                localStorage.setItem('cart', JSON.stringify(cartItems));
                displayCart();
            }
        } else {
            event.target.value = 1; // If invalid quantity, reset to 1
        }
    }
});

// Event listener for remove button
cartContainer.addEventListener('click', (event) => {
    if (event.target.classList.contains('remove-btn')) {
        const itemId = event.target.getAttribute('data-id');
        cartItems = cartItems.filter(item => item.id !== itemId);
        localStorage.setItem('cart', JSON.stringify(cartItems));
        displayCart();
    }
});

// Clear cart button
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

// Checkout button event
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

// Checkout form submission
checkoutForm.addEventListener('submit', (event) => {
    event.preventDefault();

    const formValid = checkoutForm.checkValidity();
    if (!formValid) {
        Swal.fire('Error', 'Please fill all the fields correctly.', 'error');
        return;
    }

    const formData = new FormData(checkoutForm);

    // Append cart data to form
    cartItems.forEach((item, index) => {
        formData.append(`items[${index}][id]`, item.id);
        formData.append(`items[${index}][name]`, item.name);
        formData.append(`items[${index}][price]`, item.price);
        formData.append(`items[${index}][quantity]`, item.quantity);
    });

    formData.append('totalAmount', totalPriceElement.textContent);

    // Simulate form submission
    fetch('/Cart/SubmitOrder', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            Swal.fire('Order placed!', 'Thank you for your order.', 'success');
        })
        .catch(error => {
            console.error('Error:', error);
            Swal.fire('Order placed!', 'Thank you for your order.', 'success');
        })
        .finally(() => {
            localStorage.removeItem('cart');
            cartItems = [];
            displayCart();
            totalPriceElement.textContent = '0.00';
            checkoutSection.style.display = 'none';
            checkoutForm.reset();
        });
});

// Initial call to display the cart
displayCart();
