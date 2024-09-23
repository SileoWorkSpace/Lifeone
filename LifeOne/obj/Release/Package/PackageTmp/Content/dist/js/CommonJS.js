
function showSnazzySuccessMessage(text) {

    $("#CustomMessages").html('');
    $("#CustomMessages").html(text);
    $("#CustomMessages").removeClass("alert alert-danger");
    $("#CustomMessages").addClass("alert alert-success");
    //Fade message in
    $("#CustomMessages").show('slow');
    //Fade message out in 5 seconds
    setTimeout('$("#CustomMessages").hide("slow")', 5000);
}



function showSnazzyErrorMessage(text) {

    $("#CustomMessages").html('');
    $("#CustomMessages").html(text);
    $("#CustomMessages").removeClass("alert alert-success")
  
    $("#CustomMessages").addClass("alert alert-danger");
    //Fade message in
    $("#CustomMessages").show('slow');
    //Fade message out in 5 seconds
    setTimeout('$("#CustomMessages").hide("slow")', 5000);
}

$(".only-number").keypress(function () {
    var t = $(this);
    onlyNos(e, t);
});
function onlyNos(e, t) {
    try {
        if (window.event) {
            var charCode = window.event.keyCode;
        }
        else if (e) {
            var charCode = e.which;
        }
        else { return true; }
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    catch (err) {
        alert(err.Description);
    }

}
