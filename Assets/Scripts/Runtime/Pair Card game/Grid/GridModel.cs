using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridModel
{
    private ICard[,] _cards;
    private Vector2 _startPosition;
    private float _offsetX;
    private float _offsetY;
    private int _rows;
    private int _columns;

    public Vector2 StartPosition { get => _startPosition; }
    public float OffsetX { get => _offsetX; }
    public float OffsetY { get => _offsetY; }
    public int Rows { get => _rows; }
    public int Columns { get => _columns; }

    public GridModel(int rows, int columns, Vector2 startPosition, float offsetX, float offsetY)
    {
        _rows = rows;
        _columns = columns;
        _startPosition = startPosition;
        _offsetX = offsetX;
        _offsetY = offsetY;
        _cards = new ICard[rows, columns];
    }

    public void SetCard(int x, int y, ICard card)
    {
        _cards[x, y] = card;
    }

    public ICard GetCard(int x, int y)
    {
        return _cards[x, y];
    }

    public List<ICard> GetAllCards()
    {
        var allCards = new List<ICard>(_rows * _columns);
        foreach (var card in _cards)
        {
            allCards.Add(card);
        }
        return allCards;
    }
}
