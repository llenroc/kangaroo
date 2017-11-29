using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities;
using Hidistro.Entities.Kangaroo;

namespace Hidistro.SqlDal.Kangaroo
{
	/// <summary>
	/// -数据库操作类
	/// </summary>
	public class CardTypeInfoManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(Guid ID)
		{
			string selectSql = string.Format(@"Select * From CardTypeInfo Where ID='{0}'", ID);
			DataSet ds = DataAccessFactory.GetDataProvider().GetDataset(selectSql);
			ds.Tables[0].TableName = "CardTypeInfo";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["ID"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static CardTypeInfoEntity LoadEntity(Guid ID)
		{
			string selectSql = string.Format(@"Select * From CardTypeInfo Where ID='{0}'", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider().GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<CardTypeInfoEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From CardTypeInfo {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider().GetDataset(selectSql);
			ds.Tables[0].TableName = "CardTypeInfo";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["ID"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From CardTypeInfo {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider().GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<CardTypeInfoEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From CardTypeInfo {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider().GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<CardTypeInfoEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(Guid ID)
		{
			string deleteSql = string.Format(@"Delete From CardTypeInfo Where ID='{0}'", ID);
			DataAccessFactory.GetDataProvider().Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From CardTypeInfo {0}", where);
			DataAccessFactory.GetDataProvider().Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(CardTypeInfoEntity entity, bool isAdd)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into CardTypeInfo(ID,AmountLevel,TypeName)values(@ID,@AmountLevel,@TypeName)" :
				"Update CardTypeInfo Set ID=@ID,AmountLevel=@AmountLevel,TypeName=@TypeName Where ID=@ID";
				SqlParameter[] para = new SqlParameter[]
				{
					(entity.ID==null)?new SqlParameter("@ID",DBNull.Value):new SqlParameter("@ID",entity.ID),
					new SqlParameter("@AmountLevel",entity.AmountLevel),
					(entity.TypeName==null)?new SqlParameter("@TypeName",DBNull.Value):new SqlParameter("@TypeName",entity.TypeName),
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
