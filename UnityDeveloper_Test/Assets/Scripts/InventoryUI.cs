using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI glowboxText;
    public GameObject youWinPanel;

    private void Start()
    {
        // Find the Inventory component in the scene
        Inventory inventory = FindObjectOfType<Inventory>();

        // Subscribe to the OnGlowBoxCollected event
        inventory.OnGlowBoxCollected.AddListener(UpdateGlowBoxText);

        // Subscribe to the OnWin event
        inventory.OnWin.AddListener(YouWin);
    }

    public void UpdateGlowBoxText(int numberOfGlowBoxes)
    {
        glowboxText.text = numberOfGlowBoxes.ToString();
    }

    private void YouWin()
    {
        // Display "YouWin" panel
        youWinPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }
}
