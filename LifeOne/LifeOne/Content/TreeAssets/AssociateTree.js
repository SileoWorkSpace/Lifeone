


$(document).ready(function () {

    BindRoot();

});



function SearchLogin() {

    if ($('#txtsearchlogin').val() == '') {

        alert("Please Enter Login Id");
        $('#txtsearchlogin').focus();
        return false;
    }
    else {
        BindRoot($('#txtsearchlogin').val());
    }


}
var ImageUrl = 'http://lifeonewellness.com/Content/TreeAssets/icons/open-member.png';
function BindRoot(LoginId) {


    $('#divRootLeft').hide();
    $('#divRootLeftLeft').hide();
    $('#divRootLeftLeftLeft').hide();
    $('#divRootLeftLeftLeftRight').hide();
    $('#divRootLeftRight').hide();
    $('#divRootLeftRightLeft').hide();
    $('#divRootLeftRightRight').hide();
    $('#DivRootRight').hide();
    $('#DivRootRightLeft').hide();
    $('#DivRootRightLeftLeft').hide();
    $('#DivRootRightLeftRight').hide();
    $('#DivRootRightRight').hide();
    $('#DivRootRightRightLeft').hide();
    $('#DivRootRightRightRight1').hide();
    var Root = '';
    $.ajax({
        url: '../../Admin/AssoicateTree/RootTree',
        data: { LoginId: LoginId },
        dataType: 'json',
        type: 'POST',
        // async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {
            if (data != null) {
                ///Bind Level First Treee

                if (data.length > 0) {

                    for (var RootLevel0 = 0; RootLevel0 < data.length; RootLevel0++) {




                        if (data[RootLevel0].Level == "1") {

                            $('#txtParentId').val(data[RootLevel0].ParentId);

                            Root += "<span class='tf-nc'><span class='id-icon'><img src='" + data[RootLevel0].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLevel0].LoginId + "\");'>" +
                                "</span> <span class='id' onclick='BindRoot(\"" + data[RootLevel0].LoginId + "\");'>" + data[RootLevel0].LoginId + "</span> <span class='id-name'><a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLevel0].LoginId + "\");'>" + data[RootLevel0].MemberName + "<div class='hover-data'>" +
                                "<div class='hover-data-content'><h4>" + data[RootLevel0].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLevel0].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLevel0].JoiningDate + "</span>" +
                                "</h5><div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                                "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                                "<tr><td>Active</td><td>" + data[RootLevel0].PermanentLeg1 + "</td><td>" + data[RootLevel0].PermanentLeg2 + "</td><td>" + data[RootLevel0].TotalActive + "</td></tr>" +
                                "<tr><td>Inactive</td><td>" + data[RootLevel0].LeftNoneActive + "</td><td>" + data[RootLevel0].RightNoneActive + "</td><td>" + data[RootLevel0].TotalNonActive + "</td></tr>" +
                                "<tr><td>Total</td><td>" + data[RootLevel0].AllLeg1 + "</td><td>" + data[RootLevel0].AllLeg2 + "</td><td>" + data[RootLevel0].TotalActiveInActiveLeftRight + "</td></tr>" +
                                "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                                "</table></div></div></a>" +
                                "</span></span> ";

                            $('#DivRoot').empty();
                            $('#DivRoot').append(Root);
                            RootLeftRight(data[RootLevel0].MemId);

                        }
                    }
                }
                else {
                    alert("Invalid Login Id");
                    BindRoot();
                }

            }


        }
    });



}

