using System;
using UnityEngine;

public class SignalingTrigger : MonoBehaviour
{
    public event Action SignalingZoneEntered;
    public event Action SignalingZoneExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BanditMovement>() != null)
        {
            SignalingZoneEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BanditMovement>() != null)
        {
            SignalingZoneExited?.Invoke();
        }
    }
}
