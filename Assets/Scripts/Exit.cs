using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void Start()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("Exit");
        Application.Quit();
    }
    
}



