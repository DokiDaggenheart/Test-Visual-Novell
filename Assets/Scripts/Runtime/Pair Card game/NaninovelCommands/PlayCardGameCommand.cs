using System.Threading;
using System.Threading.Tasks;
using Naninovel;

[CommandAlias("cardGame")]
public class PlayCardGameCommand : Command
{
    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var cardGameService = Engine.GetService<CardGameService>();
        if (cardGameService == null)
        {
            UnityEngine.Debug.LogError("Card game isnt registered");
            return;
        }

        await cardGameService.ExecuteGame();
    }
}
