using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public InventorySlot oldSlot;
    private Transform player;
    private InventoryScript Inventory;

    private void Start()
    {
        Inventory = FindObjectOfType<InventoryScript>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        oldSlot = transform.GetComponentInParent<InventorySlot>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) 
        {
            DropItem();
        }
    }

    public void DropItem()
    {
        if (oldSlot.item == null) return; 

        GameObject droppedItem = Instantiate(oldSlot.item.itemPrefab, player.position + player.forward, Quaternion.identity);
        Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
        float power = GetComponent<InventoryScript>().power;
        if (rb != null)
        {
            rb.AddForce(player.forward * power, ForceMode.Impulse);
        }
        NullifySlotData();
    }

    public void NullifySlotData()
    {
        oldSlot.item = null;
        oldSlot.amount = 0;
        oldSlot.isEmpty = true;
        oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        oldSlot.iconGO.GetComponent<Image>().sprite = null;
        oldSlot.itemAmountText.text = "";
    }

    public void OnPointerUp(PointerEventData eventData) { }
    public void OnDrag(PointerEventData eventData) { }
}