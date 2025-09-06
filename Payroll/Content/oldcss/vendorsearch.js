$(function () {
    $('#vendorDialog').dialog({
        autoOpen: false,
        width: 450,
        height: 300,
        modal: true,
        title: 'Vendor Search',
        buttons: {
            'Ok': function () {
                setVendor();
                $(this).dialog('close');

            },
            'Cancel': function () {
                $(this).dialog('close');
            }
        },
        open: function () {
            $('#vendorDialog').keypress(function (e) {
                if (e.keyCode == $.ui.keyCode.ENTER) {
                    $(this).parent().find("button:eq(0)").trigger("click");
                }
            });
        }

    });

    $('#vendorsearch').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#vendorDialog').html('')
        .load(createFormUrl, function () {
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            $('#vendorDialog').dialog('open');

        });

        return false;
    });

    $('.vendorsearch').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#vendorDialog').html('')
        .load(createFormUrl, function () {
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            $('#vendorDialog').dialog('open');

        });

        return false;
    });

});