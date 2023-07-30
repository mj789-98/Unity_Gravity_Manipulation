using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectible"))
        {
            Destroy(other.gameObject);
            gameManager.UpdateScore();

            other.tag = "Untagged"; //remove tag to prevent multiple activations with same object
        }
    }

}
