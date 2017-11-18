using Hidistro.Core;
using Hidistro.Entities.Members;
using Hidistro.SaleSystem.Vshop;
using System;
using System.Collections;
using System.Web;

namespace Hidistro.ControlPanel.Store
{
    public static class HiAffiliation
    {
        public static void LoadPage()
        {
            string text = HiAffiliation.ReturnUrl();
          
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("\n", "");
               
                HttpContext.Current.Response.Redirect(text);
            }
        }

        public static string ReturnUrl()
        {
            int currentMemberUserId = Globals.GetCurrentMemberUserId(false);
            string result;
            if (currentMemberUserId > 0)
            {
              
                MemberInfo currentMember = MemberProcessor.GetCurrentMember();
                if (currentMember != null)
                {
                    result = HiAffiliation.ReturnUrlByUser(currentMember);
                    if (!result.ToLower().Contains("http") && !result.ToLower().Contains("bigeergeek") && result.ToLower().Contains("referralid"))
                    {
                        Globals.Debuglog("ReturnUrl:" + "http://"+HttpContext.Current.Request.Url.Host + result);
                        result = "http://" + HttpContext.Current.Request.Url.Host+ result;
                    }
                    Globals.Debuglog("ReturnUrl:"+ result);
                    return result;
                }
            }
            result = HiAffiliation.ReturnUrlByQueryString();
          
            return result;
        }

