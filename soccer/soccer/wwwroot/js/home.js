//document.addEventListener('DOMContentLoaded', function () {
//    const toggleButtons = document.querySelectorAll('.toggle-detail');

//    toggleButtons.forEach(button => {
//        button.addEventListener('click', function () {
//            const detailTable = this.nextElementSibling;

//            if (detailTable.style.display === 'none' || detailTable.style.display === '') {
//                detailTable.style.display = 'block';
//                this.innerText = 'Hide Detail';
//            } else {
//                detailTable.style.display = 'none';
//                this.innerText = 'Show Detail';
//            }
//        });
//    });
//});

function detailEvent(e) {
    if (e.innerText == 'Show Details') {
        e.innerText == 'Hide Details'
    } else {
        e.innerText == 'Show Details'
    }
    if (detailTable.style.display === 'none' || detailTable.style.display === '') {
        detailTable.style.display = 'block';
        this.innerText = 'Hide Detail';
    } else {
        detailTable.style.display = 'none';
        this.innerText = 'Show Detail';
    }
}

