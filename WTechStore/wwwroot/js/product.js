// Initialize current slide index
let currentSlide = 0; 
const slides = document.querySelectorAll('.slide'); // Select all slides

// Function to change slides
function changeSlide(direction) {
    // Hide the current slide
    slides[currentSlide].classList.remove('active');
    
    // Update current slide index based on the direction
    currentSlide += direction;

    // Check for boundaries (wrap-around effect)
    if (currentSlide < 0) {
        currentSlide = slides.length - 1; // Go to last slide if negative
    } else if (currentSlide >= slides.length) {
        currentSlide = 0; // Go back to first slide
    }

    // Show the new current slide
    slides[currentSlide].classList.add('active');
}

// Example product data (with multiple images)
const products = [
    {
        id: '1',
        name: 'PlayStation 5',
        price: 200,
        images: [
            './images/hero-image.webp',
            './images/6566039_bd.webp',
            './images/6566039ld.webp'
        ],
        description: 'The PlayStation 5 is powered by a custom system on a chip (SoC) designed in tandem by AMD and Sony, integrating a custom 7 nm AMD Zen 2 CPU with eight cores running at a variable frequency capped at 3.5 GHz. Zen 2 is a 64-bit x86-64 instruction set CPU microarchitecture.'
    },
    {
        id: '2',
        name: 'Nintendo Switch',
        price: 299,
        images: [
            './images/Nintendo-Switch-wJoyCons-BlRd-Standing-FL.jpg',
            './images/6522225cv12d.webp',
            './images/6522225cv11d.webp'
        ],
        description: 'Hardware. The Nintendo Switch is a hybrid video game console, consisting of a console unit, a dock, and two Joy-Con controllers. Although it is a hybrid console, Nintendo classifies it as "a home console that you can take with you on the go".'
    },
    {
        id: '3',
        name: 'PS5 controller',
        price: 49,
        images: [
            './images/dualsense-controller-product-thumbnail-01-en-14sep21.webp',
            './images/6430163cv11d.webp',
            './images/6430163cv13d.webp'
        ],
        description: 'The Nintendo Switch is a versatile gaming console that can be played at home or on the go.'
    },
    {
        id: '4',
        name: 'MCC M22-24 - MIDAS Gaming PC Build',
        price: 899,
        images: [
            './images/Hy54z87tSf4KxtWLbM2s1kymlPxyfFiURxnCLc6s.webp',
            './images/Hy54z87tSf4KxtWLbM2s1kymlPxyfFiURxnCLc6s.webp',
            './images/Hy54z87tSf4KxtWLbM2s1kymlPxyfFiURxnCLc6s.webp'
        ],
        description: 'MCC M22-24 - MIDAS Gaming PC Build || AMD Ryzen 5 7600X 6-core - GIGABYTE RTX 4060 EAGLE OC ICE 8GB Graphics Card - 16GB RGB DDR5 - 500GB SSD M.2'
    },
    {
        id: '5',
        name: 'MCC E7-24 - MIDAS Gaming PC Build',
        price: 449,
        images: [
            './images/MCC-E7-24-MIDAS-Gaming-PC-Build.jpg',
            './images/MCC-E7-24-MIDAS-Gaming-PC-Build.jpg',
            './images/MCC-E7-24-MIDAS-Gaming-PC-Build.jpg'
        ],
        description: 'MCC M22-24 - MIDAS Gaming PC Build || AMD Ryzen 5 7600X 6-core - GIGABYTE RTX 4060 EAGLE OC ICE 8GB Graphics Card - 16GB RGB DDR5 - 500GB SSD M.2'
    },
    {
        id: '6',
        name: 'FANTECH MAXFIT67 MK858 MODULAR BLUETOOTH',
        price: 49,
        images: [
            './images/FANTECH-MAXFIT67-MK858-SPACE-EDITION-MODULAR-RGB-MECHANICAL-KEYBOARD-WITH-3-CONNECTION-MODES-_1.jpg',
            './images/FANTECH-MAXFIT67-MK858-SPACE-EDITION-MODULAR-RGB-MECHANICAL-KEYBOARD-WITH-3-CONNECTION-MODES-.jpg',
            './images/FANTECH-MAXFIT67-MK858-SPACE-EDITION-MODULAR-RGB-MECHANICAL-KEYBOARD-WITH-3-CONNECTION-MODES-_2.jpg'
        ],
        description: 'FANTECH MAXFIT67 MK858 MODULAR BLUETOOTH, WIRELESS RGB MECHANICAL KEYBOARD'
    },
    {
        id: '7',
        name: 'ASUS ROG Strix G18 G814 Gaming Laptop - Intel Core i9-13980HX - RTX 4070 8GB - 18-inch QHD 2K WQXGA IPS 240Hz',
        price: 1599,
        images: [
            './images/ASUS-ROG-Strix-G18-G814-Gaming-Laptop.jpg',
            './images/ASUS-ROG-Strix-G18-G814-Gaming-Laptop-2.jpg',
            './images/ASUS-ROG-Strix-G18-G814-Gaming-Laptop-3.jpg'
        ],
        description: 'FANTECH MAXFIT67 MK858 MODULAR BLUETOOTH, WIRELESS RGB MECHANICAL KEYBOARD'
    },
    {
        id: '8',
        name: 'HP VICTUS 15 GAMING LAPTOP',
        price: 749,
        images: [
            './images/ASUS-ROG-Strix-G18-G814-Gaming-Laptop.jpg',
            './images/ASUS-ROG-Strix-G18-G814-Gaming-Laptop-2.jpg',
            './images/ASUS-ROG-Strix-G18-G814-Gaming-Laptop-3.jpg'
        ],
        description: 'HP VICTUS 15 GAMING LAPTOP - 13th Gen Intel Core i7-13700H - RTX 4050 6GB - 144Hz - BLUE'
    },
    {
        id: '10',
        name: 'Xbox Series S 512GB All-Digital Console - White',
        price: 299,
        images: [
            './images/4f2c63e0-b4f4-4e8b-b94d-d32d2a28753d.webp',
            './images/996e9a15-356d-437c-a056-aa24d70764d9.webp',
            './images/999735e4-3913-48b1-9f57-957185d162f6.webp'
        ],
        description: 'Microsoft - Xbox Series S 512GB All-Digital Console (Disc-Free Gaming) - White'
    },
    {
        id: '11',
        name: 'EA SPORTS FC 25 Standard Edition - PlayStation 5',
        price: 59,
        images: [
            './images/5102ee23-5973-47b7-82eb-ffb0408b55ec.webp',
            './images/0e13c6e1-ecaf-47f0-857f-bbfddf4f2735.webp',
            './images/d4f887ca-067a-435d-b73a-84c566d6439a.webp'
        ],
        description: 'Microsoft - Xbox Series S 512GB All-Digital Console (Disc-Free Gaming) - White'
    },
    {
        id: '12',
        name: 'FANTECH ATOM104 MK886V2 GAMING MECHANICAL KEYBOARD',
        price: 19,
        images: [
            './images/rNQNJIZgU46gZZedqpJPyXe3mAjCVl0iCb8zEUDn.webp',
            './images/wMxArdYOymfDRz8h4AMVzR9gmCGqvv07VnWlwYAa.webp',
            './images/iaM6yTStDvzqM54gp6ypmoFqStduz3BFrnEsyOW6.webp'
        ],
        description: 'FANTECH ATOM104 MK886V2 GAMING MECHANICAL KEYBOARD - SUMI EDITION BLACK'
    },
    {
        id: '13',
        name: 'LOGITECH PEBBLE 2 M350S WIRELESS + BLUETOOTH MOUSE - GRAPHITE',
        price: 19,
        images: [
            './images/LOGITECH-PEBBLE-2-M350S-WIRELESS-BLUETOOTH-MOUSE-GRAPHITE.jpg',
            './images/LOGITECH-PEBBLE-2-M350S-WIRELESS-BLUETOOTH-MOUSE-GRAPHITE-1.jpg',
            './images/LOGITECH-PEBBLE-2-M350S-WIRELESS-BLUETOOTH-MOUSE-GRAPHITE-1.jpg'
        ],
        description: 'LOGITECH PEBBLE 2 M350S WIRELESS + BLUETOOTH MOUSE - GRAPHITE'
    },
    {
        id: '9',
        name: 'Xbox Series X 1TB Console - Carbon Black',
        price: 499,
        images: [
            './images/2bc98260-0bc7-42ca-ac77-5fac686181d9.webp',
            './images/189ecf91-8b51-4069-82a2-6da1c793e7dc.webp',
            './images/Nintendo-Switch-wJoyCons-BlRd-Standing-FL3.jpg'
        ],
        description: 'The Xbox One X delivers a true 4K gaming experience with 2160p frame buffers and 6 Teraflops of graphical power. Experience richer, more luminous colors in games. With a higher contrast ratio, High Dynamic Range technology brings out the true visual depth of your games.'
    },
];