        public static string ReturnUrlByQueryString()
        {
            int num = 0;
            string str = HttpContext.Current.Request.Url.PathAndQuery.ToString();
            string result;
          
            if (((IList)HttpContext.Current.Request.QueryString.AllKeys).Contains("returnUrl"))
            {
                result = string.Empty;
              
            }
            else if (HttpContext.Current.Request.Url.AbsolutePath == "/logout.aspx")
            {
                result = string.Empty;
               
            }
        
            else
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["ReferralId"]))
                {
                    num = Globals.RequestQueryNum("ReferralId");
                 
                    if (num > 0)
                    {
                        DistributorsInfo currentDistributors = DistributorsBrower.GetCurrentDistributors(num, true);
                        if (currentDistributors == null)
                        {
                          
                            HiAffiliation.SetReferralIdCookie("0", "", false);
                            result = "/Default.aspx?ReferralId=0";
                          
                            return result;
                        }
                        HttpCookie httpCookie = HttpContext.Current.Request.Cookies["Vshop-ReferralId"];
                     
                        if (httpCookie == null)
                        {
                            HiAffiliation.SetReferralIdCookie(num.ToString(), "", false);
                        }
                        else if (httpCookie != null && httpCookie.Value != num.ToString())
                        {
                            HiAffiliation.SetReferralIdCookie(num.ToString(), "", false);
                        }
                    }
                }
                else
                {
                   
                    HttpCookie httpCookie = HttpContext.Current.Request.Cookies["Vshop-ReferralId"];
                    if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value))
                    {
                        if (HttpContext.Current.Request.QueryString.Count > 0)
                        {
                            if (!((IList)HttpContext.Current.Request.QueryString.AllKeys).Contains("ReferralId"))
                            {
                                result = str + "&ReferralId=" + httpCookie.Value;
                               
                                return result;
                            }
                            result = string.Empty;
                           
                            return result;
                        }
                        else
                        {
                            if (!((IList)HttpContext.Current.Request.QueryString.AllKeys).Contains("ReferralId"))
                            {
                                result = str + "?ReferralId=" + httpCookie.Value;
                              
                                return result;
                            }
                            result = string.Empty;
                           
                            return result;
                        }
                    }
                }
                if (((IList)HttpContext.Current.Request.QueryString.AllKeys).Contains("returnUrl") || HttpContext.Current.Request.Url.AbsolutePath == "/logout.aspx")
                {
                   
                    result = string.Empty;
                   
                }
                else if (!((IList)HttpContext.Current.Request.QueryString.AllKeys).Contains("ReferralId") && HttpContext.Current.Request.QueryString.Count > 0)
                {
                    result = str + "&ReferralId=" + num.ToString();
                   
                }
                else if (!((IList)HttpContext.Current.Request.QueryString.AllKeys).Contains("ReferralId"))
                {
                    result = str + "?ReferralId=" + num.ToString();
                   
                }
                else
                {
                    result = string.Empty;
                }
            }
           
            return result;
        }

        /// <summary>
        /// 根据当前用户获取当前页是否跳转
        /// </summary>
        public static string ReturnUrlByUser(MemberInfo mInfo)
        {
            //设置当前用户访问的店铺CookieID
            DistributorsInfo currentDistributors = DistributorsBrower.GetCurrentDistributors(Globals.ToNum(mInfo.UserId), true);
            if (currentDistributors != null)
            {
                HiAffiliation.SetReferralIdCookie(currentDistributors.UserId.ToString(), "", false);
            }
            else
            {
                HiAffiliation.SetReferralIdCookie(mInfo.ReferralUserId.ToString(), "", false);
            }

            //设置跳转地址
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies["Vshop-ReferralId"];
            string result;
            string str = HttpContext.Current.Request.Url.PathAndQuery.ToString();
            if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value))
            {
                Globals.Debuglog("1_httpCookie:" + httpCookie.Value);
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["ReferralId"]))
                {
                    Globals.Debuglog("2_ReferralId:" + HttpContext.Current.Request.QueryString["ReferralId"]);
                    HiUriHelp hiUriHelp = new HiUriHelp(HttpContext.Current.Request.QueryString);
                    string queryString = hiUriHelp.GetQueryString("ReferralId");
                   
                    if (!string.IsNullOrEmpty(queryString))
                    {
                        Globals.Debuglog("3_queryString:" + queryString);
                        if (queryString == httpCookie.Value)
                        {
                            result = string.Empty;
                            return result;
                        }
                        Globals.Debuglog("4_queryString:" + queryString);

                        //如果访问的店铺不是平台，但之前存储的店铺是平台时，强制更改【V20170906】
                        if (queryString != "0" && httpCookie.Value=="0")
                        {
                            int iReferralUserId = 0;
                            int.TryParse(queryString, out iReferralUserId);
                            if(iReferralUserId>0)
                            {
                                Globals.Debuglog("5_queryString:" + queryString);
                                httpCookie.Value = queryString;
                                MemberInfo currentMember = MemberProcessor.GetCurrentMember();
                                Globals.Debuglog("5_queryString2:" + queryString);
                                currentMember.ReferralUserId = currentMember.ReferralUserId;
                                Globals.Debuglog("5_queryString3:" + queryString);
                                bool bb=MemberProcessor.UpdateReferralUserId(currentMember.UserId, iReferralUserId);
                                Globals.Debuglog("5_queryString4:" + queryString+"__"+bb.ToString()+"__"+ currentMember.UserId);
                                HiAffiliation.SetReferralIdCookie(queryString, "", false);
                                Globals.Debuglog("5_iReferralUserId:" + iReferralUserId);
                            }
                        }

                        hiUriHelp.SetQueryString("ReferralId", httpCookie.Value);
                        result = HttpContext.Current.Request.Url.AbsolutePath + hiUriHelp.GetNewQuery();
                        Globals.Debuglog("6_result:" + result);
                        return result;
                    }
                }
                Globals.Debuglog("7_result:" );
                if (((IList)HttpContext.Current.Request.QueryString.AllKeys).Contains("returnUrl"))
                {
                    result = string.Empty;
                }
                else if (HttpContext.Current.Request.Url.AbsolutePath == "/logout.aspx")
                {
                    result = string.Empty;
                }
                else if (!((IList)HttpContext.Current.Request.QueryString.AllKeys).Contains("ReferralId") && HttpContext.Current.Request.QueryString.Count > 0)
                {
                    Globals.Debuglog("8_result:"+ httpCookie.Value);
                    result = str + "&ReferralId=" + httpCookie.Value;
                   
                }
                else if (!((IList)HttpContext.Current.Request.QueryString.AllKeys).Contains("ReferralId"))
                {
                    Globals.Debuglog("9_result:" + httpCookie.Value);
                    result = str + "?ReferralId=" + httpCookie.Value;
                  
                }
                else
                {
                    result = string.Empty;
                }
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }

        public static string GetReturnUrl(string returnUrl)
        {
            if (returnUrl.IndexOf("?") > -1)
            {
                returnUrl = returnUrl.Substring(returnUrl.IndexOf("?"));
            }
            return returnUrl;
        }

        public static void SetReferralIdCookie(string referralId, string url = "", bool isRedirect = false)
        {
            Globals.ClearReferralIdCookie();
            Globals.SetDistributorCookie(Globals.ToNum(referralId));
            if (isRedirect && !string.IsNullOrEmpty(url))
            {
                HttpContext.Current.Response.Redirect(url);
            }
        }
    }
}
