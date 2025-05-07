// weather.js - Muss im wwwroot-Verzeichnis liegen
// WICHTIG: Datei als "weather.js" im wwwroot-Verzeichnis speichern

console.log("✅ Wetter-Script wird geladen...");

// Namespace-Objekt für Wetter-Funktionen
window.WETTER = {
    chart: null,

    // Funktion zum Zeichnen des Wetter-Charts
    drawWeatherChart: function (labels, data) {
        console.log("WETTER.drawWeatherChart wird ausgeführt", labels, data);

        const canvas = document.getElementById('weatherChart');
        if (!canvas) {
            console.error("Canvas-Element nicht gefunden!");
            return false;
        }

        // Warten auf vollständiges DOM-Rendering
        setTimeout(() => {
            try {
                const ctx = canvas.getContext('2d');

                // Bestehenden Chart entfernen, wenn vorhanden
                if (window.WETTER.chart) {
                    window.WETTER.chart.destroy();
                }

                // Farbgradient für die Füllung definieren
                const gradient = ctx.createLinearGradient(0, 0, 0, canvas.height);
                gradient.addColorStop(0, 'rgba(66, 165, 245, 0.7)');   // Hellblau
                gradient.addColorStop(1, 'rgba(66, 165, 245, 0.1)');   // Durchsichtig

                // Chart erstellen
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
                        plugins: {
                            legend: {
                                display: true,
                                labels: {
                                    font: {
                                        family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
                                        size: 14
                                    }
                                }
                            },
                            tooltip: {
                                enabled: true,
                                backgroundColor: 'rgba(37, 64, 97, 0.9)',
                                titleFont: {
                                    family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
                                    size: 14
                                },
                                bodyFont: {
                                    family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
                                    size: 13
                                },
                                padding: 12,
                                displayColors: false,
                                callbacks: {
                                    title: function (tooltipItems) {
                                        return tooltipItems[0].label + ' Uhr';
                                    },
                                    label: function (context) {
                                        return context.raw + '°C';
                                    }
                                }
                            }
                        },
                        scales: {
                            x: {
                                title: {
                                    display: true,
                                    text: 'Uhrzeit',
                                    font: {
                                        family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
                                        size: 14,
                                        weight: 'bold'
                                    }
                                },
                                grid: {
                                    color: 'rgba(200, 200, 200, 0.3)'
                                },
                                ticks: {
                                    font: {
                                        family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
                                        size: 12
                                    }
                                }
                            },
                            y: {
                                title: {
                                    display: true,
                                    text: 'Temperatur (°C)',
                                    font: {
                                        family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
                                        size: 14,
                                        weight: 'bold'
                                    }
                                },
                                grid: {
                                    color: 'rgba(200, 200, 200, 0.3)'
                                },
                                ticks: {
                                    font: {
                                        family: "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif",
                                        size: 12
                                    },
                                    callback: function (value) {
                                        return value + '°C';
                                    }
                                }
                            }
                        },
                        animation: {
                            duration: 1500,
                            easing: 'easeOutQuart'
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