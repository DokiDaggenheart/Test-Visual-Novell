using Naninovel;

[CommandAlias("addScore")]
public class AddScoreCommand : Command
{
    [ParameterAlias("amount")]
    public IntegerParameter Amount;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var scoreService = Engine.GetService<ScoreService>();
        scoreService?.Increase(Amount?.Value ?? 1);
        return UniTask.CompletedTask;
    }
}