using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public Color highlightColor = new Color(1f, 1f, 0f, 0.5f);
    public Color normalColor = new Color(1f, 1f, 1f, 0.39f);
    public Image itemImg;
    public Image panelImg;

    public void SelectPanel(bool state)
    {
        if (state)
        {
            panelImg.color = highlightColor;
        }
        else
        {
            panelImg.color = normalColor;
        }
        
    }
}
