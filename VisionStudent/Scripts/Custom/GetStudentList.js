$(document).ready(function () {
    magnifierEffect()

    $(".alert").fadeTo(2000, 500).slideUp(500, function () {
        $(".alert").slideUp(500);
    });   

    $("span, .overlay").click(function () {
        $(".shows").fadeOut();
        $(".shows").css("display", "none");
        $(".zoomContainer").remove();
    });

    GetStudentData();  
   
    $("#cancel").click(function () {
        hide();
    });
});

function GotoAddStudent() {
    var url = window.location.origin + "/Student/CreateStudent";
    window.location.replace(url);
}

//show delete popup 
function Deletepop(StudentId) {
    $("#delete_message").slideDown();
    $("#delete").click(function () {
        DeleteStudent(StudentId);
    });
}

function hide() {
    $("#delete_message").slideUp();
}

function magnifierEffect() {
    $("#imgPopup").elevateZoom({
        easing: true
    });
}

function popupImage(event) {
    if (event) {     
        var srcval = event.target.src;
        $(".shows").fadeIn();
        $(".img-show img").attr("src", srcval);
        $(".img-show img").attr("data-zoom-image", srcval);
    }
    magnifierEffect()
    //ColumnWiseSearch()
}


function GetStudentData() {
    "use strict";

    var url = window.location.origin + "/Student/GetStudentListData";  
    $('#tblStudent').DataTable({
        orderCellsTop: true,
        fixedHeader: true,   
        initComplete: function () {
            var api = this.api();
            // For each column
            api
                .columns()
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();

                    if (title.toLowerCase() == "profile" || title.toLowerCase() == "action") {
                        $(cell).html('');
                    } else {
                    $(cell).html('<input type="text" placeholder="Search ' + title + '" />');
                    }

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('change', function (e) {
                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();
                        })
                        .on('keyup', function (e) {
                            e.stopPropagation();

                            $(this).trigger('change');
                            $(this)
                                .focus()[0]
                                .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },
        scrollY: 590,
        ajax: {
            url: url,
            type: 'POST',
            dataType: 'json',
            dataSrc: ''
        },
        columns: [
            {
                data: 'Photo',
                render: function (data, type) {
                    return '<img id="imgProfile" src="' + data + '" alt="Image" class="img" onclick="popupImage(event);">';
                }
            },
            { data:'StudentName'},
            { data:'Address'},
            { data:'Class'},
            { data:'Age'},
            {
                data: 'Hobbies',
                render: function (data, type) {
                    return data.replace(/,*$/, '');
                }
            },
            { data:'Gender'},
            { data:'CityName'},
            { data: 'PinCode' },
            {
                data:'Id',
                render: function (data, type) {
                    var EditUrl = window.location.origin +"/Student/GetStudentById?studentId=" + data;
                    var DeleteUrl = window.location.origin + "/Student/DeleteStudent?studentId=" + data;
                    return '<a href=' + EditUrl + ' class=""><i class="fa-solid fa-pen-to-square fa-xl"></i></a> <a onclick="Deletepop(' + data + ');"><i class="btndelete fa-sharp fa-solid fa-trash fa-xl"></i></a>';
                }
            }
        ]
    });

    $("#tblStudent_filter input").addClass("form-control");
    $("#tblStudent_filter input").attr('placeholder', 'Enter search text');
    $("#tblStudent_filter input").attr('style', 'width:140% !important;');
    $("#tblStudent_filter").attr('style', 'width:26% !important;');
    $("#tblStudent_length").addClass('pos-top');
}

function ColumnWiseSearch() {
    $('#tblStudent tfoot td').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });
}

function DeleteStudent(id) {
    var url = window.location.origin + "/Student/DeleteStudent?studentId=" + id;
    window.location.replace(url);
}
