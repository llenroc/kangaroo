using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Hidistro.SqlDal.Kangaroo;
using Hidistro.Entities.Kangaroo;
using Hidistro.Core.Entities;

namespace Hidistro.ControlPanel.Kangaroo
{
	/// <summary>
	/// -业务操作类
	/// </summary>
	public class CardTypeInfoBusiness
	{
		/// <summary>
		/// 根据主键加载数据集
		/// </summary>
		public static DataTable LoadData(Guid ID)
		{
				return CardTypeInfoManager.LoadData(ID);
		}

		/// <summary>
		/// 根据主键加载实体
		/// </summary>
		public static CardTypeInfoEntity LoadEntity(Guid ID)
		{
				return CardTypeInfoManager.LoadEntity(ID);
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0)
		{
			return CardTypeInfoManager.SelectListData(where,selectFields,orderby,top);
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null)
		{
			return CardTypeInfoManager.SelectScalar(where,selectFields,orderby);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<CardTypeInfoEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0)
		{
				return CardTypeInfoManager.SelectListEntity(where,selectFields,orderby,top);
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(Guid ID)
		{
			CardTypeInfoManager.Del(ID);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null)
		{
			CardTypeInfoManager.DelListData(where);
		}

		/// <summary>
		/// 保存
		/// </summary>
		public static bool SaveEntity(CardTypeInfoEntity entity, bool isAdd)
		{
			return CardTypeInfoManager.SaveEntity(entity, isAdd);
		}

	}
}
