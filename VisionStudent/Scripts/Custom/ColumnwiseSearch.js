$(document).ready(function () {
    // Setup - add a text input to each footer cell
    //$('#tblStudent thead .colum-search').each(function () {
    //    var title = $(this).text();
    //    $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    //});

    $('#tblStudent thead tr')
        .clone(true)
        .addClass('filters')
        .appendTo('#tblStudent thead');
});