//Root Left And Right 
function RootLeftRight(LeftRightId) {

    var RootLeft = '';
    var RootRight = '';
    $.ajax({

        url: '../../Admin/AssoicateTree/BindTree',
        data: { ParentId: LeftRightId },
        dataType: 'json',
        type: 'POST',
        async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {

            if (data != null && data.length > 0) {
                for (var RootLeftRight1 = 0; RootLeftRight1 < data.length; RootLeftRight1++) {



                    if (data[RootLeftRight1].Leg == 'L' && data[RootLeftRight1].MemId != "-1") {


                        RootLeft += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRight1].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].LoginId + "</span>" +
                            "<span class='id-name'><a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                            "<h4>" + data[RootLeftRight1].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRight1].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRight1].JoiningDate + "</span></h5>" +
                            "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                            "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                            "<tr><td>Active</td><td>" + data[RootLeftRight1].PermanentLeg1 + "</td><td>" + data[RootLeftRight1].PermanentLeg2 + "</td><td>" + data[RootLeftRight1].TotalActive + "</td></tr>" +
                            "<tr><td>Inactive</td><td>" + data[RootLeftRight1].LeftNoneActive + "</td><td>" + data[RootLeftRight1].RightNoneActive + "</td><td>" + data[RootLeftRight1].TotalNonActive + "</td></tr>" +
                            "<tr><td>Total</td><td>" + data[RootLeftRight1].AllLeg1 + "</td><td>" + data[RootLeftRight1].AllLeg2 + "</td><td>" + data[RootLeftRight1].TotalActiveInActiveLeftRight + "</td></tr>" +
                            "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                            "</table></div></div></a>" +
                            "</span></span>";

                        $('#divRootLeft').show();

                        $('#divRootLeft').empty();
                        $('#divRootLeft').append(RootLeft);
                        LeftRightLeft2(data[RootLeftRight1].MemId);

                    }
                    if (data[RootLeftRight1].Leg == 'L' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Register Now") {
                        RootLeft += "<a href='" + data[RootLeftRight1].Url + "' class='hover-link'><span class='tf-nc' target='_blank'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                            "<span class='id-name'></span></span></a>";
                        $('#divRootLeft').show();
                        $('#divRootLeft').empty();
                        $('#divRootLeft').append(RootLeft);
                        LeftRightLeft2(data[RootLeftRight1].MemId);

                    }


                    if (data[RootLeftRight1].Leg == 'R' && data[RootLeftRight1].MemId != "-1") {
                        RootRight += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRight1].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].LoginId + "</span> <span class='id-name'>" +
                            "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                            "<h4>" + data[RootLeftRight1].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRight1].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRight1].JoiningDate + "</span></h5>" +
                            "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                            "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                            "<tr><td>Active</td><td>" + data[RootLeftRight1].PermanentLeg1 + "</td><td>" + data[RootLeftRight1].PermanentLeg2 + "</td><td>" + data[RootLeftRight1].TotalActive + "</td></tr>" +
                            "<tr><td>Inactive</td><td>" + data[RootLeftRight1].LeftNoneActive + "</td><td>" + data[RootLeftRight1].RightNoneActive + "</td><td>" + data[RootLeftRight1].TotalNonActive + "</td></tr>" +
                            "<tr><td>Total</td><td>" + data[RootLeftRight1].AllLeg1 + "</td><td>" + data[RootLeftRight1].AllLeg2 + "</td><td>" + data[RootLeftRight1].TotalActiveInActiveLeftRight + "</td></tr>" +
                            "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                            "</table></div></div></a>" +
                            "</span></span>";

                        $('#DivRootRight').show();
                        $('#DivRootRight').empty();
                        $('#DivRootRight').append(RootRight);
                        RootRightRight(data[RootLeftRight1].MemId);

                    }
                    if (data[RootLeftRight1].Leg == 'R' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Register Now") {

                        RootRight += "<a href='" + data[RootLeftRight1].Url + "' class='hover-link'><span class='tf-nc' target='_blank'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                            "<span class='id-name'></span></span></a>";

                        $('#DivRootRight').show();
                        $('#DivRootRight').empty();
                        $('#DivRootRight').append(RootRight);
                        RootRightRight(data[RootLeftRight1].MemId);
                    }

                }
            }
            else {
                alert('No data found');
                BindRoot($('#txtsearchlogin').val());
            }
        }
    });
}
////Root Left Side 
function LeftRightLeft2(RootLeftRightLeft) {

    var divRootLeftLeft = '';
    var divRootLeftRight1 = '';

    $.ajax({

        url: '../../Admin/AssoicateTree/BindTree',
        data: { ParentId: RootLeftRightLeft },
        dataType: 'json',
        type: 'POST',
        async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {


            for (var RootLeftRightLeft2 = 0; RootLeftRightLeft2 < data.length; RootLeftRightLeft2++) {



                if (data[RootLeftRightLeft2].Leg == 'L' && data[RootLeftRightLeft2].MemId != "-1") {

                    divRootLeftLeft += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRightLeft2].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRightLeft2].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRightLeft2].LoginId + "\");'>" + data[RootLeftRightLeft2].LoginId + "</span> <span class='id-name'>" +
                        "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRightLeft2].LoginId + "\");'>" + data[RootLeftRightLeft2].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRightLeft2].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRightLeft2].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRightLeft2].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRightLeft2].PermanentLeg1 + "</td><td>" + data[RootLeftRightLeft2].PermanentLeg2 + "</td><td>" + data[RootLeftRightLeft2].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRightLeft2].LeftNoneActive + "</td><td>" + data[RootLeftRightLeft2].RightNoneActive + "</td><td>" + data[RootLeftRightLeft2].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRightLeft2].AllLeg1 + "</td><td>" + data[RootLeftRightLeft2].AllLeg2 + "</td><td>" + data[RootLeftRightLeft2].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";

                    $('#divRootLeftLeft').show();
                    $('#divRootLeftLeft').empty();
                    $('#divRootLeftLeft').append(divRootLeftLeft);
                    divRootLeftLeftLeft(data[RootLeftRightLeft2].MemId);


                }
                if (data[RootLeftRightLeft2].Leg == 'L' && data[RootLeftRightLeft2].MemId == "-1" && data[RootLeftRightLeft2].LoginId == "Register Now") {

                    divRootLeftLeft += "<a href='" + data[RootLeftRightLeft2].Url + "' class='hover-link' target='_blank'><span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRightLeft2].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#divRootLeftLeft').show();
                    $('#divRootLeftLeft').empty();
                    $('#divRootLeftLeft').append(divRootLeftLeft);
                    divRootLeftLeftLeft(data[RootLeftRightLeft2].MemId);
                }
                if (data[RootLeftRightLeft2].Leg == 'L' && data[RootLeftRightLeft2].MemId == "-1" && data[RootLeftRightLeft2].LoginId == "Open") {

                    divRootLeftLeft += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRightLeft2].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRightLeft2].Url + "' class='hover-link'></a></span></span>";

                    $('#divRootLeftLeft').show();
                    $('#divRootLeftLeft').empty();
                    $('#divRootLeftLeft').append(divRootLeftLeft);
                    divRootLeftLeftLeft(data[RootLeftRightLeft2].MemId);
                }

                if (data[RootLeftRightLeft2].Leg == 'R' && data[RootLeftRightLeft2].MemId != "-1") {
                    divRootLeftRight1 += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRightLeft2].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRightLeft2].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRightLeft2].LoginId + "\");'>" + data[RootLeftRightLeft2].LoginId + "</span> <span class='id-name'>" +
                        "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRightLeft2].LoginId + "\");'>" + data[RootLeftRightLeft2].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRightLeft2].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRightLeft2].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRightLeft2].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4>656565</h4><span>Business</span></div><table cellpadding='0' cellspacing='0'>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRightLeft2].PermanentLeg1 + "</td><td>" + data[RootLeftRightLeft2].PermanentLeg2 + "</td><td>" + data[RootLeftRightLeft2].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRightLeft2].LeftNoneActive + "</td><td>" + data[RootLeftRightLeft2].RightNoneActive + "</td><td>" + data[RootLeftRightLeft2].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRightLeft2].AllLeg1 + "</td><td>" + data[RootLeftRightLeft2].AllLeg2 + "</td><td>" + data[RootLeftRightLeft2].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";

                    $('#divRootLeftRight').show();
                    $('#divRootLeftRight').empty();

                    $('#divRootLeftRight').append(divRootLeftRight1);

                    divRootLeftRight(data[RootLeftRightLeft2].MemId);

                }

                if (data[RootLeftRightLeft2].Leg == 'R' && data[RootLeftRightLeft2].MemId == "-1" && data[RootLeftRightLeft2].LoginId == "Register Now") {

                    divRootLeftRight1 += "<a href='" + data[RootLeftRightLeft2].Url + "' class='hover-link' target='_blank'><span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRightLeft2].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#divRootLeftRight').show();
                    $('#divRootLeftRight').empty();
                    $('#divRootLeftRight').append(divRootLeftRight1);
                    divRootLeftRight(data[RootLeftRightLeft2].MemId);
                }
                if (data[RootLeftRightLeft2].Leg == 'R' && data[RootLeftRightLeft2].MemId == "-1" && data[RootLeftRightLeft2].LoginId == "Open") {

                    divRootLeftRight1 += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRightLeft2].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRightLeft2].Url + "' class='hover-link'></a></span></span>";

                    $('#divRootLeftRight').show();
                    $('#divRootLeftRight').empty();
                    $('#divRootLeftRight').append(divRootLeftRight1);
                    divRootLeftRight(data[RootLeftRightLeft2].MemId);
                }

            }




        }
    });

}

