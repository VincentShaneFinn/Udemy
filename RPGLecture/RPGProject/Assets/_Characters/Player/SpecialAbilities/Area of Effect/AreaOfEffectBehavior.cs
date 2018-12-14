using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    public class AreaOfEffectBehavior : MonoBehaviour, ISpecialAbility
    {
        AreaOfEffectConfig config;
        public void SetConfig(AreaOfEffectConfig configToSet) { this.config = configToSet; }


        // Use this for initialization
        void Start()
        {
            print("AreaOfEffect behavior attached to " + gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Use(AbilityUseParams useParams)
        {
            print("Area effect used by: " + gameObject);
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, config.GetRadius(), Vector3.up, config.GetRadius());

            foreach (RaycastHit hit in hits)
            {
                var damageable = hit.collider.gameObject.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    float damageToDeal = useParams.baseDamage + config.GetDamageToEachTarget();
                    damageable.TakeDamage(damageToDeal);
                }
            }
        }
    }
}