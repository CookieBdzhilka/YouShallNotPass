using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //=============================================================================================
    //Методы объекта
    public GameObject Target;
    /// <summary>
    /// Следовать ли за объектом
    /// </summary>
    public bool Follow;
    /// <summary>
    /// Смещение камеры относительно игрока
    /// </summary>
    public Vector3 Offset;
    //рассчитываемое смещение
    private Vector3 offset;
    //=============================================================================================

    //=============================================================================================
    //Методы unity
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
    //=============================================================================================
}
