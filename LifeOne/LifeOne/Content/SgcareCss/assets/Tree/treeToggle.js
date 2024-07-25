//--------------Update 23/07/2018-------------------//
//--------------Replace title='Add New' to title=""-------//

$.ajaxSetup({ cache: false });
var current_href = $(location).attr('href');
var current_title = $(document).attr('title');
//if (current_title == 'Geneology - Distributor Dashboard' || current_title == 'Geneology - MaxGrow Nature Admin Dashboard') {

$(".container").removeAttr("id");

//$("body").click(function () {
//    $("a").popover('hide');
//});
//$("a").click(function () {
//    $("a").popover('hide');
//});


var winHeight = $(window).height();
var winWidth = $(window).width();

//fk_id,loginid, name,leg,sponsorid
function openPopup(obj, item1, item2, item3, item4, item5, JoiningDate, ProductName, AllLeg1, AllLeg2, PermanentLeg1, PermanentLeg2, InactiveLeft, InactiveRight, PCountLeg1, PCountLeg2, PaidLeg1, PaidLeg2, BalanceLeft, BalanceRight, FreeLeg1, FreeLeg2, ActivationDate,LeftBusiness,RightBusiness,LeftProductBusiness,RightProductBusiness, event) {
    ////
    debugger;
    PCountLeg1 = parseFloat(PCountLeg1 == "" ? 0 : PCountLeg1);
    PCountLeg2 = parseFloat(PCountLeg2 == "" ? 0 : PCountLeg2);

    AllLeg1 = parseFloat(AllLeg1 == "" ? 0 : AllLeg1);
    AllLeg2 = parseFloat(AllLeg2 == "" ? 0 : AllLeg2);

    PaidLeg1 = parseFloat(PaidLeg1 == "" ? 0 : PaidLeg1);
    PaidLeg2 = parseFloat(PaidLeg2 == "" ? 0 : PaidLeg2);

    event.preventDefault();
    $("a").popover('hide');

    $("#lblMemberId").text(item2);
    $("#hdn_fk_memid").val(item1);
    $('#lblParnetId').text(item5);
    $('#lblDisplayName').text(item3);

    $(".LoginId").html(item5);
    $("#lblJoiningDate").html(JoiningDate);
    $("#lblPlotDetails").html(ProductName);

    $(".TeamLeft").html(AllLeg1);
    $(".TeamRight").html(AllLeg2);

    $(".TeamTotal").html(AllLeg1+AllLeg2);

    $(".ActiveLeft").html(PermanentLeg1);
    $(".ActiveRight").html(PermanentLeg2);

    $(".ActiveTotal").html(parseFloat(PermanentLeg1) + parseFloat(PermanentLeg2));

    $(".NonActiveLeft").html(InactiveLeft);
    $(".NonActiveRight").html(InactiveRight);

    $(".NonActiveTotal").html(parseFloat(InactiveLeft) + parseFloat(InactiveRight));

    $(".BusinessLeft").html(PCountLeg1);
    $(".BusinessRight").html(PCountLeg2);

    $(".BusinessTotal").html(parseFloat(PCountLeg1) + parseFloat(PCountLeg2));

    $(".LoginBusinessLeft").html(LeftBusiness);
    $(".LoginBusinessRight").html(RightBusiness);
    $(".LoginBusinessTotal").html(parseFloat(LeftBusiness) + parseFloat(RightBusiness));

    $(".ProductBusinessLeft").html(LeftProductBusiness);
    $(".ProductBusinessRight").html(RightProductBusiness);
    $(".ProductBusinessTotal").html(parseFloat(LeftProductBusiness) + parseFloat(RightProductBusiness));

    $(".PaidBusinessLeft").html(PaidLeg1);
    $(".PaidBusinessRight").html(PaidLeg2);

    $(".PaidBusinessTotal").html(parseFloat(PaidLeg1) + parseFloat(PaidLeg2));

    $(".CarryLeft").html(parseFloat(PCountLeg1) - parseFloat(PaidLeg1));
    $(".CarryRight").html(parseFloat(PCountLeg2) - parseFloat(PaidLeg2));
    $(".CarryTotal").html((parseFloat(PCountLeg1) - parseFloat(PaidLeg1)) +(parseFloat(PCountLeg2) - parseFloat(PaidLeg2)));

    $(".TotalBalance").html(parseFloat(BalanceLeft) + parseFloat(BalanceRight));

    $("#lblActivationDate").html(ActivationDate);
    $(".FreeLeg1").html(FreeLeg1);
    $(".FreeLeg2").html(FreeLeg2);
    $(".TotalFreeLeg").html(parseFloat(FreeLeg1) + parseFloat(FreeLeg2));

    $(".TotalActiveLeft").html(parseFloat(FreeLeg1) + parseFloat(PermanentLeg1));
    $(".TotalActiveRight").html(parseFloat(FreeLeg2) + parseFloat(PermanentLeg2));
    $(".TotalActiveAll").html(parseFloat(parseFloat(FreeLeg1) + parseFloat(PermanentLeg1)) + parseFloat(parseFloat(FreeLeg2) + parseFloat(PermanentLeg2)));

    // $("#" + obj.id).find('img').popover(options);
    if (winWidth > 768) {
       
        $('#' + obj.id).attr("data-content", $("#member-details").html());
        $('[data-toggle="popover"]').popover({
            container: 'body',
            html: true,
            placement: 'auto',
            trigger: 'hover',
            //content: function () {
            //    // get the url for the full size img
            //    var url = $(this).data('full');
            //    return '<img src="' + url + '">'
            //}

        });
       
    }
    else {        
        $("#member-details").attr("style", "width:90%;right:0px;background-color: #20813e;padding:10px;")
        $("#close_popover").show();
        $('.popover-body').show();       
    }
}
function closePopup(obj) {   
   //// $('.popover-body').hide();
   // $("a").popover('hide');
   // if (winWidth < 998) {
   //     // $("#member-details").addClass('active');
   // }
   // else {
   // }
   // $('#' + obj.id).removeAttr("data-content", $("#member-details").html());
   // //$("#" + obj.id).popover('hide');
}

