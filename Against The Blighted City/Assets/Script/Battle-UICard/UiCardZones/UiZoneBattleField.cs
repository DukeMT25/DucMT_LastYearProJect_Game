using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
///     Battlefield Zone.
/// </summary>
public class UiZoneBattleField : UiBaseDropZone
{
    protected override void OnPointerUp(PointerEventData eventData) => CardHand?.PlaySelected();
}
