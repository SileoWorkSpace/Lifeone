



$(document).ready(function () {

    //var hashValue
    //var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    //var urlparam = url[0].split('=');
    //if (urlparam[0] == 'UID') {
    //    hashValue = urlparam[1];
    //}
    //if (hashValue != undefined) {
    //    ScreenId = hashValue;
    //    /// GL Clearance
    //}


    GetPClbMemebersList();

});






function GetPClbMemebersList() {
    //  debugger
  

    $.ajax({
        type: "Get",
        url: "/Premimumclub/PremiumClubMembersList/",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            //   debugger
            var table = $('#tblPremimumclubmemberList').DataTable();
            table.destroy();
            var data1 = [];
            for (var i = 0; i < data.length; i++) {
                data1.push([
                    parseInt(i + 1),
                
                
                    data[i].LoginId,
                
                    data[i].DisplayName,
                    data[i].Mobile,
                    data[i].DOJ,
                    data[i].DOA,
                    data[i].PackageName,
                    data[i].NoofSp
                   
                ]);

            }
            $("#tblPremimumclubmemberList").DataTable({
                "columnDefs": [
                  

                ],
                destroy: true,
                data: data1,
                deferRender: true,
                scrollY: 500,
                scrollX: true,
                scrollCollapse: true,
                // scroller: true,
                paging: false,
                select: true
            })
          
        },
        error: function (response, y, z) {

        
         
            alert("Message: " );
        }
    });
   
}



