using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float chaseRadius = 8f;

    [SerializeField] float attackRadius = 4f;
    [SerializeField] float damagePerShot = 9f;
    [SerializeField] float timeBetweenShots = .2f;
    [SerializeField] Vector3 aimOffset = new Vector3(0,1f,0);


    [SerializeField] Projectile projectileToUse = null;
    [SerializeField] Transform projectileSocket = null;

    bool isAttacking = false;
    float currentHealthPoints = 100f;
    AICharacterControl aiCharacterControl = null;
    GameObject player = null;

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRadius && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(FireProjectile());
        }

        if(distanceToPlayer > attackRadius)
        {
            isAttacking = false;
            //stopattacking?
        }

        if (distanceToPlayer <= chaseRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(transform);
        }
    }

    private IEnumerator FireProjectile()
    {
        Projectile newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        newProjectile.SetDamage(damagePerShot);
        Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
        float pSpeed = newProjectile.projectileSpeed;
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * pSpeed;

        yield return new WaitForSeconds(timeBetweenShots);
        isAttacking = false;
    }

    public void TakeDamage(float Damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - Damage, 0, maxHealthPoints);
        if (currentHealthPoints <= 0) { Destroy(gameObject); }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(255f, 0, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = new Color(0, 0, 255f, .5f);
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
