using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel
{
    public int Id { get; }
    public bool IsMatched { get; private set; }
    public bool IsFlipped { get; private set; }
    public void Flip() => IsFlipped = !IsFlipped;
    public void Match() => IsMatched = true;

    public CardModel(int id)
    {
        Id = id;
        IsMatched = false;
        IsFlipped = false;
    }
}
