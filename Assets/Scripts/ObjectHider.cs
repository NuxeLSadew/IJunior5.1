using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ObjectHider : MonoBehaviour
{
    private BoxCollider2D _collider;
    private SpriteRenderer _renderer;
    private bool _isHided;

    public bool IsHided => _isHided;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Hide()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
        _isHided = true;
    }

    public void Show()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
        _isHided = false;
    }
}