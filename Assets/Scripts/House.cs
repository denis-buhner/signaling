using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Signaling _signaling;
    [SerializeField] private SignalingTrigger _signalingTrigger;

    private float _minSignalingVolume;
    private float _maxSignalingVolume;

    private void OnValidate()
    {
        if (_minSignalingVolume > _maxSignalingVolume)
        {
            _minSignalingVolume = _maxSignalingVolume;
        }
    }

    private void OnEnable()
    {
        _signalingTrigger.SignalingZoneEnter += StartAlarm;
        _signalingTrigger.SignalingZoneExit += StopAlarm;

        _minSignalingVolume = _signaling.MinVolume;
        _maxSignalingVolume = _signaling.MaxVolume;
    }

    private void OnDisable()
    {
        _signalingTrigger.SignalingZoneEnter -= StartAlarm;
        _signalingTrigger.SignalingZoneExit -= StopAlarm;
    }

    private void StartAlarm()
    {
        _signaling.ChangeVolume(_minSignalingVolume, _maxSignalingVolume);
    }

    private void StopAlarm()
    {
        _signaling.ChangeVolume(_maxSignalingVolume, _minSignalingVolume);
    }
}
