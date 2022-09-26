using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class UpdateFather : MonoBehaviour, ICharacterPlayerVisitor
{
    public int UpdateCost = 0;
    public string TXT;
    private TextMeshPro UpdateText;
    private void Awake()
    {
        UpdateText = GetComponentInChildren<TextMeshPro>();
        UpdateText.text = $"{TXT}\nPrice: {UpdateCost}";
    }
    public void CharacterPlaerEnter(CharacterPlayer characterPlayer)
    {
        characterPlayer.OnUpdateAreaEnter?.Invoke(this);
    }

    public void CharacterPlayerExit(CharacterPlayer characterPlayer)
    {
        characterPlayer.OnUpdateAreaExit?.Invoke();
    }
    public virtual void UpdateEffect(CharacterPlayer characterPlayer)
    {
        if (characterPlayer.BonusCount < UpdateCost)
            return;

        characterPlayer.BonusCount = characterPlayer.BonusCount - UpdateCost;
        AbstractEffect(characterPlayer);
    }
    protected abstract void AbstractEffect(CharacterPlayer characterPlayer);
}
