// Retrieve wishlist items from localStorage or initialize an empty array
const wishlistItems = JSON.parse(localStorage.getItem('wishlist')) || [];
const wishlistContainer = document.getElementById('wishlist-items').querySelector('tbody');
const wishlistCount = document.getElementById('wishlist-count');

// Function to display wishlist items with images, names, descriptions, and remove buttons
function displayWishlist() {
  wishlistContainer.innerHTML = ''; // Clear the table before rendering

  wishlistItems.forEach(item => {
    const listItem = document.createElement('tr');
    listItem.innerHTML = `
      <td><img src="${item.image}" alt="${item.name}" class="wishlist-image" /></td>
      <td>${item.name}</td>
      <td>$${item.price}</td>
      <td><button class="remove-btn" data-id="${item.id}">Remove</button></td>
    `;
    wishlistContainer.appendChild(listItem);
  });

  // Attach event listeners to all remove buttons
  const removeButtons = document.querySelectorAll('.remove-btn');
  removeButtons.forEach(button => {
    button.addEventListener('click', removeFromWishlist);
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
    updateCounts();
  }
}

// Function to update the wishlist count
function updateCounts() {
  wishlistCount.textContent = wishlistItems.length;
}

// Call the function to display wishlist on page load
displayWishlist();
updateCounts();
