using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Collider2D swordColliderUp;
    [SerializeField] Collider2D swordColliderDown;
    [SerializeField] Collider2D swordColliderLeft;
    [SerializeField] Collider2D swordColliderRight;

    [SerializeField] Player player;
    [SerializeField] Animator animator;

    [SerializeField] float KonckbackForce;

    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayerAttackUp()
    {
        swordColliderUp.enabled = true;
        isAttacking = true;
    }

    public void PlayerAttackDown()
    {
        swordColliderDown.enabled = true;
        isAttacking = true;
    }

    public void PlayerAttackLeft()
    {
        swordColliderLeft.enabled = true;
        isAttacking = true;
    }

    public void PlayerAttackRight()
    {
        swordColliderRight.enabled = true;
        isAttacking = true;
    }

    public void PlayerEndAttack()
    {
        swordColliderUp.enabled = false;
        swordColliderDown.enabled = false;
        swordColliderLeft.enabled = false;
        swordColliderRight.enabled = false;
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Enemy") && (isAttacking))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Vector2 playerPosition = gameObject.GetComponentInParent<Transform>().position;
                Vector2 hitDirection = ((Vector2)other.gameObject.transform.position - playerPosition).normalized;
                Vector2 knockback = hitDirection * KonckbackForce;


                float xp_gained = enemy.InflictDamage(player.getPlayerAttack(), knockback);
                if (xp_gained > 0)
                {
                    player.increaseXP(xp_gained);
                }
            }
        }
    }
}
