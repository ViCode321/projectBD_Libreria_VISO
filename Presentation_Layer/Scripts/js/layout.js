function showNavbar(toggleId, navId, bodyId, headerId) {
    const toggle = document.getElementById(toggleId),
        nav = document.getElementById(navId),
        bodypd = document.getElementById(bodyId),
        headerpd = document.getElementById(headerId);

    // Utiliza localStorage para recuperar el estado del menú
    const isMenuOpen = localStorage.getItem('isMenuOpen') === 'true';

    // Configura el estado inicial del menú en función de localStorage
    if (isMenuOpen) {
        nav.classList.add("show");
        toggle.classList.add("bx-x");
        bodypd.classList.add("body-pd");
        headerpd.classList.add("body-pd");
    }

    toggle.addEventListener("click", () => {
        // Cambia el estado del menú
        const isCurrentlyOpen = nav.classList.contains("show");
        if (isCurrentlyOpen) {
            localStorage.setItem('isMenuOpen', 'false');
        } else {
            localStorage.setItem('isMenuOpen', 'true');
        }

        // show navbar
        nav.classList.toggle("show");
        // change icon
        toggle.classList.toggle("bx-x");
        // add padding to body
        bodypd.classList.toggle("body-pd");
        // add padding to header
        headerpd.classList.toggle("body-pd");
    });
}

// Main function to initialize the script
function init() {
    showNavbar("header-toggle", "nav-bar", "body-pd", "header");
}

function setActiveLink() {
    const linkColor = document.querySelectorAll(".nav_link");
    const currentURL = window.location.pathname;

    linkColor.forEach((link) => {
        // Elimina la clase "active" de todos los enlaces
        link.classList.remove("active");

        // Compara la URL actual con el enlace y agrega la clase "active" si coincide
        if (link.getAttribute("href") === currentURL) {
            link.classList.add("active");
        }
    });
}
// Llama a la función para establecer el enlace activo cuando se carga la página
setActiveLink();
// Call the initialization function
init();