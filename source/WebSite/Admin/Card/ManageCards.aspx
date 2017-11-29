<%@ Page Language="C#" Title="" AutoEventWireup="true"   MasterPageFile="~/Admin/AdminNew.Master" Inherits="Hidistro.UI.Web.Admin.Card.ManageCards" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<%@ Register Src="../Ascx/ucDateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="Hi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/utility/skins/blue.css" type="text/css" media="screen" />
    <Hi:Script ID="Script5" runat="server" Src="/utility/jquery.artDialog.js" />
    <Hi:Script ID="Script6" runat="server" Src="/utility/Window.js" />
    
  
    <style type="text/css">
        /*.selectthis {border-color:red; color:red; border:1px solid;}*/
        .tdClass{text-align:center;}
        .labelClass{margin-right:10px;}
        .thCss{text-align:center;}
        .selectthis{border:1px solid;border-color:#999999; color:#c93027;margin-right:2px;}
        .selectthis:hover {border:1px solid;border-color:#999999; color:#c93027;margin-right:2px;}
        .aClass{border:1px solid;border-color:#999999; color:#999999;margin-right:2px;}
        .aClass:hover{border:1px solid;border-color:#999999; color:#999999;margin-right:2px;}
        #datalist td{word-break: break-all;}
        #ctl00_ContentPlaceHolder1_grdMemberList th {margin:0px;border-left:0px;border-right:0px;background-color:#F7F7F7;text-align:center; vertical-align:middle;}
        #ctl00_ContentPlaceHolder1_grdMemberList td {margin:0px;border-left:0px;border-right:0px;text-align:center;vertical-align:middle;}
        .table-bordered > thead > tr > th {
        border:none;}
        .table-bordered {border-right: none;border-left: none;}
        .bl mb5 {cursor: pointer
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="thisForm" runat="server" class="form-horizontal">                     
        <div>
            <div class="page-header">
                <h2>会员卡管理</h2>
            </div>
        <!--搜索-->
            <a class="btn btn-success" onclick="AddCard()">添加会员卡</a>
        <!--数据列表区域-->
        <div>
            <div class="form-inline mb10">
                 <div class="set-switch">
                    <div class="form-inline  mb10">
                        <div class="form-group" style ="">
                            <label class="ml10">卡片状态：</label>
                            <asp:DropDownList  ID="ddlStatus" runat="server" CssClass="form-control  resetSize inputw150">
                                 <asp:ListItem Text="全部" Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Text="正常" Value="0" >未绑定</asp:ListItem>
                                <asp:ListItem Text="已失效" Value="1">已绑定</asp:ListItem>
                            </asp:DropDownList>
                        </div> 
                        <div class="form-group mr20" style ="margin-left :30px;">
                             <label for="ctl00_ContentPlaceHolder1_calendarStartDate_txtDateTimePicker">创建时间：</label>
                            <Hi:DateTimePicker CalendarType="StartDate" ID="calendarStartDate" runat="server" CssClass="form-control resetSize inputw100" />
                            至
                                <Hi:DateTimePicker ID="calendarEndDate" runat="server" CalendarType="EndDate" CssClass="form-control resetSize inputw100" />
                        </div>
                        <div class="form-group mr20" style="margin-left:0px;">
                            <label for="sellshop1" class="ml10">会员卡号：</label>
                            <asp:TextBox ID="txtCardNumber" CssClass="form-control resetSize" runat="server" /> 
                        </div>

                    </div>
                    <div class="form-inline  mb10">
                         <div class="form-group mr20">
                            <label for="sellshop1" class="ml10">会员姓名：</label>
                            <asp:TextBox ID="txtUserName" CssClass="form-control resetSize" style="width:150px" runat="server" />
                        </div>
                         <div class="form-group mr20">
                            <label for="sellshop1" class="ml10">联系电话：</label>
                            <asp:TextBox ID="txtUserPhone" CssClass="form-control "  runat="server" />
                        </div>
                  
                        <div class="form-group" style ="margin-left :30px">                    
                            <asp:Button ID="btnSearchButton" runat="server" CssClass="btn resetSize btn-primary" Text="搜索" />
                            
                        </div>
                        <div class="form-group mr20" style ="margin-left :15px">  
                            <asp:LinkButton ID="LinkButtonClear"  CssClass="bl mb5" runat="server">清除条件</asp:LinkButton>
                        </div>     
                    </div>   
                </div>          
             </div>

       
            <div class="title-table">
            <div style="margin-bottom:5px;  margin-top:10px;">        
                <div class="form-inline" id="pagesizeDiv" style="float: left; width:100%; margin-bottom:5px;">                
                </div> 
                  <div class="page-box">
                    <div class="page fr">
                        <div class="form-group" style="margin-right:0px;margin-left:0px;background:#fff;">
                            <label for="exampleInputName2">每页显示数量：</label>
                       <UI:PageSize runat="server"  ID="hrefPageSize1" />
                        </div>
                    </div>
                </div>
                <div class="pageNumber" style="float: right;  height:29px; margin-bottom:5px; display:none;" >
                    <label>每页显示数量：</label>
                    <div class="pagination" style="display:none;">
                        <UI:Pager runat="server" ShowTotalPages="false" ID="pager" />
                    </div>
                </div>
                <!--结束-->                           
            </div>
                <table class="table table-hover mar table-bordered" style="border-bottom: none;"><thead><tr >
                    <th style="text-align: center;width:80px">会员姓名/电话</th>  
                    <th style="text-align: center;width:90px">会员卡号</th>
                    <th style="text-align: center;width:90px">卡片类型</th>
                    <th style="text-align: center;width:70px">当前余额</th>
                    <th style="text-align: center;width:70px">卡片来源</th>
                    <th style="text-align: center;width:80px">所属门店</th>
                    <th style="text-align: center;width:100px">创建时间</th>
                    <th  style="text-align: center;width:50px">卡片状态</th>
                    <th  style="text-align: center;width:30px">操作</th>
		</tr></thead></table></div>
            <div id="datalist">
            <UI:Grid ID="grdCardList" runat="server" ShowHeader="false" AutoGenerateColumns="false"
                DataKeyNames="ID" HeaderStyle-CssClass="table_title" CssClass="table table-hover mar table-bordered"
                GridLines="None" Width="100%">
                <Columns>
                  
                    <asp:TemplateField HeaderText="会员姓名/电话" ItemStyle-Width="80" SortExpression="UserName"  HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <p><asp:Literal ID="RealName" runat="server" Text='<%# Eval("UserName") +"<br>"+Eval("CellPhone") %>'/></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="会员卡号" ItemStyle-Width="90" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <p><asp:Literal ID="CellPhone" runat="server" Text='<%# Eval("CardNumber") %>'/></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="卡片类型/面值" ItemStyle-Width="90" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <p><%# Eval("typename")+"<br>面值 : "+Eval("AmountLevel","{0:F2}")%></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                      
                     <asp:TemplateField HeaderText="当前余额" ItemStyle-Width="70" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <p><%# Eval("balance","{0:F2}")%></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="卡片来源" ItemStyle-Width="70" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <p><%# Eval("cardfrom")%></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="所属门店" ItemStyle-Width="80" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <p><%# Eval("shopname")%></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="创建时间" ItemStyle-Width="100" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <p><%# Eval("createtime")%></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="卡片状态" ItemStyle-Width="50" HeaderStyle-HorizontalAlign="center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <p><%# Eval("status").ToString() == "1" ?"已绑定":"未绑定"%></p>
                        </ItemTemplate>
                    </asp:TemplateField>


                   
                     <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center"   ItemStyle-Width="30">
                        <ItemStyle CssClass="spanD spanN actionBtn"/>
                        <ItemTemplate>
                            
                            <p><a onclick="EditCard('<%# Eval("ID") %>')" href="javascript:void(0)">编辑</a></p>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </UI:Grid>   

            </div>      
        </div>
        <!--数据列表底部功能区域-->

           <input type="hidden" id="hdUserId" runat="server" value="" />
            <asp:Button ID="huifuUser" Text="huifu" runat="server" Style="display:none"/>
            <asp:Button ID="BatchHuifu" Text="huifu" runat="server" Style="display:none"/>
             <asp:Button ID="BatchCreatDist" Text="huifu" runat="server" Style="display:none"/>


        <div class="bottomPageNumber clearfix">
            <div class="pageNumber">
                <div class="pagination" style="width: auto">
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
                </div>
            </div>
        </div>
       
        </div>

       



        </form>
    <script type="text/javascript">

        function AddCard() {
            location.href = "EditCard.aspx";
        }

        function EditCard(id) {
            location.href = "EditCard.aspx?"+id;
        }

        $(document).ready(function () {

            $('#selectAll').click(function () {
                var check = $(this).prop('checked');
                $("input[type='checkbox']").each(function () {
                    $(this).prop('checked', check);
                });
            });


            $('#datalist').find('th').each(function () {
                $(this).css('text-align', 'center');
            });

            $('#pagesizeDiv').find('a').each(function () {
                if ($(this).attr("class") != "selectthis") {
                    $(this).removeClass();
                    $(this).addClass('aClass');
                }

            });

        });
     
        function change(id)
        {

            alert(id)
        }






    </script>
</asp:Content>