function divRootLeftLeftLeft(RootLeftRightLeftLeft3) {
    var divRootLeftLeftLeft = '';
    var divRootLeftLeftLeft1 = '';
    $.ajax({

        url: '../../Admin/AssoicateTree/BindTree',
        data: { ParentId: RootLeftRightLeftLeft3 },
        dataType: 'json',
        type: 'POST',
        async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {


            for (var RootLeftRightLeftLeft3 = 0; RootLeftRightLeftLeft3 < data.length; RootLeftRightLeftLeft3++) {



                if (data[RootLeftRightLeftLeft3].Leg == 'L' && data[RootLeftRightLeftLeft3].MemId != "-1") {

                    divRootLeftLeftLeft += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRightLeftLeft3].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRightLeftLeft3].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRightLeftLeft3].LoginId + "\");'>" + data[RootLeftRightLeftLeft3].LoginId + "</span> <span class='id-name'>" +
                        "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRightLeftLeft3].LoginId + "\");'>" + data[RootLeftRightLeftLeft3].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRightLeftLeft3].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRightLeftLeft3].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRightLeftLeft3].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRightLeftLeft3].PermanentLeg1 + "</td><td>" + data[RootLeftRightLeftLeft3].PermanentLeg2 + "</td><td>" + data[RootLeftRightLeftLeft3].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRightLeftLeft3].LeftNoneActive + "</td><td>" + data[RootLeftRightLeftLeft3].RightNoneActive + "</td><td>" + data[RootLeftRightLeftLeft3].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRightLeftLeft3].AllLeg1 + "</td><td>" + data[RootLeftRightLeftLeft3].AllLeg2 + "</td><td>" + data[RootLeftRightLeftLeft3].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";


                    $('#divRootLeftLeftLeft').show();
                    $('#divRootLeftLeftLeft').empty();
                    $('#divRootLeftLeftLeft').append(divRootLeftLeftLeft);
                }

                if (data[RootLeftRightLeftLeft3].Leg == 'L' && data[RootLeftRightLeftLeft3].MemId == "-1" && data[RootLeftRightLeftLeft3].LoginId == "Register Now") {

                    divRootLeftLeftLeft += "<a href='" + data[RootLeftRightLeftLeft3].Url + "' class='hover-link' Target='_blank'><span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRightLeftLeft3].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#divRootLeftLeftLeft').show();
                    $('#divRootLeftLeftLeft').empty();
                    $('#divRootLeftLeftLeft').append(divRootLeftLeftLeft);
                    // divRootLeftRight(data[RootLeftRightLeftLeft3].MemId);
                }
                if (data[RootLeftRightLeftLeft3].Leg == 'L' && data[RootLeftRightLeftLeft3].MemId == "-1" && data[RootLeftRightLeftLeft3].LoginId == "Open") {

                    divRootLeftLeftLeft += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRightLeftLeft3].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRightLeftLeft3].Url + "' class='hover-link'></a></span></span>";

                    $('#divRootLeftLeftLeft').show();
                    $('#divRootLeftLeftLeft').empty();
                    $('#divRootLeftLeftLeft').append(divRootLeftLeftLeft);
                    // divRootLeftRight(data[RootLeftRightLeftLeft3].MemId);
                }

                if (data[RootLeftRightLeftLeft3].Leg == 'R' && data[RootLeftRightLeftLeft3].MemId != "-1") {
                    divRootLeftLeftLeft1 += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRightLeftLeft3].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRightLeftLeft3].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRightLeftLeft3].LoginId + "\");'>" + data[RootLeftRightLeftLeft3].LoginId + "</span> <span class='id-name'>" +
                        "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRightLeftLeft3].LoginId + "\");'>" + data[RootLeftRightLeftLeft3].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRightLeftLeft3].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRightLeftLeft3].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRightLeftLeft3].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'><tr>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRightLeftLeft3].PermanentLeg1 + "</td><td>" + data[RootLeftRightLeftLeft3].PermanentLeg2 + "</td><td>" + data[RootLeftRightLeftLeft3].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRightLeftLeft3].LeftNoneActive + "</td><td>" + data[RootLeftRightLeftLeft3].RightNoneActive + "</td><td>" + data[RootLeftRightLeftLeft3].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRightLeftLeft3].AllLeg1 + "</td><td>" + data[RootLeftRightLeftLeft3].AllLeg2 + "</td><td>" + data[RootLeftRightLeftLeft3].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";

                    $('#divRootLeftLeftLeftRight').show();
                    $('#divRootLeftLeftLeftRight').empty();
                    $('#divRootLeftLeftLeftRight').append(divRootLeftLeftLeft1);

                }
                if (data[RootLeftRightLeftLeft3].Leg == 'R' && data[RootLeftRightLeftLeft3].MemId == "-1" && data[RootLeftRightLeftLeft3].LoginId == "Register Now") {

                    divRootLeftLeftLeft1 += "<a href='" + data[RootLeftRightLeftLeft3].Url + "' class='hover-link' target='_blank'><span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRightLeftLeft3].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#divRootLeftLeftLeftRight').show();
                    $('#divRootLeftLeftLeftRight').empty();
                    $('#divRootLeftLeftLeftRight').append(divRootLeftLeftLeft1);
                    // divRootLeftRight(data[RootLeftRightLeftLeft3].MemId);
                }
                if (data[RootLeftRightLeftLeft3].Leg == 'R' && data[RootLeftRightLeftLeft3].MemId == "-1" && data[RootLeftRightLeftLeft3].LoginId == "Open") {

                    divRootLeftLeftLeft1 += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRightLeftLeft3].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRightLeftLeft3].Url + "' class='hover-link'></a></span></span>";

                    $('#divRootLeftLeftLeftRight').show();
                    $('#divRootLeftLeftLeftRight').empty();
                    $('#divRootLeftLeftLeftRight').append(divRootLeftLeftLeft1);
                    // divRootLeftRight(data[RootLeftRightLeftLeft3].MemId);
                }
            }

        }
    });

}


