# Realistic Character Controller

As the name suggests this will be a first-person character controller.

### Setup

In Unity set up a Ground plane first.<br>
Then set the layer to "Ground". If you do not have a ground layer you can add one by clicking Layer -> Add Layer -> name a new layer "Ground".
> Layer
>> Add Layer
>>> Name a new Layer "Ground". 

Images for layer setup.

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
<pp>
Input.GetAxis is to get data from the Input Manager and then we multiply the value with the corresponding sides and save it in a Vector3 variable. This variable is temporary. (The X-axis is for sideways movement and the Z-axis is for forward and backward movement).<br>
    
Then to move we use Controller.Move (The temporary variable multiplied by speed multiplied by Time.deltaTime to smooth out the movement).<br>

For Jumping first, we will check for the key press using an if statement.<br>
    
The formula for Jump is Jumping = SquareRoot ((Jump Height * -2f) * gravity).
    The formula is the velocity needed to jump a certain height.
    
Then to increase freefall speed we will keep adding gravity. This will cause the gravity to keep increasing every time the character jumps. So to reset the gravity when the character lands, create an empty object at the base of the character and have it reset the gravity when the character is on Ground.
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
Without this the character would fall faster with every jump.

Mouse Script

The script is placed on the Camera that is child to the character controller.
```.cs
public class Mouse : MonoBehaviour
{
    public Transform Player;
    public float mouseSensitivity = 100f;

    float xRotation = 0f;

```
The variables are a reference to the player transform.
Mouse sensitivity can be changed in the pause menu.
xRotation is defined to clamp is rotation from top to bottom so the camera does not perform a flip.

```.cs
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }   

```
This is to basically hide the cursor when the game starts.
The cursor appears when the Esc key is pressed.


```.cs

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

```
This is storing the value of the different axes from the input manager multiplied by the mouse sensitivity and Time.deltaTime.
```.cs
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
```
This is subtracting the mouseY from the xRotation and clamping the value from -90f to 90f.
```.cs

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }
}
```
So now we apply the xRotation(y axis) to the camera.
And apply the X axis rotation to the player.

**Need to look into the final explanation**
