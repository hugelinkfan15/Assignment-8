﻿/*
* Kayden Miller
* Assignment 8
* Class for controlling target objects
*/
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 12; private float maxSpeed = 16;
    private float maxTorque = 10; private float xRange = 4;
    private float ySpawnPos = -5;

    private GameManager gM;

    public int pointValue;

    public ParticleSystem explode;
    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(),ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(),RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gM.gameActive)
        {
            gM.UpdateScore(pointValue);
            Instantiate(explode, transform.position, explode.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!gameObject.CompareTag("Bad"))
        {
            gM.GameOver();
        }
        Destroy(gameObject);
    }

    Vector3 RandomForce() { return Vector3.up * Random.Range(minSpeed, maxSpeed); }
    float RandomTorque() { return Random.Range(-maxTorque, maxTorque); }
    Vector3 RandomSpawnPos() { return new Vector3(Random.Range(-xRange, xRange), ySpawnPos); }
}
