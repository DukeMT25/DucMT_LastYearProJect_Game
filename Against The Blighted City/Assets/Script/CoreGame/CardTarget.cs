using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTarget : MonoBehaviour
{
    BattleSceneManager battleSceneManager;
    Fighter enemyFighter;

    UiZoneTarget dropZonetg; 

    private void Awake()
    {
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
        enemyFighter = GetComponent<Fighter>();
    }
    public void PointerEnter()
    {
        if (enemyFighter == null)
        {
            Debug.Log("fighta is null");
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            enemyFighter = GetComponent<Fighter>();
        }

        if (battleSceneManager.selectedCard != null && battleSceneManager.selectedCard.card.cardType == Card.CardType.Attack)
        {
            //target == enemy
            battleSceneManager.cardTarget = enemyFighter;
            //Debug.Log("set target");
        }
    }
    public void PointerExit()
    {
        battleSceneManager.cardTarget = null;
        //Debug.Log("drop target");
    }
}
