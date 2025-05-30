using Naninovel;

[CommandAlias("checkScore")]
public class CheckScoreCommand : Command
{
    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        return UniTask.CompletedTask;
    }
}
