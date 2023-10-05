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
    detailTrId = "#detail_" + e.id;
    if (e.innerText === 'Show Details') {
        e.innerText = 'Hide Details'
        $(detailTrId).css('display','block')
    } else {
        e.innerText = 'Show Details'
        $(detailTrId).css('display', 'none')

    }
}

