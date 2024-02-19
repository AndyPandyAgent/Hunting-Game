using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    public float health;
    public float[] position;

    public EnemyStats(S_AnimalBehaviour enemy)
    {
        health = enemy.health;

        Vector3 enemyPos = enemy.transform.position;

        position = new float[]{enemyPos.x, enemyPos.y, enemyPos.z};
    }
}
