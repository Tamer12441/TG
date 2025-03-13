using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public GameObject currentCanvas;
    public GameObject finishCanvas;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            currentCanvas.gameObject.SetActive(false);
            finishCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
