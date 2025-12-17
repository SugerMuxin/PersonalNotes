
>1. 公用的组件节点
>![[Pasted image 20251202110706.png]]
```
            UIObjectFactory.SetPackageItemExtension("ui://GTA_MiniGame/Btn_all_YKF", typeof(KSGoodsItem));
            UIObjectFactory.SetPackageItemExtension("ui://GTA_MiniGame/Btn_item_YKF", typeof(KSBackPackItem));
            UIObjectFactory.SetPackageItemExtension("ui://GTA_MiniGame/item_Discard", typeof(KSTrashComponent));
            UIObjectFactory.SetPackageItemExtension("ui://GTA_MiniGame/Btn_equip_YKF", typeof(KSEquipItem));
            UIObjectFactory.SetPackageItemExtension("ui://GTA_MiniGame/EquipType_YKF", typeof(KSEquipTypeItem));

```



>2.KS拖拽组件

```
KSDragItem item = KSDragItem.GetDragItem();
item.SetDragInfo(configInfo);
item.SetData(_icon.url, _localType, packCount);
DragDropManager.inst.StartDrag(this, item);
```


>3.物品操作
```
	KSGoodsConfigInfo.cs
		ReDurability      //耐久度修改//
		AddModifySlot     //添加改装//
		RemoveModifySlot  //移除改装//
```


