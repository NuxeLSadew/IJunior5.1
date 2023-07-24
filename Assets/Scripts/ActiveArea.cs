using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ActiveArea : MonoBehaviour
{
    [SerializeField] private float _secondsToActivate;
    [SerializeField] private float _recoverySpeedModifier = 1;
    [SerializeField] private UnityEvent _onEnterEvent;
    [SerializeField] private UnityEvent<float> _onStayEvent;
    [SerializeField] private UnityEvent _onActivateEvent;
    [SerializeField] private UnityEvent _onAwayEvent;
    [SerializeField] private bool _isCanBeActivatedMultipleTimes;
    [SerializeField] private bool _isStayEventActiveWhileTimeRecovering;

    private float _currentTimer;
    private bool _isCollisionStay;
    private bool _isActivated;

    public float CurrentTimer
    {
        get => _currentTimer;
        set
        {
            if (value > _secondsToActivate)
            {
                value = _secondsToActivate;
            }

            if (value < 0)
            {
                value = 0;
            }

            _currentTimer = value;
        }
    }

    public bool IsActivated => _isActivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Movement>(out _))
        {
            _onEnterEvent.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Movement>(out _))
        {
            _isCollisionStay = true;

            if (_currentTimer < _secondsToActivate)
            {
                _currentTimer += Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Movement>(out _))
        {
            _onAwayEvent.Invoke();
            _isCollisionStay = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isCollisionStay == false && _currentTimer > 0)
        {
            _currentTimer -= Time.deltaTime * _recoverySpeedModifier;
        }

        if (_isCollisionStay || (_currentTimer > 0 && _isStayEventActiveWhileTimeRecovering))
        {
            InvokeOnStayEvent();
        }

        if (_currentTimer >= _secondsToActivate && IsActivated == false)
        {
            _isActivated = true;
            _onActivateEvent.Invoke();

            if (_isCanBeActivatedMultipleTimes == false)
            {
                this.enabled = false;
            }
        }

        if (IsActivated && _currentTimer < _secondsToActivate)
        {
            _isActivated = false;
        }
    }

    private void InvokeOnStayEvent()
    {
        float timeProportion = _currentTimer / _secondsToActivate;
        _onStayEvent.Invoke(timeProportion);
    }
}
