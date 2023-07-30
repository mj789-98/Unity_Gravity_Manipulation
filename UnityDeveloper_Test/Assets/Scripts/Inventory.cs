using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int NumberOfGlowBoxes { get; private set; }

    public UnityEvent<int> OnGlowBoxCollected;
    public UnityEvent OnWin;

    public void GlowBoxCollected()
    {
        NumberOfGlowBoxes++;
        OnGlowBoxCollected.Invoke(NumberOfGlowBoxes);

        // Check if the winning condition is met
        if (NumberOfGlowBoxes >= 20)
        {
            OnWin.Invoke();
        }
    }
}