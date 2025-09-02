using UnityEngine;

public class Zombie : MonoBehaviour, IDamageable
{
    public int health = 5;

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        Debug.Log("Zombie has been damaged: " + health + " health remaining");
    }
}
