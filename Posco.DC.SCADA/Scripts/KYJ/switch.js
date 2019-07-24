$(document).ready(function () {
var switchList = {
    "data":[
           carr,carr1,carr2,carr3
        ]
};

function switchInfo() {
    
    $.ajax({
        url: "/EnergySys/Switch",
        type: "GET",
        success: function (data) {
            var dataJson = JSON.parse(data);

            var arr = [];
            var arr1 = [];
            var arr2 = [];
            var arr3 = [];
            var arr4 = [];
            var count = 1;

            var a;
            $.each(dataJson, function (index, item) {

                if (index < 6) {
                    arr[index] = { "id": count.toString(), "status": item }
                    a = index;
                }
                else if (5 < index && index < 12) {
                    arr1[index - 6] = { "id": count.toString(), "status": item }
                }
                else if (11 < index && index < 18) {
                    arr2[index - 12] = { "id": count.toString(), "status": item }
                }
                else if (index > 17) {
                    arr3[index - 18] = { "id": count.toString(), "status": item }
                }
                count++;
            })
            var switchList2 = {
                "data":[arr,arr1,arr2,arr3]
            }
            switchList = switchList2;
        }
    })
    var strHtml = '';
    var data = switchList.data;
    
    var line = 1;
    for (index in data) {
        strHtml += '<tr class="trTitle"><td class="numTitle">#'+(line++)+'</td>';
        for (item  in data[index]) {

            strHtml += '<td id="switch'+data[index][item].id+'" class="'+switchStatus(data[index][item].status)+'"></td>' ;

        }
        strHtml += '</tr>';

    }
    $('#switchTable').html(strHtml)
}
function switchStatus(status)
{
    switch (status)
    {
        case "0":
            return 'labelOff';
        case "1":
            return  'labelOn';
        case "2":
            return  'unKnown';
    }
}
function variety(id,status) {
    $("#switch"+id).html(switchStatus(status))
}
variety(7, 2);
switchInfo();
setInterval(switchInfo, 1000*300);
})
   //var switchList = {
        //    "data": [
        //           carr, carr1, carr2, carr3
        //    ]
        //};
        //var strHtml = '';
        //var data = switchList.data;

        //var line = 1;
        //for (index in data) {
        //    strHtml += '<tr class="trTitle"><td class="numTitle">#' + (line++) + '</td>';

        //    for (item in data[index]) {

        //        if (data[index][item].status == "1")
        //            strHtml += '<td id="switch' + data[index][item].id + '" class="numTitle"><img src="/Content/KYJ/images/on.png" /></td>';
        //        else if (data[index][item].status == "0") {
        //            strHtml += '<td id="switch' + data[index][item].id + '" class="numTitle"><img src="/Content/KYJ/images/off.png" /></td>';
        //        }
        //        else {
        //            strHtml += '<td id="switch' + data[index][item].id + '" class="numTitle"></td>';
        //        }
        //    }
        //    strHtml += '</tr>';

        //}
//$('#switchTable').html(strHtml);
//var carr = []; var carr1 = []; var carr2 = []; var carr3 = [];
//var count = 1;
//$.each(JSON.parse(str.swi), function (index, item) {
//    if (index < 6) {
//        carr[index] = { "id": count.toString(), "status": item }
//    }
//    else if (5 < index && index < 12) {
//        carr1[index - 6] = { "id": count.toString(), "status": item }
//    }

//    else if (11 < index && index < 18) {
//        carr2[index - 12] = { "id": count.toString(), "status": item }
//    }
//    else if (index > 17) {
//        carr3[index - 18] = { "id": count.toString(), "status": item }
//    }
//    count++;
//})
 //<!-- 开关机情况 -->
 //           @*<div class="optim">
 //               <div class="title">
 //                   <img src="~/Content/KYJ/images/icon07.png" />
 //                   开关机情况
 //               </div>
 //               <div class="onOff">
 //                   <table border="0" id="">
 //                       <tr>
 //                           <td class="numTitle"></td>
 //                           <td class="numTitle">1#</td>
 //                           <td class="numTitle">2#</td>
 //                           <td class="numTitle">3#</td>
 //                           <td class="numTitle">4#</td>
 //                           <td class="numTitle">5#</td>
 //                           <td class="numTitle">6#</td>
 //                           <td class="numTitle">7#</td>
 //                       </tr>
 //                   </table>
 //                   <table border="0" id="switchTable"></table>
 //               </div>
 //           </div>*@