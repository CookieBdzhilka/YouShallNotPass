using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateShootSpeed : UpdateFather
{
    protected override void AbstractEffect(CharacterPlayer characterPlayer)
    {
        characterPlayer.ShootSpeed = characterPlayer.ShootSpeed - 0.1f;
    }
}
