using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class THealthBar : MonoBehaviour
{
    //����������� ������
    [SerializeField] public Character HealthRef;
    //������� ��������
    [SerializeField] public GameObject objHealth;
    //�����
    [SerializeField] public Text HealthText;

    [SerializeField]
    private float MaxWidth;
    [SerializeField]
    private float RightOffset;

    //=============================================================================================
    //������ Unity
    public void Start()
    {
        MaxWidth = GetComponent<RectTransform>().sizeDelta.x;
        RightOffset = objHealth.GetComponent<RectTransform>().offsetMax.x;

        HealthRef.OnHealthChanged += OnHealthChanged;
        UpdateText();
    }
    private void OnDisable()
    {
        HealthRef.OnHealthChanged -= OnHealthChanged;
    }
    //=============================================================================================

    //=============================================================================================
    //������ �������
    public void OnHealthChanged(int NewHealth)
    {
        int MaxHealth = HealthRef.MaxHealth;
        Vector2 NewOffset;
        NewOffset.x = -((MaxWidth / MaxHealth) * (MaxHealth - NewHealth));
        NewOffset.y = objHealth.GetComponent<RectTransform>().offsetMax.y;
        objHealth.GetComponent<RectTransform>().offsetMax = NewOffset;

        UpdateText();
    }
    public void UpdateText()
    {
        HealthText.text = $"Health: {HealthRef.Health}/ {HealthRef.MaxHealth}";
    }
    //=============================================================================================
}
