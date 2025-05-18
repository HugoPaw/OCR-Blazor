let rotationX = 0;
let rotationY = 0;

window.rotateBall = function (dir) {
    if (dir === "up") rotationX -= 15;
    if (dir === "down") rotationX += 15;
    if (dir === "left") rotationY -= 15;
    if (dir === "right") rotationY += 15;

    const ball = document.getElementById("ball3d");
    if (ball) {
        ball.style.transform = `rotateX(${rotationX}deg) rotateY(${rotationY}deg)`;
    }
}
