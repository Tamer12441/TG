using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public int amount;
    public bool isEmpty = true;
    public GameObject iconGO;
    public TMP_Text itemAmountText;

    private void Awake()
    {
        iconGO = transform.GetChild(0).GetChild(0).gameObject;
        itemAmountText = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();

    }
    public void SetIcon(Sprite icon)
    {
        iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        iconGO.GetComponent<Image>().sprite = icon;
    }
    public void ClearSlot()
    {
        item = null;
        amount = 0;
        isEmpty = true;

        // Очищаем иконку
        iconGO.GetComponent<Image>().sprite = null;
        iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);  // Делаем иконку прозрачной

        itemAmountText.text = "";
    }
}
