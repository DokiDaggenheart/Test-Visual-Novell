using Naninovel;
using UnityEngine;

[InitializeAtRuntime]
public class CardGameService : IEngineService
{
    private GridBinder _grid;

    public void Init(GridBinder grid)
    {
        _grid = grid;
    }

    public async UniTask InitializeServiceAsync()
    {
        await UniTask.WaitUntil(() => _grid != null);
    }

    public void ResetService() 
    {

    }

    public void DestroyService() 
    {
        _grid = null;
    }

    public async UniTask ExecuteGame()
    {
        _grid.StartGame();
        await UniTask.WaitUntil(() => _grid.isGameEnded);
        _grid.gameObject.SetActive(false);
    }
}