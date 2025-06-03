using UnityEngine;
using TMPro;
using Naninovel;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        Engine.GetService<ScoreService>().Init(this);
    }

    public void UpdateScore(int value)
    {
        if (value != 0)
            scoreText.text = $"Score: {value}";
        else
            scoreText.text = "";
    }
}