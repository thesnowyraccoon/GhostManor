using UnityEngine;
using UnityEngine.InputSystem;

public class DamageTester : MonoBehaviour
{
    public GameObject[] damageables;

    public void OnDamage(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        foreach (GameObject gameObject in damageables)
        {
            IDamageable damageable = gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }
        }
    }
}
