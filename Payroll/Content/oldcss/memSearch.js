$(function () {
    $('#memDialog').dialog({
        autoOpen: false,
        width: 450,
        height: 300,
        modal: true,
        title: 'Member Search',
        buttons: {
            'Ok': function () {
                setMem();
                $(this).dialog('close');

            },
            'Cancel': function () {
                $(this).dialog('close');
            }
        },
        open: function () {
            $('#memDialog').keypress(function (e) {
                if (e.keyCode == $.ui.keyCode.ENTER) {
                    $(this).parent().find("button:eq(0)").trigger("click");
                }
            });
        }
    });

    $('#memsearch').click(function () {
        var createFormUrl = $(this).attr('href');  
        $('#memDialog').html('')
        .load(createFormUrl, function () {  
            // The createGenreForm is loaded on the fly using jQuery load. 
            // In order to have client validation working it is necessary to tell the 
            // jQuery.validator to parse the newly added content
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            $('#memDialog').dialog('open');            
        });
        return false;
    });

    $('.memsearch').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#memDialog').html('')
        .load(createFormUrl, function () {
            // The createGenreForm is loaded on the fly using jQuery load. 
            // In order to have client validation working it is necessary to tell the 
            // jQuery.validator to parse the newly added content
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            $('#memDialog').dialog('open');            
        });
        return false;
    });
});