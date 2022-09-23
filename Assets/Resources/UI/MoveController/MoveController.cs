using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveController : MonoBehaviour
{
    //=============================================================================================
    // ���� ������
    [SerializeField]
    private Vector2 joyValue;
    public Vector2 JoyValue { get { return joyValue; } private set { joyValue = value; } }
    //=============================================================================================

    //=============================================================================================
    //������� �������
    public GameObject Joystick;
    public IControllObject controllObject;
    //=============================================================================================

    //=============================================================================================
    //������ Unity
    public void Awake()
    {
        ResetJoyStick();
    }
    //=============================================================================================

    //=============================================================================================
    //������ ������
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
    //��������
    public IEnumerator MoveAtPointerCorut()
    {
        while (true)
        {
            Vector3 MousePos = Input.mousePosition;
            RectTransform ContainerRect = GetComponent<RectTransform>();
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), MousePos, Camera.main, out var localPosition))
            {

                localPosition.x = Mathf.Clamp(localPosition.x, -ContainerRect.sizeDelta.x / 2, ContainerRect.sizeDelta.x / 2);
                localPosition.y = Mathf.Clamp(localPosition.y, -ContainerRect.sizeDelta.y / 2, ContainerRect.sizeDelta.y / 2);

                Joystick.transform.localPosition = Vector3.MoveTowards(Joystick.transform.localPosition, localPosition, 1000 * Time.deltaTime);
            }

            joyValue = new Vector2((localPosition.x / ContainerRect.sizeDelta.x) * 2, (localPosition.y / ContainerRect.sizeDelta.y) * 2);

            controllObject.MoveObject(joyValue);
            yield return null;
        }
    }
    //=============================================================================================
}
