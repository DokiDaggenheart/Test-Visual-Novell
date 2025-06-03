using Naninovel;

[CommandAlias("checkScore")]
public class CheckScoreCommand : Command
{
    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var scoreService = Engine.GetService<ScoreService>();
        scoreService?.CheckScore();
        return UniTask.CompletedTask;
    }
}
