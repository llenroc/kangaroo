using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Kangaroo;
using Hidistro.ControlPanel.Members;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Kangaroo;
using Hidistro.Entities.Members;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Components.Validation;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Hidistro.UI.Web.Admin.Card
{
	public class EditCard : AdminPage
	{
		protected string ReUrl = "EditCard.aspx";

        private Guid id;

		protected string htmlOperatorName = "编辑";
        protected System.Web.UI.HtmlControls.HtmlGenericControl EditTitle;
		protected TextBox txtCardCount;
        protected TextBox txtCardNumber;
        protected DropDownList DDLCardType;
        protected TextBox txtBalance;
        protected Repeater rptUser;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidUserId;


        protected System.Web.UI.WebControls.Button btnEdit;



        protected EditCard() : base("m11", "cdp01")
		{
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            DataTable td = MemberHelper.GetMemberSelectInfo();
            this.rptUser.DataSource = td;
            this.rptUser.DataBind();

            if (this.Page.Request.QueryString["ID"] != null)
			{
				if (!Guid.TryParse(this.Page.Request.QueryString["ID"], out this.id))
				{
                    this.ShowMsg("aaaa", true);
					base.GotoResourceNotFound();
					return;
				}
			}
			else
			{
				this.htmlOperatorName = "新增";
			}
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			if (!this.Page.IsPostBack)
			{
                DataTable dtCardType = CardInfoBusiness.GetCardTypes();
                DDLCardType.DataSource = dtCardType;
                DDLCardType.DataTextField = "TypeName";
                DDLCardType.DataValueField = "ID";
                DDLCardType.DataBind();
                this.LoadCardInfo();
			}
		}


        protected void btnEdit_Click(object sender, System.EventArgs e)
		{
            if (string.IsNullOrEmpty(hidUserId.Value))
            {
                this.ShowMsg("请选择所属用户！", false);
            }
            CardTypeInfoEntity cardtype = CardTypeInfoBusiness.LoadEntity(new Guid(DDLCardType.SelectedValue));
            if (cardtype != null)
            {
                //新建状态下时,余额不能小于卡的面值
                if (Globals.ToNum(txtBalance.Text) < cardtype.AmountLevel)
                {
                    this.ShowMsg("该会员卡的初始余额不能小于" + cardtype.AmountLevel.ToString("F2"), false);
                    return;
                }
            }

            ManagerInfo currentManager = ManagerHelper.GetCurrentManager();

            if(this.Page.Request.QueryString["ID"] == null)
            {
                CardInfoEntity cardInfo = new CardInfoEntity()
                {
                    ID = Guid.NewGuid(),
                    CardNumber = txtCardNumber.Text.Trim(),
                    DefaultMoney = cardtype.AmountLevel,
                    CreateTime = DateTime.Now,
                    ManagerId = currentManager.UserId,
                    ShopId = new Guid("767146AD-7323-44EE-841C-5C2DF99F7737"),//默认为绿地国博店
                    CardTypeId = new Guid(DDLCardType.SelectedValue),
                    MemberId = Globals.ToNum(hidUserId.Value),
                    Status = 1,//已绑定
                    CardFrom = "收银系统",
                    ExpirationDate = DateTime.Now.AddYears(3),//默认为三年后过期
                    Balance = Globals.ToNum(txtBalance.Text.Trim()),
                };
                if (CardInfoBusiness.SaveEntity(cardInfo, true))
                {
                    this.ShowMsgAndReUrl("创建成功", true, "ManageCards.aspx");
                }
            }
            else
            {
                CardInfoEntity cardInfo = CardInfoBusiness.LoadEntity(id);
                cardInfo.Balance = decimal.Parse(txtBalance.Text);
                cardInfo.UpdateTime = DateTime.Now;
                if (CardInfoBusiness.SaveEntity(cardInfo,false))
                {
                    this.ShowMsgAndReUrl("编辑成功", true, "ManageCards.aspx");
                }
            }


		}

		private void LoadCardInfo()
		{
			if (this.Page.Request.QueryString["ID"] != null)
			{
				CardInfoEntity mcinfo = CardInfoBusiness.LoadEntity (this.id);
				if (mcinfo == null)
				{
					base.GotoResourceNotFound();
					return;
				}
                txtBalance.Text = mcinfo.Balance.ToString("F2");


            }
		}
	}
}