if (winWidth > 998) {
    $("#chart").on("mouseover", function () {
        // $("#member-details").addClass('active');
    });
    $("#chart").on("mouseout", function () {
        // $("#member-details").removeClass('active');
    });
}
$("#close_popover").on("click", function () {
    $("#member-details").removeAttr("style", "width:90%;right:0px;background-color: #20813e;padding:10px;")
    $("#close_popover").hide();
});



var fk_memid = "0";
$("#btnRoot").click(function () {

    var loginid = $("#firtsMemberID").val();
    var topId = $("#linkGoUp").find("#lblRequest").val();
    if (parseInt(loginid) > parseInt(topId)) {
        //alert("You Are on top...!")
    }
    else {
        fk_memid = $("#firtsMemberID").val();
        CheckParent(loginid, fk_memid, "clicked");
        //GetDataFirst(loginid, "clicked");
        $("#txtSearchID").val('');
        $("#hfCustomerId").val('');
    }

});

$("#linkGoUp").click(function () {
     
    if ($(this).find("#lblRequest").val() == "0") {
        //fk_memid = $("#firtsMemberID").val();
        // GetDataFirst(fk_memid, "clicked");
    }
    else {
        if (parseInt($(this).find("#lblRequest").val()) < parseInt($("#firtsMemberID").val())) {
            //alert("Can't Reach With Relation with Parent")
        } else {
            fk_memid = $(this).find("#lblRequest").val();
            var ses_fk_memid = $("#firtsMemberID").val();
            //GetDataFirst(fk_memid, "clicked");
            CheckParent(fk_memid, ses_fk_memid, "clicked");
        }

    }

    // GetData(fk_memid);
});


var fk_memid = "0";
var clicked = 0;

function stopEvnts(event) {
    
    event.preventDefault();
    event.stopPropagation();
}

function ClickHere(obj, event) {
   //^ alert(JSON.stringify(obj));
    event.preventDefault();
    event.stopPropagation();
   
    $("a").popover('hide');    
    var ses_fk_memid = $("#firtsMemberID").val();
    if ($("#" + obj.id).find('input[type="hidden"]').val() == "-1") {
       return false;
    }
    $("#member-details").removeClass('active');
    var mID = $("#" + obj.id).find('input[type="hidden"]').val();
    clicked = 1;
    //GetDataFirst(mID, "clicked");
    CheckParent(mID, ses_fk_memid, "clicked");
}

function doDoubleClickAction() {
    alert("double click not allowed..!");
    window.setTimeout(function () {
        location.href = "/Associate/AssociateTeam/GetTreeMlm";
    }, 1000);
}

$(window).on("load", function () {
    var id = $("#firtsMemberID").val();
    
    GetDataFirst(id);
    GetData(id);
});


$("#close-it").click(function () {
    $("#member-details").removeClass('active');
});
$(document).ready(function () {
    if (clicked != 1) {
        fk_memid = $("#firtsMemberID").val();
        GetDataFirst(fk_memid, null);
        //CheckParent(fk_memid,0,null);

    }
});





var winHeight = $(window).height();
var winWidth = $(window).width();
var clickOption = "";
if (winWidth < 0) {
    //clickOption = "";
    //dblOption = "ondblclick='ClickHere(this)'";
    clickOption = "onClick='ClickHere(this,event)' style='font-family:arial;color:#000000;font-size:10px'";  
    
}
else {
    clickOption = "onClick='ClickHere(this,event)' style='font-family:arial;color:#000000;font-size:10px'";
    
    // dblOption = "ondblclick='ClickHere(this)'";
}



