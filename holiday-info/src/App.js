import React, { useState } from 'react';
import axios from 'axios';

const HolidayApp = () => {
    const [holidays, setHolidays] = useState([]);
    const [year, setYear] = useState('');
    const [countryCode, setCountryCode] = useState('');

    const fetchHolidays = async () => {
        try {
            const response = await axios.get('http://localhost:7071/api/MyHttpTrigger', {
                params: { year: 2025, countryCode: 'US' },
            });
    
            const uniqueHolidays = Array.from(
                new Map(response.data.map(holiday => [holiday.name, holiday])).values()
            );
    
            setHolidays(uniqueHolidays);
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
                    <li key={`${holiday.id}-${holiday.name}`}>
                        {holiday.name} - {holiday.holidayDate}
                    </li>
                ))}
            </ul>

        </div>
    );
};

export default HolidayApp;