
>1. 场景初始化



```

            var searchconfig = GameConfig.GetDataWithTypeById<KSsearchGoods>(searchId);
            var info = KSGoodsController.Instance.CreateGoods(goodId, count);
            //设置耐久度
            if (info.equipInfo != null)
            {
                float durability = 0;
                switch (info.GoodType)
                {
                    case KSGoodType.Weapon:
                        durability = info.GetConfig<Shot_weapon>().durability.floatValue();
                        break;
                    case KSGoodType.Equip:
                        durability = info.GetConfig<Shot_equip>().durability.floatValue();
                        break;
                }
                float MaxDurability = durability * UnityEngine.Random.Range((searchconfig.durability - searchconfig.durabilityOffset).FloatValue(), (searchconfig.durability + searchconfig.durabilityOffset).FloatValue());
                float Durability = MaxDurability * UnityEngine.Random.Range(searchconfig.durabilityFloor.FloatValue(), 1.0f);
                //Debug.LogError($"MaxDurability ------------- {MaxDurability}");
                info.equipInfo.Init(Durability, MaxDurability);
                }

```