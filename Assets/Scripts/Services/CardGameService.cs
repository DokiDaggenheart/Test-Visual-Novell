using Zenject;
using Naninovel;

[InitializeAtRuntime]
public class CardGameService : IEngineService
{
    private GridBinder _grid;
    private readonly InputManager inputManager;
    private readonly ScriptPlayer scriptPlayer;

    public CardGameService(InputManager inputManager, ScriptPlayer scriptPlayer)
    {
        this.inputManager = inputManager;
        this.scriptPlayer = scriptPlayer;
    }

    public async UniTask InitializeServiceAsync()
    {
        _grid = GridBinder.Instance;
    }

    public void ResetService()
    {

    }

    public void DestroyService()
    {

    }

    public async UniTask ExecuteGame()
    {
        _grid.StartGame();
        await UniTask.WaitUntil(() => _grid.isGameEnded);
        _grid.gameObject.SetActive(false);
    }
}
