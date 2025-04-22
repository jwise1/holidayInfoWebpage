import React, { useState } from 'react';
import axios from 'axios';

const HolidayApp = () => {
    const [holidays, setHolidays] = useState([]);
    const [year, setYear] = useState('');
    const [countryCode, setCountryCode] = useState('');

    const fetchHolidays = async (year, countryCode) => {
        setHolidays([]); // Clear previous holidays
        try {
            const response = await axios.get('http://localhost:7071/api/MyHttpTrigger', {
                params: { year, countryCode },
            });
    
            setHolidays(response.data);
        } catch (error) {
            console.error('Error fetching holidays:', error.message);
        }
    };

    return (
        <div>
            <h1>Holidays</h1>
            <input
                type="text"
                placeholder="Enter Year"
                value={year}
                onChange={(e) => setYear(e.target.value)}
            />
            <input
                type="text"
                placeholder="Enter Country Code"
                value={countryCode}
                onChange={(e) => setCountryCode(e.target.value)}
            />
            <button onClick={() => fetchHolidays(year, countryCode)}>Fetch Holidays</button>
            <ul>
                {holidays.map((holiday) => (
                    <li key={holiday.id}>
                        {holiday.name} - {new Date(holiday.date).toLocaleDateString()}
                    </li>
                ))}
            </ul>


        </div>
    );
};

export default HolidayApp;