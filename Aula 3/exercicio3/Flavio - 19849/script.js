function setAttributes() {
    let element = document.getElementById("link");
    let id = element.getAttribute("id");
    let target = element.getAttribute("target");
    let href = element.getAttribute("href");
    let type = element.getAttribute("type");

    document.getElementById("id").textContent  = id;
    document.getElementById("target").textContent  = target;
    document.getElementById("href").textContent  = href;
    document.getElementById("type").textContent  = type;
}

function setColor() {
    let elements = document.getElementsByClassName("titulo");
    let leng = elements.length;
    for(let i=0;i<leng;i++){
        let element = elements[i];
        element.style.color = "red";
    }
}

function getValue() {
    let value = prompt("Novo valor da linha selecionada?:");
    if (value != null && value != "") {
        let element = document.getElementById("change");
        element.textContent = value;
        element.style.backgroundColor = "#99e599";
    }
}