using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Data", order = 51)]
public class CardData : ScriptableObject
{
    public Sprite frontSprite;
    public Sprite backSprite;
    public int id;
}
