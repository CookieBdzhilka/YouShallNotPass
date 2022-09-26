using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour, ICharacterPlayerVisitor
{
    private int bonusValue;

    //=============================================================================================
    //Статичные методы
    public static Bonus CreateMe(Vector3 StartPos = new Vector3(), int BonusValue = 1)
    {
        Bonus NewObject = Instantiate(Resources.Load<Bonus>("Bonus/objBonus"));
        NewObject.transform.position = StartPos;
        NewObject.bonusValue = BonusValue;
        return NewObject;
    }
    //=============================================================================================

    public void CharacterPlaerEnter(CharacterPlayer characterPlayer)
    {
        characterPlayer.BonusCount = characterPlayer.BonusCount + bonusValue;
        Destroy(gameObject);
    }

    public void CharacterPlayerExit(CharacterPlayer characterPlayer)
    {
    }
}
