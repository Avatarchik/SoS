﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKillable
{
    public static Player instance;
    public Vector3 pos;
    public float health = 100f;
    public AudioClip damageSoundClip;

    private AudioSource audioSource;

    void Awake()
    {//Singleton Pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void decreaseHealth(int damage)
    {
        health -= damage;
    }

    public bool isDead()
    {
        return health <= 0;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        pos = transform.position;
        
        if(pos.y < -10)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

        if (isDead())
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            decreaseHealth(10);
            audioSource.clip = damageSoundClip;
            audioSource.Play();
        }
    }
}
