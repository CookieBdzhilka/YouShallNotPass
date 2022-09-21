using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPrefab : MonoBehaviour
{
    public UnityAction<ICharacterPlayerVisitor> OnTriggerEnterEvent;
    public UnityAction<ICharacterPlayerVisitor> OnTriggerExitEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICharacterPlayerVisitor>() != null)
        {
            OnTriggerEnterEvent?.Invoke(other.GetComponent<ICharacterPlayerVisitor>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ICharacterPlayerVisitor>() != null)
        {
            OnTriggerExitEvent?.Invoke(other.GetComponent<ICharacterPlayerVisitor>());
        }
    }
}