function divRootLeftRight(divRootLeftRight) {
    var divRootLeftLeft = '';
    var divRootLeftRight1 = '';
    $.ajax({

        url: '../../Admin/AssoicateTree/BindTree',
        data: { ParentId: divRootLeftRight },
        dataType: 'json',
        type: 'POST',
        async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {


            for (var RootLeftRight2 = 0; RootLeftRight2 < data.length; RootLeftRight2++) {



                if (data[RootLeftRight2].Leg == 'L' && data[RootLeftRight2].MemId != "-1") {
                    divRootLeftLeft += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRight2].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRight2].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRight2].LoginId + "\");'>" + data[RootLeftRight2].LoginId + "</span> <span class='id-name'>" +
                        "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRight2].LoginId + "\");'>" + data[RootLeftRight2].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRight2].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRight2].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRight2].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRight2].PermanentLeg1 + "</td><td>" + data[RootLeftRight2].PermanentLeg2 + "</td><td>" + data[RootLeftRight2].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRight2].LeftNoneActive + "</td><td>" + data[RootLeftRight2].RightNoneActive + "</td><td>" + data[RootLeftRight2].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRight2].AllLeg1 + "</td><td>" + data[RootLeftRight2].AllLeg2 + "</td><td>" + data[RootLeftRight2].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";


                    $('#divRootLeftRightLeft').show();
                    $('#divRootLeftRightLeft').empty();
                    $('#divRootLeftRightLeft').append(divRootLeftLeft);

                }
                if (data[RootLeftRight2].Leg == 'L' && data[RootLeftRight2].MemId == "-1" && data[RootLeftRight2].LoginId == "Register Now") {

                    divRootLeftLeft += "<a href='" + data[RootLeftRight2].Url + "' class='hover-link' target='_blank'><span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight2].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#divRootLeftRightLeft').show();
                    $('#divRootLeftRightLeft').empty();
                    $('#divRootLeftRightLeft').append(divRootLeftLeft);
                    // divRootLeftRight(data[RootLeftRightLeftLeft3].MemId);
                }
                if (data[RootLeftRight2].Leg == 'L' && data[RootLeftRight2].MemId == "-1" && data[RootLeftRight2].LoginId == "Open") {

                    divRootLeftLeft += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight2].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRight2].Url + "' class='hover-link'></a></span></span>";

                    $('#divRootLeftRightLeft').show();
                    $('#divRootLeftRightLeft').empty();
                    $('#divRootLeftRightLeft').append(divRootLeftLeft);
                    // divRootLeftRight(data[RootLeftRightLeftLeft3].MemId);
                }

                if (data[RootLeftRight2].Leg == 'R' && data[RootLeftRight2].MemId != "-1") {

                    divRootLeftRight1 += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRight2].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRight2].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRight2].LoginId + "\");'>" + data[RootLeftRight2].LoginId + "</span> <span class='id-name'>" +
                        "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRight2].LoginId + "\");'>" + data[RootLeftRight2].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRight2].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRight2].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRight2].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRight2].PermanentLeg1 + "</td><td>" + data[RootLeftRight2].PermanentLeg2 + "</td><td>" + data[RootLeftRight2].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRight2].LeftNoneActive + "</td><td>" + data[RootLeftRight2].RightNoneActive + "</td><td>" + data[RootLeftRight2].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRight2].AllLeg1 + "</td><td>" + data[RootLeftRight2].AllLeg2 + "</td><td>" + data[RootLeftRight2].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";

                    $('#divRootLeftRightRight').show();
                    $('#divRootLeftRightRight').empty();
                    $('#divRootLeftRightRight').append(divRootLeftRight1);
                }
                if (data[RootLeftRight2].Leg == 'R' && data[RootLeftRight2].MemId == "-1" && data[RootLeftRight2].LoginId == "Register Now") {

                    divRootLeftRight1 += "<a href='" + data[RootLeftRight2].Url + "' class='hover-link' target='_blank'><span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight2].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#divRootLeftRightRight').show();
                    $('#divRootLeftRightRight').empty();
                    $('#divRootLeftRightRight').append(divRootLeftRight1);
                    // divRootLeftRight(data[RootLeftRightLeftLeft3].MemId);
                }
                if (data[RootLeftRight2].Leg == 'R' && data[RootLeftRight2].MemId == "-1" && data[RootLeftRight2].LoginId == "Open") {

                    divRootLeftRight1 += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight2].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRight2].Url + "' class='hover-link'></a></span></span>";

                    $('#divRootLeftRightRight').show();
                    $('#divRootLeftRightRight').empty();
                    $('#divRootLeftRightRight').append(divRootLeftRight1);
                    // divRootLeftRight(data[RootLeftRightLeftLeft3].MemId);
                }

            }

        }
    });

}

