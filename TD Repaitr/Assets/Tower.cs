using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private List<Unit> Units = new List<Unit>();
    private bool IsShooting;
    public float Damage;
    public float Health;
    public float AttackSpeed;
    public Projectile Attack;
    private Unit _Target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Units.Any() && !IsShooting)
        {
            Shoot();
            IsShooting = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("unit");
        var unit = other.gameObject.GetComponent<Unit>();
        if (unit != null)
        {
            Debug.Log("Added unit");
            Units.Add(unit);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var unit = other.gameObject.GetComponent<Unit>();
        if (unit != null && Units.Contains(unit))
        {
            Units.Remove(unit);
        }
    }

    public void Shoot()
    {
        Debug.Log("Shooted");
        var projectile = Instantiate<Projectile>(Attack, transform.position, Attack.transform.rotation);
        projectile.SetParrent(this);
        projectile.Shoot(Units.FirstOrDefault());
        Units[0].OnDie += RemoveTarget;
        Health--;

        if (Units.Any())
        {
            Invoke("Shoot", AttackSpeed);
        }
        else
        {
            IsShooting = false;
        }
    }

    private void RemoveTarget(Unit unit)
    {
        if (Units.Contains(unit))
        {
            Units.Remove(unit);
        }
    }
}
