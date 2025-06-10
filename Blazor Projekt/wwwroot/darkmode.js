console.log("✅ darkmode.js wurde geladen");

window.toggleDarkMode = function (isDark) {
    const main = document.getElementById("main-content");
    if (!main) return;

    if (isDark) {
        main.classList.add("dark-mode");
    } else {
        main.classList.remove("dark-mode");
    }
};
