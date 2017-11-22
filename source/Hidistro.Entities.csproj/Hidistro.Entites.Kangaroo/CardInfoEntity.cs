using System;
using System.Data;
using System.Collections;

namespace Hidistro.Entities.Kangaroo
{
    /// <summary>
    /// -实体类
    /// </summary>
    [Serializable]
	public  class CardInfoEntity {

		#region 字段名
		public static string FieldID = "ID";
		public static string FieldMemberId = "MemberId";
		public static string FieldCardNumber = "CardNumber";
		public static string FieldCardTypeId = "CardTypeId";
		public static string FieldBalance = "Balance";
		public static string FieldDefaultMoney = "DefaultMoney";
		public static string FieldCreateTime = "CreateTime";
		public static string FieldUpdateTime = "UpdateTime";
		public static string FieldManagerId = "ManagerId";
		public static string FieldCardFrom = "CardFrom";
		public static string FieldShopId = "ShopId";
		public static string FieldStatus = "Status";
		public static string FieldExpirationDate = "ExpirationDate";
		#endregion

		#region 属性
		private int  _iD;
		public int  ID
		{
			get{ return _iD;}
			set{ _iD=value;}
		}
		private int  _memberId;
		public int  MemberId
		{
			get{ return _memberId;}
			set{ _memberId=value;}
		}
		private string  _cardNumber;
		public string  CardNumber
		{
			get{ return _cardNumber;}
			set{ _cardNumber=value;}
		}
		private int  _cardTypeId;
		public int  CardTypeId
		{
			get{ return _cardTypeId;}
			set{ _cardTypeId=value;}
		}
		private decimal  _balance;
		public decimal  Balance
		{
			get{ return _balance;}
			set{ _balance=value;}
		}
		private decimal  _defaultMoney;
		public decimal  DefaultMoney
		{
			get{ return _defaultMoney;}
			set{ _defaultMoney=value;}
		}
		private DateTime  _createTime;
		public DateTime  CreateTime
		{
			get{ return _createTime;}
			set{ _createTime=value;}
		}
		private DateTime  _updateTime;
		public DateTime  UpdateTime
		{
			get{ return _updateTime;}
			set{ _updateTime=value;}
		}
		private int  _managerId;
		public int  ManagerId
		{
			get{ return _managerId;}
			set{ _managerId=value;}
		}
		private string  _cardFrom;
		public string  CardFrom
		{
			get{ return _cardFrom;}
			set{ _cardFrom=value;}
		}
		private int  _shopId;
		public int  ShopId
		{
			get{ return _shopId;}
			set{ _shopId=value;}
		}
		private int  _status;
		public int  Status
		{
			get{ return _status;}
			set{ _status=value;}
		}
		private DateTime  _expirationDate;
		public DateTime  ExpirationDate
		{
			get{ return _expirationDate;}
			set{ _expirationDate=value;}
		}
		#endregion

		#region 构造函数
		public CardInfoEntity(){}

		public CardInfoEntity(DataRow dr)
		{
			if (dr[FieldID] != DBNull.Value)
			{
			_iD = (int )dr[FieldID];
			}
			if (dr[FieldMemberId] != DBNull.Value)
			{
			_memberId = (int )dr[FieldMemberId];
			}
			if (dr[FieldCardNumber] != DBNull.Value)
			{
			_cardNumber = (string )dr[FieldCardNumber];
			}
			if (dr[FieldCardTypeId] != DBNull.Value)
			{
			_cardTypeId = (int )dr[FieldCardTypeId];
			}
			if (dr[FieldBalance] != DBNull.Value)
			{
			_balance = (decimal )dr[FieldBalance];
			}
			if (dr[FieldDefaultMoney] != DBNull.Value)
			{
			_defaultMoney = (decimal )dr[FieldDefaultMoney];
			}
			if (dr[FieldCreateTime] != DBNull.Value)
			{
			_createTime = (DateTime )dr[FieldCreateTime];
			}
			if (dr[FieldUpdateTime] != DBNull.Value)
			{
			_updateTime = (DateTime )dr[FieldUpdateTime];
			}
			if (dr[FieldManagerId] != DBNull.Value)
			{
			_managerId = (int )dr[FieldManagerId];
			}
			if (dr[FieldCardFrom] != DBNull.Value)
			{
			_cardFrom = (string )dr[FieldCardFrom];
			}
			if (dr[FieldShopId] != DBNull.Value)
			{
			_shopId = (int )dr[FieldShopId];
			}
			if (dr[FieldStatus] != DBNull.Value)
			{
			_status = (int )dr[FieldStatus];
			}
			if (dr[FieldExpirationDate] != DBNull.Value)
			{
			_expirationDate = (DateTime )dr[FieldExpirationDate];
			}
		}
		#endregion

	}
}
