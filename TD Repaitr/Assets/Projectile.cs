using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Unit _unit;
    public float Speed;
    public Tower parent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_unit != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _unit.transform.position, Speed);
        }

    }

    public void Shoot(Unit unit)
    {
        _unit = unit;
    }

    private void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<Unit>();
        if (unit != null && unit == _unit)
        {
            unit.TakeDamage(parent.Damage);
            Destroy(gameObject);
        }
    }

    internal void SetParrent(Tower tower)
    {
        parent = tower;
    }
}
