using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryScript : MonoBehaviour
{
    [Header("Inventory")]
    public Transform inventoryParent;
    public InventoryManager inventoryManager;
    public int currentSlotID = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;
    public Transform itemContainer;
    public InventorySlot activeSlot = null;
    private Transform allItems;
    private Transform player;

    private Vector3[] initialScales;

    [Header("DropPower")]
    public float power;

    [Header("DotWeen")]
    public float animationDuration = 0.2f;
    public float selectedScale = 1.1f;
    public float normalScale = 1.0f; 

    private void Start()
    {
        GameObject handObject = GameObject.FindGameObjectWithTag("Hand");

        if (handObject != null)
        {
            allItems = handObject.transform;
        }
        initialScales = new Vector3[allItems.childCount];
        for (int i = 0; i < allItems.childCount; i++)
        {
            initialScales[i] = allItems.GetChild(i).localScale; 
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;

        activeSlot = inventoryParent.GetChild(currentSlotID).GetComponent<InventorySlot>();
        UpdateSlotVisuals();
    }

    private void Update()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw != 0) 
        {
            if (mw > 0)
            {
                SwitchToNextSlot();
            }
            else if (mw < 0)
            {
                SwitchToPreviousSlot();
            }
        }

        for (int i = 0; i < inventoryParent.childCount; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                SwitchToSlot(i);
            }
        }

        // Обработка выбрасывания предмета по нажатию правой кнопки мыши
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            if (activeSlot != null && activeSlot.item != null)
            {
                ThrowItem(activeSlot);
            }
        }
    }

    private void SwitchToNextSlot()
    {
        int nextSlotID = currentSlotID + 1;
        if (nextSlotID >= inventoryParent.childCount)
        {
            nextSlotID = 0;
        }
        SwitchToSlot(nextSlotID);
    }

    private void SwitchToPreviousSlot()
    {
        int previousSlotID = currentSlotID - 1;
        if (previousSlotID < 0)
        {
            previousSlotID = inventoryParent.childCount - 1;
        }
        SwitchToSlot(previousSlotID);
    }

    private void SwitchToSlot(int slotID)
    {
        if (slotID == currentSlotID)
            return;

        // Скрытие текущего слота
        Transform currentSlotTransform = inventoryParent.GetChild(currentSlotID);
        currentSlotTransform.GetComponent<Image>().sprite = notSelectedSprite;
        currentSlotTransform.DOScale(normalScale, animationDuration).SetEase(Ease.OutBack);

        // Обновляем текущий слот
        currentSlotID = slotID;
        activeSlot = inventoryParent.GetChild(currentSlotID).GetComponent<InventorySlot>();

        //Отображение нового слота
        Transform newSlotTransform = inventoryParent.GetChild(currentSlotID);
        newSlotTransform.GetComponent<Image>().sprite = selectedSprite;
        newSlotTransform.DOScale(selectedScale, animationDuration).SetEase(Ease.OutBack);

        ShowItemInHand();
    }

    private void ThrowItem(InventorySlot slot)
    {
        if (slot.item == null || slot.isEmpty)
            return;

        GameObject itemObject = Instantiate(slot.item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);

        Item itemComponent = itemObject.GetComponent<Item>();
        if (itemComponent != null)
        {
            itemComponent.amount = slot.amount;
        }

        Rigidbody rb = itemObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(player.forward * 5f, ForceMode.Impulse); 
        }
        slot.item = null;
        slot.amount = 0;
        slot.isEmpty = true;
        slot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        slot.iconGO.GetComponent<Image>().sprite = null;
        slot.itemAmountText.text = "";

        ShowItemInHand();
    }

    private void UpdateSlotVisuals()
    {
        for (int i = 0; i < inventoryParent.childCount; i++)
        {
            Transform slotTransform = inventoryParent.GetChild(i);
            if (i == currentSlotID)
            {
                slotTransform.GetComponent<Image>().sprite = selectedSprite;
                slotTransform.localScale = Vector3.one * selectedScale;
            }
            else
            {
                slotTransform.GetComponent<Image>().sprite = notSelectedSprite;
                slotTransform.localScale = Vector3.one * normalScale;
            }
        }
    }

    private void ShowItemInHand()
    {
        HideItemsInHand();
        if (activeSlot.item == null)
        {
            return;
        }

        for (int i = 0; i < allItems.childCount; i++)
        {
            if (activeSlot.item.inHandName == allItems.GetChild(i).name)
            {
                GameObject itemInHand = allItems.GetChild(i).gameObject;

                itemInHand.SetActive(true);
                itemInHand.transform.localScale = Vector3.zero;
                itemInHand.transform.DOScale(initialScales[i], animationDuration).SetEase(Ease.OutBack);
            }
        }
    }

    private void HideItemsInHand()
    {
        for (int i = 0; i < allItems.childCount; i++)
        {
            GameObject itemInHand = allItems.GetChild(i).gameObject;

            if (itemInHand.activeSelf)
            {
                itemInHand.transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack)
                    .OnComplete(() => itemInHand.SetActive(false));
            }
        }
    }
}