using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateButton : MonoBehaviour
{
    public CharacterPlayer Target;
    private Button ButtonUpdate;
    public void Awake()
    {
        ButtonUpdate = GetComponentInChildren<Button>();
        Target.OnUpdateAreaEnter += ShowButton;
        Target.OnUpdateAreaExit += HideButton;

        HideButton();
    }
    public void OnDestroy()
    {
        Target.OnUpdateAreaEnter -= ShowButton;
        Target.OnUpdateAreaExit -= HideButton;
    }
    void ShowButton(UpdateFather updateFather)
    {
        ButtonUpdate.onClick.AddListener(() => updateFather.UpdateEffect(Target));
        gameObject.SetActive(true);
    }
    void HideButton()
    {
        ButtonUpdate.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }
}