function GetData(id) {

    if (id != null) {
        Get(id);
    }
    var memId = 0;
    var cnt = 1;
    function Get(Id) {
        $.ajax({
            type: "POST",
            url: "/Associate/AssociateTeam/GetGeneology",
            data: '{memID: ' + Id + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {

                alert("Failed: " + response.d);
            },
            error: function (response) {
                //alert(JSON.stringify(response));
                alert("Error: " + response.d);
            }
        });
    }




    function OnSuccess(response) {
        //
        ////
      //  alert(JSON.stringify(response));

        var uls = 0;
        var ul = $("<ul></ul>");
        var freeBlock = "";
        var freeBlockL = "";
        var freeBlockR = "";
        var model = response;

        

        if (!$.trim(model)) {
            $(".alert-warning").show();
            $("#lblWarning").text("No Member Found...For this Associate");
            //$("#userTree").hide();

            freeBlockL = "<li id='1'><a href='Registration.cshtml?Pid='123466'&amp;lg=L' target='_blank'><img src='/Content/NetBizContent/assets/Tree/Img/block-member.png'><br><span>Join Now</span></a></li>";
            freeBlockR = "<li id='2'><a href='Registration.cshtml?Pid='123466'&amp;lg=R' target='_blank'><img src='/Content/NetBizContent/assets/Tree/Img/block-member.png'><br><span>Join Now</span></a></li>";

            ul.append(freeBlockL + freeBlockR);
            $("#firstRow").fadeIn(500);
            window.setTimeout(function () {
                $('#alertbox .alert').hide(500, 0).slideUp(1500, function () {
                    $('#alertbox .alert').slideUp(500);
                });
            }, 2000);

        }
        else {
            var $child = "";
            var level = 0;
            $.each(response, function (i, k) {
                level++;
            });

            $.each(model, function (i, item1) {

                debugger;
                id = item1.Fk_MemID;
               
                if (level == 2) {
                   
                    if (id != '-1') {
                      
                        child = " <li id='level" + i + "'><a data-toggle='popover' " + clickOption + " id='levelLink" + i + "' href='" + item1.Href + "'  class='from " + item1.CssClass + "' " +
                                                  "  " +
                            " href='#' onmouseout='closePopup(this)'>" +
                            "<img src='/Content/NetBizContent/assets/Tree/Img/" + item1.CssClass + ".png' id='levelLink" + i + "'  onmouseover='openPopup(this," + item1.Fk_MemID + "," + '"' + item1.LoginId + '"' + ", " + '"' + item1.MemberName + '"' + "," + '"' + item1.Leg + '"' + "," + '"' + item1.ParentLoginId + '"' + "," + '"' + item1.JoiningDate + '"' + "," + '"' + item1.ProductName + '"' + "," + '"' + item1.AllLeg1 + '"' + "," + '"' + item1.AllLeg2 + '"' + "," + '"' + item1.PermanentLeg1 + '"' + "," + '"' + item1.PermanentLeg2 + '"' + "," + '"' + item1.InactiveLeft + '"' + "," + '"' + item1.InactiveRight + '"' + "," + '"' + item1.PCountLeg1 + '"' + "," + '"' + item1.PCountLeg2 + '"' + "," + '"' + item1.PaidLeg1 + '"' + "," + '"' + item1.PaidLeg2 + '"' + "," + '"' + item1.BalanceLeft + '"' + "," + '"' + item1.BalanceRight + '"' + "," + '"' + item1.FreeLeg1 + '"' + "," + '"' + item1.FreeLeg2 + '"' + "," + '"' + item1.ActivationDate + '"' + "," + '"' + item1.LeftBusiness + '"' + "," + '"' + item1.RightBusiness + '"' + "," + '"' + item1.LeftProductBusiness + '"' + "," + '"' + item1.RightProductBusiness + '"' + ",event)'><br>" +
                                                  " <span >" + item1.LoginId + "</span><br> <span  id='levelLink" + i + "'>" + item1.MemberName + "</span>" +
                                                  "<input type='hidden' id='" + item1.Fk_MemID + "' value='" + item1.Fk_MemID + "' />" +
                                                  "</a><br/></li>";
                    }
                    else {
                        child = " <li id='level" + i + "'><a data-toggle='tooltip' data-original-title='' title='' id='levelLink" + i + "' href='" + item1.Href + "' target='_blank' class='from " + item1.CssClass + "' " + ">" +
                                                 "<img src='/Content/NetBizContent/assets/Tree/Img/" + item1.CssClass + ".png'><br>" +
                                                 "<span >" + item1.LoginId + "</span><br> <span " + clickOption + " id='levelLink" + i + "'> " + item1.MemberName + "</span>" +
                                                 "<input type='hidden' id='" + item1.Fk_MemID + "' value='" + item1.Fk_MemID + "' />" +
                                                 "</a></li>";
                    }
                    ul.append(child);
                }
                else {
                    if (id != '-1') {
                        child = " <li id='level" + i + "'><a data-toggle='popover' " + clickOption + " id='levelLink" + i + "' href='" + item1.Href + "' class='from " + item1.CssClass + "' " +
                                                  "onmouseout='closePopup(this)' href='#'>" +
                            "<img src='/Content/NetBizContent/assets/Tree/Img/" + item1.CssClass + ".png' id='levelLink" + i + "' onmouseover='openPopup(this," + item1.Fk_MemID + "," + '"' + item1.LoginId + '"' + ", " + '"' + item1.MemberName + '"' + "," + '"' + item1.Leg + '"' + "," + '"' + item1.ParentLoginId + '"' + "," + '"' + item1.JoiningDate + '"' + "," + '"' + item1.ProductName + '"' + "," + '"' + item1.AllLeg1 + '"' + "," + '"' + item1.AllLeg2 + '"' + "," + '"' + item1.PermanentLeg1 + '"' + "," + '"' + item1.PermanentLeg2 + '"' + "," + '"' + item1.InactiveLeft + '"' + "," + '"' + item1.InactiveRight + '"' + "," + '"' + item1.PCountLeg1 + '"' + "," + '"' + item1.PCountLeg2 + '"' + "," + '"' + item1.PaidLeg1 + '"' + "," + '"' + item1.PaidLeg2 + '"' + "," + '"' + item1.BalanceLeft + '"' + "," + '"' + item1.BalanceRight + '"' + "," + '"' + item1.FreeLeg1 + '"' + "," + '"' + item1.FreeLeg2 + '"' + "," + '"' + item1.ActivationDate + '"' + "," + '"' + item1.LeftBusiness + '"' + "," + '"' + item1.RightBusiness + '"' + "," + '"' + item1.LeftProductBusiness + '"' + "," + '"' + item1.RightProductBusiness + '"' + ",event)'><br>" +
                                                  " <span >" + item1.LoginId + "</span><br> <span  id='levelLink" + i + "'> " + item1.MemberName + "</span>" +
                                                  "<input type='hidden' id='" + item1.Fk_MemID + "' value='" + item1.Fk_MemID + "' />" +
                                                  "</a><br/></li>";
                    }
                    else {
                        child = " <li id='level" + i + "'><a id='levelLink" + i + "' href='" + item1.Href + "' target='_blank' class='from " + item1.CssClass + "' " + ">" +
                                                 "<img src='/Content/NetBizContent/assets/Tree/Img/" + item1.CssClass + ".png'><br>" +
                                                 " <span >" + item1.LoginId + "</span><br> <span " + clickOption + " id='levelLink" + i + "'> " + item1.MemberName + "</span>" +
                                                 "<input type='hidden' id='" + item1.Fk_MemID + "' value='" + item1.Fk_MemID + "' />" +
                                                 "</a><br/></li>";
                    }
                    //freeBlock = "<li id='1'><a href='Registration.cshtml?Pid=" + item1.LoginId + "&amp;lg=" + item1.Leg + "' target='_blank'><img src='/Content/img/open-member.png'><br><span>Join Now</span></a></li>";
                    freeBlock = "<li id='1'><a href='Registration.cshtml?Pid=" + item1.LoginId + "&amp;lg=" + item1.Leg + "' target='_blank'><img src='/Content/NetBizContent/assets/Tree/Img/block-member.png'><br><span>Join Now</span></a></li>";

                    if (item1.Leg == 'L') {
                        ul.append(child + freeBlock);
                    }
                    else  {
                        ul.append(freeBlock + child);
                    }
                }

                //if (winWidth > 766) {
                if (id != null) {
                    $.ajax({
                        type: "POST",
                        url: "/Associate/AssociateTeam/GetGeneology",
                        data: '{memID: ' + id + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response2) {
                            var model2 = response2;
                            var ul2 = $("<ul></ul>");
                            var level2 = 0;
                            if (!$.trim(model2)) {

                                freeBlockL = "<li id='1'><a href='Registration.cshtml?Pid='123466'&amp;lg=L' target='_blank'><img src='/Content/NetBizContent/assets/Tree/Img/block-member.png'><br><span>Join Now</span></a></li>";
                                freeBlockR = "<li id='2'><a href='Registration.cshtml?Pid='123466'&amp;lg=R' target='_blank'><img src='/Content/NetBizContent/assets/Tree/Img/block-member.png'><br><span>Join Now</span></a></li>";
                                ul2.append(freeBlockL + freeBlockR);
                                //$("#firstRow").fadeIn(2000);
                                window.setTimeout(function () {
                                    $('#alertbox .alert').hide(500, 0).slideUp(1500, function () {
                                        $('#alertbox .alert').slideUp(500);
                                    });
                                }, 2000);
                                $("#level" + i).append(ul2);
                            }
                            else {
                                $.each(response2, function (i1, k2) {
                                    level2++;
                                });
                                $.each(model2, function (i1, item2) {
                                    id = item2.Fk_MemID;
                                    if (level2 == 2) {
                                        if (id != '-1') {
                                            child = " <li id='level" + i + i1 + "'><a data-toggle='popover' " + clickOption + " id='levelLink" + i + i1 + "' href='" + item2.Href + "' class='from " + item2.CssClass + "' " +
                                                      "  " +
                                                      " onmouseout='closePopup(this)' href='#'>" +
                                                "<img src='/Content/NetBizContent/assets/Tree/Img/" + item2.CssClass + ".png' id='levelLink" + i + i1 + "' onmouseover='openPopup(this," + item2.Fk_MemID + "," + '"' + item2.LoginId + '"' + ", " + '"' + item2.MemberName + '"' + "," + '"' + item2.Leg + '"' + "," + '"' + item2.ParentLoginId + '"' + "," + '"' + item2.JoiningDate + '"' + "," + '"' + item2.ProductName + '"' + "," + '"' + item2.AllLeg1 + '"' + "," + '"' + item2.AllLeg2 + '"' + "," + '"' + item2.PermanentLeg1 + '"' + "," + '"' + item2.PermanentLeg2 + '"' + "," + '"' + item2.InactiveLeft + '"' + "," + '"' + item2.InactiveRight + '"' + "," + '"' + item2.PCountLeg1 + '"' + "," + '"' + item2.PCountLeg2 + '"' + "," + '"' + item2.PaidLeg1 + '"' + "," + '"' + item2.PaidLeg2 + '"' + "," + '"' + item2.BalanceLeft + '"' + "," + '"' + item2.BalanceRight + '"' + "," + '"' + item2.FreeLeg1 + '"' + "," + '"' + item2.FreeLeg2 + '"' + "," + '"' + item2.ActivationDate + '"' + "," + '"' + item2.LeftBusiness + '"' + "," + '"' + item2.RightBusiness + '"' + "," + '"' + item2.LeftProductBusiness + '"' + "," + '"' + item2.RightProductBusiness + '"' + ",event)'><br>" +
                                                      " <span>" + item2.LoginId + "</span><br> <span  id='levelLink" + i + i1 + "'> " + item2.MemberName + "</span>" +
                                                      "<input type='hidden' id='" + item2.Fk_MemID + "' value='" + item2.Fk_MemID + "' />" +
                                                      "</a><br/></li>";
                                        }
                                        else {
                                            child = " <li id='level" + i + i1 + "'><a data-toggle='tooltip' data-original-title='' title='' id='levelLink" + i + i1 + "' target='_blank' href='" + item2.Href + "' class='from " + item2.CssClass + "' " + ">" +
                                                    "<img src='/Content/NetBizContent/assets/Tree/Img/" + item2.CssClass + ".png'><br>" +
                                                    " <span>" + item2.LoginId + "</span><br> <span " + clickOption + " id='levelLink" + i + i1 + "'> " + item2.MemberName + "</span>" +
                                                    "<input type='hidden' id='" + item2.Fk_MemID + "' value='" + item2.Fk_MemID + "' />" +
                                                    "</a><br/></li>";

                                        }
                                        ul2.append(child);
                                    }
                                    else {
                                        if (id != '-1') {
                                            child = " <li id='level" + i + i1 + "'><a data-toggle='popover' " + clickOption + " id='levelLink" + i + i1 + "' href='" + item2.Href + "' class='from " + item2.CssClass + "' " +
                                                     " " +
                                                     " onmouseout='closePopup(this)' href='#'>" +
                                                "<img src='/Content/NetBizContent/assets/Tree/Img/" + item2.CssClass + ".png'  id='levelLink" + i + i1 + "' onmouseover='openPopup(this," + item2.Fk_MemID + "," + '"' + item2.LoginId + '"' + ", " + '"' + item2.MemberName + '"' + "," + '"' + item2.Leg + '"' + "," + '"' + item2.ParentLoginId + '"' + "," + '"' + item2.JoiningDate + '"' + "," + '"' + item2.ProductName + '"' + "," + '"' + item2.AllLeg1 + '"' + "," + '"' + item2.AllLeg2 + '"' + "," + '"' + item2.PermanentLeg1 + '"' + "," + '"' + item2.PermanentLeg2 + '"' + "," + '"' + item2.InactiveLeft + '"' + "," + '"' + item2.InactiveRight + '"' + "," + '"' + item2.PCountLeg1 + '"' + "," + '"' + item2.PCountLeg2 + '"' + "," + '"' + item2.PaidLeg1 + '"' + "," + '"' + item2.PaidLeg2 + '"' + "," + '"' + item2.BalanceLeft + '"' + "," + '"' + item2.BalanceRight + '"' + "," + '"' + item2.FreeLeg1 + '"' + "," + '"' + item2.FreeLeg2 + '"' + "," + '"' + item2.ActivationDate + '"' + "," + '"' + item2.LeftBusiness + '"' + "," + '"' + item2.RightBusiness + '"' + "," + '"' + item2.LeftProductBusiness + '"' + "," + '"' + item2.RightProductBusiness + '"' + ",event)' ><br>" +
                                                     " <span>" + item2.LoginId + "</span><br> <span  id='levelLink" + i + i1 + "'> " + item2.MemberName + "</span>" +
                                                     "<input type='hidden' id='" + item2.Fk_MemID + "' value='" + item2.Fk_MemID + "' />" +
                                                     "</a><br/></li>";
                                        }
                                        else {
                                            child = " <li id='level" + i + i1 + "'><a id='levelLink" + i + i1 + "' target='_blank' href='" + item2.Href + "' class='from " + item2.CssClass + "' " + ">" +
                                                    "<img src='/Content/NetBizContent/assets/Tree/Img/" + item2.CssClass + ".png'><br>" +
                                                    " <span>" + item2.LoginId + "</span><br> <span id='levelLink" + i + i1 + "'> " + item2.MemberName + "</span>" +
                                                    "<input type='hidden' id='" + item2.Fk_MemID + "' value='" + item2.Fk_MemID + "' />" +
                                                    "</a><br/></li>";

                                        }
                                        freeBlock = "<li id='1'><a href='Registration.cshtml?Pid=" + item2.LoginId + "&amp;lg=" + item2.Leg + "' target='_blank'><img src='/Content/NetBizContent/assets/Tree/Img/block-member.png'><br><span>Join Now</span></a></li>";

                                        if (item2.Leg == 'L') {
                                            ul2.append(child + freeBlock);
                                        }
                                        else   {
                                            ul2.append(freeBlock + child);
                                        }
                                    }
                                    //if (winWidth > 980) {
                                    if (id != null) {
                                        $.ajax({
                                            type: "POST",
                                            url: "/Associate/AssociateTeam/GetGeneology",
                                            data: '{memID: ' + id + '}',
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function (response3) {
                                                var model3 = response3;
                                                var ul3 = $("<ul></ul>");
                                                var level3 = 0;
                                                if (!$.trim(model3)) {
                                                    freeBlockL = "<li id='1'><a href='Registration.cshtml?Pid='123466'&amp;lg=L' target='_blank'><img src='/Content/NetBizContent/assets/Tree/Img/block-member.png'><br><span>Join Now</span></a></li>";
                                                    freeBlockR = "<li id='2'><a href='Registration.cshtml?Pid='123466'&amp;lg=R' target='_blank'><img src='/Content/NetBizContent/assets/Tree/Img/block-member.png'><br><span>Join Now</span></a></li>";

                                                    //freeBlockL = "<li id='1'><a href='Registration.cshtml?Pid='123466'&amp;lg=L' target='_blank'><img src='/Content/img/grey-max.png'><br><span>Join Now</span></a></li>";
                                                    //freeBlockR = "<li id='2'><a href='Registration.cshtml?Pid='123466'&amp;lg=R' target='_blank'><img src='/Content/img/grey-max.png'><br><span>Join Now</span></a></li>";                                                            

                                                    ul3.append(freeBlockL + freeBlockR);
                                                    $("#firstRow").fadeIn(500);
                                                    window.setTimeout(function () {
                                                        $('#alertbox .alert').hide(500, 0).slideUp(1500, function () {
                                                            $('#alertbox .alert').slideUp(500);
                                                        });
                                                    }, 2000);
                                                    $("#level" + i + i1).append(ul3);
                                                }
                                                else {
                                                    $.each(response3, function (i2, k3) {
                                                        level3++;
                                                    });
                                                    $.each(model3, function (i2, item3) {
                                                        id = item3.Fk_MemID;
                                                        if (level3 == 2) {
                                                            if (id != '-1') {
                                                                // alert("3rd Level Id(!-1): " + id);
                                                                child = " <li id='level" + i + i1 + i2 + "'><a data-toggle='popover' " + clickOption + " id='levelLink" + i + i1 + i2 + "' href='" + item3.Href + "' class='from " + item3.CssClass + "' " +
                                                                          "  " +
                                                                          " onmouseout='closePopup(this)' href='#'>" +
                                                                    "<img src='/Content/NetBizContent/assets/Tree/Img/" + item3.CssClass + ".png' id='levelLink" + i + i1 + i2 + "' onmouseover='openPopup(this," + item3.Fk_MemID + "," + '"' + item3.LoginId + '"' + ", " + '"' + item3.MemberName + '"' + "," + '"' + item3.Leg + '"' + "," + '"' + item3.ParentLoginId + '"' + "," + '"' + item3.JoiningDate + '"' + "," + '"' + item3.ProductName + '"' + "," + '"' + item3.AllLeg1 + '"' + "," + '"' + item3.AllLeg2 + '"' + "," + '"' + item3.PermanentLeg1 + '"' + "," + '"' + item3.PermanentLeg2 + '"' + "," + '"' + item3.InactiveLeft + '"' + "," + '"' + item3.InactiveRight + '"' + "," + '"' + item3.PCountLeg1 + '"' + "," + '"' + item3.PCountLeg2 + '"' + "," + '"' + item3.PaidLeg1 + '"' + "," + '"' + item3.PaidLeg2 + '"' + "," + '"' + item3.BalanceLeft + '"' + "," + '"' + item3.BalanceRight + '"' + "," + '"' + item3.FreeLeg1 + '"' + "," + '"' + item3.FreeLeg2 + '"' + "," + '"' + item3.ActivationDate + '"' + "," + '"' + item3.LeftBusiness + '"' + "," + '"' + item3.RightBusiness + '"' + "," + '"' + item3.LeftProductBusiness + '"' + "," + '"' + item3.RightProductBusiness + '"' + ",event)'><br>" +
                                                                          " <span >" + item3.LoginId + "</span><br> <span  id='levelLink" + i + i1 + i2 + "'> " + item3.MemberName + "</span>" +
                                                                          "<input type='hidden' id='" + item3.Fk_MemID + "' value='" + item3.Fk_MemID + "' />" +
                                                                          "</a></li>";
                                                            }
                                                            else {
                                                                //alert("3rd Level Id: " + id);
                                                                child = " <li id='level" + i + i1 + i2 + "'><a data-toggle='tooltip' target='_blank' data-original-title='' title='' id='levelLink" + i + i1 + i2 + "' href='" + item3.Href + "' class='from " + item3.CssClass + "' " +
                                                                          ">" +
                                                                          "<img src='/Content/NetBizContent/assets/Tree/Img/" + item3.CssClass + ".png'><br>" +
                                                                          " <span >" + item3.LoginId + "</span><br> <span " + clickOption + " id='levelLink" + i + i1 + i2 + "'> " + item3.MemberName + "</span>" +
                                                                          "<input type='hidden' id='" + item3.Fk_MemID + "' value='" + item3.Fk_MemID + "' />" +
                                                                          "</a><br/></li>";
                                                            }
                                                            ul3.append(child);
                                                            $('[data-rel="tooltip"]').tooltip
                                                        }
                                                        else {
                                                            if (id != '-1') {
                                                                child = " <li id='level" + i + i1 + i2 + "'><a data-toggle='popover' " + clickOption + " id='levelLink" + i + i1 + i2 + "' href='" + item3.Href + "' class='from " + item3.CssClass + "' " +
                                                                            "  " +
                                                                           " onmouseout='closePopup(this)' href='#'>" +
                                                                    "<img src='/Content/NetBizContent/assets/Tree/Img/" + item3.CssClass + ".png' id='levelLink" + i + i1 + i2 + "' onmouseover='openPopup(this," + item3.Fk_MemID + "," + '"' + item3.LoginId + '"' + ", " + '"' + item3.MemberName + '"' + "," + '"' + item3.Leg + '"' + "," + '"' + item3.ParentLoginId + '"' + "," + '"' + item3.JoiningDate + '"' + "," + '"' + item3.ProductName + '"' + "," + '"' + item3.AllLeg1 + '"' + "," + '"' + item3.AllLeg2 + '"' + "," + '"' + item3.PermanentLeg1 + '"' + "," + '"' + item3.PermanentLeg2 + '"' + "," + '"' + item3.InactiveLeft + '"' + "," + '"' + item3.InactiveRight + '"' + "," + '"' + item3.PCountLeg1 + '"' + "," + '"' + item3.PCountLeg2 + '"' + "," + '"' + item3.PaidLeg1 + '"' + "," + '"' + item3.PaidLeg2 + '"' + "," + '"' + item3.BalanceLeft + '"' + "," + '"' + item3.BalanceRight + '"' + "," + '"' + item3.FreeLeg1 + '"' + "," + '"' + item3.FreeLeg2 + '"' + "," + '"' + item3.ActivationDate + '"' + "," + '"' + item3.LeftBusiness + '"' + "," + '"' + item3.RightBusiness + '"' + "," + '"' + item3.LeftProductBusiness + '"' + "," + '"' + item3.RightProductBusiness + '"' + ",event)'><br>" +
                                                                           " <span >" + item3.LoginId + "</span><br> <span  id='levelLink" + i + i1 + i2 + "'> " + item3.MemberName + "</span>" +
                                                                           "<input type='hidden' id='" + item3.Fk_MemID + "' value='" + item3.Fk_MemID + "' />" +
                                                                           "</a><br/></li>";
                                                            }
                                                            else {
                                                                child = " <li id='level" + i + i1 + i2 + "'><a id='levelLink" + i + i1 + i2 + "' target='_blank' href='" + item3.Href + "' class='from " + item3.CssClass + "' " +
                                                                          ">" +
                                                                          "<img src='/Content/NetBizContent/assets/Tree/Img/" + item3.CssClass + ".png'><br>" +
                                                                          " <span >" + item3.LoginId + "</span><br> <span> " + item3.MemberName + "</span>" +
                                                                          "<input type='hidden' id='" + item3.Fk_MemID + "' value='" + item3.Fk_MemID + "' />" +
                                                                          "</a><br/></li>";
                                                            }
                                                            freeBlock = "<li id='1'><a href='Registration.cshtml?Pid=" + item3.LoginId + "&amp;lg=" + item3.Leg + "' target='_blank'><img src='/Content/NetBizContent/assets/Tree/Img/block-member.png'><br><span>Join Now</span></a></li>";
                                                            //freeBlock = "<li id='1'><a href='Registration.cshtml?Pid=" + item3.LoginId + "&amp;lg=" + item3.Leg + "' target='_blank'><img src='Content/img/grey-max.png'><br><span>Join Now</span></a></li>";

                                                            if (item3.Leg == 'L') {
                                                                ul3.append(child + freeBlock);
                                                            }
                                                            else {
                                                                ul3.append(freeBlock + child);
                                                            }
                                                        }
                                                    });
                                                    $("#level" + i + i1).append(ul3);
                                                }
                                            },
                                            failure: function (response) {
                                                alert("Failed: " + response.d);
                                            },
                                            error: function (response) {
                                                alert("Error: " + response.d);
                                            },
                                            complete: function () {
                                            }
                                        });
                                    }
                                        //}
                                    else {
                                    }
                                });
                                $("#level" + i).append(ul2);
                            }
                            $("#firstRow").append(ul);

                        },
                        failure: function (response) {
                            alert("Failed: " + response.d);
                        },
                        error: function (response) {
                            alert("Error: " + response.d);
                        },
                        complete: function () {
                            //$("#firstRow").fadeIn(900);
                            //$('[id*="level"]').fadeIn(500);
                        }
                    });
                }
                    //}
                else {
                    $("#firstRow").fadeIn(1000);
                    $("#firstHead").attr("style", "opacity:1;");
                    $('[id*="level"]').fadeIn(500);
                    $("#firstRow").append(ul);
                }
            });
            $("#firstRow").append(ul);
        }
        $("#firstRow").append(ul);

        //  alert(uls);
        remove();
    }

}

