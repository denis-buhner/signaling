using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField][Range(0,1f)] private float _volumeChangeStep;
    [SerializeField][Range(0, 1f)] private float _minVolume;
    [SerializeField][Range(0, 1f)] private float _maxVolume;

    private Coroutine _changeVolume;

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
    }

    public void StartSignaling()
    {
        TryStopChangeVolume();

        _audioSource.Play();
        _changeVolume = StartCoroutine(ChangeVolume(_maxVolume));
    }

    public void StopSignaling()
    {
        TryStopChangeVolume();

        _changeVolume = StartCoroutine(ChangeVolume(_minVolume));
    }

    private void TryStopChangeVolume()
    {
        if (_changeVolume != null)
        {
            StopCoroutine(_changeVolume);
            _changeVolume = null;
        }
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeChangeStep*Time.deltaTime);

            yield return null;
        }

        if(_audioSource.volume == _minVolume)
        {
            _audioSource.Stop();
        }

        _changeVolume = null;
    }
}
