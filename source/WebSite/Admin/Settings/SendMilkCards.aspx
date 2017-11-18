<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SimplePage.Master" AutoEventWireup="true" CodeBehind="SendMilkCards.aspx.cs" Inherits="Hidistro.UI.Web.Admin.settings.SendMilkCards" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>

<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/utility/skins/blue.css" type="text/css" media="screen" />

    <style>
        textarea {
            overflow: auto;
        }
        .col-xs-9 {
    width: 100%;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <form id="formSendMilkCard" runat="server">
        <div class="manualreleasebox boxsize" style="margin-left:97px;margin-top:36px">
            <span class="title">请指会员用户</span>
                <span class="title" id="spanCart"  style="margin-left:380px"></span>
            <div class="form-horizontal" >
                <div class="form-group mt10">
                    <div class="col-xs-9" id="divUserList">
                    <p style="text-align:center;width:500px" >未选择用户</p>
                     
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group" >
            <label for="inputEmail3" class="col-xs-2 control-label"></label>
            <div class="col-xs-10" style="margin-left: 20rem; margin-top: 3rem;">
                <asp:Button class="btn btn-primary" id="btnSend"  runat="server" Text="确定发送" OnClick="btnSend_Click"  OnClientClick="return sendCart()"/>
                <input   class="btn btn-primary" style="    background-color: #5cb85c;border-color: #4cae4c;" type="button" onclick="selectMember()" value="选择用户"/>
            </div>
        </div>
        <div  ></div>


         
        <input  type="hidden" id="hidUserId" runat="server" />
    </form>

    <script>

        function selectMember() {
            DialogFrame("../shop/usersselect.aspx?page=SendMilkCard", "选择用户", 580, 390, GetSelectedUserList);
        }
        function GetSelectedUserList() {
            var data = "posttype=getselecteduserbyCart&t=" + (new Date()).getTime();
            $.ajax({
                url: "SendMilkCards.aspx",
                type: "post",
                data: data,
                datatype: "json",
                success: function (json) {
                    if (json.success == "1") {
                        //json.icount
                        var dataLength = json.userlist.length;
                  
                        if (dataLength > 0) {
                            var HidUserId = "";
                            $("#divUserList").html("");
                            $("#divUserList").html("<table  class='mt10 table table-bordered table-hover'><thead><tr><th>昵称</th><th>手机</th><th>用户名</th></tr></thead><tbody id='tbUserList'></tbody></table>");
                            for (var i = 0; i < dataLength; i++) {
                                HidUserId += json.userlist[i].userid + ",";
                                var html = "<tr><td>" + json.userlist[i].name + "</td><td>" + json.userlist[i].tel + "</td><td>" + json.userlist[i].bindname + "</td></tr>";
                                $("#tbUserList").append(html);
                            }
                            $("#ctl00_ContentPlaceHolder1_hidUserId").val(HidUserId);
                            var html = "<tr><td colspan='3'>总共选择了<span class='red'>" + json.icount + "</span>个用户</td></tr>";
                            $("#tbUserList").append(html);
                        }
                        else
                        {
                            location.reload();
                        }
                    } else {
                        var html = "<tr><td colspan='3'>未选择用户</td></tr>";
                        $("#tbUserList").append(html);
                    }
                }
            });
        }
        function sendCart() {
            var cardIDList = getParam("cardids").split(',');
            var userId = $("#ctl00_ContentPlaceHolder1_hidUserId").val();
            userId = userId.substring(0, userId.length - 1).split(',');
            if (userId == "")
            {
                alert("未选择任何用户");
                return false;
            }
            if (parseInt(cardIDList.length) != parseInt(userId.length))
            {
                alert("奶卡数量与用户不匹配");
                return false;
            }
        }
        $(function () {
            var cardIDList = getParam("cardids").split(',');
            $("#spanCart").html("一共" + cardIDList.length + "奶卡")
        })
    </script>

</asp:Content>
