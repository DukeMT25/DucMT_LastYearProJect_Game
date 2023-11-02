using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(IMouseInput))]
public class UiCardDrawerClick : MonoBehaviour
{
    UiPlayerHandUtils CardDrawer { get; set; }
    IMouseInput Input { get; set; }

    void Awake()
    {
        CardDrawer = transform.parent.GetComponentInChildren<UiPlayerHandUtils>();
        Input = GetComponent<IMouseInput>();
        Input.OnPointerClick += DrawCard;
    }

    void DrawCard(PointerEventData obj) => CardDrawer.DrawCard();
}
