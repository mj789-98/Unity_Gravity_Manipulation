using UnityEngine;

public class GlowBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        if (inventory != null)
        {
            inventory.GlowBoxCollected();
            gameObject.SetActive(false);
        }
    }
}