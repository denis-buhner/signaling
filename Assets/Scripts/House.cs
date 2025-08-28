using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Signaling _signaling;
    [SerializeField] private SignalingTrigger _signalingTrigger;

    private void OnEnable()
    {
        _signalingTrigger.SignalingZoneEntered += StartAlarm;
        _signalingTrigger.SignalingZoneExited += StopAlarm;
    }

    private void OnDisable()
    {
        _signalingTrigger.SignalingZoneEntered -= StartAlarm;
        _signalingTrigger.SignalingZoneExited -= StopAlarm;
    }

    private void StartAlarm()
    {
        _signaling.StartSignaling();
    }

    private void StopAlarm()
    {
        _signaling.StopSignaling();
    }
}
