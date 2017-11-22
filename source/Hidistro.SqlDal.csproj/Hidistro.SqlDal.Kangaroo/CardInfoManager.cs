using Hidistro.Core;
using Hidistro.Entities;
using Hidistro.Entities.Kangaroo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Hidistro.SqlDal.Kangaroo
{
	/// <summary>
	/// -数据库操作类
	/// </summary>
	public class CardInfoManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(Guid ID)
		{
			string selectSql = string.Format(@"Select * From CardInfo Where ID='{0}'", ID);
			DataSet ds = DataAccessFactory.GetDataProvider().GetDataset(selectSql);
			ds.Tables[0].TableName = "CardInfo";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["ID"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static CardInfoEntity LoadEntity(Guid ID)
		{
			string selectSql = string.Format(@"Select * From CardInfo Where ID='{0}'", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider().GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<CardInfoEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From CardInfo {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider().GetDataset(selectSql);
			ds.Tables[0].TableName = "CardInfo";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["ID"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From CardInfo {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider().GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<CardInfoEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From CardInfo {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider().GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<CardInfoEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(Guid ID)
		{
			string deleteSql = string.Format(@"Delete From CardInfo Where ID='{0}'", ID);
			DataAccessFactory.GetDataProvider().Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From CardInfo {0}", where);
			DataAccessFactory.GetDataProvider().Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(CardInfoEntity entity, bool isAdd)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into CardInfo(ID,MemberId,CardTypeId,ManagerId,ShopId,Status,Balance,DefaultMoney,CreateTime,UpdateTime,ExpirationDate,CardNumber,CardFrom)values(@ID,@MemberId,@CardTypeId,@ManagerId,@ShopId,@Status,@Balance,@DefaultMoney,@CreateTime,@UpdateTime,@ExpirationDate,@CardNumber,@CardFrom)" :
				"Update CardInfo Set ID=@ID,MemberId=@MemberId,CardTypeId=@CardTypeId,ManagerId=@ManagerId,ShopId=@ShopId,Status=@Status,Balance=@Balance,DefaultMoney=@DefaultMoney,CreateTime=@CreateTime,UpdateTime=@UpdateTime,ExpirationDate=@ExpirationDate,CardNumber=@CardNumber,CardFrom=@CardFrom Where ID=@ID";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@ID",entity.ID),
					new SqlParameter("@MemberId",entity.MemberId),
					new SqlParameter("@CardTypeId",entity.CardTypeId),
					new SqlParameter("@ManagerId",entity.ManagerId),
					new SqlParameter("@ShopId",entity.ShopId),
					new SqlParameter("@Status",entity.Status),
					new SqlParameter("@Balance",entity.Balance),
					new SqlParameter("@DefaultMoney",entity.DefaultMoney),
					(entity.CreateTime==null || entity.CreateTime==DateTime.MinValue)?new SqlParameter("@CreateTime",DBNull.Value):new SqlParameter("@CreateTime",entity.CreateTime),
					(entity.UpdateTime==null || entity.UpdateTime==DateTime.MinValue)?new SqlParameter("@UpdateTime",DBNull.Value):new SqlParameter("@UpdateTime",entity.UpdateTime),
					(entity.ExpirationDate==null || entity.ExpirationDate==DateTime.MinValue)?new SqlParameter("@ExpirationDate",DBNull.Value):new SqlParameter("@ExpirationDate",entity.ExpirationDate),
					(entity.CardNumber==null)?new SqlParameter("@CardNumber",DBNull.Value):new SqlParameter("@CardNumber",entity.CardNumber),
					(entity.CardFrom==null)?new SqlParameter("@CardFrom",DBNull.Value):new SqlParameter("@CardFrom",entity.CardFrom),
				};
				DataAccessFactory.GetDataProvider().Execute(execSql, para);
				return true;
			}
			catch
			{
				return false;
			}
		}

	}
}
