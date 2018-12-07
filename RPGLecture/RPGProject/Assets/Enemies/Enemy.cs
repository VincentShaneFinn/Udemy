﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float maxHealthPoints = 100f;
    float currentHealthPoints = 100f;

    // Use this for initialization
    void Start()
    {

    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }
}
