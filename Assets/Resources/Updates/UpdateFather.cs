using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpdateFather : MonoBehaviour, ICharacterPlayerVisitor
{
    public int UpdateCost = 0;
    public void CharacterPlaerEnter(CharacterPlayer characterPlayer)
    {
        characterPlayer.OnUpdateAreaEnter?.Invoke(this);
    }

    public void CharacterPlayerExit(CharacterPlayer characterPlayer)
    {
        characterPlayer.OnUpdateAreaExit?.Invoke();
    }
    public abstract void UpdateEffect(CharacterPlayer characterPlayer);
}
