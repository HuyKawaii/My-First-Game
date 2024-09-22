using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(transform.parent.GetComponent<PlayerCombat>().playerDamage);

        }
    }
}
