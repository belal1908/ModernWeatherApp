
    if (typeof apiKey === 'undefined') {
    var apiKey = "d8cab8bea5e32d2b21acf2fb9419eebf";
}
    const weatherUrl = "https://api.openweathermap.org/data/2.5/weather?units=metric&q=";
    const forecastUrl = "https://api.openweathermap.org/data/2.5/forecast?units=metric&q=";
    
    const searchBox = document.querySelector(".search input");
    const searchButton = document.querySelector(".search button");
    const weatherIcon = document.querySelector(".weather-icon");
    const weatherCard = document.querySelector(".card");
    const forecastContainer = document.querySelector(".forecast-container");
    const forecastSection = document.querySelector(".forecast");
    const autoLocationBtn = document.querySelector(".auto-location");

    async function fetchWeather(city) {
        try {
            const response = await fetch(`${weatherUrl}${city}&appid=${apiKey}`);
            if (!response.ok) {
                throw new Error("Invalid City Name!");
            }
            const data = await response.json();
            updateWeatherUI(data);
            fetchForecast(city);
        } catch (error) {
            document.querySelector(".error").style.display = "block";
            document.querySelector(".weather").style.display = "none";
            forecastSection.style.display = "none";
        }
    }

    function updateWeatherUI(data) {
        document.querySelector(".city").innerHTML = data.name;
        document.querySelector(".temp").innerHTML = Math.round(data.main.temp) + "°c";
        document.querySelector(".humidity").innerHTML = data.main.humidity + "%";
        document.querySelector(".wind").innerHTML = data.wind.speed + " km/h";

        const weatherCondition = data.weather[0].main;
        const weatherIcons = {
            Clouds: "images/clouds.png",
            Clear: "images/clear.png",
            Rain: "images/rain.png",
            Drizzle: "images/drizzle.png",
            Mist: "images/mist.png",
        };

        weatherIcon.src = weatherIcons[weatherCondition] || "images/default.png";
        document.querySelector(".error").style.display = "none";
        document.querySelector(".weather").style.display = "block";
    }

    async function fetchForecast(city) {
        try {
            const response = await fetch(`${forecastUrl}${city}&appid=${apiKey}`);
            const data = await response.json();
            updateForecastUI(data);
        } catch (error) {
            console.error("Error fetching forecast:", error);
        }
    }

    function updateForecastUI(data) {
        forecastContainer.innerHTML = "";
        const dailyForecast = {};

        data.list.forEach(item => {
            const date = item.dt_txt.split(" ")[0];
            if (!dailyForecast[date]) {
                dailyForecast[date] = item;
            }
        });

        Object.values(dailyForecast).forEach(day => {
            const date = new Date(day.dt_txt).toLocaleDateString("en-US", { weekday: "short" });
            const icon = day.weather[0].main.toLowerCase();
            const temp = Math.round(day.main.temp) + "°c";

            forecastContainer.innerHTML += `
                <div class="forecast-item">
                    <p>${date}</p>
                    <img src="images/${icon}.png">
                    <p>${temp}</p>
                </div>`;
        });

        forecastSection.style.display = "block";
    }

    function getUserLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(async (position) => {
                const lat = position.coords.latitude;
                const lon = position.coords.longitude;
                const geoUrl = `https://api.openweathermap.org/data/2.5/weather?lat=${lat}&lon=${lon}&units=metric&appid=${apiKey}`;
                
                try {
                    const response = await fetch(geoUrl);
                    const data = await response.json();
                    fetchWeather(data.name);
                } catch (error) {
                    console.error("Error fetching user location weather:", error);
                }
            }, (error) => {
                alert("Location access denied. Please enter a city manually.");
            });
        } else {
            alert("Geolocation is not supported by your browser.");
        }
    }

    function resetApp() {
        searchBox.value = "";
        document.querySelector(".weather").style.display = "none";
        document.querySelector(".error").style.display = "none";
        forecastSection.style.display = "none";
    }

    searchButton.addEventListener("click", () => {
        if (searchBox.value.trim() !== "") {
            fetchWeather(searchBox.value);
        }
    });

    autoLocationBtn.addEventListener("click", () => {
        getUserLocation();
    });

    document.querySelectorAll(".city-btn").forEach(button => {
        button.addEventListener("click", () => fetchWeather(button.dataset.city));
    });

    document.addEventListener("click", (event) => {
        if (!weatherCard.contains(event.target)) {
            resetApp();
        }
    });

    function toggleMode() {
    document.querySelector(".weather-page").classList.toggle("dark-mode");

    let modeButton = document.querySelector(".mode-toggle");
    modeButton.innerHTML = `<img src="images/contrast.png" alt="Theme Toggle" width="24px">`;
}






