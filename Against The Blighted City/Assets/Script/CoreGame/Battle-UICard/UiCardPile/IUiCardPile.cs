using System;

/// <summary>
///     A pile of cards.
/// </summary>
public interface IUiCardPile
{
    Action<IUiCard[]> OnPileChanged { get; set; }
    void AddCard(IUiCard uiCard);
    void RemoveCard(IUiCard uiCard);
}
