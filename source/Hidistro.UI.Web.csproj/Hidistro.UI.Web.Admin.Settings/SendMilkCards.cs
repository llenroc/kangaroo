using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Hidistro.UI.Web.Admin.settings
{
	public class SendMilkCards : AdminPage
	{

        protected TextBox usernamename;
        protected HtmlInputHidden hidUserId;
        private string cardies;//站点id
        public string adminName;

        protected SendMilkCards() : base("m09", "00000")
		{
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{

            string text = Globals.RequestFormStr("posttype");
            if (text == "getselecteduserbyCart")
            {
                base.Response.ContentType = "application/json";
                ManagerInfo currentManager = ManagerHelper.GetCurrentManager();
                this.adminName = currentManager.UserName;
                System.Data.DataTable dataTable2 = NoticeHelper.GetSelectedUser(this.adminName, "SendMilkCard").Tables[0];
                int count2 = dataTable2.Rows.Count;
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                if (count2 > 0)
                {
                    int j = 0;
                    stringBuilder.Append(string.Concat(new string[]
                    {
                            "{\"name\":\"",
                            Globals.String2Json(dataTable2.Rows[j]["username"].ToString()),
                            "\",\"tel\":\"",
                            Globals.String2Json(dataTable2.Rows[j]["CellPhone"].ToString()),
                            "\",\"userid\":\"",
                            Globals.String2Json(dataTable2.Rows[j]["userid"].ToString()),
                            "\",\"bindname\":\"",
                            Globals.String2Json(dataTable2.Rows[j]["UserBindName"].ToString()),
                            "\"}"
                    }));
                    for (j = 1; j < count2; j++)
                    {
                        stringBuilder.Append(string.Concat(new string[]
                        {
                                ",{\"name\":\"",
                                Globals.String2Json(dataTable2.Rows[j]["username"].ToString()),
                                "\",\"tel\":\"",
                                Globals.String2Json(dataTable2.Rows[j]["CellPhone"].ToString()),
                                "\",\"userid\":\"",
                                Globals.String2Json(dataTable2.Rows[j]["userid"].ToString()),
                                "\",\"bindname\":\"",
                                Globals.String2Json(dataTable2.Rows[j]["UserBindName"].ToString()),
                                "\"}"
                        }));
                    }
                }
                string s = string.Concat(new object[]
                {
                        "{\"success\":\"1\",\"icount\":",
                        count2,
                        ",\"userlist\":[",
                        stringBuilder.ToString(),
                        "]}"
                });
                base.Response.Write(s);
                base.Response.End();
                return;
            }
        }

		protected void btnSend_Click(object sender, System.EventArgs e)
		{
            string[] openids = this.hidUserId.Value.TrimEnd(',').Split(',');
            string[] cardids = this.Page.Request.QueryString["cardids"].Split(',');

            if (openids.Length != cardids.Length)
            {
                this.ShowMsg("奶卡数量与用户数量不匹配，请保持一致再提交！",false);
                return;
            }
            int sendCounts = VShopHelper.SendMilkCards(openids, cardids);
            if (sendCounts>0)
            {
                this.ShowMsg("发送成功！勾选了"+ cardids.Length+"张奶卡，"+ openids.Length+"个用户，成功发送了"+sendCounts+"位用户", true);
            }
            else
            {
                this.ShowMsg("发送失败！", false);
            }

        }
	}
}