////Root Right Side 

function RootRightRight(LeftRightId) {

    var DivRootRightLeft = '';
    var DivRootRightRight1 = '';
    $.ajax({

        url: '../../Admin/AssoicateTree/BindTree',
        data: { ParentId: LeftRightId },
        dataType: 'json',
        type: 'POST',
        async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {


            for (var RootLeftRight1 = 0; RootLeftRight1 < data.length; RootLeftRight1++) {



                if (data[RootLeftRight1].Leg == 'L' && data[RootLeftRight1].MemId != "-1") {


                    DivRootRightLeft += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRight1].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'><a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRight1].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRight1].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRight1].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRight1].PermanentLeg1 + "</td><td>" + data[RootLeftRight1].PermanentLeg1 + "</td><td>" + data[RootLeftRight1].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRight1].LeftNoneActive + "</td><td>" + data[RootLeftRight1].RightNoneActive + "</td><td>" + data[RootLeftRight1].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRight1].AllLeg1 + "</td><td>" + data[RootLeftRight1].AllLeg2 + "</td><td>" + data[RootLeftRight1].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";

                    $('#DivRootRightLeft').show();
                    $('#DivRootRightLeft').empty();
                    $('#DivRootRightLeft').append(DivRootRightLeft);
                    DivRootRightRightLeft(data[RootLeftRight1].MemId);
                    // LeftRightLeft2(data[RootLeftRight1].MemId);

                }
                if (data[RootLeftRight1].Leg == 'L' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Register Now") {

                    DivRootRightLeft += "<a href='" + data[RootLeftRight1].Url + "' class='hover-link' target='_blank'><span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#DivRootRightLeft').show();
                    $('#DivRootRightLeft').empty();
                    $('#DivRootRightLeft').append(DivRootRightLeft);
                    DivRootRightRightLeft(data[RootLeftRight1].MemId);
                }
                if (data[RootLeftRight1].Leg == 'L' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Open") {

                    DivRootRightLeft += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRight1].Url + "' class='hover-link'></a></span></span>";

                    $('#DivRootRightLeft').show();
                    $('#DivRootRightLeft').empty();
                    $('#DivRootRightLeft').append(DivRootRightLeft);
                    DivRootRightRightLeft(data[RootLeftRight1].MemId);
                }
                if (data[RootLeftRight1].Leg == 'R' && data[RootLeftRight1].MemId != "-1") {
                    DivRootRightRight1 += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRight1].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].LoginId + "</span> <span class='id-name'>" +
                        "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRight1].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRight1].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRight1].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRight1].PermanentLeg1 + "</td><td>" + data[RootLeftRight1].PermanentLeg2 + "</td><td>" + data[RootLeftRight1].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRight1].LeftNoneActive + "</td><td>" + data[RootLeftRight1].RightNoneActive + "</td><td>" + data[RootLeftRight1].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRight1].AllLeg1 + "</td><td>" + data[RootLeftRight1].AllLeg2 + "</td><td>" + data[RootLeftRight1].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";

                    $('#DivRootRightRight').show();
                    $('#DivRootRightRight').empty();
                    $('#DivRootRightRight').append(DivRootRightRight1);
                    DivRootRightRightRight(data[RootLeftRight1].MemId);


                }
                if (data[RootLeftRight1].Leg == 'R' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Register Now") {

                    DivRootRightRight1 += "<a href='" + data[RootLeftRight1].Url + "' class='hover-link' target='_blank'><span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#DivRootRightRight').show();
                    $('#DivRootRightRight').empty();
                    $('#DivRootRightRight').append(DivRootRightRight1);
                    DivRootRightRightRight(data[RootLeftRight1].MemId);
                }

                if (data[RootLeftRight1].Leg == 'R' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Open") {

                    DivRootRightRight1 += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRight1].Url + "' class='hover-link'></a></span></span>";

                    $('#DivRootRightRight').show();
                    $('#DivRootRightRight').empty();
                    $('#DivRootRightRight').append(DivRootRightRight1);
                    DivRootRightRightRight(data[RootLeftRight1].MemId);
                }

            }



        }
    });
}


