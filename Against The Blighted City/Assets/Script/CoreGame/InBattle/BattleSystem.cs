using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    [SerializeField] UiPlayerHandUtils CardDrawer;

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

    //Unit playerUnit;
    //Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;




    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        //SetupBattle();
    }

    IEnumerator SetupBattle()
    {
        //Player
        //GameObject playerGO = Instantiate(playerPrefab, playerPos);
        //playerUnit = playerGO.GetComponent<Unit>();

        //Default Enemy
        //GameObject enemyGO = Instantiate(enemyPrefab, enemyPos);
        //enemyUnit = enemyGO.GetComponent<Unit>();

        //Other Enemy
        // CheckNumber(enemyPrefab2, enemyPos2);
        // CheckNumber(enemyPrefab3, enemyPos3);
        // CheckNumber(enemyPrefab4, enemyPos4);


        //HUD
        //playerHUD.SetHUD(playerUnit);
        //enemyHUD.SetHUD(enemyUnit);

        //starting cards
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.4f);
            CardDrawer.DrawCard();
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

}
