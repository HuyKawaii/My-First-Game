using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum EnemyState {Roaming, Following};

    ParticleSystem particleSystem;
    AudioManager audioManager;
    GameObject player;
    protected Rigidbody2D enemyRB;


    [SerializeField] protected float enemySpeed = 4.5f;
    [SerializeField] protected float health = 10.0f;
    [SerializeField] protected float direction = 1.0f;
    int damage = 1;
    float knockBackForce = 20.0f;

    float rayOffSet = 0.5f;
    float wallRay = 0.3f;
    float ledgeRay = 1f;
    [SerializeField] float detectDistance = 10.0f;
    RaycastHit2D wallAndLedge;
    EnemyState enemyState;
    float distanceFromPlayer;

    [SerializeField] Material flickerMatertial;
    Material originalMeterial;
    SpriteRenderer enemySpriteRenderer;
    [SerializeField] float stutterDuration = 0.6f;
    Coroutine flashCoroutine;
    Coroutine stutterCoroutine;
    protected bool isStun;
    int scoreWorth = 100;

    [SerializeField] LayerMask platform;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        particleSystem = transform.Find("EnemyDeathParticle").GetComponent<ParticleSystem>();
    }
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        originalMeterial = enemySpriteRenderer.material;
        flickerMatertial = new Material(flickerMatertial);
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Walk();
    }

    protected virtual void Walk()
    {
        if (!isStun)
        {
            StateOfEnemy();
            enemyRB.velocity = new Vector2(enemySpeed * direction, 0);
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        Stutter();
        audioManager.Hit();
        
        if (health <= 0)
        {
            Died();
        }
    }

    void Died()
    {
        particleSystem =  ObjectPooler.Instance.SpawnFromPool("EnemyDeathParticle", transform.position, transform.rotation).GetComponent<ParticleSystem>();
        particleSystem.Play();
        LevelManager.instance.GainScore(scoreWorth);
        Destroy(gameObject);
    }

    protected virtual void StateOfEnemy()
    {
        distanceFromPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceFromPlayer >= detectDistance || !PlayerController.isAlive)
        {
            enemyState = EnemyState.Roaming;
        }
        else
        {
            enemyState = EnemyState.Following;
        }

        if (enemyState == EnemyState.Roaming)
        {
            CheckWallAndLedge();
        }
        if (enemyState == EnemyState.Following)
        {
            FollowPlayer();
        }

    }

    void FollowPlayer()
    {
        if (player.transform.position.x - transform.position.x < 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }
    protected void CheckWallAndLedge()
    {
        wallAndLedge = Physics2D.Raycast(new Vector2(transform.position.x + rayOffSet,transform.position.y), Vector2.right, wallRay, platform);
        Debug.DrawRay(new Vector2(transform.position.x + rayOffSet, transform.position.y), Vector2.right, Color.yellow);
        if (wallAndLedge.collider != null)
        {
            direction = -1;
        }

        wallAndLedge = Physics2D.Raycast(new Vector2(transform.position.x - rayOffSet, transform.position.y), Vector2.left, wallRay, platform);
        Debug.DrawRay(new Vector2(transform.position.x - rayOffSet, transform.position.y), Vector2.left, Color.yellow);
        if (wallAndLedge.collider != null)
        {
            direction = 1;
        }

        wallAndLedge = Physics2D.Raycast(new Vector2(transform.position.x + rayOffSet, transform.position.y), Vector2.down, ledgeRay, platform);
        Debug.DrawRay(new Vector2(transform.position.x + rayOffSet, transform.position.y), Vector2.down, Color.yellow);
        if (wallAndLedge.collider == null)
        {
            direction = -1;
        }

        wallAndLedge = Physics2D.Raycast(new Vector2(transform.position.x - rayOffSet, transform.position.y), Vector2.down, ledgeRay, platform);
        Debug.DrawRay(new Vector2(transform.position.x - rayOffSet, transform.position.y), Vector2.down, Color.yellow);
        if (wallAndLedge.collider == null)
        {
            direction = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            DamagePlayer(collision.gameObject);
        }
    }

    void DamagePlayer(GameObject player)
    {
        player.GetComponent<PlayerCombat>().GotHit(damage);
        player.GetComponent<PlayerCombat>().KnockBack(knockBackForce);
    }

    void Stutter()
    {
        if (stutterCoroutine != null)
        {
            StopCoroutine(stutterCoroutine);
        }
        stutterCoroutine = StartCoroutine(StutterCoroutine());
    }
    IEnumerator StutterCoroutine()
    {
        isStun = true;
        enemySpriteRenderer.material = flickerMatertial;
        enemyRB.velocity = Vector2.zero;
        yield return new WaitForSeconds(stutterDuration);
        enemySpriteRenderer.material = originalMeterial;
        isStun = false;
    }
}
