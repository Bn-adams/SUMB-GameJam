using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShelfDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Text NameText;
    public Text DescText;
    public Text PriceText;

    public Image Icon;

    public int Stock;

    public GameObject DescriptionBox;

    public Upgrade HeldUpgrade;

    public void UpdateDisplay(Upgrade shelf)
    {
        NameText.text = shelf.name;
        DescText.text = shelf.description;
        PriceText.text = shelf.price.ToString();
        Stock = shelf.stock;
        HeldUpgrade = shelf;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DescriptionBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionBox.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(HeldUpgrade.price <= GameObject.Find("Player").GetComponent<Pc>().Gold){
            HeldUpgrade.DoUpgrade();
            UpdateDisplay(HeldUpgrade);
        }
        
    }

}