function GetDataFirst(id, text) {
    //if (id == '-1') {
    //    return false;
    //}

    $.ajax({
        type: "POST",
        url: "/Associate/AssociateTeam/GetRootMember",
        data: '{memID: ' + id + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess2,
        failure: function (response) {
            //alert("1st fail: " + response);
        },
        error: function (response) {
            //alert("1st error: " + response);
        }
    });

    function OnSuccess2(response) {
       
        var modal = response;
        $.each(modal, function (i, item) {

            $("#headClass span").text(item.MemberName + "(" + item.LoginId + ")");
            var status = item.Status;
            if (status == "T") {
                status = "inactive-member"
            }
            else if (status == "P") {
                status = "active-member"
            }
            else {
                status = "block-member"
            }
            var $firstHead = $("#firstHead");
            $firstHead.html("<img src='/Content/NetBizContent/assets/Tree/Img/" + status + ".png' id='firstHead' onmouseout='closePopup(this)' onmouseover='openPopup(this," + item.Fk_MemID + "," + '"' + item.LoginId + '"' + ", " + '"' + item.MemberName + '"' + "," + '"' + item.Leg + '"' + "," + '"' + item.ParentLoginId + '"' + "," + '"' + item.JoiningDate + '"' + "," + '"' + item.ProductName + '"' + "," + '"' + item.AllLeg1 + '"' + "," + '"' + item.AllLeg2 + '"' + "," + '"' + item.PermanentLeg1 + '"' + "," + '"' + item.PermanentLeg2 + '"' + "," + '"' + item.InactiveLeft + '"' + "," + '"' + item.InactiveRight + '"' + "," + '"' + item.PCountLeg1 + '"' + "," + '"' + item.PCountLeg2 + '"' + "," + '"' + item.PaidLeg1 + '"' + "," + '"' + item.PaidLeg2 + '"' + "," + '"' + item.BalanceLeft + '"' + "," + '"' + item.BalanceRight + '"' + "," + '"' + item.FreeLeg1 + '"' + "," + '"' + item.FreeLeg2 + '"' + "," + '"' + item.ActivationDate + '"' + "," + '"' + item.LeftBusiness + '"' + "," + '"' + item.RightBusiness + '"' + "," + '"' + item.LeftProductBusiness + '"' + "," + '"' + item.RightProductBusiness + '"' + ",event)'><br><span >" + item.LoginId + "</span><br /><span id='firstHead' " + clickOption + ">" + item.MemberName + "<input type='hidden' value=" + item.Fk_MemID + " /></span>");

            $firstHead.attr("data-toggle", "popover");
            $("#firstHead").attr("class", status);
            $("#firstHead").attr("style", "opacity: 1;");
            $("#lblRequest").val(item.ParentId);
            if (text == "clicked") {
                $("#firstRow").html('');
                $("#firstRow").append($firstHead);
                GetData(item.Fk_MemID);
            }
        });

    }
}

