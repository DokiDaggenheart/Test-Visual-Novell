using Naninovel;
using System;
using UnityEngine;

[InitializeAtRuntime]
public class ScoreService : IEngineService
{
    public event Action<int> OnScoreChanged;
    private int _score;
    private ScoreUI _scoreUI;
    private CustomVariableManager _variableManager;

    public ScoreService(CustomVariableManager variableManager)
    {
        _variableManager = variableManager;
    }

    public async UniTask InitializeServiceAsync()
    {
        _score = 0;
    }

    public void Init(ScoreUI scoreUI)
    {
        _scoreUI = scoreUI;
        OnScoreChanged += scoreUI.UpdateScore;
        OnScoreChanged?.Invoke(_score);
    }

    public void ResetService()
    {
        _score = 0;
        OnScoreChanged?.Invoke(_score);
    }

    public void DestroyService()
    {
        if (_scoreUI != null)
        {
            OnScoreChanged -= _scoreUI.UpdateScore;
            UnityEngine.Object.Destroy(_scoreUI.gameObject);
            _scoreUI = null;
        }
    }

    public void Increase(int amount = 1)
    {
        _score += amount;
        OnScoreChanged?.Invoke(_score);
        Debug.Log(_score);
    }

    public void CheckScore()
    {
        if (_score >= 10)
            Engine.GetService<ICustomVariableManager>().SetVariableValue("hasEnoughScore", "true");
        else
            Engine.GetService<ICustomVariableManager>().SetVariableValue("hasEnoughScore", "false");
    }

    public int GetCurrentScore() => _score;
}
