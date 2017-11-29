using System;
using System.Data;
using System.Collections;

namespace Hidistro.Entities.Kangaroo {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class CardTypeInfoEntity {

		#region 字段名
		public static string FieldID = "ID";
		public static string FieldTypeName = "TypeName";
		public static string FieldAmountLevel = "AmountLevel";
		#endregion

		#region 属性
		private Guid  _iD;
		public Guid  ID
		{
			get{ return _iD;}
			set{ _iD=value;}
		}
		private string  _typeName;
		public string  TypeName
		{
			get{ return _typeName;}
			set{ _typeName=value;}
		}
		private decimal  _amountLevel;
		public decimal  AmountLevel
		{
			get{ return _amountLevel;}
			set{ _amountLevel=value;}
		}
		#endregion

		#region 构造函数
		public CardTypeInfoEntity(){}

		public CardTypeInfoEntity(DataRow dr)
		{
			if (dr[FieldID] != DBNull.Value)
			{
			_iD = (Guid )dr[FieldID];
			}
			if (dr[FieldTypeName] != DBNull.Value)
			{
			_typeName = (string )dr[FieldTypeName];
			}
			if (dr[FieldAmountLevel] != DBNull.Value)
			{
			_amountLevel = (decimal )dr[FieldAmountLevel];
			}
		}
		#endregion

	}
}
