
$(function () {
    // Initialize numeric spinner input boxes
    //$(".numeric-spinner").spinedit();
    // Initialize modal dialog
    // attach modal-container bootstrap attributes to links with .modal-link class.
    // when a link is clicked with these attributes, bootstrap will display the href content in a modal dialog.
    $('body').on('click', '.modal-link', function (e) {
        e.preventDefault();
        $(this).attr('data-target', '#modal-container');
        $(this).attr('data-toggle', 'modal');
    });
    // Attach listener to .modal-close-btn's so that when the button is pressed the modal dialog disappears
    $('body').on('click', '.modal-close-btn', function () {
        $('#modal-container').modal('hide');
        $('body').css('padding-right', '0');
    });
    //clear modal cache, so that new content can be loaded
    $('#modal-container').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal');
        $('body').css('padding-right', '0');
    });
    $('#CancelModal').on('click', function () {
        return false;
    });
});

function OpenEmpSearch() {
    var url = $("#espath").val();
    $('#empDialog .modal-content .modal-body').html('').load(url, function () {
        $('#empDialog').modal();
        return false;
    });
}

//$(function () {
//    $('#empDialog').dialog({
//        autoOpen: false,
//        width: 450,
//        height: 300,
//        modal: true,
//        title: 'Employee Search',
//        buttons: {
//            'Ok': function () {
//                setEmp();
//                $(this).dialog('close');

//            },
//            'Cancel': function () {
//                $(this).dialog('close');
//            }
//        },
//        open: function () {
//            $('#empDialog').keypress(function (e) {
//                if (e.keyCode == $.ui.keyCode.ENTER) {
//                    $(this).parent().find("button:eq(0)").trigger("click");
//                }
//            });
//        }

//    });

//    $('#empsearch').click(function () {
//        var createFormUrl = $(this).attr('href');  
//        $('#empDialog').html('')
//        .load(createFormUrl, function () {  
//            // The createGenreForm is loaded on the fly using jQuery load. 
//            // In order to have client validation working it is necessary to tell the 
//            // jQuery.validator to parse the newly added content
//            jQuery.validator.unobtrusive.parse('#createGenreForm');
//            $('#empDialog').dialog('open');
            
//        });

//        return false;
//    });

//    $('.empsearch').click(function () {
//        var createFormUrl = $(this).attr('href');
//        $('#empDialog').html('')
//        .load(createFormUrl, function () {
//            // The createGenreForm is loaded on the fly using jQuery load. 
//            // In order to have client validation working it is necessary to tell the 
//            // jQuery.validator to parse the newly added content
//            jQuery.validator.unobtrusive.parse('#createGenreForm');
//            $('#empDialog').dialog('open');
            
//        });

//        return false;
//    });
//});