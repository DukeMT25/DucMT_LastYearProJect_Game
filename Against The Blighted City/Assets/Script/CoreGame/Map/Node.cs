using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public Image clickedIcon;
    public Image availableIcon;
    public Floor floor;
    public void ClickMe()
    {
        floor.ClickedOnMe(this);
    }
}
