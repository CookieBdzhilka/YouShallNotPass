using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : Character, IControllObject
{
    private void Awake()
    {
        WalkSpeed = 5;
    }
    private void Start()
    {
        FindObjectOfType<MoveController>().controllObject = this;
    }
    public void MoveObject(Vector2 MoveVector)
    {
        Vector3 NewPos;
        NewPos.x = transform.position.x - MoveVector.y;
        NewPos.z = transform.position.z + MoveVector.x;
        NewPos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, NewPos, WalkSpeed * MoveVector.magnitude * Time.deltaTime);
    }
}
