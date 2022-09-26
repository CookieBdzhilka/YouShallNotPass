using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateForce : UpdateFather
{
    public override void UpdateEffect(CharacterPlayer characterPlayer)
    {
        if (characterPlayer.BonusCount < UpdateCost)
            return;

        characterPlayer.BonusCount = characterPlayer.BonusCount - UpdateCost;
        characterPlayer.Force = characterPlayer.Force + 1;
    }
}
