﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    [CreateAssetMenu(menuName = ("RPG/Special Abiltiy/Self Heal"))]
    public class SelfHealConfig : AbilityConfig
	{
		[Header("Self Heal Specific")]
		[SerializeField] float extraHealth = 50f;

		public override AbilityBehavior GetBehaviorComponent(GameObject gameObjectToattachTo)
		{
            return gameObjectToattachTo.AddComponent<SelfHealBehaviour>();
        }

		public float GetExtraHealth()
		{
			return extraHealth;
		}
	}
}