﻿using System.Windows.Controls;

namespace DQ11
{
	class BagToolItem : AllStatus
	{
		private readonly ComboBox mItem;
		private readonly TextBox mCount;
		private readonly uint mAddress;
		private uint mPage;

		public BagToolItem(ComboBox item, TextBox count, uint address)
		{
			mItem = item;
			mCount = count;
			mAddress = address;
		}

		public override void Init()
		{
			Item item = Item.Instance();
			mItem.Items.Add(item.None);
			foreach (ItemInfo info in item.Tools)
			{
				mItem.Items.Add(info);
			}
		}

		public override void Open()
		{
			uint address = 0x3E34 + mPage * 12 * 4 + mAddress;
			SaveData saveData = SaveData.Instance();
			uint id = saveData.ReadNumber(address, 2);
			Item item = Item.Instance();
			// 不明があれば削る.
			if (!(mItem.Items[mItem.Items.Count - 1] is ItemInfo))
			{
				mItem.Items.RemoveAt(mItem.Items.Count - 1);
			}

			ItemInfo info = item.GetToolItemInfo(id);
			if (info == null)
			{
				mItem.Items.Add("不明" + id.ToString());
				mItem.SelectedIndex = mItem.Items.Count - 1;
			}
			else
			{
				mItem.Text = info.Name;
			}

			uint count = saveData.ReadNumber(address + 2, 2);
			mCount.Text = count.ToString();
		}

		public override void Save()
		{
			ItemInfo info = mItem.SelectedItem as ItemInfo;
			if (info == null) return;
			uint address = 0x3E34 + mPage * 12 * 4 + mAddress;
			SaveData saveData = SaveData.Instance();
			saveData.WriteNumber(address, 2, info.ID);

			uint count;
			if (uint.TryParse(mCount.Text, out count) == false) return;
			if (count < 1) count = 1;
			if (count > 99) count = 99;
			if (info == Item.Instance().None) count = 0;
			saveData.WriteNumber(address + 2, 2, count);
		}

		public override void Page(uint page)
		{
			mPage = page;
		}
	}
}