// Get the product ID from the URL
const productId = new URLSearchParams(window.location.search).get('id');

// Find the product by ID
const product = products.find(p => p.id === productId);

// Populate the product details if found
if (product) {
    document.getElementById('product-name').textContent = product.name;
    document.getElementById('product-description').textContent = product.description;
    document.getElementById('product-price').textContent = `$${product.price.toFixed(2)}`;
    
    // Populate the slider images
    document.getElementById('slide1').src = product.images[0];
    document.getElementById('slide2').src = product.images[1];
    document.getElementById('slide3').src = product.images[2];
}

// Add to Cart button functionality
const addToCartButton = document.getElementById('add-to-cart-btn');

addToCartButton.addEventListener('click', () => {
    const quantity = parseInt(document.getElementById('quantity').value);
    
    // Check if the quantity is valid
    if (isNaN(quantity) || quantity <= 0) {
        Swal.fire({
            title: "has been added to your cart!",
            text: `${product.name}`,
            icon: "info"
            });
        return;
    }

    const productData = { id: product.id, name: product.name, price: product.price, quantity, image: product.images[0] };
    
    // Get the current cart from localStorage or initialize an empty array
    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    const existingItemIndex = cart.findIndex(item => item.id === productData.id);

    if (existingItemIndex !== -1) {
        // Update quantity if the item already exists
        cart[existingItemIndex].quantity += quantity; 
    } else {
        // Add new product to the cart
        cart.push(productData); 
    }

    // Save the updated cart to localStorage
    localStorage.setItem('cart', JSON.stringify(cart));
    
    // Update the cart count in the UI
    updateCartCount(cart.length);

    // Confirmation message
    Swal.fire({
        title: `${product.name  } has been added to your cart!`,
        text: `Quantity: is : ${quantity  }`,
        icon: "success"
    });
});

// Function to update cart count in the UI
function updateCartCount(count) {
    const cartCountElement = document.getElementById('cart-count');
    cartCountElement.textContent = count;
}

// Initialize cart count on page load
document.addEventListener('DOMContentLoaded', () => {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    updateCartCount(cart.length);
});
