using System;
using UnityEngine;

public class SignalingTrigger : MonoBehaviour
{
    public event Action SignalingZoneEnter;
    public event Action SignalingZoneExit;

    private void OnTriggerEnter(Collider other)
    {
        SignalingZoneEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        SignalingZoneExit?.Invoke();
    }
}
