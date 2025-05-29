using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GridController
{
    public event Action OnGameEnded;
    private readonly List<CardData> _cardsData;
    private readonly GridModel _gridModel;
    private List<ICard> MatchedCards;
    private List<ICard> FlippedCards = new List<ICard>();
    private ICardSpawner _cardSpawner;

    [Inject]
    public GridController(GridModel model, List<CardData> cardsData, ICardSpawner cardSpawner)
    {
        _gridModel = model;
        _cardsData = cardsData;
        _cardSpawner = cardSpawner;
    }

    public async void OnCardClicked(ICard card)
    {
        if (FlippedCards.Contains(card) || FlippedCards.Count >= 2)
            return;

        if (card.Flip())
            FlippedCards.Add(card);

        if (FlippedCards.Count == 2)
        {
            await CheckMatch();
        }
    }

    public void Init()
    {
        MatchedCards = new List<ICard>();
        GenerateGrid();
        OnGameEnded += _cardSpawner.DespawnCards;
    }

    private void GenerateGrid()
    {
        var cardsOnGrid = GenerateCardPairs(_gridModel.Rows * _gridModel.Columns);
        Shuffle(ref cardsOnGrid);
        for (int x = 0; x < _gridModel.Rows; x++)
        {
            for (int y = 0; y < _gridModel.Columns; y++)
            {
                ICard card = _cardSpawner
                    .SpawnCard(new Vector3(_gridModel.StartPosition.x + x * _gridModel.OffsetX, _gridModel.StartPosition.y + y * _gridModel.OffsetY, 0), cardsOnGrid[x * _gridModel.Columns + y]);

                _gridModel.SetCard(x, y, card);
                card.OnClicked += () => OnCardClicked(card);
            }
        }
    }

    private CardData[] GenerateCardPairs(int totalCards)
    {
        CardData[] cards = new CardData[totalCards];
        for (int i = 0; i < totalCards / 2; i++)
        {
            var card = _cardsData[i % _cardsData.Count];
            cards[i * 2] = card;
            cards[i * 2 + 1] = card;
        }
        return cards;
    }

    private void Shuffle(ref CardData[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }

    private async UniTask CheckMatch()
    {
        await UniTask.Delay(1000);

        if (FlippedCards[0].GetId() == FlippedCards[1].GetId())
        {
            MatchedCards.Add(FlippedCards[0]);
            MatchedCards.Add(FlippedCards[1]);
            FlippedCards[0].Match();
            FlippedCards[1].Match();
        }
        else
        {
             FlippedCards[0].Flip();
             FlippedCards[1].Flip();
        }

        FlippedCards.Clear();

        if (MatchedCards.Count == _gridModel.GetAllCards().Count)
            OnGameEnded?.Invoke();
    }
}