/// Commented --
$("#btnSearch").click(function () {
    //For Searching
    //alert($("#txtSearchID").val());
    if ($("#txtSearchID").val() != "") {
        $.ajax({
            url: '/Associate/AssociateTeam/SearchCustomersByLoginId',
            data: "{ 'prefix': '" + $("#txtSearchID").val() + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: OnSuccessThis,
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            select: function (e, i) {
                $("[id$=hfCustomerId]").val(i.item.val);
            },
        });
    }


    function OnSuccessThis(data) {

        //console.log(data);
        var r = data;
        $(r).each(function () {
            $("[id$=hfCustomerId]").val(this.split('-')[1]);
            if ($("#hfCustomerId").val() != "") {
                if ($("#txtSearchID").val() == "") {
                    fk_memid = $("#firtsMemberID").val();

                }
                else {

                    var ses_fk_memid = $("#firtsMemberID").val();
                    fk_memid = $("#hfCustomerId").val();
                    CheckParent(fk_memid, ses_fk_memid, "clicked")
                    $("#firstRow").find('ul').remove();
                    //GetDataFirst(fk_memid);
                    //alert("Success, " + fk_memid + ", " + ses_fk_memid);

                    //GetData(fk_memid);
                }
            }
        })
    }
});
$('#txtSearchID').keypress(function (e) {
    var key = e.which;
    if (key == 13) // the enter key code
    {
        $('#btnSearch').click();
        return false;
    }
}); 


