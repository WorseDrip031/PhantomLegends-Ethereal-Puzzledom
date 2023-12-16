using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] float Attack;
    [SerializeField] float Defense;
    [SerializeField] float Speed;
    [SerializeField] int XPAward;
    [SerializeField] WorldItem WorldItemToDrop;

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ExpText expText;
    private Canvas canvas;

    private bool isAlive = true;
    private bool isItemDropped = false;

    public int enemyID;

    // Start is called before the first frame update
    void Start()
    {
        enemyID = EnemyID.GetEnemyID();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
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

    public int InflictDamage(float damage, Vector2 knockback)
    {
        float damageDealt = damage - Defense;
        if (damageDealt > 0)
        {
            animator.SetTrigger("BeingHit");
            Health -= damageDealt;
            Debug.Log(Health);
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
        spawnedExpText.setText("+" + XPAward + " XP");
        RectTransform textTransform = spawnedExpText.GetComponent<RectTransform>();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        textTransform.SetParent(canvas.transform);
        animator.SetTrigger("Defeated");
        rb.simulated = false;
    }

    public void RemoveEnemy()
    {
        if (WorldItemToDrop != null)
        {
            if (!isItemDropped)
            {
                Instantiate(WorldItemToDrop, transform.position, Quaternion.identity);
                isItemDropped = true;
            }
        }
        Destroy(gameObject);
    }
}
