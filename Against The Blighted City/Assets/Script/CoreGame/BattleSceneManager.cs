using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    [Header("Cards")]
    public List<CardUI> deck;
    public List<Card> drawPile = new List<Card>();
    public List<CardUI> cardsInHand = new List<CardUI>();
    public List<Card> discardPile = new List<Card>();
    public CardUI selectedCard;

    [SerializeField] UiPlayerHand CardHand;
    [SerializeField] UiPlayerHandUtils CardDrawer;

    [Header("Stats")]
    public Fighter cardTarget;
    public Fighter player;
    public int maxEnergy;
    public int energy;
    public int drawAmount = 5;
    public Turn turn;
    public enum Turn { Player, Enemy };

    [Header("UI")]
    public Button endTurnButton;
    public TMP_Text drawPileCountText;
    public TMP_Text discardPileCountText;
    public TMP_Text energyText;
    public Transform topParent;
    public Transform enemyParent;
    public EndScreen endScreen;

    [Header("Enemies")]
    public List<Enemy> enemies = new List<Enemy>();
    List<Fighter> enemyFighters = new List<Fighter>();
    public GameObject[] possibleEnemies;
    public GameObject[] possibleElites;
    bool eliteFight;

    CardActions cardActions;
    GameManager gameManager;
    PlayerStatsUI playerStatsUI;
    public Animator banner;
    public TMP_Text turnText;
    public GameObject gameover;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        cardActions = GetComponent<CardActions>();
        playerStatsUI = FindObjectOfType<PlayerStatsUI>();

    }
    public void StartHallwayFight()
    {
        BeginBattle(possibleEnemies);
    }
    public void StartEliteFight()
    {
        eliteFight = true;
        BeginBattle(possibleElites);
    }
    public void BeginBattle(GameObject[] prefabsArray)
    {
        turnText.text = "Player's Turn";

        GameObject newEnemy = Instantiate(prefabsArray[Random.Range(0, prefabsArray.Length)], enemyParent);

        if (endScreen != null)
            endScreen.gameObject.SetActive(false);

        Enemy[] eArr = FindObjectsOfType<Enemy>();
        enemies = new List<Enemy>();

        #region discard hand
        
        ClearHand();

        #endregion

        discardPile = new List<Card>();
        drawPile = new List<Card>();
        cardsInHand = new List<CardUI>();

        foreach (Enemy e in eArr) { enemies.Add(e); }
        foreach (Enemy e in eArr) { enemyFighters.Add(e.GetComponent<Fighter>()); }
        foreach (Enemy e in enemies) e.DisplayIntent();

        energy = maxEnergy;

        discardPile.AddRange(gameManager.playerDeck);
        ShuffleCards();
        DrawCards(drawAmount);

        #region relic checks

        if (gameManager.PlayerHasRelic("Preserved Insect") && eliteFight)
            enemyFighters[0].currentHealth = (int)(enemyFighters[0].currentHealth * 0.25);

        if (gameManager.PlayerHasRelic("Anchor"))
            player.AddBlock(10);

        if (gameManager.PlayerHasRelic("Lantern"))
            energy += 1;

        if (gameManager.PlayerHasRelic("BagofMarbles"))
            enemyFighters[0].AddBuff(Buff.Type.vulnerable, 1);

        if (gameManager.PlayerHasRelic("BagofPreparation"))
            DrawCards(2);

        if (gameManager.PlayerHasRelic("Vajra"))
            player.AddBuff(Buff.Type.strength, 1);

        #endregion



        energyText.text = energy.ToString();

    }
    public void ShuffleCards()
    {
        discardPile.Shuffle();
        drawPile = new List<Card>(discardPile);
        discardPile = new List<Card>();
        discardPileCountText.text = discardPile.Count.ToString();
    }
    public void DrawCards(int amountToDraw)
    {
        int cardsDrawn = 0;
        while (cardsDrawn < amountToDraw && cardsInHand.Count <= 10)
        {
            if (drawPile.Count < 1)
                ShuffleCards();

            var drawnCard = CardDrawer.DrawCard();
            CardUI upperLayer = drawnCard.gameObject.GetComponentInChildren<CardUI>();

            upperLayer.LoadCard(drawPile[0]);

            cardsInHand.Add(upperLayer);

            drawPile.Remove(drawPile[0]);
            drawPileCountText.text = drawPile.Count.ToString();
            cardsDrawn++;
        }
    }
    
    public void PlayCard(CardUI cardUI)
    {
        if (this.energy < cardUI.GetCardCostAmount()) CardHand?.Unselect();

        else
        {
            //Debug.Log("played card");
            //GoblinNob is enraged
            if (cardUI.card.cardType != Card.CardType.Attack && enemies[0].GetComponent<Fighter>().enrage.buffValue > 0)
                enemies[0].GetComponent<Fighter>().AddBuff(Buff.Type.strength, enemies[0].GetComponent<Fighter>().enrage.buffValue);

            cardActions.PerformAction(cardUI.card, cardTarget);

            energy -= cardUI.GetCardCostAmount();
            energyText.text = energy.ToString();

            CardHand?.PlaySelected();

            selectedCard = null;
            cardTarget = null;

            DiscardCard(cardUI.card);
            cardsInHand.Remove(cardUI);
        }
    }
    public void DiscardCard(Card card)
    {
        discardPile.Add(card);
        discardPileCountText.text = discardPile.Count.ToString();
    }

    public void ClearHand()
    {
        // Clear Hand
        List<CardUI> cardsInHandCopy = new List<CardUI>(cardsInHand);
        for (int i = cardsInHandCopy.Count - 1; i >= 0; i--)
        {
            if (CardHand.Cards.Count >= 1) CardHand.PlayCard(CardHand.Cards.RandomItem());

            DiscardCard(cardsInHandCopy[i].card);
            cardsInHand.Remove(cardsInHandCopy[i]);
        }
    }

    public void ChangeTurn()
    {
        Debug.Log("ChangeTurn called. Player: " + player + ", FighterHealthBar: " + (player != null ? player.fighterHealthBar : "null"));

        if (turn == Turn.Player)
        {
            turn = Turn.Enemy;
            endTurnButton.enabled = false;

            ClearHand();

            foreach (Enemy e in enemies)
            {
                if (e.thisEnemy == null)
                    e.thisEnemy = e.GetComponent<Fighter>();

                if (e.thisEnemy != null)
                {
                    //reset block
                    e.thisEnemy.currentBlock = 0;
                    e.thisEnemy.fighterHealthBar.DisplayBlock(0);
                }
            }

            player.EvaluateBuffsAtTurnEnd();
            StartCoroutine(HandleEnemyTurn());
        }
        else
        {
            foreach (Enemy e in enemies)
            {
                e.DisplayIntent();
            }
            turn = Turn.Player;

            //reset block
            player.currentBlock = 0;
            player.fighterHealthBar.DisplayBlock(0);

            energy = maxEnergy;
            energyText.text = energy.ToString();

            endTurnButton.enabled = true;
            DrawCards(drawAmount);

            turnText.text = "Player's Turn";
        }
    }
    private IEnumerator HandleEnemyTurn()
    {
        turnText.text = "Enemy's Turn";

        yield return new WaitForSeconds(1.5f);

        foreach (Enemy enemy in enemies)
        {
            enemy.midTurn = true;
            enemy.TakeTurn();
            while (enemy.midTurn)
                yield return new WaitForEndOfFrame();
        }
        Debug.Log("Turn Over");
        ChangeTurn();
    }
    public void EndFight(bool win)
    {
        if (!win)
            gameover.SetActive(true);

        if (gameManager.PlayerHasRelic("Burning Blood"))
        {
            player.currentHealth += 6;
            if (player.currentHealth > player.maxHealth)
                player.currentHealth = player.maxHealth;

            player.UpdateHealthUI(player.currentHealth);
        }

        player.ResetBuffs();
        ClearHand();
        HandleEndScreen();

        gameManager.UpdateFloorNumber();
        gameManager.UpdateGoldNumber(enemies[0].goldDrop);

    }
    public void HandleEndScreen()
    {
        //gold
        endScreen.gameObject.SetActive(true);
        endScreen.goldReward.gameObject.SetActive(true);
        endScreen.cardRewardButton.gameObject.SetActive(true);

        endScreen.goldReward.relicName.text = enemies[0].goldDrop.ToString() + " Gold";
        gameManager.UpdateGoldNumber(gameManager.goldAmount += enemies[0].goldDrop);

        //relics
        if (enemies[0].nob)
        {
            gameManager.relicLibrary.Shuffle();
            endScreen.relicReward.gameObject.SetActive(true);
            endScreen.relicReward.DisplayRelic(gameManager.relicLibrary[0]);
            gameManager.relicList.Add(gameManager.relicLibrary[0]);
            gameManager.relicLibrary.Remove(gameManager.relicLibrary[0]);
            playerStatsUI.DisplayRelics();
        }
        else
        {
            endScreen.relicReward.gameObject.SetActive(false);
        }

    }
}
