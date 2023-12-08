using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TMP_Text amount;
    private void OnEnable()
    {
        amount.text = "Floors Climbed: " + (FindObjectOfType<GameManager>().battleNumber - 1).ToString();
    }
}
