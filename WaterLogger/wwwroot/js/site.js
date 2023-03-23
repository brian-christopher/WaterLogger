function calculate() {
    const tbl = document.getElementById("records");
    const resultArea = document.getElementById("result");

    let result = 0;
    for (let i = 1; i < tbl.rows.length;  i++) {
        result = result + +tbl.rows[i].cells[1].innerHTML;
    } 
    resultArea.innerHTML = result;
} 