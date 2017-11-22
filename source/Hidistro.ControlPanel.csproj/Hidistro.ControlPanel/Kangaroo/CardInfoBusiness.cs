using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Hidistro.SqlDal.Kangaroo;
using Hidistro.Entities.Kangaroo;


namespace Hidistro.ControlPanel.Kangaroo
{
	/// <summary>
	/// -业务操作类
	/// </summary>
	public class CardInfoBusiness
	{
		/// <summary>
		/// 根据主键加载数据集
		/// </summary>
		public static DataTable LoadData(Guid ID)
		{
				return CardInfoManager.LoadData(ID);
		}

		/// <summary>
		/// 根据主键加载实体
		/// </summary>
		public static CardInfoEntity LoadEntity(Guid ID)
		{
				return CardInfoManager.LoadEntity(ID);
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0)
		{
			return CardInfoManager.SelectListData(where,selectFields,orderby,top);
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null)
		{
			return CardInfoManager.SelectScalar(where,selectFields,orderby);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<CardInfoEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0)
		{
				return CardInfoManager.SelectListEntity(where,selectFields,orderby,top);
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(Guid ID)
		{
			CardInfoManager.Del(ID);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null)
		{
			CardInfoManager.DelListData(where);
		}

		/// <summary>
		/// 保存
		/// </summary>
		public static bool SaveEntity(CardInfoEntity entity, bool isAdd)
		{
			return CardInfoManager.SaveEntity(entity, isAdd);
		}

	}
}
