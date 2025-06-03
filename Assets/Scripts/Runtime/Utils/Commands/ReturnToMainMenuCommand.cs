using Naninovel;

[CommandAlias("returnToMainMenu")]
public class ReturnToMainMenuCommand : Command
{
    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        Engine.GetService<IScriptPlayer>().Stop();
        await Engine.GetService<IStateManager>().ResetStateAsync();
        Engine.GetService<IUIManager>().GetUI<Naninovel.UI.TitleMenu>().Show();
    }
}