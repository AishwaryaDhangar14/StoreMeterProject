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

function OpenDistSearch(obj) {
    csobj = obj;
    ShowDistSearch(obj);
}
function ShowDistSearch(obj) {
    var url = $("#DistPath").val();
        $('#distDialog .modal-content .modal-body').html('').load(url, function () {
            $('#distDialog').modal();
            return false;
        });
}
