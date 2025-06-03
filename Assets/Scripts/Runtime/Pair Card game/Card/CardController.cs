using System;
using UnityEngine;

public class CardController : MonoBehaviour, ICard
{
    private CardModel _model;
    private CardView _view;
    public event Action OnClicked;

    public void Awake()
    {
        _view = GetComponent<CardView>();
    }

    public void InitializeCard(CardData data)
    {
        _view.Initialize(data);
        _model = new CardModel(data.id);
        _view.UpdateView(false);
    }

    public void Match()
    {
        _model.Match();
    }

    public bool Flip()
    {
        if (_model.IsMatched && _model.IsFlipped)
            return false;
        _model.Flip();
        _view.UpdateView(_model.IsFlipped);
        return true;
    }

    public int GetId()
    {
        return _model.Id;
    }

    private void OnMouseDown()
    {
        OnClicked?.Invoke();
    }
}
