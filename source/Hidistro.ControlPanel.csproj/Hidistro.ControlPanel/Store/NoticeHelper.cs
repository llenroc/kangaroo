using Hidistro.Core.Entities;
using Hidistro.Entities.Store;
using Hidistro.SqlDal.Store;
using System;
using System.Data;

namespace Hidistro.ControlPanel.Store
{
	public class NoticeHelper
	{
		public NoticeHelper()
		{
		}

		public static void AddUser(int userid, string adminname ,string fromPage = "")
		{
			(new NoticeUserDao()).AddUser(userid, adminname, fromPage);
		}

		public static bool DelNotice(int noticeid)
		{
			return (new NoticeDao()).DelNotice(noticeid);
		}

		public static void DelUser(int userid, string adminname)
		{
			(new NoticeUserDao()).DelUser(userid, adminname);
		}

		public static NoticeInfo GetNoticeInfo(int id)
		{
			return (new NoticeDao()).GetNoticeInfo(id);
		}

		public static DbQueryResult GetNoticeRequest(NoticeQuery query)
		{
			return (new NoticeDao()).GetNoticeRequest(query);
		}

		public static DataSet GetSelectedUser(string adminName,string fromPage="")
		{
			return (new NoticeDao()).GetSelectedUser(adminName, fromPage);
		}

		public static int GetSelectedUser(int noticeid)
		{
			return (new NoticeDao()).GetSelectedUser(noticeid);
		}

		public static DataSet GetTempSelectedUser(string adminName)
		{
			return (new NoticeUserDao()).GetTempSelectedUser(adminName);
		}

		public static bool GetUserIsSel(int userid, string adminName,string frompage)
		{
			return (new NoticeDao()).GetUserIsSel(userid, adminName, frompage);
		}

		public static bool NoticePub(int noticeid)
		{
			return (new NoticeDao()).NoticePub(noticeid);
		}

		public static int SaveNotice(NoticeInfo info)
		{
			return (new NoticeDao()).SaveNotice(info);
		}
	}
}