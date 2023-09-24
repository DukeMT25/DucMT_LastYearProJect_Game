using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    //Player
    public GameObject playerPrefab;
    public Transform playerPos;


    //Default Enemy
    public GameObject enemyPrefab;
    public Transform enemyPos;

    //Other Enemy
    // public GameObject enemyPrefab2;
    // public GameObject enemyPrefab3;
    // public GameObject enemyPrefab4;
    // public Transform enemyPos2;
    // public Transform enemyPos3;
    // public Transform enemyPos4;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    //Take note from UiPlayerHandUtils
    #region Fields
    int Count { get; set; } 
    [SerializeField] [Tooltip("Prefab of the Card C#")]
    GameObject cardPrefabCs;

    [SerializeField] [Tooltip("World point where the deck is positioned")]
    Transform deckPosition;

    [SerializeField] [Tooltip("Game view transform")]
    Transform gameView;

    [SerializeField] 
    UiPlayerHand PlayerHand;
    #endregion

    void Start()
    {
        state = BattleState.START;
        //StartCoroutine(SetupBattle());
        SetupBattle();
    }

    void SetupBattle()
    {
        //Player
        GameObject playerGO = Instantiate(playerPrefab, playerPos);
        playerUnit = playerGO.GetComponent<Unit>();

        //Default Enemy
        GameObject enemyGO = Instantiate(enemyPrefab, enemyPos);
        enemyUnit = enemyGO.GetComponent<Unit>();

        //Other Enemy
        // CheckNumber(enemyPrefab2, enemyPos2);
        // CheckNumber(enemyPrefab3, enemyPos3);
        // CheckNumber(enemyPrefab4, enemyPos4);


        //HUD
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        //starting cards
        for (int i = 0; i < 5; i++)
        {
            //yield return new WaitForSeconds(0.4f);
            DrawCard();
        }

        //TURN
        state = BattleState.PLAYERTURN;
        //PlayerTurn();
    }

    void CheckNumber(GameObject ep1, Transform ep2)
    {
        if(ep1 != null && ep2 != null)
        {
            Instantiate(ep1, ep2);
        }
    }

    // void PlayerTurn()
    // {
    //     //somethinghere
    // }

    #region Operations

        [Button]
        public void DrawCard()
        {
            //TODO: Consider replace Instantiate by an Object Pool Pattern
            GameObject cardGo = Instantiate(cardPrefabCs, gameView);
            cardGo.name = "Card_" + Count;
            IUiCard card = cardGo.GetComponent<IUiCard>();
            card.transform.position = deckPosition.position;
            Count++;
            PlayerHand.AddCard(card);
        }

        [Button]
        public void PlayCard()
        {
            if (PlayerHand.Cards.Count > 0)
            {
                var randomCard = PlayerHand.Cards.RandomItem();
                PlayerHand.PlayCard(randomCard);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab)) DrawCard();
            if (Input.GetKeyDown(KeyCode.Space)) PlayCard();
            if (Input.GetKeyDown(KeyCode.Escape)) Restart();
        }

        public void Restart() => SceneManager.LoadScene(0);

        #endregion
}
