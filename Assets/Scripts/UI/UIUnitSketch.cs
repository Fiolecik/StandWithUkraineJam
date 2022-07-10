using System;
using System.Collections;
using System.Collections.Generic;
using Team;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitSketch : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text count;

    private void Awake()
    {
        icon = GetComponent<Image>();
    }

    public void SetUnit(UnitController.CardHolder cardHolder)
    {
        icon.sprite = cardHolder.basicCard.sprite;
        name.text = cardHolder.basicCard.name;
        count.text = cardHolder.countOfCards.ToString();
    }
}
