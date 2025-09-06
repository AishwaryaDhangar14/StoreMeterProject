$(function () {
    $('#genreDialog').dialog({
        autoOpen: false,
        width: 450,
        height: 300,
        modal: true,
        position: ['top',100],
        title: 'Account Search',
        buttons: {
            'Ok': function ()
            {
                setAC();
                $("#srefCode").val($("input#acd").val());
                $("#srefCode_2").val($("input#sacode").val());
                $(this).dialog('close');

            },
            'Cancel': function ()
            {
                $(this).dialog('close');
            }
        },
        open: function ()
        {
            $('#genreDialog').keypress(function (e) {
                if (e.keyCode == $.ui.keyCode.ENTER) {
                    $(this).parent().find("button:eq(0)").trigger("click");
                    
                }
            });
        }
    });

    $('#acsearch').click(function () {
        var createFormUrl = $(this).attr('href');  
        $('#genreDialog').html('')
        .load(createFormUrl, function () {  
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            $('#genreDialog').dialog('open');
            $('#headwise').hide();
        });

        return false;
    });

    $('.acntsearch').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#genreDialog').html('')
        .load(createFormUrl, function () {
            // The createGenreForm is loaded on the fly using jQuery load. 
            // In order to have client validation working it is necessary to tell the 
            // jQuery.validator to parse the newly added content
            jQuery.validator.unobtrusive.parse('#createGenreForm');
            //$('#headwise').hide();
            $('#genreDialog').dialog('open');
                       
        });

        return false;
    });
});	