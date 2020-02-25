﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator startScene(){
        yield return new WaitForSeconds(12.0f);
        SceneManager.LoadScene("Start");
    }
}
