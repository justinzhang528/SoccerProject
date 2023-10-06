function getDetailById(e) {
    detailTitle = e.parentElement.previousElementSibling.previousElementSibling.previousElementSibling.innerText
    teams = detailTitle.split('vs')
    $.ajax({
        url: '/detail?id=' + e.id,
        type: 'GET',
        success: function (data) {
            $('#detail_title').text(detailTitle);
            $('#team_H').text(teams[0]);
            $('#team_A').text(teams[1]);
            $('#firstHalf_H').text(data.firstHalf_H);
            $('#firstHalf_A').text(data.firstHalf_A);
            $('#secondHalf_H').text(data.secondHalf_H);
            $('#secondHalf_A').text(data.secondHalf_A);
            $('#firstHalf_A').text(data.firstHalf_A);
            $('#firstHalf_A').text(data.firstHalf_A);
            $('#regularTime_H').text(data.regularTime_H);
            $('#regularTime_A').text(data.regularTime_A);
            $('#corners_H').text(data.corners_H);
            $('#corners_A').text(data.corners_A);
            $('#penalties_H').text(data.penalties_H);
            $('#penalties_A').text(data.penalties_A);
            $('#yellowCards_H').text(data.yellowCards_H);
            $('#yellowCards_A').text(data.yellowCards_A);
            $('#redCards_H').text(data.redCards_H);
            $('#redCards_A').text(data.redCards_A);
            $('#firstET_H').text(data.firstET_H);
            $('#firstET_A').text(data.firstET_A);
            $('#secondET_H').text(data.secondET_H);
            $('#secondET_A').text(data.secondET_A);
            $('#penaltiesShootout_H').text(data.penaltiesShootout_H);
            $('#penaltiesShootout_A').text(data.penaltiesShootout_A);
        }
    });
}

