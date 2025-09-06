$(function () {
    $('#PrintReceipt').dialog({
        autoOpen: false,
        width: 700,
        height: 600,
        modal: true,
        title: 'Print Receipt',
        buttons: {
            'Ok': function () {
                $(this).dialog('close');

            },
            'Cancel': function () {
                $(this).dialog('close');
            }
        },
        open: function () {
            $('#PrintReceipt').keypress(function (e) {
                if (e.keyCode == $.ui.keyCode.ENTER) {
                    $(this).parent().find("button:eq(0)").trigger("click");
                }
            });
        }

    });

    $('#prcpt').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#PrintReceipt').html('')
        .load(createFormUrl, function () {
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            $('#PrintReceipt').dialog('open');

        });

        return false;
    });

    $('.prcpt').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#PrintReceipt').html('')
        .load(createFormUrl, function () {
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            $('#PrintReceipt').dialog('open');

        });

        return false;
    });

});