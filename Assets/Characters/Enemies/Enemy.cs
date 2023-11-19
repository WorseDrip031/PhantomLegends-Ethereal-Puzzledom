using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] float Attack;
    [SerializeField] float Defense;
    [SerializeField] float Speed;
    [SerializeField] float XPAward;

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float InflictDamage(float damage, Vector2 knockback)
    {
        float damageDealt = damage - Defense;
        if (damageDealt > 0)
        {
            animator.SetTrigger("BeingHit");
            Health -= damageDealt;
            if ((Health <= 0) && (isAlive))
            {
                isAlive = false;
                Defeated();
                return XPAward;
            }
            rb.AddForce(knockback);
        }
        return 0;
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
