using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    public float smooth = 1.0f;
    public float openDistance = 3.0f; 
    private KeyCode openKey = KeyCode.E; 

    private float DoorOpenAngle = -90.0f;
    private float DoorCloseAngle = 0.0f;

    private Transform player; 
    private InventoryScript inventory; 

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        inventory = FindObjectOfType<InventoryScript>();
    }

    void Update()
    {
        if (open)
        {
            var target = Quaternion.Euler(0, DoorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
        }
        else
        {
            var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
        }

        if (Vector3.Distance(transform.position, player.position) <= openDistance)
        {
            if (Input.GetKeyDown(openKey))
            {
                if (HasKeyInHand())
                {
                    OpenDoor();
                }
            }
        }
    }

    public void OpenDoor()
    {
        open = !open;
    }

    private bool HasKeyInHand()
    {
        if (inventory.activeSlot != null && inventory.activeSlot.item != null)
        {
            if (inventory.activeSlot.item.itemType == ItemType.Key)
            {
                return true;
            }
        }
        return false;
    }
}