using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class CardView : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Sprite _frontSprite;
    private Sprite _backSprite;
    private bool _isFlipping;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Sprite frontSprite, Sprite backSprite)
    {
        _frontSprite = frontSprite;
        _backSprite = backSprite;
    }

    public void Initialize(CardData data)
    {
        Initialize(data.frontSprite, data.backSprite);
    }

    public async UniTask UpdateView(bool isFlipped, CancellationToken token = default)
    {
        await UniTask.WaitUntil(() => !_isFlipping);
        var newCardSprite = isFlipped ? _frontSprite : _backSprite;
        await FlipCard(newCardSprite, token);
    }

    public UniTask FlipCard(Sprite sprite, CancellationToken token = default, float duration = 0.5f)
    {
        return RotateCardAsync(duration, sprite, token);
    }

    private async UniTask RotateCardAsync(float duration, Sprite sprite, CancellationToken token)
    {
        _isFlipping = true;
        Vector3 startScale = new Vector3(2f, 2f, 2f);
        float halfDuration = duration / 2f;
        await AnimateScaleX(startScale.x, 0f, halfDuration, token);
        _spriteRenderer.sprite = sprite;
        await AnimateScaleX(0f, startScale.x, halfDuration, token);
        transform.localScale = startScale;
        _isFlipping = false;
    }

    private async UniTask AnimateScaleX(float from, float to, float duration, CancellationToken token)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float scaleX = Mathf.Lerp(from, to, elapsed / duration);
            transform.localScale = new Vector3(scaleX, 2f, 2f);
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
    }
}
