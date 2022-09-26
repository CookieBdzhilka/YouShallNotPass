using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text objText;
    public string CountText;
    public CharacterPlayer Target;
    private void Start()
    {
        SetText(Target.BonusCount);
    }
    private void OnEnable()
    {
        Target.OnBonusChanged += SetText;
    }
    private void OnDisable()
    {
        Target.OnBonusChanged -= SetText;
    }
    private void SetText(int Count)
    {
        objText.text = CountText + ": " + Count.ToString();
    }
}
