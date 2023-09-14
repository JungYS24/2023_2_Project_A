using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public enum SLOTSTATE
    {
        EMPTY,
        FULL
    }

    public int id;
    public Item itemObject;
    public SLOTSTATE state = SLOTSTATE.EMPTY;

    private void ChangeStateTo(SLOTSTATE  targetState)
    {
        state = targetState;
    }
    public void ItemGrabbed()
    {
        Destroy(itemObject.gameObject);
        ChangeStateTo(SLOTSTATE.EMPTY);

    }

    public void CreateItem (int id)
    {
        string itemPath = "Prefabs/Item_" + id.ToString("000");
        var itemGO = (GameObject)Instantiate(Resources.Load(itemPath));

        itemGO.transform.SetParent(this.transform);
        itemGO.transform.localPosition = Vector3.zero;
        itemGO.transform.localScale = Vector3.one;

        itemObject = itemGO.GetComponent<Item>();
        itemObject.Init(id, this);

        ChangeStateTo(SLOTSTATE.FULL);
    }
}
