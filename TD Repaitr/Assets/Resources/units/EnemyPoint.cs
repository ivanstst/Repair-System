using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoint : MonoBehaviour
{
    public bool IsLast;
    public float Health;

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(this);
            //Win condition
        }
    }
}
