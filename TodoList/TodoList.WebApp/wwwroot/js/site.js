// Pflichtfelder mit Stern markieren
document.querySelectorAll("[data-val-required]").forEach(element => {
    let label = element.parentElement.firstElementChild;

    // Fügt einen Stern hinzu zu Pflichtfeldern
    if (label.nodeName == "LABEL") {
        label.classList.add("required-label");
    }
})