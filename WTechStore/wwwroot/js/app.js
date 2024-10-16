const switcherlist = document.querySelectorAll(".Switcher li");
const images = document.querySelectorAll(".product");

switcherlist.forEach((li) => {
    li.addEventListener("click", (e) => {
        switcherlist.forEach((item) => item.classList.remove("activen"));
        e.currentTarget.classList.add("activen");
        const { cat } = e.currentTarget.dataset;
        images.forEach((img) => (img.style.display = "none"));
        document.querySelectorAll(cat).forEach((el) => (el.style.display = "block"));
    });
});

const wishlistButtons = document.querySelectorAll('.wishlist-btn');
const cartButtons = document.querySelectorAll('.cart-btn');
const wishlistCount = document.getElementById('wishlist-count');
const cartCount = document.getElementById('cart-count');

function updateCounts() {
    const wishlist = JSON.parse(localStorage.getItem('wishlist')) || [];
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    wishlistCount.textContent = wishlist.length;
    cartCount.textContent = cart.length;
    console.log(`Wishlist Count: ${wishlist.length}, Cart Count: ${cart.length}`);
}

wishlistButtons.forEach(button => {
    button.addEventListener('click', () => {
        const product = button.parentElement;
        const productId = product.getAttribute('data-id');
        const productName = product.querySelector('h2').textContent;
        const productImage = product.querySelector('img').src;
        const productPriceText = product.querySelector('p').textContent;
        const productPrice = parseFloat(productPriceText.replace('Price: $', ''));
        const productData = { id: productId, name: productName, image: productImage, price: productPrice };

        let wishlist = JSON.parse(localStorage.getItem('wishlist')) || [];

        if (!wishlist.find(item => item.id === productId)) {
            wishlist.push(productData);
            localStorage.setItem('wishlist', JSON.stringify(wishlist));
            Swal.fire({
                title: " has been added to your wishlist!",
                text: `${productName}`,
                icon: "success"
            });
        } else {
            Swal.fire({
                title: "This product is already in your wishlist!",
                text: `${productName}`,
                icon: "info"
            });
        }

        updateCounts();
    });
});

cartButtons.forEach(button => {
    button.addEventListener('click', () => {
        const product = button.parentElement;
        const productId = product.getAttribute('data-id');
        const productName = product.querySelector('h2').textContent;
        const productPriceText = product.querySelector('p').textContent;
        const productPrice = parseFloat(productPriceText.replace('Price: $', ''));
        const productImage = product.querySelector('img').src;

        const productData = { id: productId, name: productName, price: productPrice, image: productImage };

        let cart = JSON.parse(localStorage.getItem('cart')) || [];

        if (!cart.find(item => item.id === productId)) {
            cart.push(productData);
            localStorage.setItem('cart', JSON.stringify(cart));
            Swal.fire({
                title: "has been added to your cart!",
                text: `${productName}`,
                icon: "success"
            });
        } else {
            Swal.fire({
                title: "This product is already in your cart!",
                text: `${productName}`,
                icon: "info"
            });
        }
        updateCounts();
    });
});

updateCounts();
