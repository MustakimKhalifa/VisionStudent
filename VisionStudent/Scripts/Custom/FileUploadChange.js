$(document).ready(function () {
    $("#File").change(function (event) {

        console.log("change")
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (event) {
                $("#imgProfile").prop('src', event.target.result)
                event.target.result = '';
            };
            reader.readAsDataURL(file);
        } else {
            $("#imgProfile").prop('src', '/Upload/no-profile.png');
        }
    });

    //$(document).on("click", "#imgProfile", function () {
    //    $("#File").trigger("click");
    //});
});