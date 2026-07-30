function toggleMobileMenu() {
    const mobileMenu = document.getElementById("mobileMenu");
    const mobileNav = document.getElementById("mobileNav");
    const isOpen = !mobileNav?.classList.contains("-translate-x-full");

    if (isOpen) {
        closeMobileMenu();
    } else {
        mobileMenu?.classList.remove("hidden");
        mobileNav?.classList.remove("-translate-x-full");
        document.getElementById("menuOpenIcon")?.classList.add("hidden");
        document.getElementById("menuCloseIcon")?.classList.remove("hidden");
        document.body.classList.add("menu-open");
    }
}

function closeMobileMenu() {
    document.getElementById("mobileMenu")?.classList.add("hidden");
    document.getElementById("mobileNav")?.classList.add("-translate-x-full");
    document.getElementById("menuOpenIcon")?.classList.remove("hidden");
    document.getElementById("menuCloseIcon")?.classList.add("hidden");
    document.body.classList.remove("menu-open");
}

document.addEventListener("click", function (e) {
    const trigger = e.target.closest(".dropdown-trigger");
    document.querySelectorAll(".dropdown-menu").forEach(function (menu) {
        if (trigger && trigger.contains(menu)) {
            menu.classList.toggle("hidden");
        } else {
            menu.classList.add("hidden");
        }
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const gozlemci = new IntersectionObserver(
        function (girisler) {
            girisler.forEach(function (giris) {
                if (giris.isIntersecting) {
                    giris.target.classList.add("gorunur");
                    gozlemci.unobserve(giris.target);
                }
            });
        },
        { threshold: 0.15 }
    );

    document.querySelectorAll(".scroll-reveal").forEach(function (eleman) {
        gozlemci.observe(eleman);
    });
});
