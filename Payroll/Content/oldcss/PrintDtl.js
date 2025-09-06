$(function () {
    $('#PDtlDialog').dialog({
        autoOpen: false,
        width: 900,
        height: 625,
        modal: true,
        title: 'Enquiry Form Report',
        buttons: {
            'Ok': function () {
                $(this).dialog('close');
            }

        },
        open: function () {
            $('#PDtlDialog').keypress(function (e) {
                if (e.keyCode == $.ui.keyCode.ENTER) {
                    $(this).parent().find("button:eq(0)").trigger("click");
                }
            });
        }

    });

    //$('#fdtl').click(function () {
    //    var createFormUrl = $(this).attr('href');
    //    $('#PDtlDialog').html('')
    //    .load(createFormUrl, function () {
    //        jQuery.validator.unobtrusive.parse('#createGenreForm');
    //        $('#PDtlDialog').dialog('open');

    //    });

    //    return false;
    //});

    $('.fdtl').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#PDtlDialog').html('')
        .load(createFormUrl, function () {
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            $('#PDtlDialog').dialog('open');
        });
        return false;
    });

});