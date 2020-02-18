﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class akInfantry : MonoBehaviour
{
    public float normalspeed;
    private float fastspeed;
    private float speed;
    public float distance;

    private bool right = true;
    private bool detected = false;

    public Transform eyeDetection;
    private Transform target;
    public Transform targetDetection;

    public float fireRate;
    private float nextFire;
    public GameObject bulletPrefab;
    public Transform shotSpawner;
    private float oldPosition = 0.0f;
    private bool shooting;
    public float distanceToShot;

    //Sonido
    private AudioSource[] sounds;
    private AudioSource shot;
    private AudioSource alert;

    //Script general
    Human human;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        nextFire = 0.0f;
        oldPosition = transform.position.x;
        right = true;
        sounds = GetComponents<AudioSource>();
        shot = sounds[0];
        alert = sounds[1];
        speed = normalspeed;
        fastspeed = 2f * normalspeed;
        human = GetComponent<Human>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!human.muerto)
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(eyeDetection.position, Vector2.down, distance);
            if (groundInfo.collider == true)
            {

                if (!groundInfo.collider.gameObject.tag.Equals("Player") && !detected)
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);

                }

            }
            else
            {
                if (right)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    right = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    right = true;
                }
            }
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (detected)
            {
                if (distanceToTarget > distanceToShot)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, gameObject.transform.position.y, gameObject.transform.position.z), speed * Time.deltaTime);
                }
                if (Time.time > nextFire && distanceToTarget < distanceToShot)
                {
                    StartCoroutine(DoBlinks(0.9f));
                    nextFire = Time.time + fireRate;
                    GameObject tempBullet = Instantiate(bulletPrefab, shotSpawner.position, shotSpawner.rotation);

                    if (!right)
                    {
                        tempBullet.transform.eulerAngles = new Vector3(0, 0, 180);
                    }

                }


                if (transform.position.x > target.position.x) // he's looking right
                {
                    right = false;
                }

                if (transform.position.x < target.position.x) // he's looking left
                {
                    right = true;
                }

                if (!right)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);

                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }

            if (distanceToTarget > distance)
            {
                detected = false;
                speed = normalspeed;
            }
            else if (!detected)
            {
                alert.Play();
                detected = true;
                speed = fastspeed;
            }

            if (transform.position.x > oldPosition) // he's looking right
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                right = true;
            }

            if (transform.position.x < oldPosition) // he's looking left
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                right = false;
            }
            oldPosition = transform.position.x;
        }
    }


    IEnumerator DoBlinks(float seconds)
    {
        shot.Play();
        GetComponent<Animator>().SetBool("Shooting", true);

        yield return new WaitForSeconds(seconds);

        //make sure renderer is enabled when we exit
        GetComponent<Animator>().SetBool("Shooting", false);

    }
}
