$(function () {
    // Initialize numeric spinner input boxes
    //$(".numeric-spinner").spinedit();
    // Initialize modal dialog
    // attach modal-container bootstrap attributes to links with .modal-link class.
    // when a link is clicked with these attributes, bootstrap will display the href content in a modal dialog.
    $("#srefCode").val($("input#acd").val());
    $("#srefCode_2").val($("input#sacode").val());
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

function OpenAccSearch(obj, cflg, tp = "") {
    csobj = obj;
    csflag = cflg;
    ShowAccSearch(obj, tp);
}

function ShowAccSearch(obj, tp) {
    if (tp == "") {
        var url = "../../Search/SearchAC"; /*$("#aspath").val();*/
        $('#genreDialog .modal-content .modal-body').html('').load(url, function () {
            $('#genreDialog').modal();
            return false;
        });
    }
    else {
        var url = "../../Search/SearchAC"; //$("#atspath").val();
        $('#genreDialog .modal-content .modal-body').html('').load(url + "?brcd=&actpe=" + tp, function () {
            $('#genreDialog').modal();
            return false;
        });
    }
}
