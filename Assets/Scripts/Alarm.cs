using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _secondsToMaxVolume;

    private AudioSource _audioSource;
    private Coroutine _volumeChangeCoroutine;
    private VolumeChangeDirection _volumeChangeDirection;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Movement>(out _))
        {
            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }

            Debug.Log($"{_audioSource.isPlaying}");
            _volumeChangeDirection = VolumeChangeDirection.Up;
            _volumeChangeCoroutine = StartCoroutine(VolumeChange());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Movement>(out _))
        {
            _volumeChangeDirection = VolumeChangeDirection.Down;
        }
    }

    private void FixedUpdate()
    {
        if (_audioSource.volume <= 0 && _audioSource.isPlaying)
        {
            _audioSource.Stop();
            StopCoroutine(_volumeChangeCoroutine);
        }
    }

    private IEnumerator VolumeChange()
    {
        float initialVolume = 0.0001f;

        _audioSource.volume += initialVolume;

        while (_audioSource.volume > 0)
        {
            switch (_volumeChangeDirection)
            {
                case VolumeChangeDirection.Up:
                    VolumeUpByOneFrame();
                    break;

                case VolumeChangeDirection.Down:
                    VolumeDownByOneFrame();
                    break;
            }

            yield return null;
        }
    }

    private void VolumeUpByOneFrame()
    {
        float maxVolume = 1;

        if (_audioSource.volume >= maxVolume)
        {
            return;
        }

        float changeStep = Time.deltaTime / _secondsToMaxVolume;
        _audioSource.volume += changeStep;
    }

    private void VolumeDownByOneFrame()
    {
        float minVolume = 0;

        if (_audioSource.volume <= minVolume)
        {
            return;
        }

        float changeStep = Time.deltaTime / _secondsToMaxVolume;
        _audioSource.volume -= changeStep;
    }

    private enum VolumeChangeDirection
    {
        Down,
        Up
    }
}
