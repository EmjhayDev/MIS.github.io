document.addEventListener('DOMContentLoaded', function () {
    const apiKey = '1df3461e87c0b27062b5740b341fa09a'; // Your OpenWeatherMap API key

    // Function to fetch weather data based on coordinates
    function fetchWeather(latitude, longitude) {
        fetch(`https://api.openweathermap.org/data/2.5/weather?lat=${latitude}&lon=${longitude}&appid=${apiKey}&units=metric`)
            .then(response => response.json())
            .then(data => {
                if (data.cod === 200) {
                    // Update the weather information on the page
                    document.getElementById('weather-desc').textContent = data.weather[0].description;
                    document.getElementById('weather-temp-value').textContent = data.main.temp;
                    document.getElementById('weather-city-name').textContent = data.name;
                } else {
                    console.error('Error fetching weather data:', data.message);
                }
            })
            .catch(error => console.error('Error:', error));
    }

    // Check if the browser supports Geolocation
    if (navigator.geolocation) {
        // Get the user's current position
        navigator.geolocation.getCurrentPosition(
            function (position) {
                const latitude = position.coords.latitude;
                const longitude = position.coords.longitude;
                fetchWeather(latitude, longitude);
            },
            function (error) {
                console.error('Error getting location:', error);
            }
        );
    } else {
        console.error('Geolocation is not supported by this browser.');
    }
});