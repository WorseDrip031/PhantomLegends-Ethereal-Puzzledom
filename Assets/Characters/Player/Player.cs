using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float MaxHealth;
    [SerializeField] float Health;
    [SerializeField] float Attack;
    [SerializeField] float Defense;
    [SerializeField] float Speed;
    [SerializeField] float XP;

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] HealthBar healthBar;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getPlayerMaxHealth()
    {
        return MaxHealth;
    }

    public float getPlayerHealth()
    {
        return Health;
    }

    public float getPlayerAttack()
    {
        return Attack;
    }

    public float getPlayerSpeed()
    {
        return Speed;
    }

    public float getPlayerXP()
    {
        return XP;
    }

    public void increaseXP(float amount)
    {
        XP += amount;
    }

    public void InflictDamage(float damage, Vector2 knockback)
    {
        float damageDealt = damage - Defense;
        if (damageDealt > 0)
        {
            animator.SetTrigger("BeingHit");
            Health -= damageDealt;
            healthBar.SetHealth(Health);
            if ((Health <= 0) && (isAlive))
            {
                isAlive = false;
                Defeated();
            }
            rb.AddForce(knockback);
        }
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
        rb.simulated = false;
    }

    public void RemovePlayer()
    {
        Destroy(gameObject);
    }
}
