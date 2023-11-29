using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public Character character;
    public List<Card> playerDeck = new List<Card>();
    public List<Card> cardLibrary = new List<Card>();

    public List<Relic> relicList = new List<Relic>();
    public List<Relic> relicLibrary = new List<Relic>();

    public int battleNumber = 1;
    public int goldAmount;
    PlayerStatsUI playerStatsUI;

    private void Awake()
    {
        playerStatsUI = FindObjectOfType<PlayerStatsUI>();
    }

    public void LoadCharacterStats()
    {
        relicList.Add(character.startingRelic);
        playerStatsUI.playerStatsUIObject.SetActive(true);
        playerStatsUI.DisplayRelics();
    }

    public bool PlayerHasRelic(string relicName)
    {
        foreach (Relic relic in relicList)
        {
            if (relic.name == relicName) return true;
        }
        return false;
    }

    public void UpdateFloorNumber()
    {
        battleNumber += 1;

        switch (battleNumber) 
        {
            case 1:
                playerStatsUI.battleText.text = battleNumber + "st Floor";
                break;
            case 2:
                playerStatsUI.battleText.text = battleNumber + "nd Floor";
                break;
            case 3:
                playerStatsUI.battleText.text = battleNumber + "rd Floor";
                break;
            default:
                playerStatsUI.battleText.text = battleNumber + "th Floor";
                break;

        }
    }

    public void UpdateGoldNumber(int newGold)
    {
        goldAmount += newGold;
        playerStatsUI.moneyAmountText.text = goldAmount.ToString();
    }

    public void DisplayHealth(int healthAmount, int maxHealth)
    {
        //playerStatsUI.healthDisplayText.text = $"{healthAmount} / {maxHealth}";
    }
}
