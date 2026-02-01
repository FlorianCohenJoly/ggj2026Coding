using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<ItemPanel> itemPanels;
    public ItemPanel currentSelected;

    public Button continueBtn;
    public Button settingBtn;
    public Button quitBtn;
    public Button backBtn;

    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject inventairPanel;

    public void Start()
    {
        continueBtn.onClick.AddListener(() =>
        {
            menuPanel.SetActive(false);
        });
        settingBtn.onClick.AddListener(() =>
        {
            menuPanel.SetActive(false);
            settingsPanel.SetActive(true);
        });
        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        backBtn.onClick.AddListener(() =>
        {
            settingsPanel.SetActive(false);
            menuPanel.SetActive(true);
        });
    }
    
    public void SelectItem(int id)
    {
        if(currentSelected != null)
        {
            currentSelected.panelImg.color = currentSelected.normalColor;
        }
        itemPanels[id].panelImg.color = itemPanels[id].highlightColor;
        currentSelected = itemPanels[id];
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf); 
        inventairPanel.SetActive(!inventairPanel.activeSelf);
    }


}
