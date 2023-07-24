using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Coin : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPickedUpEvent;

    private CoinCollector _holder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CoinCollector>(out CoinCollector coinCollector))
        {
            _holder = coinCollector;
            InvokeOnPickedUpEvent();
        }
    }

    public void InvokeOnPickedUpEvent()
    {
        _onPickedUpEvent.Invoke();
    }
}
