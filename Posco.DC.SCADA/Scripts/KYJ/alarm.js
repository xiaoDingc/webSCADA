//var int =  = self.setInterval("al("clock()", 1000);
        var int = self.setInterval("clock()", 5000);
        var numb = 0;

        function clock() {
            numb += 1
            var htm = "<tr><td>您好，1#空压站3#空压机温度过高异常显示" + numb + "</td></tr>"
            $("#alarm").prepend(htm);
        }

        //<!-- 报警记录 -->
        //          @*<div class="news_report">
        //                  <div class="title">
        //                      <img src="~/Content/KYJ/images/icon05.png" />
        //                      报警记录
        //                  </div>
        //                  <div class="news_list" id="alarm">
        //                      <table>
        //                          <tr>
        //                              <td>
        //                                   <img src="./images/new_list02.png"/> 您好，1#空压站3#空压机温度过高异常显示0
        //                              </td>
        //                          </tr>
        //                      </table>
        //                  </div>
                      //</div>*@