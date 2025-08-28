using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField][Range(0,1f)] private float _volumeChangeStep;

    private Coroutine _startSignal;
    private float _minVolume = 0;
    private float _maxVolume = 1;

    public float MinVolume => _minVolume;
    public float MaxVolume => _maxVolume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
    }

    public void ChangeVolume(float startVolume, float finishVolume)
    {
        if (_startSignal != null)
        {
            StopCoroutine(_startSignal);
            _startSignal = StartCoroutine(StartSignal(startVolume, finishVolume)); ;
        }
        else
        {
            _startSignal = StartCoroutine(StartSignal(startVolume, finishVolume));
        } 
    }

    private IEnumerator StartSignal(float startVolume, float finishVolume)
    {
        if (finishVolume > startVolume)
        {
            _audioSource.Play();
        }

        while (_audioSource.volume != finishVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, finishVolume, _volumeChangeStep*Time.deltaTime);

            yield return null;
        }

        if (finishVolume < startVolume)
        {
            _audioSource.Stop();
        }

        _startSignal = null;
    }
}
