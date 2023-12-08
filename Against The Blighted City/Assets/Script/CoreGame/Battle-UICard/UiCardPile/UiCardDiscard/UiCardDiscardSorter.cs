using System;
using System.Collections;
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
        StartCoroutine(MoveAndDeactivate(lastCard, disPos, parameters.MovementSpeed));

        //move others
        for (var i = 0; i < cards.Length - 1; i++)
        {
            var card = cards[i];
            card.transform.rotation = Quaternion.Euler(0, 0, -12.5f);
            card.MoveToWithZ(backGravPos, parameters.MovementSpeed);
        }
    }

    IEnumerator MoveAndDeactivate(IUiCard card, Vector3 targetPosition, float movementSpeed)
    {
        // Move the card to the target position
        card.MoveToWithZ(targetPosition, movementSpeed);

        // Wait until the movement is complete
        yield return new WaitForSecondsRealtime(1f);

        // Set the game object inactive
        card.gameObject.SetActive(false);
    }

    //--------------------------------------------------------------------------------------------------------------
}
