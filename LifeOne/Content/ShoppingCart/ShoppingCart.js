
$(document).ready(function () {

    GetCookiesValue();

});

function WriteCookie(Token) {
    Remove_ShoppingCookies();
    GLBcreateCookie('CookiesId', Token, 130);
    $("#GLB_CookiesId").val(Token);
}





function GetCookiesValue() {


    var GLB_CookiesId = GLB_readCookie('CookiesId');
    if (GLB_CookiesId != "" && GLB_CookiesId != undefined && GLB_CookiesId != NaN && GLB_CookiesId != null) {
        $("#GLB_CookiesId").val(GLB_CookiesId);
        fGetCartDetails(GLB_CookiesId);
    } else {
        GenerateToken();
    }

}

function GenerateToken() {

    $.ajax({

        url: '/Home/GenerateShoppingToken',
        data: {},
        dataType: 'json',
        type: 'POST',
        async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {
        
            if (data != "" && data != undefined && data != NaN && data != null) {
                WriteCookie(data);

            }

        },
        error: function (error) {


        }
    });

}


function fGetCartDetails(Token) {

    var obj = {};

    obj.Pk_ProductId = 0;
    obj.Quantity = 1;
    obj.OpCode = 5;
    obj.TokenNo = Token;



    $.ajax({

        url: '/Home/AddToCart/',
        /*data: JSON.stringify(Model.RoutingConditional),*/
        data: { Pk_ProductId: 0, Quantity: 0, OpCode: 5, TokenNo: Token },
        dataType: 'json',
        type: 'POST',
        async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {

            if (data != "" && data != undefined && data != NaN && data != null) {
                $(".glbTotalCartItems").text(data);

            }

        },
        error: function (error) {


        }
    });

}


function fAddToCart(ProductId) {
    debugger;
    var obj = {};
    if (ProductId == null || ProductId == undefined || ProductId == NaN || ProductId == "0") {

        alert("Something went wrong. Please refresh the page and try again.");
        return false;
    } else {
        $('#Pk_ProductId').val(ProductId);
        obj.Pk_ProductId = ProductId;
        obj.Quantity = 1;
        obj.OpCode = 1;
        obj.TokenNo = $("#GLB_CookiesId").val();      
    }
     
    $.ajax({       
        url: '/Home/AddToCart/',
        // data: JSON.stringify(obj),
        data: { Pk_ProductId: obj.Pk_ProductId, Quantity: obj.Quantity, OpCode: obj.OpCode, TokenNo: obj.TokenNo },
        dataType: 'json',
        type: 'POST',
        async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {
            console.log(data);
            if (data != "" && data != undefined && data != NaN && data != null) {
                if (data.Status == "0") {
                    window.location.href = "/Home/Login";
                }
                else if (data.Status = "1") {
                    $(".glbTotalCartItems").text(data);
                }
                
            }

        },
        error: function (error) {


        }
    });

}



function fAddMultiQutyToCart(ProductId) {
    debugger;
    var obj = {};
    var abc = $("#Quantity").val();

    if ($("#Quantity").val() == null || $("#Quantity").val() == "" || $("#Quantity").val() == undefined || $("#Quantity").val() == NaN || parseInt($("#Quantity").val()) <= 0) {
        alert("Please Add Quantity");
        return false;

    }


    if (ProductId == null || ProductId == undefined || ProductId == NaN || ProductId == "0") {

        alert("Something went wrong. Please refresh the page and try again.");
        return false;
    } else {
        obj.Pk_ProductId = ProductId;
        obj.Quantity = $("#Quantity").val();
        obj.OpCode = 1;
        obj.TokenNo = $("#GLB_CookiesId").val();
    }


    $.ajax({

        url: '/Home/AddToCart/',
        // data: JSON.stringify(obj),
        data: { Pk_ProductId: obj.Pk_ProductId, Quantity: obj.Quantity, OpCode: obj.OpCode, TokenNo: obj.TokenNo },
        dataType: 'json',
        type: 'POST',
        async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {
           
            if (data != "" && data != undefined && data != NaN && data != null) {

                $(".glbTotalCartItems").text(data);
            }

        },
        error: function (error) {


        }
    });

}

function fAddMultiQutyToCart12(ProductId, Quantity) {
    debugger;
    var obj = {};
    //var abc = $("#Quantity").val();

    //if ($("#Quantity").val() == null || $("#Quantity").val() == "" || $("#Quantity").val() == undefined || $("#Quantity").val() == NaN || parseInt($("#Quantity").val()) <= 0) {
    //    alert("Please Add Quantity");
    //    return false;

    //}

    if (Quantity == null || Quantity == 0 || Quantity == NaN || Quantity == undefined) {
        alert("Somthing went Wrong .Please refresh the page")
    }

    if (ProductId == null || ProductId == undefined || ProductId == NaN || ProductId == "0") {

        alert("Something went wrong. Please refresh the page and try again.");
        return false;
    } else {
        obj.Pk_ProductId = ProductId;
        obj.Quantity = Quantity;
        obj.OpCode = 1;
        obj.TokenNo = $("#GLB_CookiesId").val();
    }


    $.ajax({

        url: '/Home/AddToCart/',
        // data: JSON.stringify(obj),
        data: { Pk_ProductId: obj.Pk_ProductId, Quantity: obj.Quantity, OpCode: obj.OpCode, TokenNo: obj.TokenNo },
        dataType: 'json',
        type: 'POST',
        async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {

            if (data != "" && data != undefined && data != NaN && data != null) {

                $(".glbTotalCartItems").text(data);
            }

        },
        error: function (error) {

        }
    });

}

//function AddToCart(ProductId, Quantity) {
//    debugger;
//    $(".errortext").removeClass("errortext");
    
//    var obj = {};
//    //var abc = $("#Quantity").val();

//    //if ($("#Quantity").val() == null || $("#Quantity").val() == "" || $("#Quantity").val() == undefined || $("#Quantity").val() == NaN || parseInt($("#Quantity").val()) <= 0) {
//    //    alert("Please Add Quantity");
//    //    return false;

//    //}

//    if (Quantity == null || Quantity == 0 || Quantity == NaN || Quantity == undefined) {
//        alert("Somthing went Wrong .Please refresh the page")
//    }

//    if (ProductId == null || ProductId == undefined || ProductId == NaN || ProductId == "0") {

//        alert("Something went wrong. Please refresh the page and try again.");
//        return false;
//    } else {
//        obj.Pk_ProductId = ProductId;
//        obj.Quantity = Quantity;
//        obj.OpCode = 1;
//        obj.TokenNo = $("#GLB_CookiesId").val();
//    }
    
//    $.ajax({
//        url: '/Home/CheckLogin/',
//        type: 'Post',
//        data: { Pk_ProductId: obj.Pk_ProductId, Quantity: obj.Quantity, OpCode: obj.OpCode, TokenNo: obj.TokenNo },
//        success: function (response) {

//            window.location.href = "/Home/Login?Status=" + response + "&&ProductId=" + obj.Pk_ProductId + "&&Quantity=" + obj.Quantity;
         
//        }, error: function () {
//            //alert('Some Thing wrong!');
    
//        }       
//    });   
//}
