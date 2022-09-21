using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected float WalkSpeed = 5;
    protected int health = 10;

    public void SetHealth(int NewHealth)
    {
        health = NewHealth;
        if (health < 0)
            Destroy(gameObject);
    }
}
