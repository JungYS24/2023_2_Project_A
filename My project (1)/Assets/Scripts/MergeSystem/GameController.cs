using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Slot[] slots;

    private Vector3 _target;
    private ItemInfo carryingItem;

    private Dictionary<int, Slot> slotDictionary;

    void Start()
    {
        slotDictionary = new Dictionary<int, Slot>();

        for( int i = 0; i <slots.Length;i++)
        {
            slots[i].id = i;
            slotDictionary.Add(i, slots[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SendRaycast();
        }

        if (Input.GetMouseButton(0) && carryingItem)
        {
            OnItemSelected();
        }
        if (Input.GetMouseButtonUp(0))
        {
            SendRaycast();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceRandomItem();
        }
    }
    
    void SendRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var slot = hit.transform.GetComponent<Slot>();
            if(slot.state == Slot.SLOTSTATE.FULL && carryingItem == null)
            {
                string itemPath = "Prefabs/Item_Grabbed_" + slot.itemObject.id.ToString("000");
                var itemGO = (GameObject)Instantiate(Resources.Load(itemPath));
                itemGO.transform.position = slot.transform.position;
                itemGO.transform.localScale = Vector3.one * 2;
                carryingItem = itemGO.GetComponent<ItemInfo>();
                carryingItem.InitDummy(slot.id, slot.itemObject.id);
                slot.ItemGrabbed();

            }
            else if (slot.state == Slot.SLOTSTATE.EMPTY  && carryingItem != null)
            {
                slot.CreateItem(carryingItem.itemId);
                Destroy(carryingItem.gameObject);
            }
            else if (slot.state == Slot.SLOTSTATE.FULL && carryingItem != null)
            {
                if(slot.itemObject.id == carryingItem.itemId)
                {
                    OnItemMergedWithTarget(slot.id);

                }
                else
                {
                    OnItemCarryFail();
                }
            }

        }
        else
        {
            if(!carryingItem)
            {
                return;
            }
            OnItemCarryFail();
        }
    }

    void OnItemSelected()
    {
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _target.z = 0;

        var delta = 10 * Time.deltaTime;

        delta *= Vector3.Distance(transform.position, _target);
        carryingItem.transform.position = Vector3.MoveTowards(carryingItem.transform.position, _target, delta);
    }

    void OnItemMergedWithTarget( int targertSlotId)
    {
        var slot = GetSlotById(targertSlotId);
        Destroy(slot.itemObject.gameObject);
        slot.CreateItem(carryingItem.itemId + 1);
        Destroy(carryingItem.gameObject);
    }

    void OnItemCarryFail()
    {
        var slot = GetSlotById(carryingItem.slotId);
        slot.CreateItem(carryingItem.itemId);
        Destroy(carryingItem.gameObject);
    }

    void PlaceRandomItem()
    {
        if(AllSlotsOccupied())
        {
            Debug.Log(" ½½·Ô´Ù Ã¡À½ -> »ý¼ººÒ°¡ ");
            return;
        }

        var rand = UnityEngine.Random.Range(0, slots.Length);

        var slot = GetSlotById(rand);
        while (slot.state == Slot.SLOTSTATE.FULL)
        {
            rand = UnityEngine.Random.Range(0, slots.Length);

            slot = GetSlotById(rand);
        }
        slot.CreateItem(0);
    }

    bool AllSlotsOccupied()
    {
        foreach(var slot in slots)
        {
            if(slot.state== Slot.SLOTSTATE.EMPTY)
            {
                return false;
            }
        }
        return true;
    }

    Slot GetSlotById(int id)
    {
        return slotDictionary[id];
    }
}
