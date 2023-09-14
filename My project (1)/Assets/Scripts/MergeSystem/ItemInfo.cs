using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public int slotId;
    public int itemId;

    public void InitDummy(int slotId, int ItemId)
    {
        this.slotId = slotId;
        this.itemId = ItemId;
    }
}
