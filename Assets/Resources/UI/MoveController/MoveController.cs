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
        StartCoroutine(nameof(MoveAtPointerCorut));
    }
    public void StopMoveAtPointer()
    {
        StopCoroutine(nameof(MoveAtPointerCorut));
        ResetJoyStick();
    }
    public void ResetJoyStick()
    {
        Joystick.transform.localPosition = new Vector3(0, 0, 0);
        joyValue = new Vector2(0, 0);
    }
    //=============================================================================================

    //=============================================================================================
    //Корутины
    public IEnumerator MoveAtPointerCorut()
    {
        while (true)
        {
            Vector3 MousePos = Input.mousePosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), MousePos, Camera.main, out var localPosition))
            {
                
                Vector2 JoystickPositionX = new Vector2(Joystick.transform.localPosition.x, 0);
                Vector2 JoystickPositionY = new Vector2(0, Joystick.transform.localPosition.y);
                Vector2 localPositionX = new Vector2(localPosition.x, 0);
                Vector2 localPositionY = new Vector2(0, localPosition.y);

                localPosition.x = GetComponent<RectTransform>().rect.Contains(JoystickPositionX) ||
                                    GetComponent<RectTransform>().rect.Contains(localPositionX) ? localPosition.x : JoystickPositionX.x;
                localPosition.y = GetComponent<RectTransform>().rect.Contains(JoystickPositionY) ||
                                    GetComponent<RectTransform>().rect.Contains(localPositionY) ? localPosition.y : JoystickPositionY.y;
                
                //if(GetComponent<RectTransform>().rect.Contains(Joystick.transform.localPosition))
                Joystick.transform.localPosition = Vector3.MoveTowards(Joystick.transform.localPosition, localPosition, 1000 * Time.deltaTime);
            }

            joyValue = new Vector2(Joystick.transform.localPosition.x/Joystick.GetComponent<RectTransform>().sizeDelta.x
                                        , Joystick.transform.localPosition.y / Joystick.GetComponent<RectTransform>().sizeDelta.y);

            controllObject.MoveObject(joyValue);
            yield return null;
        }
    }
    //=============================================================================================
}
