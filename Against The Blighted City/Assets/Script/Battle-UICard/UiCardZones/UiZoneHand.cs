using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
///     GameController hand zone.
/// </summary>
public class UiZoneHand : UiBaseDropZone
{
    protected override void OnPointerUp(PointerEventData eventData) => CardHand?.Unselect();
}
