using UnityEngine;

public class LerpedColorChanger : MonoBehaviour
{
    [SerializeField] private Color _targetColor;

    private SpriteRenderer _renderer;
    private Color _baseColor;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _baseColor = _renderer.color;
    }

    public void ChangeColor(float value)
    {
        _renderer.color = Color.Lerp(_targetColor, _baseColor, value);
    }

    public void ChangeColorReversed(float value)
    {
        _renderer.color = Color.Lerp(_baseColor, _targetColor, value);
    }
}
