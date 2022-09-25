using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject Target;
    public int Damage { get; private set; } = 1;

    //=============================================================================================
    //Статичные методы
    public static Missile CreateMe(GameObject target, Vector3 StartPos = new Vector3(), int damage = 1)
    {
        Missile NewObject = Instantiate(Resources.Load<Missile>("Missile/objMissile"));
        NewObject.transform.position = StartPos;
        NewObject.Target = target;
        NewObject.Damage = damage;
        return NewObject;
    }
    //=============================================================================================

    //=============================================================================================
    //Методы Unity
    private void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 10 * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IMissileVisitor>() != null)
        {
            collision.gameObject.GetComponent<IMissileVisitor>().Shooted(this);
            Destroy(gameObject);
        }
    }
    //=============================================================================================
}
