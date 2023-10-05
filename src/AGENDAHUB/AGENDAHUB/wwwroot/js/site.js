const openMenuBtn = document.getElementById("openMenu");
const closeMenuBtn = document.getElementById("closeMenu");
const myOffcanvas = new bootstrap.Offcanvas(document.getElementById('myOffcanvas'));

openMenuBtn.addEventListener("click", () => {
    myOffcanvas.show();
});

closeMenuBtn.addEventListener("click", () => {
    myOffcanvas.hide();
});