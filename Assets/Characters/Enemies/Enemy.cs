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
    [SerializeField] ExpText expText;
    [SerializeField] Canvas canvas;

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float getEnemyAttack()
    {
        return Attack;
    }

    public float getEnemySpeed()
    {
        return Speed;
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
        ExpText spawnedExpText = Instantiate(expText);
        spawnedExpText.setText("+" + XPAward + " EXP");
        RectTransform textTransform = spawnedExpText.GetComponent<RectTransform>();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        textTransform.SetParent(canvas.transform);
        animator.SetTrigger("Defeated");
        rb.simulated = false;
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
