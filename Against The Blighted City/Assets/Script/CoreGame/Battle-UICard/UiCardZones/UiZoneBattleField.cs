using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
///     Battlefield Zone.
/// </summary>
public class UiZoneBattleField : UiBaseDropZone
{
    BattleSceneManager battleSceneManager;

    protected override void Awake()
    {
        base.Awake();

        battleSceneManager = FindObjectOfType<BattleSceneManager>();
    } 

    protected override void OnPointerUp(PointerEventData eventData)
    {
        if (battleSceneManager.selectedCard.card.cardType == Card.CardType.Skill || battleSceneManager.selectedCard.card.cardType == Card.CardType.Power)
        {
            battleSceneManager.PlayCard(battleSceneManager.selectedCard);
        }
        else CardHand?.Unselect();
    }
}
