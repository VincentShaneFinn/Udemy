using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

    [SerializeField] int enemyLayer = 9;
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float damagePerHit = 10f;
    [SerializeField] float minTimeBetweenHits = .5f;
    [SerializeField] float maxAttackRange = 2f;
    float lastHitTime = 0f;

    GameObject currentTarget;
    float currentHealthPoints;
    CameraRaycaster cameraRaycaster;

    public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }

    void Start()
    {
        currentHealthPoints = maxHealthPoints;
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClicked;
    }

    void OnMouseClicked(RaycastHit hit, int layerHit)
    {
        if (layerHit == enemyLayer)
        {
            var enemy = hit.collider.gameObject;


            //check enemy in range
            if(Vector3.Distance(enemy.transform.position,transform.position) > maxAttackRange)
            {
                return;
            }



            if(Time.time - lastHitTime > minTimeBetweenHits)
            {
                currentTarget = enemy;
                var enemyComponent = currentTarget.GetComponent<Enemy>();
                enemyComponent.TakeDamage(damagePerHit);
                lastHitTime = Time.time;
            }

        }
    }

    public void TakeDamage(float Damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - Damage, 0, maxHealthPoints);
        if(currentHealthPoints <= 0) { Destroy(gameObject); }
    }
}
