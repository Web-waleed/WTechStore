// Retrieve wishlist items from localStorage or initialize an empty array
const wishlistItems = JSON.parse(localStorage.getItem('wishlist')) || [];
const wishlistContainer = document.getElementById('wishlist-items').querySelector('tbody');

// Function to display wishlist items with "Remove" and "Add to Cart" buttons
function displayWishlist() {
    wishlistContainer.innerHTML = ''; // Clear the table before rendering

    wishlistItems.forEach(item => {
        const listItem = document.createElement('tr');
        listItem.innerHTML = `
      <td><img src="${item.image}" alt="${item.name}" class="wishlist-image" width="100"/></td>
      <td>${item.name}</td>
      <td>$${item.price}</td>
      <td>
        <button class="remove-btn btn btn-danger" data-id="${item.id}">Remove</button>
        <button class="add-to-cart-btn btn btn-success" data-id="${item.id}">Add to Cart</button>
      </td> <!-- Updated: Add both buttons inside Actions column -->
    `;
        wishlistContainer.appendChild(listItem);
    });

    // Attach event listeners to all remove buttons
    const removeButtons = document.querySelectorAll('.remove-btn');
    removeButtons.forEach(button => {
        button.addEventListener('click', removeFromWishlist);
    });

    // Attach event listeners to all "Add to Cart" buttons
    const addToCartButtons = document.querySelectorAll('.add-to-cart-btn');
    addToCartButtons.forEach(button => {
        button.addEventListener('click', addToCartFromWishlist);
    });
}

// Function to remove a product from the wishlist
function removeFromWishlist(event) {
    const productId = event.target.getAttribute('data-id');

    const index = wishlistItems.findIndex(item => item.id === productId);
    if (index !== -1) {
        wishlistItems.splice(index, 1);
        localStorage.setItem('wishlist', JSON.stringify(wishlistItems));
        event.target.parentElement.parentElement.remove();
    }
}

// Function to add a product to the cart from the wishlist
function addToCartFromWishlist(event) {
    const productId = event.target.getAttribute('data-id');

    const item = wishlistItems.find(item => item.id === productId);
    if (item) {
        let cartItems = JSON.parse(localStorage.getItem('cart')) || [];
        cartItems.push(item); // Add the product to the cart
        localStorage.setItem('cart', JSON.stringify(cartItems));

        // Optionally, remove the product from the wishlist after adding to cart
        removeFromWishlist(event);

        alert('Item added to cart!');
    }
}

// Call the function to display wishlist on page load
displayWishlist();
