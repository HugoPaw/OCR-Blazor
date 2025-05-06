window.drawWeatherChart = (labels, data) => {
    console.log("[JS] drawWeatherChart aufgerufen", labels, data);

    const canvas = document.getElementById('weatherChart');
    if (!canvas) {
        console.error("Canvas-Element nicht gefunden!");
        return;
    }

    const ctx = canvas.getContext('2d');

    new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels.map(l =>
                new Date(l).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })
            ),
            datasets: [{
                label: 'Temperatur (°C)',
                data: data,
                borderColor: 'rgba(75, 192, 192, 1)',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                tension: 0.3,
                fill: true,
                pointRadius: 4,
                pointHoverRadius: 6
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: true
                },
                tooltip: {
                    enabled: true
                }
            },
            scales: {
                x: {
                    title: {
                        display: true,
                        text: 'Zeit'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: 'Temperatur (°C)'
                    }
                }
            }
        }
    });
};
