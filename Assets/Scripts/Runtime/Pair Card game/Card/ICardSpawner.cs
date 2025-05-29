using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardSpawner
{
    public ICard SpawnCard(Vector3 position, CardData data);
    public void DespawnCards();
}
