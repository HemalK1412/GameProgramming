# Realistic Character Controller

As the name suggests this will be a first-person character controller.

In Unity set up a Ground plane first. Then set the layer to "Ground". If you do not have a ground layer you can add one by clicking Layer -> Add Layer -> name a new layer "Ground".
         Add a video for layer setup


Make an Empty Object, name it Player, and add the Character Controller Component to it.
         The character controller already has a capsule collider built into it. So if you have a character model import it and set it to be the child of the Player object and adjust the height and radius so it envelops the model. I will be using a Cylinder as my Player so here are my values
         Player -> Character controller component
                  Radius = 0.6
                  Height = 3.6
         Cylinder 
                  Scale = 1.2, 1.8, 1.2

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
These are the variables we will need 
         Speed and JumpHeight can be set to whatever you want.
         Gravity is the freefall velocity of humans at 9.8m/s. This is negative cause it's in the -y direction.
         
         
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
Input.GetAxis is to get data from the Input Manager and then we multiply the value with the corresponding sides and save it in a Vector3 variable. This variable is temporary. 
Then to move we use Controller.Move(The temporary variable multiplied by speed multiplied by Time.deltaTime to smooth out the movement.
For Jumping first, we will check for the key press using an if statement.
The formula for Jump is Jumping = Jump Height 

