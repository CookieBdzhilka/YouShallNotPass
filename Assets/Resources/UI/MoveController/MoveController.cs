using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveController : MonoBehaviour
{
    //=============================================================================================
    // Поля класса
    [SerializeField]
    private Vector2 joyValue;
    private bool OnControl;
    public Vector2 JoyValue { get { return joyValue; } private set { joyValue = value; } }
    //=============================================================================================

    //=============================================================================================
    //Внешние объекты
    public GameObject Joystick;
    public IControllObject controllObject;
    //=============================================================================================

    //=============================================================================================
    //Методы Unity
    public void Awake()
    {
        ResetJoyStick();
    }
    //=============================================================================================

    //=============================================================================================
    //Методы класса
    public void MoveAtPointer()
    {
        OnControl = true;
    }
    public void StopMoveAtPointer()
    {
        OnControl = false;
        ResetJoyStick();
        controllObject?.StopObject();
    }
    public void ResetJoyStick()
    {
        Joystick.transform.localPosition = new Vector3(0, 0, 0);
        joyValue = new Vector2(0, 0);
    }
    //=============================================================================================

    //=============================================================================================
    //Корутины
    private void FixedUpdate()
    {
        if (!OnControl)
            return;

        MoveAtPointerCorut();
    }
    public void MoveAtPointerCorut()
    {
        Vector3 MousePos;

        if (Input.touches.Length > 0)
            MousePos = Input.touches[0].position;
        else
            MousePos = Input.mousePosition;

        RectTransform ContainerRect = GetComponent<RectTransform>();
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), MousePos, Camera.main, out var localPosition))
        {

            localPosition.x = Mathf.Clamp(localPosition.x, -ContainerRect.sizeDelta.x / 2, ContainerRect.sizeDelta.x / 2);
            localPosition.y = Mathf.Clamp(localPosition.y, -ContainerRect.sizeDelta.y / 2, ContainerRect.sizeDelta.y / 2);

            Joystick.transform.localPosition = Vector3.MoveTowards(Joystick.transform.localPosition, localPosition, 1000 * Time.deltaTime);
        }

        joyValue = new Vector2((localPosition.x / ContainerRect.sizeDelta.x) * 2, (localPosition.y / ContainerRect.sizeDelta.y) * 2);

        controllObject.MoveObject(joyValue);
    }
    //=============================================================================================
}
