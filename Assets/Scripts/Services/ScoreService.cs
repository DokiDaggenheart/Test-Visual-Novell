using UnityEngine;
using Naninovel;
using System;

public class ScoreService : IEngineService
{
    private int score;
    private ScoreUI scoreUI;

    public event Action<int> OnScoreChanged;

    public bool Initialized { get; private set; }

    public async UniTask InitializeServiceAsync()
    {
        var prefab = Resources.Load<ScoreUI>("ScoreUI");
        if (prefab == null)
        {
            Debug.LogError("ScoreUI prefab not found in Resources folder.");
            return;
        }

        scoreUI = UnityEngine.Object.Instantiate(prefab);
        UnityEngine.Object.DontDestroyOnLoad(scoreUI.gameObject);

        OnScoreChanged += scoreUI.UpdateScore;
        OnScoreChanged?.Invoke(score);

        Initialized = true;
        await UniTask.CompletedTask;
    }

    public void ResetService()
    {
        score = 0;
        OnScoreChanged?.Invoke(score);
    }

    public void DestroyService()
    {
        if (scoreUI != null)
        {
            OnScoreChanged -= scoreUI.UpdateScore;
            UnityEngine.Object.Destroy(scoreUI.gameObject);
            scoreUI = null;
        }

        Initialized = false;
    }

    public void Increase(int amount = 1)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }

    public void Decrease(int amount = 1)
    {
        score -= amount;
        OnScoreChanged?.Invoke(score);
    }

    public int GetCurrentScore() => score;
}
