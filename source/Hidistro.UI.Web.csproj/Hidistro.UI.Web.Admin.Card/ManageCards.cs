
using ASPNET.WebControls;
using Hidistro.ControlPanel.Kangaroo;
using Hidistro.ControlPanel.Store;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entites.Kangaroo;
using Hidistro.Entities.FenXiao;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using Hidistro.UI.Web.Admin.Ascx;
using System;
using System.Web.UI.WebControls;

namespace Hidistro.UI.Web.Admin.Card
{
   public class ManageCards : AdminPage
    {
        protected ucDateTimePicker calendarStartDate;
        protected ucDateTimePicker calendarEndDate;
        protected System.Web.UI.WebControls.TextBox txtUserName;
        protected System.Web.UI.WebControls.TextBox txtUserPhone;
        protected System.Web.UI.WebControls.TextBox txtCardNumber;
        protected DropDownList ddlStatus;
        protected PageSize hrefPageSize1;
        protected Pager pager1;
        protected Grid grdCardList;
        protected System.Web.UI.WebControls.Button btnSearchButton;
        protected System.Web.UI.WebControls.Button btnManyChangeStatus;
        protected System.Web.UI.WebControls.LinkButton LinkBtn;
        protected System.Web.UI.WebControls.LinkButton LinkButtonClear;
        protected System.Web.UI.HtmlControls.HtmlGenericControl days;
        protected System.Web.UI.HtmlControls.HtmlGenericControl alreadySend;
        protected System.Web.UI.HtmlControls.HtmlGenericControl NoSend;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spantotaldays;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spantotalcount;
        

        public ManageCards() : base("m11", "cdp01")
		{
        }

       
        protected void Page_Load(object sender, System.EventArgs e)
        {
           
            if (!this.Page.IsPostBack)
            {
                this.ViewState["ClientType"] = ((base.Request.QueryString["clientType"] != null) ? base.Request.QueryString["clientType"] : null);
                this.BindData();
            }
            //this.btnManyChangeStatus.Click += new System.EventHandler(this.btnManyChangeStatus_Click);
            this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
            //this.LinkBtn.Click += new System.EventHandler(this.LinkBtn_Click);
            this.LinkButtonClear.Click += new System.EventHandler(this.LinkButtonClear_Click);
        }

      
      
        private void ReBind(bool isSearch)
        {
            System.Collections.Specialized.NameValueCollection nameValueCollection = new System.Collections.Specialized.NameValueCollection();
           
            //nameValueCollection.Add("Username", this.txtSearchText.Text);
          
          
            //nameValueCollection.Add("MemberStatus", this.MemberStatus.SelectedItem.Value);
            //nameValueCollection.Add("clientType", (this.ViewState["ClientType"] != null) ? this.ViewState["ClientType"].ToString() : "");
            //nameValueCollection.Add("pageSize", this.pager.PageSize.ToString(System.Globalization.CultureInfo.InvariantCulture));
            //nameValueCollection.Add("phone", this.txtPhone.Text);
            //if (!isSearch)
            //{
            //    nameValueCollection.Add("pageIndex", this.pager.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            //}
            base.ReloadPage(nameValueCollection);
        }

        protected void BindData()
        {
            CardInfoQuery cardinfoQuery = GetQuery();
            DbQueryResult cardlist = CardInfoBusiness.GetCardInfoList(cardinfoQuery);
            this.grdCardList.DataSource = cardlist.Data;


            this.grdCardList.DataBind();
            this.pager1.TotalRecords = (this.pager1.TotalRecords = cardlist.TotalRecords);

            
            this.txtCardNumber.Text = cardinfoQuery.CardNumber;
            this.ddlStatus.SelectedValue = cardinfoQuery.Status;
            this.calendarStartDate.Text = cardinfoQuery.StartTime;
            this.calendarEndDate.Text = cardinfoQuery.EndTime;
            this.txtUserName.Text = cardinfoQuery.UserName;
            return;
        }

        


        private void LinkButtonClear_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("ManageCards.aspx?action=clear");
             
        }

        private void btnSearchButton_Click(object sender, System.EventArgs e)
        {
            System.Collections.Specialized.NameValueCollection queryStrings = new System.Collections.Specialized.NameValueCollection();
            queryStrings.Add("CardNumber", this.txtCardNumber.Text);
            queryStrings.Add("UserName", this.txtUserName.Text);
            queryStrings.Add("Status", this.ddlStatus.SelectedValue);
            queryStrings.Add("StartTime", this.calendarStartDate.Text);
            queryStrings.Add("EndTime", this.calendarEndDate.Text);
            queryStrings.Add("UserPhone",this.txtUserPhone.Text);

            if (Request.QueryString["action"]!= null)
            {
                queryStrings.Add("action", Request.QueryString["action"].ToString());
            }
                base.ReloadPage(queryStrings);
        }



        public CardInfoQuery GetQuery()
        {
            CardInfoQuery entity = new CardInfoQuery();

            
            if (Request.QueryString["UserName"] !=null) 
            entity.UserName = Request.QueryString["UserName"].ToString();

            if (Request.QueryString["Status"] != null)
                entity.Status = Request.QueryString["Status"].ToString();

            if (Request.QueryString["StartTime"] != null)
                entity.StartTime = Request.QueryString["StartTime"].ToString();

            if (Request.QueryString["EndTime"] != null)
                entity.EndTime = Request.QueryString["EndTime"].ToString();
            
            if (Request.QueryString["CardNumber"] != null)
               entity.CardNumber = Request.QueryString["CardNumber"].ToString();

          
          
            entity.PageIndex = this.pager1.PageIndex;
            entity.PageSize = this.pager1.PageSize;
            entity.SortBy = "CreateTime";
            entity.SortOrder = SortAction.Desc;


            return entity;

        }
        


        private void btnManyChangeStatus_Click(object sender, System.EventArgs e)
        {
            string text = "";
            ManagerHelper.CheckPrivilege(Privilege.DeleteMember);
            foreach (System.Web.UI.WebControls.GridViewRow gridViewRow in this.grdCardList.Rows)
            {
                System.Web.UI.WebControls.CheckBox checkBox = (System.Web.UI.WebControls.CheckBox)gridViewRow.FindControl("checkboxCol");
                if (checkBox.Checked)
                {
                    text = text + "'"+ this.grdCardList.DataKeys[gridViewRow.RowIndex].Value.ToString() + "'"+",";
                }
            }
            text = text.TrimEnd(new char[]
            {
                ','
            });
            if (string.IsNullOrEmpty(text))
            {
                this.ShowMsg("请先选择要已配送的任务！", false);
                return;
            }
          
            if (VShopHelper.ChangStatus(text, ManagerHelper.GetCurrentManager().UserId))
            {
                this.ShowMsg("成功已配送了任务！", true);
                this.BindData();
            }
        }




        private void ddlApproved_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.ReBind(false);
        }
        
       
    }
}