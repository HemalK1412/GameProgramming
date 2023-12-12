using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Renderer rend;
    public float health = 100f;
    PlayerInventory playerInventory;

    void Awake()
    {
        rend =  GetComponent<Renderer>();
    
    }
    public void TakeDmg( float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
        Die();
        }
        colorChange();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void colorChange()
    {
        if (health > 50)
        {
            rend.material.color = Color.green;
        }
        else
        {
            rend.material.color = Color.red;
        }
    }
}
