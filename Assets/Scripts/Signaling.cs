using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField][Range(0,1)] private float _minVolume;
    [SerializeField][Range(0, 1)] private float _maxVolume;
    [SerializeField][Range(0,0.1f)] private float _volumeChangeStep;
    [SerializeField] private float _volumeChangeTime;

    private Coroutine _startSignaling;
    private Coroutine _stopSignaling;

    private void OnValidate()
    {
        if (_minVolume > _maxVolume)
        {
            _minVolume = _maxVolume;
        }
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
        _audioSource.mute = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_startSignaling == null)
        {
            _startSignaling = StartCoroutine(StartSignaling());
        }

        if(_stopSignaling != null)
        {
            StopCoroutine(_stopSignaling);
            _stopSignaling = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_stopSignaling == null)
        {
            _stopSignaling = StartCoroutine(StopSignaling());
        }

        if (_startSignaling != null)
        {
            StopCoroutine(_startSignaling);
            _startSignaling = null;
        }
    }

    private IEnumerator StartSignaling()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_volumeChangeTime);
        _audioSource.mute = false;

        while (_audioSource.volume < _maxVolume)
        {
            if(_audioSource.volume + _volumeChangeStep > _maxVolume)
            {
                _audioSource.volume = _maxVolume;
            }
            else
            {
                _audioSource.volume += _volumeChangeStep;
            }

            yield return waitForSeconds;
        }
    }
    private IEnumerator StopSignaling()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_volumeChangeTime);

        while (_audioSource.volume > _minVolume)
        {
            if (_audioSource.volume - _volumeChangeStep < _minVolume)
            {
                _audioSource.volume = _minVolume;
            }
            else
            {
                _audioSource.volume -= _volumeChangeStep;
            }

            yield return waitForSeconds;
        }

        _audioSource.mute = true;
    }
}
