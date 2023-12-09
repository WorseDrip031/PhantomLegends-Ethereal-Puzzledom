using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseDistanceThreshold;
    [SerializeField] float attackDistanceThreshold;
    [SerializeField] float attackDelay;
    [SerializeField] float passedTime;
    [SerializeField] float KonckbackForce;

    [SerializeField] Enemy enemy;
    [SerializeField] Player player;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] Collider2D attackColliderLeft;
    [SerializeField] Collider2D attackColliderRight;

    private Vector2 movement;
    private bool canMove = true;
    private bool isAttacking = false;
    private bool hasAttackedAlready = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector2.Distance(player.transform.localPosition, transform.position);
        if (distance < chaseDistanceThreshold)
        {
            animator.SetBool("IsIdle", false);
            if (distance <= attackDistanceThreshold)
            {

                if (passedTime >= attackDelay)
                {
                    passedTime = 0;
                    animator.SetTrigger("Attacking");
                }
            }
            else
            {
                movement = (player.transform.localPosition - transform.position).normalized;
                animator.SetFloat("Horizontal", movement.x);
                if (canMove)
                {
                    rb.MovePosition(rb.position + movement * enemy.getEnemySpeed() * Time.fixedDeltaTime);
                }
            }
        }
        else
        {
            // Idle
            animator.SetBool("IsIdle", true);
        }

        if (passedTime < attackDelay)
        {
            passedTime += Time.deltaTime;
        }
    }

    public void EnemyAttackLeft()
    {
        canMove = false;
        isAttacking = true;
        attackColliderLeft.enabled = true;
    }

    public void EnemyAttackRight()
    {
        canMove = false;
        isAttacking = true;
        attackColliderRight.enabled = true;
    }

    public void EnemyEndAttack()
    {
        canMove = true;
        isAttacking = false;
        attackColliderLeft.enabled = false;
        attackColliderRight.enabled = false;
        hasAttackedAlready = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Player") && (isAttacking))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (!hasAttackedAlready)
                {
                    hasAttackedAlready = true;

                    Vector2 enemyPosition = gameObject.GetComponentInParent<Transform>().position;
                    Vector2 hitDirection = ((Vector2)other.gameObject.transform.position - enemyPosition).normalized;
                    Vector2 knockback = hitDirection * KonckbackForce;

                    player.InflictDamage(enemy.getEnemyAttack(), knockback);
                }
            }
        }
    }
}
