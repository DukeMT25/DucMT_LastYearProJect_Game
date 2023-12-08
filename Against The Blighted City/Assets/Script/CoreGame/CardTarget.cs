using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTarget : MonoBehaviour
{
    IUiPlayerHand cardHand { get; set; }

    BattleSceneManager battleSceneManager;
    Fighter enemyFighter;

    [SerializeField] UiPlayerHand hand;

    private void Awake()
    {
        cardHand = hand;
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
        enemyFighter = GetComponent<Fighter>();
    }
    public void PointerUp()
    {
        if (enemyFighter == null)
        {
            Debug.Log("fighta is null");

            cardHand?.Unselect();
        }
        else
        {
            //if (battleSceneManager.selectedCard.card.cardType == Card.CardType.Attack)
            //{
                //Debug.Log("oh my god");
                battleSceneManager.cardTarget = enemyFighter;
                //cardHand?.PlaySelected();
                battleSceneManager.PlayCard(battleSceneManager.selectedCard);
            //}
            //else if (battleSceneManager.selectedCard.card.cardType == Card.CardType.Skill || )
        }
        
    }
}
