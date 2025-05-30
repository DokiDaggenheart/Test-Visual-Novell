using System.Collections.Generic;
using UnityEngine;

public class CardPooler : MonoBehaviour
{
    [SerializeField] private CardView cardPrefab;
    [SerializeField] private int initialPoolSize = 12;
    [SerializeField] private int expandBatchSize = 4;
    private readonly Queue<CardView> _cardPool = new();

    private void Awake()
    {
        ExpandPool(initialPoolSize);
    }

    private void ExpandPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var card = Instantiate(cardPrefab, transform);
            card.gameObject.SetActive(false);
            _cardPool.Enqueue(card);
        }
    }

    public CardView GetCard()
    {
        if (_cardPool.Count == 0)
        {
            ExpandPool(expandBatchSize);
        }

        var card = _cardPool.Dequeue();
        card.gameObject.SetActive(true);
        return card;
    }

    public void ReturnCard(CardView card)
    {
        card.gameObject.SetActive(false);
        _cardPool.Enqueue(card);
    }
}