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

    protected Upgrade HeldUpgrade;

    public int Stock;

    public GameObject DescriptionBox;

    public void UpdateDisplay(Shelf shelf)
    {
        NameText.text = shelf.name;
        DescText.text = shelf.description;
        PriceText.text = shelf.price.ToString();
        HeldUpgrade = shelf.upgrade;
        Stock = shelf.stock;
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
        HeldUpgrade.DoUpgrade();
    }

}
