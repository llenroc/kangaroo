<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminNew.Master" AutoEventWireup="true" CodeBehind="EditCard.aspx.cs" Inherits="Hidistro.UI.Web.Admin.Card.EditCard" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="page-header">
            <h2 id="EditTitle" runat="server"><%=htmlOperatorName %>会员卡</h2>
   </div>

    <form runat="server">
          <div class="form-horizontal" id="Distributorform">

                <div class="form-group">
                    <label class="col-xs-2 pad resetSize control-label" for="pausername"><em>*</em>&nbsp;&nbsp;所属用户：</label>
                    <div class="form-inline col-xs-9 pt3">
                        <asp:TextBox   ClientIDMode="Static"   CssClass="form-control  inputw120" ID="txtUser"     oninput="inputchange(this)"  onfocus="inputgetfocus(this)"   onblur="inputlosefocus(this)" runat="server" />
                        <small>动态检索，输入用户手机号或者用户名后点击对应项</small>
                        <ul id="bcul"  style="display:none">
                        <asp:Repeater id="rptUser" runat="server" >
                            <ItemTemplate>
                                <li><span name="<%#Eval("UserId") %>" username="<%#Eval("UserName") %>" onclick="selectde(this)" >用户 : <%#Eval("UserName") %>  手机号 :  <%#Eval("CellPhone") %></span></li>
                                <input type="hidden" id="hidFilialeId" />
                            </ItemTemplate>
                        </asp:Repeater>
                        </ul>
                    </div>
                    
                </div>

                <div class="form-group">
                        <label for="inputEmail3" class="col-xs-2 control-label"><em>*</em>卡号：</label>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control  inputw120"></asp:TextBox>
                        </div>
                </div>
                <div class="form-group">
                        <label for="inputEmail3" class="col-xs-2 control-label"><em>*</em>卡片类型：</label>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="DDLCardType" runat="server"></asp:DropDownList>
                        </div>
                </div>
                <div class="form-group">
                        <label for="inputEmail3" class="col-xs-2 control-label"><em>*</em>初始余额：</label>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtBalance" runat="server" CssClass="form-control  inputw120"></asp:TextBox>
                        </div>
                </div>
                <div class="form-group">
                        <label for="inputEmail3" class="col-xs-2 control-label">收银来源：</label>
                        <div class="col-xs-3">
                            <asp:DropDownList ID="DDLMoneyFrom" runat="server" >
                                <asp:ListItem>现金</asp:ListItem>
                                <asp:ListItem>支付宝</asp:ListItem>
                                <asp:ListItem>微信</asp:ListItem>
                                <asp:ListItem>美团</asp:ListItem>
                                <asp:ListItem>大众点评</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                </div>
                <div class="form-group">
                        <label for="inputEmail3" class="col-xs-2 control-label">备注信息：</label>
                        <div class="col-xs-3">
                            <asp:TextBox ID="txtMemo" runat="server" CssClass="form-control  inputw120"></asp:TextBox>
                        </div>
                </div>


               <div class="form-group">
                        <div class="col-xs-offset-2 col-xs-10">
                             <asp:Button ID="btnEdit" runat="server" Text="确 定"  CssClass="btn btn-success" />
                        </div>
                 </div>
      </div>
        <input type="hidden" id="hidUserId" runat="server" clientidmode="static" />
    </form>
        <script>

            function inputlosefocus(e) {
                setTimeout(function () {
                    document.getElementById("bcul").style.display = "none";
                }, 200)
            }
            function inputchange(e) {
                if (!$(e).val()) return;
                var li = document.getElementById("bcul").children;
                for (var i = 0; i < li.length; i++) {
                    if (li[i].innerText.toLowerCase().indexOf($(e).val().toLowerCase()) >= 0) {
                        document.getElementById("bcul").style.display = "block";
                        li[i].style.display = "block";
                    }
                    else {
                        li[i].style.display = "none";
                    }
                }
            }
            //文本框得到焦点
            function inputgetfocus(e) {
                inputchange(e)
            }
            function selectde(e) {
                var userid = $(e).attr("name");
                var username = $(e).attr("username");
                $("#hidUserId").val(userid);
                $("#txtUser").val(username);
                document.getElementById("bcul").style.display = "none";
            }
            $(function () {
                $("#Distributorform").formvalidation({
                    //'submit': '#ctl00_ContentPlaceHolder1_PassCheck',
                    'ctl00$ContentPlaceHolder1$hidUserId': {
                        validators: {
                            notEmpty: {
                                message: '不能为空'
                            }
                        }, regexp: {
                            regexp: /^\d+(\.\d+)?$/,
                            message: '数据类型错误，只能输入实数型数值'
                        }
                    },
                    'ctl00$ContentPlaceHolder1$txtCardNumber': {
                        validators: {
                            notEmpty: {
                                message: '不能为空'
                            }, stringLength: {
                                min: 14,
                                max: 14,
                                message: '会员等级名称不能为空，长度必须为14个字符'
                            },
                            regexp: {
                                regexp: /^\d+(\.\d+)?$/,
                                message: '数据类型错误，只能输入实数型数值'
                            }
                        }
                    },
                    'ctl00$ContentPlaceHolder1$txtFreeQuantityPerDay': {
                        validators: {
                            notEmpty: {
                                message: ''
                            },
                            regexp: {
                                regexp: /^[0-9]\d{0,1}(\.\d+)?$/,
                                message: '数据类型错误，100以内数值'
                            }
                        }
                    },

                });


            });
   </script>
</asp:Content>
