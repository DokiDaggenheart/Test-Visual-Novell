using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CardSpawner : ICardSpawner
{
    private CardPooler _cardPooler;
    private List<CardView> CardsOnBoard;
    [Inject]
    private void Construct(CardPooler cardPooler)
    {
        _cardPooler = cardPooler;
        CardsOnBoard = new List<CardView>();
    }

    public ICard SpawnCard(Vector3 position, CardData data)
    {
        var card = _cardPooler.GetCard();
        card.transform.position = position;
        CardsOnBoard.Add(card);
        var controller = card.gameObject.GetComponent<CardController>();
        controller.InitializeCard(data);
        return controller;
    }

    public void DespawnCards()
    {
        foreach(CardView card in CardsOnBoard)
        {
            _cardPooler.ReturnCard(card);
        }
    }
}
