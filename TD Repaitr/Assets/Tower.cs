using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private List<Unit> Units = new List<Unit>();
    private bool CanShoot = true;
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
        if (Units.Any() && CanShoot)
        {
            Shoot();
            CanShoot = false;
            Invoke("CoolDownShoot", AttackSpeed);
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
            unit.OnDie += RemoveTarget;
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
        Health--;
    }

    private void RemoveTarget(Unit unit)
    {
        if (Units.Contains(unit))
        {
            Units.Remove(unit);
        }
    }

    private void CoolDownShoot()
    {
        CanShoot = true;
    }
}