function DivRootRightRightLeft(DivRootRightRight) {
    var Right = '';
    var Left = '';
    $.ajax({

        url: '../../Admin/AssoicateTree/BindTree',
        data: { ParentId: DivRootRightRight },
        dataType: 'json',
        type: 'POST',
        async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {


            for (var RootLeftRight1 = 0; RootLeftRight1 < data.length; RootLeftRight1++) {



                if (data[RootLeftRight1].Leg == 'L' && data[RootLeftRight1].MemId != "-1") {
                    Left += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRight1].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].LoginId + "</span> <span class='id-name'>" +
                        "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRight1].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRight1].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRight1].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRight1].PermanentLeg1 + "</td><td>" + data[RootLeftRight1].PermanentLeg2 + "</td><td>" + data[RootLeftRight1].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRight1].LeftNoneActive + "</td><td>" + data[RootLeftRight1].RightNoneActive + "</td><td>" + data[RootLeftRight1].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRight1].AllLeg1 + "</td><td>" + data[RootLeftRight1].AllLeg2 + "</td><td>" + data[RootLeftRight1].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";

                    $('#DivRootRightLeftLeft').show();
                    $('#DivRootRightLeftLeft').empty();
                    $('#DivRootRightLeftLeft').append(Left);

                }
                if (data[RootLeftRight1].Leg == 'L' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Register Now") {

                    Left += "<a href='" + data[RootLeftRight1].Url + "' class='hover-link' target='_blank'><span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#DivRootRightLeftLeft').show();
                    $('#DivRootRightLeftLeft').empty();
                    $('#DivRootRightLeftLeft').append(Left);

                }
                if (data[RootLeftRight1].Leg == 'L' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Open") {

                    Left += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRight1].Url + "' class='hover-link'></a></span></span>";

                    $('#DivRootRightLeftLeft').show();
                    $('#DivRootRightLeftLeft').empty();
                    $('#DivRootRightLeftLeft').append(Left);

                }
                if (data[RootLeftRight1].Leg == 'R' && data[RootLeftRight1].MemId != "-1") {

                    Right += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRight1].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].LoginId + "</span> <span class='id-name'>" +
                        "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRight1].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRight1].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRight1].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRight1].PermanentLeg1 + "</td><td>" + data[RootLeftRight1].PermanentLeg2 + "</td><td>" + data[RootLeftRight1].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRight1].LeftNoneActive + "</td><td>" + data[RootLeftRight1].RightNoneActive + "</td><td>" + data[RootLeftRight1].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRight1].AllLeg1 + "</td><td>" + data[RootLeftRight1].AllLeg2 + "</td><td>" + data[RootLeftRight1].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";

                    $('#DivRootRightLeftRight').show();
                    $('#DivRootRightLeftRight').empty();
                    $('#DivRootRightLeftRight').append(Right);
                }
                if (data[RootLeftRight1].Leg == 'R' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Register Now") {

                    Right += "<a href='" + data[RootLeftRight1].Url + "' class='hover-link' target='_blank'><span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#DivRootRightLeftRight').show();
                    $('#DivRootRightLeftRight').empty();
                    $('#DivRootRightLeftRight').append(Right);

                }
                if (data[RootLeftRight1].Leg == 'R' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Open") {

                    Right += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRight1].Url + "' class='hover-link'></a></span></span>";

                    $('#DivRootRightLeftRight').show();
                    $('#DivRootRightLeftRight').empty();
                    $('#DivRootRightLeftRight').append(Right);

                }

            }

        }
    });



}

