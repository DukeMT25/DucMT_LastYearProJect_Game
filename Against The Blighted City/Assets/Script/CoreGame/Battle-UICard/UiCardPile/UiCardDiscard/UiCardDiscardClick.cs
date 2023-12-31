using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
///     Dicard/Play cards when the object is clicked.
/// </summary>
[RequireComponent(typeof(IMouseInput))]
public class UiCardDiscardClick : MonoBehaviour
{
    UiPlayerHandUtils Utils { get; set; }
    IMouseInput Input { get; set; }

    void Awake()
    {
        Utils = transform.parent.GetComponentInChildren<UiPlayerHandUtils>();
        Input = GetComponent<IMouseInput>();
        Input.OnPointerClick += PlayRandom;
    }

    void PlayRandom(PointerEventData obj) => Utils.PlayCard();
}
