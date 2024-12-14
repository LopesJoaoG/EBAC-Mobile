using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : CollectableBase
{
    public int amount;
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.instance.AddCoins(base.collectableTag, amount);
        
    }
}
