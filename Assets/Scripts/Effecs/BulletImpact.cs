﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,1.0f);
        Invoke("disable", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void disable(){
        gameObject.SetActive(false);
        
    }
}
