using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class CoinCollector : MonoBehaviour
{
    [SerializeField] private UnityEvent _pickUpEvent;

    private bool _isHoldingCoin;

    public event UnityAction PickUpEvent
    {
        add => _pickUpEvent.AddListener(value);
        remove => _pickUpEvent.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out _))
        {
            if (_isHoldingCoin)
            {
                Debug.Log("Нет места!");
                return;
            }

            PickUpCoin();

            SpriteRenderer coinRenderer = collision.GetComponent<SpriteRenderer>();
            SpriteRenderer collectorRenderer = GetComponent<SpriteRenderer>();
            coinRenderer.sortingOrder = collectorRenderer.sortingOrder + 1;
            collision.transform.SetParent(gameObject.transform);
            collision.transform.position = transform.position;
        }
    }

    public void PickUpCoin()
    {
        _isHoldingCoin = true;
        _pickUpEvent.Invoke();
    }
}
