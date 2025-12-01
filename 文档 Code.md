
>1. 公用的节点
```
            UIObjectFactory.SetPackageItemExtension("ui://GTA_MiniGame/Btn_all_YKF", typeof(KSGoodsItem));
            UIObjectFactory.SetPackageItemExtension("ui://GTA_MiniGame/Btn_item_YKF", typeof(KSBackPackItem));
            UIObjectFactory.SetPackageItemExtension("ui://GTA_MiniGame/Btn_equip_YKF", typeof(KSEquipItem));

```



>2. 搜索功能
![[Pasted image 20251118090439.png]]

```
/// <summary>
/// 战场探索（宝箱搜寻，战斗怪物遗落）//
/// <param name="rewards"></param>
/// <param name="searched">宝箱是否已经探索过</param>
/// </summary>
   public void BattleSearching(List<Tuple<string, int>> rewards, bool needSearch = true) {
            KSBattleRewardsView.create(rewards, needSearch);
        }
 
 
 // 调用 Example
 List<Tuple<string, int>> rewardIds = new List<Tuple<string, int>>();
 rewardIds.Add(new Tuple<string, int>("500001", 3));
 rewardIds.Add(new Tuple<string, int>("500002", 1));
 rewardIds.Add(new Tuple<string, int>("500003", 4));
 rewardIds.Add(new Tuple<string, int>("500004", 8));
 KSGoodsController.Instance.BattleSearching(rewardIds,true);

```

>3.拖拽

```
KSDragItem item = KSDragItem.GetDragItem();
item.SetDragInfo(configInfo);
item.SetData(_icon.url, _localType, packCount);
DragDropManager.inst.StartDrag(this, item);
```


