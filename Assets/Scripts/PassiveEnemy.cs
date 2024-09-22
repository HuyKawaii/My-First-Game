using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEnemy : Enemy
{
    protected override void Walk()
    {
        if (!isStun)
        {
            CheckWallAndLedge();
            enemyRB.velocity = new Vector2(enemySpeed * direction, 0);
        }
    }
}
