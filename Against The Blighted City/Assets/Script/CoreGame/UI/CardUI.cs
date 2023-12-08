using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class CardUI : UiCardComponent, IUiCard
{
    public Card card;
    public TMP_Text cardTitleText;
    public TMP_Text cardDescriptionText;
    public TMP_Text cardTypeText;
    public TMP_Text cardCostText;
    public Image cardImage;

    BattleSceneManager battleSceneManager;
    public bool isUpgraded;

    private void Awake()
    {
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
        
    }

    public void LoadCard(Card _card)
    {
        battleSceneManager = FindObjectOfType<BattleSceneManager>();

        card = _card;
        cardTitleText.text = card.cardTitle;
        cardDescriptionText.text = card.GetCardDescriptionAmount();
        cardTypeText.text = card.cardType.ToString();
        cardCostText.text = card.GetCardCostAmount().ToString();
        cardImage.sprite = card.cardIcon;
    }

    public void SelectCard()
    {
        //Debug.Log("clicked");
        battleSceneManager.selectedCard = this;
    }
    public void DeselectCard()
    {
        Debug.Log("card is deselected");
        //battleSceneManager.selectedCard = null;
    }
    public int GetCardCostAmount()
    {
        return int.Parse(cardCostText.text); 
    }
}
