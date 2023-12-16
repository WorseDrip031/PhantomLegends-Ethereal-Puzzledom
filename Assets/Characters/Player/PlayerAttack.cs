using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Collider2D swordColliderUp;
    [SerializeField] Collider2D swordColliderDown;
    [SerializeField] Collider2D swordColliderLeft;
    [SerializeField] Collider2D swordColliderRight;

    [SerializeField] Player player;
    [SerializeField] Animator animator;
    [SerializeField] AudioManager audioManager;

    [SerializeField] float KonckbackForce;

    private bool isAttacking = false;
    private List<int> enemiesAttacked = new List<int>();

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
        audioManager.PlaySFX(audioManager.swordAttack);
        swordColliderUp.enabled = true;
        isAttacking = true;
    }

    public void PlayerAttackDown()
    {
        audioManager.PlaySFX(audioManager.swordAttack);
        swordColliderDown.enabled = true;
        isAttacking = true;
    }

    public void PlayerAttackLeft()
    {
        audioManager.PlaySFX(audioManager.swordAttack);
        swordColliderLeft.enabled = true;
        isAttacking = true;
    }

    public void PlayerAttackRight()
    {
        audioManager.PlaySFX(audioManager.swordAttack);
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
        enemiesAttacked.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Enemy") && (isAttacking))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                foreach(int enemyID in enemiesAttacked)
                {
                    if (enemyID == enemy.enemyID)
                    {
                        return;
                    }
                }
                enemiesAttacked.Add(enemy.enemyID);

                Vector2 playerPosition = gameObject.GetComponentInParent<Transform>().position;
                Vector2 hitDirection = ((Vector2)other.gameObject.transform.position - playerPosition).normalized;
                Vector2 knockback = hitDirection * KonckbackForce;

                int xp_gained = enemy.InflictDamage(player.getPlayerAttack(), knockback);
                if (xp_gained > 0)
                {
                    player.increaseXP(xp_gained);
                }
            }
        }
    }
}
