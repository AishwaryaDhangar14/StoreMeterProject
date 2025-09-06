$(function () {
    $('#BDtlDialog').dialog({
        autoOpen: false,
        width: 450,
        height: 400,
        modal: true,
        title: 'Book Details',
        buttons: {
            'Ok': function () {
                $(this).dialog('close');
            }
        
        },
        open: function () {
            $('#BDtlDialog').keypress(function (e) {
                if (e.keyCode == $.ui.keyCode.ENTER) {
                    $(this).parent().find("button:eq(0)").trigger("click");
                }
            });
        }

    });

    //$('#bkdtl').click(function () {
    //    var createFormUrl = $(this).attr('href');
    //    $('#BDtlDialog').html('')
    //    .load(createFormUrl, function () {
    //        jQuery.validator.unobtrusive.parse('#createGenreForm');
    //        $('#BDtlDialog').dialog('open');

    //    });

    //    return false;
    //});

    $('.bkdtl').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#BDtlDialog').html('')
        .load(createFormUrl, function () {
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            $('#BDtlDialog').dialog('open');

        });

        return false;
    });

});