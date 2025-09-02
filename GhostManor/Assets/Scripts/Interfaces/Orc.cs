using UnityEngine;

public class Orc : MonoBehaviour, IDamageable
{
    public int health = 10;

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

        Debug.Log("Orc has been damaged: " + health + " health remaining");
    }
}
