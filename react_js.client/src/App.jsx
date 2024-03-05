import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [forecasts, setForecasts] = useState();

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents = forecasts === null ? < div > forecasts </div> : <div></div>;

    return contents;
    
    async function populateWeatherData() {
        const response = await fetch('weather');
        const data = await response;
        setForecasts(data);
    }
}

export default App;