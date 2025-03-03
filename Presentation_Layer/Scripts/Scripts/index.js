﻿$(document).ready(function () {
    // Agrega la animación de temblor
    var shakeAnimation = `
            @keyframes shake {
                0% { transform: translate(1px, 1px) rotate(0deg); }
                10% { transform: translate(-1px, -2px) rotate(-1deg); }
                20% { transform: translate(-3px, 0px) rotate(1deg); }
                30% { transform: translate(3px, 2px) rotate(0deg); }
                40% { transform: translate(1px, -1px) rotate(1deg); }
                50% { transform: translate(-1px, 2px) rotate(-1deg); }
                60% { transform: translate(-3px, 1px) rotate(0deg); }
                70% { transform: translate(3px, 1px) rotate(-1deg); }
                80% { transform: translate(-1px, -1px) rotate(1deg); }
                90% { transform: translate(1px, 2px) rotate(0deg); }
                100% { transform: translate(1px, -2px) rotate(-1deg); }
            }

            .shake {
                animation: shake 0.5s;
                animation-iteration-count: infinite;
            }
        `;

    // Agrega la animación al estilo del documento
    var styleElement = document.createElement('style');
    styleElement.innerHTML = shakeAnimation;
    document.head.appendChild(styleElement);

    $('.icon1').click(function () {
        $(this).addClass('shake');
        setTimeout(function () {
            window.location.href = 'https://www.google.com';
        }, 1000);
    });
});
