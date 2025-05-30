using Naninovel;
using UnityEngine;

[InitializeAtRuntime]
public class CardGameService : IEngineService
{
    private GridBinder _grid;
    private readonly CustomVariableManager _variableManager;

    public CardGameService(CustomVariableManager customVariableManager)
    {
        _variableManager = customVariableManager;
    }

    public void Init(GridBinder grid)
    {
        _grid = grid;
    }

    public async UniTask InitializeServiceAsync()
    {
        await UniTask.WaitUntil(() => _grid != null);
        Debug.Log("Initialization completed");
    }

    public void ResetService() { }

    public void DestroyService() { }

    public async UniTask ExecuteGame()
    {
        Debug.Log("Game Wants to execute");
        _grid.StartGame();
        await UniTask.WaitUntil(() => _grid.isGameEnded);
        _grid.gameObject.SetActive(false);
    }
}