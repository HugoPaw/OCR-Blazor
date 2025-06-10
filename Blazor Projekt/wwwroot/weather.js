console.log("✅ Wetter-Script wird geladen...");

window.WETTER = {
    chart: null,

    drawWeatherChart: function (labels, data) {
        console.log("WETTER.drawWeatherChart wird ausgeführt", labels, data);

        const canvas = document.getElementById('weatherChart');
        if (!canvas) {
            console.error("Canvas-Element nicht gefunden!");
            return false;
        }

        setTimeout(() => {
            try {
                const ctx = canvas.getContext('2d');

                if (window.WETTER.chart) {
                    window.WETTER.chart.destroy();
                }

                const gradient = ctx.createLinearGradient(0, 0, 0, canvas.height);
                gradient.addColorStop(0, 'rgba(66, 165, 245, 0.7)');
                gradient.addColorStop(1, 'rgba(66, 165, 245, 0.1)');

                window.WETTER.chart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Temperatur (°C)',
                            data: data,
                            borderWidth: 3,
                            borderColor: 'rgba(30, 136, 229, 1)',
                            backgroundColor: gradient,
                            tension: 0.4,
                            fill: true,
                            pointRadius: 5,
                            pointBackgroundColor: 'white',
                            pointBorderColor: 'rgba(30, 136, 229, 1)',
                            pointBorderWidth: 2,
                            pointHoverRadius: 7,
                            pointHoverBackgroundColor: 'white',
                            pointHoverBorderColor: 'rgba(25, 118, 210, 1)',
                            pointHoverBorderWidth: 3
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        scales: {
                            x: {
                                title: {
                                    display: true,
                                    text: 'Uhrzeit'
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

                console.log("✅ Chart erfolgreich erstellt!");
            } catch (err) {
                console.error("❌ Fehler beim Erstellen des Charts:", err);
            }
        }, 500);

        return true;
    }
};

console.log("✅ Wetter-Script vollständig geladen. WETTER-Objekt verfügbar.");
