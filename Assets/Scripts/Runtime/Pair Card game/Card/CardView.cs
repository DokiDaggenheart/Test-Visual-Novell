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

    public UniTask FlipCard(Sprite sprite, CancellationToken token = default, float duration = 1f)
    {
        return RotateCardAsync(duration, sprite, token);
    }

    private async UniTask RotateCardAsync(float duration, Sprite sprite, CancellationToken token)
    {
        _isFlipping = true;
        float elapsed = 0f;
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 180f;
        bool isSpriteChanged = false;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, elapsed / duration);

            if (!isSpriteChanged && yRotation > startRotation + 90)
            {
                _spriteRenderer.sprite = sprite;
                isSpriteChanged = true;
            }

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);

            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, endRotation, transform.eulerAngles.z);
        _isFlipping = false;
    }
}
