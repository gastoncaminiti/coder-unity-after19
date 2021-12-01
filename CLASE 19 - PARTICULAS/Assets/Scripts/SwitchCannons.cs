using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchCannons : MonoBehaviour
{
    [SerializeField] private UnityEvent onSwitchCannons;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.CompareTag("Player"));
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("DESACTIVAR CAÑONES");
            onSwitchCannons?.Invoke();
        }
    }
}
