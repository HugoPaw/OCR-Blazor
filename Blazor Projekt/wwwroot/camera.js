window.openCameraAndGetImage = async function () {
    return new Promise((resolve, reject) => {
        const input = document.createElement('input');
        input.type = 'file';
        input.accept = 'image/*';
        input.capture = 'environment'; // bevorzugt Rückkamera bei Mobilgeräten
        input.onchange = () => {
            const file = input.files[0];
            const reader = new FileReader();
            reader.onload = () => resolve(reader.result); // gibt base64 zurück
            reader.onerror = error => reject(error);
            reader.readAsDataURL(file);
        };
        input.click();
    });
};