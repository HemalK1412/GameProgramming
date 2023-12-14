# Teleport Tutorial

This script will cover a basic method of teleporting the player to a set location.

### Setup

A player, a portal, and a destination object.

![Setup](https://github.com/HemalK1412/GameProgramming/blob/469a1eebaf2b9a7e522d1e015c2f04b5a9a336ca/Tutorials/Images(Tutorials)/Teleport/Setup.png)

The assets can be changed to whatever you like and the destination object can be an empty object it just needs to be enabled in the inspector.

Make sure the actual portal has the "Is Trigger" box ticked in the inspector.

![Is trigger](https://github.com/HemalK1412/GameProgramming/blob/e2cb6a2684b88a8c17e0b5e7ca2cd29f08522f24/Tutorials/Images(Tutorials)/Teleport/Screenshot%202023-12-14%20053115.png)

### Script

#### Portal

```cs

public class Portal : MonoBehaviour
{
    [SerializeField] Transform destination;

```

We only need one variable and that is the Transform for our destination object. 

```.cs

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            PlayerMovementScript player = other.GetComponent<PlayerMovementScript>();
            player.Teleport(destination.position);
        }
    }
}

```
