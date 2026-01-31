using System.Data;
using UnityEngine;

public class InventoryManage : MonoBehaviour
{
    public UIManager uIManager;
        [System.Serializable]
    public class InventorySlot
    {
        public Mask mask;
        public bool isOccupied;
    }

    [Header("Inventaire")]
    public InventorySlot[] slots;
    private int currentIndex = 0;
    private int deblocked = 0;

    public void AddMask()
    {
        deblocked++;
        uIManager.itemPanels[deblocked - 1].itemImg.gameObject.SetActive(true);
        slots[deblocked - 1].isOccupied = true;
    }

     // Sélectionner le slot suivant
    public void NextSlot()
    {
        int startIndex = currentIndex;

        do
        {
            currentIndex = (currentIndex + 1) % slots.Length;
        } while (!slots[currentIndex].isOccupied && currentIndex != startIndex);

    }

    // Sélectionner le slot précédent
    public void PreviousSlot()
    {
        int startIndex = currentIndex;

        do
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = slots.Length - 1;
        } while (!slots[currentIndex].isOccupied && currentIndex != startIndex);

    }

/*  public Mask GetCurrentMask()
    {
        if (slots.Length == 0 || !slots[currentIndex].isOccupied) return null;
        return slots[currentIndex].mask;
    }*/

}
