using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 5f;
    float damageCaused = 10f;

    public void SetDamage(float newValue)
    {
        damageCaused = newValue;
    }

    void OnCollisionEnter(Collision collider)
    {
        var damageable = collider.gameObject.GetComponent(typeof(IDamageable));
        if (damageable)
        {
            (damageable as IDamageable).TakeDamage(damageCaused);
            Destroy(gameObject);
        }
    }
}