function CheckParent(fk_id, ses_fk_id, txt) {
    var fkmemId = 0;
    if (fk_id != null && ses_fk_id != null) {
        $.ajax({
            url: '/Associate/AssociateTeam/CheckParentForTreeView',
            data: "{ 'memId': '" + fk_id + "','sessionMemId': '" + ses_fk_id + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: OnCheckParent,
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            select: function (e, i) {
                $("[id$=hfCustomerId]").val(i.item.val);
            },
        });

        function OnCheckParent(data) {
            var r = data;
            $(r).each(function () {
                var result = this.split('-')[0];
                var returnId = this.split('-')[1];
                if (returnId != null) {
                    GetDataFirst(returnId, txt);
                    $("a").popover('hide');
                    //GetData(returnId);
                }
            })
        }
    }
}


//$(function () {
//    $("#txtSearchID").autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: '/Models/WebService/AgentServices.asmx/SearchCustomersByLoginId',
//                data: "{ 'prefix': '" + request.term + "'}",
//                dataType: "json",
//                type: "POST",
//                contentType: "application/json; charset=utf-8",
//                success: function (data) {
//                    response($.map(data.d, function (item) {
//                        return {
//                            label: item.split('-')[0],
//                            val: item.split('-')[1]
//                        }
//                    }))
//                },
//                error: function (response) {
//                    alert(response.responseText);
//                },
//                failure: function (response) {
//                    alert(response.responseText);
//                }
//            });
//        },
//        select: function (e, i) {
//            $("[id$=hfCustomerId]").val(i.item.val);
//        },
//        minLength: 2
//    });
//});



//}
function remove() {
    //if (winWidth < 768) {
    //    $("[id*=levelLink]").removeAttr("data-toggle", "popover");
    //    $("[id*=levelLink]").removeAttr("onclick");
    //    $("[id*=levelLink]").removeAttr("onmouseout");
    //    $("[id*=levelLink]").removeAttr("data-toggle");
    //    $("[id*=levelLink]").attr("onClick", "ClickHere(this)")
    //}
}
