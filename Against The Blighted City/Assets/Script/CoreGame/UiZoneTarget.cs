using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiZoneTarget : UiBaseDropZone
{
    BattleSceneManager battleSceneManager;
    Fighter enemyFighter;

    protected override void Awake()
    {
        base.Awake();
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
        enemyFighter = GetComponent<Fighter>();
    }

    protected override void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("clicked");
        CardHand?.Unselect();

        //if (enemyFighter == null)
        //{
        //    Debug.Log("fighta is null");
        //    battleSceneManager = FindObjectOfType<BattleSceneManager>();
        //    enemyFighter = GetComponent<Fighter>();
        //    CardHand?.Unselect();
        //}
        //if (battleSceneManager.selectedCard != null && battleSceneManager.selectedCard.card.cardType == Card.CardType.Attack)
        //{
        //    Debug.Log("oh my god");
        //    battleSceneManager.cardTarget = enemyFighter;
        //    CardHand?.PlaySelected();

        //}

        //CardHand?.Unselect();
    }

}
