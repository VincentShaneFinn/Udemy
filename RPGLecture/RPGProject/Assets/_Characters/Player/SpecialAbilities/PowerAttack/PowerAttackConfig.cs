using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters {

    [CreateAssetMenu(menuName = ("RPG/Special Abilities/PowerAttack"))]
    public class PowerAttackConfig : SpecialAbilityConfig {

        [Header("Power Attack Specific")]
        [SerializeField] float extraDamage = 10f;

        override public ISpecialAbility AddComponent(GameObject gameObjectToAttachTo)
        {
            var behaviorComponent = gameObjectToAttachTo.AddComponent<PowerAttackBehavior>();
            behaviorComponent.SetConfig(this);

            return behaviorComponent;
        }

    }
}