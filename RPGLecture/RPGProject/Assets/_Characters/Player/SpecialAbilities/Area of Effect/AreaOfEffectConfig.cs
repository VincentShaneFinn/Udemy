using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters {

    [CreateAssetMenu(menuName = ("RPG/Special Abilities/AreaOfEffect"))]
    public class AreaOfEffectConfig : SpecialAbilityConfig {

        [Header("AOE Specific")]
        [SerializeField] float radius = 5f;
        [SerializeField] float damageToEachTarget = 15f;

        public override void AttachComponentTo(GameObject gameObjectToAttachTo)
        {
            var behaviorComponent = gameObjectToAttachTo.AddComponent<AreaOfEffectBehavior>();
            behaviorComponent.SetConfig(this);
            behavior = behaviorComponent;
        }

        public float GetRadius()
        {
            return radius;
        }

        public float GetDamageToEachTarget()
        {
            return damageToEachTarget;
        }

    }
}