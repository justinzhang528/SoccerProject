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

