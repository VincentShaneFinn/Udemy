﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;

    ScoreBoard scoreBoard;
    [SerializeField] int scorePerHit = 12; // maybe put on an enemy
    [SerializeField] int hp = 100; // maybe put on an enemy

    // Use this for initialization
    void Start () {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
	}

    private void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject col) //col is the name of the particle object
    {
        scoreBoard.ScoreHit(scorePerHit);
        //lower hp, could be via enemy, or a static value, or from the bullet
        hp--;
        if(hp <= 0)
        {
            killEnemy();
        }

    }

    private void killEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
