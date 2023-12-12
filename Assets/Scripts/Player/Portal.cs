using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform destination;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            PlayerMovementScript player = other.GetComponent<PlayerMovementScript>();
            player.Teleport(destination.position);
        }
    }
}
