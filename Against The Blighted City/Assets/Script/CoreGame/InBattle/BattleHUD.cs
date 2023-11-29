using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Slider hpSlider;

    public void SetHUD(Fighter fighter)
    {
        hpSlider.maxValue = fighter.maxHealth;
        hpSlider.value = fighter.currentHealth;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
