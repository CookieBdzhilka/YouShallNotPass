using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    protected float WalkSpeed = 5;
    protected int health = 10;

    public UnityAction<Character> OnDestroyEvent;

    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke(this);
    }
    public void SetHealth(int NewHealth)
    {
        health = NewHealth;
        if (health < 0)
            Destroy(gameObject);
    }
}
