# Realistic Character Controller

As the name suggests this will be a first-person character controller.

### Setup

In Unity set up a Ground plane first.<br>
Then set the layer to "Ground". If you do not have a ground layer you can add one by clicking Layer -> Add Layer -> name a new layer "Ground".
> Layer
>> Add Layer
>>> Name a new Layer "Ground". 

![Layer Setup](https://github.com/HemalK1412/GameProgramming/blob/4e7482f7154eb4338d6a1f93045f447c4d07ae01/Tutorials/Images(Tutorials)/Character%20Controller/Add%20Layer.png)

<p>
Make an Empty Object, name it Player, and add the Character Controller Component to it.<br>

The character controller already has a capsule collider built into it. So if you have a character model import it and set it to be the child of the Player object and adjust the height and radius so it envelops the model. I will be using a Cylinder as my Player so here are my values.
</p>

> Player -> Character controller component
>> Radius = 0.6
>> Height = 3.6

> Cylinder 
>> Scale = 1.2, 1.8, 1.2

Then add a camera as a child to the Player and place it in front of the eyes (you can set the vision cone however you like.)

Then to the Player object add the script "PlayerMovementScript" 

### Script

```.cs
{
    public CharacterController controller;

    public float speed = 10f;
    public float gravity = -9.8f;
    public float JumpHeight = 3f;

    Vector3 velocity;
```

These are the variables we will need:

Speed and JumpHeight can be set to whatever you want(this will depend on the type and build of your character).

Gravity is the freefall velocity of humans at 9.8m/s and is constant. This is negative cause it's in the -y direction.
         
         
```.cs

    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime); 

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); 
    }
}



```
<p>
    
Input.GetAxis is to get data from the Input Manager and then we multiply the value with the corresponding sides and save it in a Vector3 variable. This variable is temporary. (The X-axis is for sideways movement and the Z-axis is for forward and backward movement).<br>
    
Then to move we use Controller.Move (The temporary variable multiplied by speed multiplied by Time.deltaTime to smooth out the movement).<br>

For Jumping first, we will check for the key press using an if statement.<br>
    
The formula for Jump is Jumping = SquareRoot ((Jump Height * -2f) * gravity).

The formula is the velocity needed to jump a certain height.
    
Then to increase freefall speed we will keep adding gravity. This will cause the gravity to keep increasing every time the character jumps. 

So to reset the gravity when the character lands, create an empty object at the base of the character and have it reset the gravity when the character is on Ground.

</p>
We will some new variables.

```.cs

    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public  LayerMask groundMask;

  
```
The new object we created will serve as the Ground Check transform.

The Ground distance can be adjusted and is the distance at which it will look for the Ground Plane.

The layer mask is to isolate the ground plane collider.

```.cs
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }
```

When the character lands it checks for the ground using Physics.Checksphere at the position at a distance of 0.4f for the Ground layer. 

If isGrounded is true and the current velocity is 0 it resets the velocity component to -1f.

Without this, the character would fall faster with every jump.

### Mouse Script

The script is placed on the Camera that is child to the character controller.

```.cs
public class Mouse : MonoBehaviour
{
    public Transform Player;
    public float mouseSensitivity = 100f;

    float xAxis = 0f;

```
The variables are a reference to the player transform.

Mouse sensitivity can be adjusted in the inspector.

xAxis is defined to clamp its rotation from top to bottom so the camera does not perform a flip.

```.cs
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }   

```

This is to hide the cursor when the game starts.

The cursor appears when the Esc key is pressed.

```.cs

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

```

This is storing the value of the different axes from the input manager multiplied by the mouse sensitivity and Time.deltaTime.

So the confusing part about this script is why xAxis interacting with mouseY.

The mouse movement is 2 Dimensional.

![mouse axis](https://github.com/HemalK1412/GameProgramming/blob/dc9de70dfe8e030320b925b2bb2896424ba9cae4/Tutorials/Images(Tutorials)/Character%20Controller/Mouse%20Axis.jpg)

But character movement is 3 Dimensional

![character axis](https://github.com/HemalK1412/GameProgramming/blob/29b830fb9d6e62e1bd7d5291063b318f1788af14/Tutorials/Images(Tutorials)/Character%20Controller/Character%20Axis.png)

So mouseX(side to side) translates to characterY(side to side) rotation and mouseY(up-down) translates to characterX(up-down) rotation.

The xAxis defined in the script is the character rotation for the X-axis(up-down).

```.cs
        xAxis -= mouseY;
        xAxis = Mathf.Clamp(xAxis, -90f, 90f);
```

We take xAxis(character Up-down) and decrease mouseY(mouse up-down) from it because if we add it the rotation is flipped.

Then we clamp the axis so the camera does not perform a flip. 

```.cs

        transform.localRotation = Quaternion.Euler(xAxis, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }
}
```

Then we apply the xAxis(character up-down) rotation to the camera. (So the entire character does not rotate.)

And we rotate the player on Vector3.up which is the Y-axis(side to side) of the player with the mouseX(mouse side to side).

We rotate the player here so we move forward where the camera looks.

If we only rotated the camera here it would mean only the camera would rotate and the player would start moving sideways.


### Complete Scripts

#### Movement Script

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
}
```

#### Look Script

```.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerLook : MonoBehaviour
{
    public Transform Player;
    public float mouseSensitivity = 100f;

    float xAxis = 0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }   

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAxis -= mouseY;
        xAxis = Mathf.Clamp(xAxis, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xAxis, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }
}
```

