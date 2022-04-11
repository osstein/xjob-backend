// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let sideMenu = document.getElementById("side-menu");
let toggleButton = document.getElementById("toggle-button");

toggleButton.addEventListener("click", () => {
  toggleSideMenu();
});
sideMenu.addEventListener("click", () => {
  toggleSideMenu();
});

const toggleSideMenu = () => {
  sideMenu.classList.toggle("open-side-menu");
  if (sideMenu.classList.contains("open-side-menu")) {
    toggleButton.style.backgroundColor = "#000000";
  } else {
    toggleButton.style.backgroundColor = "#FFEA00";
  }
};

window.onload = () => {
  toggleButton.style.backgroundColor = "#FFEA00";
};
