using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float health;
    public float[] position;

    public PlayerManager(PlayerManager player)
    {
        health = player.health;

        Vector3 playerPos = player.transform.position;

        position = new float[] { playerPos.x, playerPos.y, playerPos.z };
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        print("died");
    }
}
