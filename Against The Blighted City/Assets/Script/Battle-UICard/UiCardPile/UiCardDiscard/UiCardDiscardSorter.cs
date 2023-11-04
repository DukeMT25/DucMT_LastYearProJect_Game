using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UiCardDiscardPile))]
public class UiCardDiscardSorter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("World point where the discard is positioned")]
    Transform discardPosition;

    [SerializeField] UiCardParameters parameters;

    IUiCardPile CardDiscard { get; set; }

    //--------------------------------------------------------------------------------------------------------------

    void Awake()
    {
        CardDiscard = GetComponent<UiCardDiscardPile>();
        CardDiscard.OnPileChanged += Sort;
    }

    //--------------------------------------------------------------------------------------------------------------

    public void Sort(IUiCard[] cards)
    {
        if (cards == null)
            throw new ArgumentException("Can't sort a card list null");

        var lastPos = cards.Length - 1;
        var lastCard = cards[lastPos];
        var disPos = discardPosition.position + new Vector3(0, 0, -5);
        var backGravPos = discardPosition.position;

        //move last
        lastCard.MoveToWithZ(disPos, parameters.MovementSpeed);

        //move others
        for (var i = 0; i < cards.Length - 1; i++)
        {
            var card = cards[i];
            card.MoveToWithZ(backGravPos, parameters.MovementSpeed);
        }
    }

    //--------------------------------------------------------------------------------------------------------------
}
