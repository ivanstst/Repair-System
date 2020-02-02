using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public delegate void OnDeath(Unit unit);
    public OnDeath OnDie;
    public float Health;
    public float MoveSpeed;
    public EnemyPoint[] EnemyPoints;
    private int Current;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, EnemyPoints[Current].transform.position, MoveSpeed);
    }

    internal void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var enemyPoint = other.GetComponent<EnemyPoint>();
        if (enemyPoint != null)
        {
            if (enemyPoint.IsLast)
            {
                StartAttack();
            }
            else
            {
                Current ++;
            }
        }
    }

    private void StartAttack()
    {
        
    }

    private void Die()
    {
        OnDie.Invoke(this);
    }

    public void AssignPath(EnemyPoint[] points)
    {
        EnemyPoints = points;
        Current = 0;
    }
}