function DivRootRightRightRight(DivRootRightRight) {
    var Right123 = '';
    var Left = '';
    $.ajax({

        url: '../../Admin/AssoicateTree/BindTree',
        data: { ParentId: DivRootRightRight },
        dataType: 'json',
        type: 'POST',
        //async: false,
        containtType: 'application/json;charset=utf-8',
        success: function (data) {


            for (var RootLeftRight1 = 0; RootLeftRight1 < data.length; RootLeftRight1++) {



                if (data[RootLeftRight1].Leg == 'L' && data[RootLeftRight1].MemId != "-1") {

                    Left += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRight1].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].LoginId + "</span> <span class='id-name'>" +
                        "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRight1].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRight1].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRight1].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRight1].PermanentLeg1 + "</td><td>" + data[RootLeftRight1].PermanentLeg2 + "</td><td>" + data[RootLeftRight1].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRight1].LeftNoneActive + "</td><td>" + data[RootLeftRight1].RightNoneActive + "</td><td>" + data[RootLeftRight1].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRight1].AllLeg1 + "</td><td>" + data[RootLeftRight1].AllLeg2 + "</td><td>" + data[RootLeftRight1].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";

                    $('#DivRootRightRightLeft').show();
                    $('#DivRootRightRightLeft').empty();
                    $('#DivRootRightRightLeft').append(Left);

                }
                if (data[RootLeftRight1].Leg == 'L' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Register Now") {

                    Left += "<a href='" + data[RootLeftRight1].Url + "' class='hover-link'><span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#DivRootRightRightLeft').show();
                    $('#DivRootRightRightLeft').empty();
                    $('#DivRootRightRightLeft').append(Left);

                }
                if (data[RootLeftRight1].Leg == 'L' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Open") {

                    Left += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRight1].Url + "' class='hover-link'></a></span></span>";

                    $('#DivRootRightRightLeft').show();
                    $('#DivRootRightRightLeft').empty();
                    $('#DivRootRightRightLeft').append(Left);

                }
                if (data[RootLeftRight1].Leg == 'R' && data[RootLeftRight1].MemId != "-1") {

                    Right123 += "<span class='tf-nc'><span class='id-icon'> <img src='" + data[RootLeftRight1].ActiveInActiveIcon + "' alt='' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'></span> <span class='id' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].LoginId + "</span> <span class='id-name'>" +
                        "<a href='javascript:void(0);' class='hover-link' onclick='BindRoot(\"" + data[RootLeftRight1].LoginId + "\");'>" + data[RootLeftRight1].MemberName + "<div class='hover-data'><div class='hover-data-content'>" +
                        "<h4>" + data[RootLeftRight1].MemberName + "</h4><h5>Sponsor Id : <span>" + data[RootLeftRight1].SponsorLoginId + "</span></h5><h5>Joining On : <span>" + data[RootLeftRight1].JoiningDate + "</span></h5>" +
                        "<div class='busin-id'><h4></h4><span></span></div><table cellpadding='0' cellspacing='0'>" +
                        "<tr><td>Type</td><td>Left</td><td>Right</td><td>Total</td></tr>" +
                        "<tr><td>Active</td><td>" + data[RootLeftRight1].PermanentLeg1 + "</td><td>" + data[RootLeftRight1].PermanentLeg2 + "</td><td>" + data[RootLeftRight1].TotalActive + "</td></tr>" +
                        "<tr><td>Inactive</td><td>" + data[RootLeftRight1].LeftNoneActive + "</td><td>" + data[RootLeftRight1].RightNoneActive + "</td><td>" + data[RootLeftRight1].TotalNonActive + "</td></tr>" +
                        "<tr><td>Total</td><td>" + data[RootLeftRight1].AllLeg1 + "</td><td>" + data[RootLeftRight1].AllLeg2 + "</td><td>" + data[RootLeftRight1].TotalActiveInActiveLeftRight + "</td></tr>" +
                        "<tr><td>BV</td><td>0</td><td>0</td><td>0</td></tr>" +
                        "</table></div></div></a>" +
                        "</span></span>";

                    $('#DivRootRightRightRight1').show();
                    $('#DivRootRightRightRight1').empty();
                    $('#DivRootRightRightRight1').append(Right123);
                }
                if (data[RootLeftRight1].Leg == 'R' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Register Now") {

                    Right123 += "<a href='" + data[RootLeftRight1].Url + "' class='hover-link'><span class='tf-nc' target='_blank'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'></span></span></a>";

                    $('#DivRootRightRightRight1').show();
                    $('#DivRootRightRightRight1').empty();
                    $('#DivRootRightRightRight1').append(Right123);

                }
                if (data[RootLeftRight1].Leg == 'R' && data[RootLeftRight1].MemId == "-1" && data[RootLeftRight1].LoginId == "Open") {


                    Right123 += "<span class='tf-nc'><span class='id-icon'> <img src='" + ImageUrl + "' alt=''></span> <span class='id'>" + data[RootLeftRight1].LoginId + "</span>" +
                        "<span class='id-name'><a href='" + data[RootLeftRight1].Url + "' class='hover-link'></a></span></span>";

                    $('#DivRootRightRightRight1').show();
                    $('#DivRootRightRightRight1').empty();
                    $('#DivRootRightRightRight1').append(Right123);

                }

            }

        }
    });



}
function UpLine() {


    if ($('#txtParentId').val() != null) {

        var model =
        {
            LoginId: $('#txtParentId').val()
        };
        $.ajax({
            url: '../../Admin/AssoicateTree/SearchUpline',
            data: JSON.stringify(model),
            dataType: 'json',
            type: 'POST',
            async: false,
            contentType: 'application/json;charset=utf-8',
            success: function (data) {

                BindRoot(data.LoginId);
            },
            error: function (error) { }


        });
    }
    else {
        alert("Invalid Login Id");
    }


}

