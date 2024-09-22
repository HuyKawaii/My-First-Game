using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public AudioManager audioManager;
    [SerializeField] GameObject attackPos;
    [SerializeField] LifeCounter lifeCounter;
    [SerializeField] DeathMenuManager deathMenu;
    PlayerController playerController;
    public float cooldown;
    float attackCooldown = 0.3f;
    [SerializeField] LayerMask enemyLayer;
    public float attackStun;
    public bool isAttacking;

    public float playerDamage = 2.0f;
    public int playerHealth = 3;
    [SerializeField] float flickerDuration = 0.1f;
    [SerializeField] float immuneDuration = 1f;
    bool isImmune;
    SpriteRenderer playerRenderer;
    [SerializeField] Rigidbody2D playerRB;
    public Animator animator;
    void Start()
    {
        Debug.Log(playerHealth);
        cooldown = attackCooldown;
        isImmune = false;
        playerRenderer = GetComponent<SpriteRenderer>();
        playerRB = GetComponent<Rigidbody2D>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (PlayerController.isAlive)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (cooldown <= 0)
        {
            isAttacking = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isAttacking = true;
                cooldown = attackCooldown;
                animator.SetTrigger("Attack");
                audioManager.Whoosh();
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
        attackPos.SetActive(isAttacking);
    }

    public void GotHit(int damageTaken)
    {
        if (isImmune)
        {
            return;
        }
        else
        {
            lifeCounter.ReduceHealth(damageTaken);
            playerHealth -= damageTaken;

            if (playerHealth > 0)
            {
                audioManager.GotHit();
                OnGotHit();
            }
            else if (playerHealth <= 0)
            {
                playerController.Die();
                deathMenu.OnPlayerDie();
                audioManager.Death();
            }
        }
        
    }

    public void OnGotHit()
    {
        isImmune = true;
        gameObject.layer = 8;
        StartCoroutine(ImmuneCoutdown());
        StartCoroutine(Flicker());
    }

    IEnumerator ImmuneCoutdown()
    {
        yield return new WaitForSeconds(immuneDuration);
        isImmune = false;
        gameObject.layer = 3;
    }

    IEnumerator Flicker()
    {
        while (isImmune)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(flickerDuration);
        }
        playerRenderer.enabled = true;
    }

    public void KnockBack(float force)
    {
        playerRB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        Debug.Log("Knocked Player");
    }
}
