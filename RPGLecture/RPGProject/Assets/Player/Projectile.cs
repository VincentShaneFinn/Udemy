﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        var damageable = collider.gameObject.GetComponent(typeof(IDamageable));
        if (damageable)
        {
            (damageable as IDamageable).TakeDamage(10);
        }
    }
}
