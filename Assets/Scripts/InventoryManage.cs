using System.Data;
using UnityEngine;

public class InventoryManage : MonoBehaviour
{
    public UIManager uIManager;
        [System.Serializable]
    public class InventorySlot
    {
        public Mask mask;
        public bool isOccuped;
    }

    [Header("Inventaire")]
    public InventorySlot[] slots;
    public int currentIndex = 0;
    private int deblocked = 0;
    public Mask currentSelected;

    public void AddMask()
    {
        deblocked++;
        uIManager.itemPanels[deblocked - 1].itemImg.gameObject.SetActive(true);
        slots[deblocked - 1].isOccuped = true;
    }

   public void NextSlot()
    {
        int startIndex = currentIndex;

        do
        {
            currentIndex = (currentIndex + 1) % slots.Length;

            if (slots[currentIndex].isOccuped)
            {
                ApplySelection();
                return;
            }

        } while (currentIndex != startIndex);
    }

    public void PreviousSlot()
    {
        int startIndex = currentIndex;

        do
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = slots.Length - 1;

            if (slots[currentIndex].isOccuped)
            {
                ApplySelection();
                return;
            }

        } while (currentIndex != startIndex);
    }

    void ApplySelection()
    {
        uIManager.SelectItem(currentIndex);

        if (currentSelected != null)
        {
            currentSelected.UnUsedMask();
            currentSelected.gameObject.SetActive(false);
        }

        currentSelected = slots[currentIndex].mask;

        if (currentSelected != null)
            currentSelected.gameObject.SetActive(true);
    }

/*  public Mask GetCurrentMask()
    {
        if (slots.Length == 0 || !slots[currentIndex].isOccupied) return null;
        return slots[currentIndex].mask;
    }*/

}
