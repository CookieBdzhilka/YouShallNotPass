using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateForce : UpdateFather
{
    protected override void AbstractEffect(CharacterPlayer characterPlayer)
    {
        characterPlayer.Force = characterPlayer.Force + 1;
    }
}
