$(document).ready(function () {
    var cid = "14681";
    $.ajax({
        url: '../chart.asmx/get_training_session_titles',
        dataType: "json",
        method: 'post',
        data: { cid: cid },
        success: function (data) {
            var sel = $('.manager');
            sel.empty();
            sel.append('<option value=-1> - Select Training Session - </option>');
            $(data).each(function (index, sess) {
                sel.append('<option value=' + sess.ID + '>' + sess.Name + '</option>');
            });
        },
        error: function (err) {
            alert(JSON.stringify(err));
        }
    });
});

$('.manager').change(function () {
    var session_id = $(this).val();
    if (session_id == -1) {
        //        $('#CEsubmodule').prop('disabled', true);
        return;
    } else {
        $.ajax({
            url: '../chart.asmx/get_training_analyis',
            method: 'post',
            dataType: 'json',
            data: { session_id: session_id },
            success: function (data) {
                training_option.series[0].data = [
               { value: data.training, name: 'Approved Training' },
               { value: data.attended, name: 'Training Attended' },
                { value: data.training, name: 'Training Assessment' },
                { value: data.learning, name: 'Learning Assessment' },
                { value: data.skills, name: 'Skill Assessment' }

            ];
                console.log(training_option);
                training.setOption(training_option);
                //alert('sucess');
                //                var aData = data.d;
                //                var aLabels = aData[0];
                //                var aDatasets1 = aData[1];
                //                var aDatasets2 = aData[2];
                //alert(data.d[0]);
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
});