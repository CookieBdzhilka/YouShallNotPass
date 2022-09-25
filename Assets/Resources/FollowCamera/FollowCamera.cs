using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject Target;
    public bool Follow;
    public Vector3 Offset;

    private Vector3 offset;
    private void Start()
    {
        offset = Target.transform.position + Offset;
    }
    private void Update()
    {
        if (!Follow)
            return;

        transform.position = Vector3.Lerp(Target.transform.position, Target.transform.position + offset, 0.125f);
        transform.LookAt(Target.transform.position);
    }
}
