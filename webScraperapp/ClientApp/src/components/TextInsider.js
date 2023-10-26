import React, { useState, useMemo, useEffect } from 'react';
import PropTypes from 'prop-types';

import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);

const options = {

    responsive: true,
    plugins: {
        legend: {
            position: 'top',
        },
        title: {
            display: true,
            text: 'Top 10 most occured words',
        },
    },
}

const TextInsider = ({ barData }) => {
    const [chartData, setChartData] = useState(barData);

    useEffect(() => {
        setChartData(barData);
    }, [barData]);

    return (
        <div>
            <Bar options={options} data={chartData} />
        </div>
    );
};

TextInsider.propTypes = {
    barData: PropTypes.object.isRequired,
};

export default TextInsider;


