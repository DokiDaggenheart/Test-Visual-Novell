using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard : IClickable
{
    public void Match();
    public bool Flip();
    public int GetId();
}
