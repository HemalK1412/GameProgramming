# Teleport Tutorial

This script will cover a basic method of teleporting the player to a set location.

### Setup

A player, a portal, and a destination object.

![Setup](https://github.com/HemalK1412/GameProgramming/blob/469a1eebaf2b9a7e522d1e015c2f04b5a9a336ca/Tutorials/Images(Tutorials)/Teleport/Setup.png)

The assets can be changed to whatever you like and the destination object can be an empty object it just needs to be enabled in the inspector.

For the character object make sure it has a "Player" tag

![PLayer Tag](https://github.com/HemalK1412/GameProgramming/blob/3964d5d8195ea45fd1418935239263031a15fe5b/Tutorials/Images(Tutorials)/Teleport/Player%20tag.png)

If the list does not have a Player tag to select you can add your custom tags by clicking the Add Tag button at the bottom of the list.

Make sure the actual portal has the "Is Trigger" box ticked in the inspector.

![Is trigger](https://github.com/HemalK1412/GameProgramming/blob/9063a59bcf1374e4962a8c5622322a017a78b406/Tutorials/Images(Tutorials)/Teleport/Is%20Trigger.png)

### Script

#### Portal

```.cs

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

We define an OnTriggerEnter function and pass the argument "Collider Other" which is it triggers with every other collider.

Then we check if the object that has triggered the function has a "Player" tag or not.

If it has the Player tag Then we call the movement script on the Player and call the teleport function on it while passing the destination transform from this object.

#### PLayer Script (Movement Script From the Character Controller Tutorial)

```.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;
    public float gravity = -9.8f;
    public float JumpHeight = 3f;
    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public  LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;



    void Update()
    {
        
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -3f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime); 

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt((JumpHeight * -2f) * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); 

        
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
        Physics.SyncTransforms();
    }
}
```

So we add another function to the script which is the Teleport Function and accept an argument which is the destination transform we sent while calling this function from the Portal Script.

This function sets the current transform of the Player to be the same as the destination transform.

But this will not teleport our player.

Since we use a character controller component for our Player we need to perform a Physics.SyncTransforms(). This will do the final teleport.

## Notes

> If your Player still does not teleport.
>> 1. Attach a Rigidbody.
>> 2. You might also need to Disable(Player.SetActive(false)) the Player before switching positions and enable(Player.SetActive(true)) afterward.
