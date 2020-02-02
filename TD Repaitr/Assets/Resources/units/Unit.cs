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
    public float Damage;
    public float AttackSpeed;
    private int Current;
    private EnemyPoint Target;
    private bool CanAttack = true;
    private Animator AnimatorController;
    private bool _dead;
    public static bool Hitted;

    // Start is called before the first frame update
    void Start()
    {
        AnimatorController = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_dead)
        {


            if (EnemyPoints[Current] != null)
            {
                // Determine which direction to rotate towards
                Vector3 targetDirection = EnemyPoints[Current].transform.position - transform.position;

                // The step size is equal to speed times frame time.
                float singleStep = 5 * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection);
            }


            if (Target == null)
            {
                RunAnim();
                transform.position = Vector3.MoveTowards(transform.position, EnemyPoints[Current].transform.position, MoveSpeed);
            }

            if (Target != null && CanAttack)
            {
                AttackAnim();
                Attack();
            }
        }
    }

    private void RunAnim()
    {
        AnimatorController.SetBool("Running", true);
        AnimatorController.SetBool("Attack", false);
        AnimatorController.SetBool("Die", false);
    }
    private void DieAnim()
    {
        AnimatorController.SetBool("Running", false);
        AnimatorController.SetBool("Attack", false);
        AnimatorController.SetBool("Die", true);
    }
    private void AttackAnim()
    {
        AnimatorController.SetBool("Running", false);
        AnimatorController.SetBool("Attack", true);
        AnimatorController.SetBool("Die", false);
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
                Target = enemyPoint;
            }
            else
            {
                Current++;
            }
        }
    }

    private void Attack()
    {
        if (!Hitted)
        {
            Hitted = true;
        }
        Debug.Log("Attack");
        Target.TakeDamage(Damage);
        Invoke("CoolDownAttack", AttackSpeed);
        CanAttack = false;
    }
    private void CoolDownAttack()
    {
        CanAttack = true;
    }
    private void Die()
    {
        DieAnim();
        _dead = true;
        OnDie.Invoke(this);
        Invoke("DestroyMe", 5);
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void AssignPath(EnemyPoint[] points)
    {
        EnemyPoints = points;
        Current = 0;
    }
}
