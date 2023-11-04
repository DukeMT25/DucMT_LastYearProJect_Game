using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Card discard holds a register with cards played by the player.
/// </summary>
public class UiCardDiscardPile : UiCardPile
{
    [SerializeField]
    [Tooltip("World point where the discard is positioned")]
    Transform graveyardPosition;

    //--------------------------------------------------------------------------------------------------------------

    IUiPlayerHand PlayerHand { get; set; }


    //--------------------------------------------------------------------------------------------------------------

    #region Unitycallbacks

    protected override void Awake()
    {
        base.Awake();
        PlayerHand = transform.parent.GetComponentInChildren<IUiPlayerHand>();
        PlayerHand.OnCardPlayed += AddCard;
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Operations

    /// <summary>
    ///     Adds a card to the graveyard or discard pile.
    /// </summary>
    /// <param name="card"></param>
    public override void AddCard(IUiCard card)
    {
        if (card == null)
            throw new ArgumentNullException("Null is not a valid argument.");

        Cards.Add(card);
        card.transform.SetParent(graveyardPosition);
        card.Discard();
        NotifyPileChange();
    }


    /// <summary>
    ///     Removes a card from the graveyard or discard pile.
    /// </summary>
    /// <param name="card"></param>
    public override void RemoveCard(IUiCard card)
    {
        if (card == null)
            throw new ArgumentNullException("Null is not a valid argument.");

        Cards.Remove(card);
        NotifyPileChange();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------
}
