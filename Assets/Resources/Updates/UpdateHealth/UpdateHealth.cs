using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHealth : UpdateFather
{
    public override void UpdateEffect(CharacterPlayer characterPlayer)
    {
        if (characterPlayer.BonusCount < UpdateCost)
            return;

        characterPlayer.BonusCount = characterPlayer.BonusCount - UpdateCost;
        characterPlayer.Health = characterPlayer.MaxHealth;
    }
}
