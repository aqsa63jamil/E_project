
    document.addEventListener("DOMContentLoaded", () => {
        const currentTimeElement = document.getElementById("current-time");

        // Function to update time
        const updateTime = () => {
            const now = new Date();
    const options = {
        timeZone: 'Asia/Karachi',
    day: '2-digit',
    month: 'short',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit', // Include seconds for live time
    hour12: false
            };

    currentTimeElement.textContent = new Intl.DateTimeFormat('en-US', options).format(now);
        };

    // Update the time immediately when the page loads
    updateTime();

    // Update the time every second (1000 ms)
    setInterval(updateTime, 1000);
    });
