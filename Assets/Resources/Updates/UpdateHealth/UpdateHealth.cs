using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHealth : UpdateFather
{
    protected override void AbstractEffect(CharacterPlayer characterPlayer)
    {
        characterPlayer.Health = characterPlayer.MaxHealth;
    }
}
