let numbers = [];

function addNumber() {
    let number = document.getElementById("number").value;
    numbers.push(parseInt(number));
    document.getElementById("numbers").innerHTML = numbers;
    document.getElementById("number").value = '';
    document.getElementById("calculate").disabled = numbers.length < 5;
}

function calculate() {
    let max = numbers.reduce(function (a, b) {
        return Math.max(a, b);
    });
    document.getElementById("valoresTotais").innerHTML = max;
}