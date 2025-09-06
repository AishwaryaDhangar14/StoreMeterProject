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

function OpenSearch(obj, cflg) {
    csobj = obj;
    csflag = cflg;
    ShowSearch(obj);
}


function ShowSearch(obj) {
    var url = $("#cspath").val();
    $('#custDialog .modal-content .modal-body').html('').load(url, function () {
        $('#custDialog').modal();
        return false;
    });
}

function OpenEmpSearch() {
    var url = $("#espath").val();
    $('#empDialog .modal-content .modal-body').html('').load(url, function () {
        $('#empDialog').modal();
        return false;
    });
}

function OpenShareSearch() {
    var url = $("#sspath").val();
    $('#shareDialog .modal-content .modal-body').html('').load(url, function () {
        $('#shareDialog').modal();
        return false;
    });
};

function OpenShareSearch(obj, tp) {
    var url = $("#sspath").val();
    if (tp == "") {
        $('#shareDialog .modal-content .modal-body').html('').load(url, function () {
            $('#shareDialog').modal();
            return false;
        });
    } else {
        $('#shareDialog .modal-content .modal-body').html('').load(url + "?brcd=&actpe=" + tp, function () {
            $('#shareDialog').modal();
            return false;
        });
    }
}

function OpenLocSearch() {
    var url = $("#lspath").val();
    $('#locDialog .modal-content .modal-body').html('').load(url, function () {
        $('#locDialog').modal();
        return false;
    });
}