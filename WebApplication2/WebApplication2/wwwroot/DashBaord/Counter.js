
    document.addEventListener("DOMContentLoaded", () => {
        const counters = document.querySelectorAll(".stat-info p span");

    const animationSpeed = 200;  // The higher the value, the slower the animation

        counters.forEach(counter => {
            const target = +counter.textContent.trim(); // Convert to number

            if (!isNaN(target) && target > 0) {  // Ensure target is a number and greater than 0
                const updateCounter = () => {
                    const current = +counter.getAttribute("data-count") || 0;  // Get current count
    const increment = Math.ceil(target / animationSpeed);  // Calculate increment

    if (current < target) {
                        const newValue = current + increment > target ? target : current + increment;
    counter.textContent = newValue;  // Update the displayed count
    counter.setAttribute("data-count", newValue);  // Store current value
    setTimeout(updateCounter, 10);  // Repeat the animation every 10ms
                    } else {
        counter.textContent = target;  // Ensure it stops at the target
                    }
                };

    counter.textContent = 0;  // Initialize the counter to 0
    updateCounter();  // Start the counter animation
            } else {
        console.warn(`Invalid target value: "${counter.textContent.trim()}" in counter.`);
            }
        });
    });
    console.log(target);  // Check if the target value is valid

