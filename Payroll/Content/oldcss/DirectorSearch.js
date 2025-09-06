$(function () {
    $('#dirDialog').dialog({
        autoOpen: false,
        width: 450,
        height: 300,
        modal: true,
        title: 'Director Search',
        buttons: {
            'Ok': function () {
                setDir();
                $(this).dialog('close');

            },
            'Cancel': function () {
                $(this).dialog('close');
            }
        },
        open: function () {
            $('#dirDialog').keypress(function (e) {
                if (e.keyCode == $.ui.keyCode.ENTER) {
                    $(this).parent().find("button:eq(0)").trigger("click");
                }
            });
        }

    });

    $('#dirsearch').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#dirDialog').html('')
        .load(createFormUrl, function () {
            // The createGenreForm is loaded on the fly using jQuery load. 
            // In order to have client validation working it is necessary to tell the 
            // jQuery.validator to parse the newly added content
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            $('#dirDialog').dialog('open');

        });

        return false;
    });

    $('.dirsearch').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#dirDialog').html('')
        .load(createFormUrl, function () {
            // The createGenreForm is loaded on the fly using jQuery load. 
            // In order to have client validation working it is necessary to tell the 
            // jQuery.validator to parse the newly added content
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            $('#dirDialog').dialog('open');

        });

        return false;
    });

});