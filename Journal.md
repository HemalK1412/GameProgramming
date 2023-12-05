# Journal 
## Journal
For Mac users, the scripts do not open right away in Visual Studio so locate the file in Explorer and open it from there.



For the gun shooting using ray cast, the gun kept shooting without mouse input.  Tried restarting Unity and copied the code to another file    but the line that checks if the button is pressed had a semi-colon so it went straight inside the if statement (Add images)


For my realistic character controller, first-person the added force component from the grenade makes the character fly and keep rotating. 
  So the character has a ground check object that looks for a ground object in the Ground layer and resets the velocity gained from falling and since the character is rotating this cannot take place. So in the character controllers' rigid body you can constraint all rotation for the characters so he still takes the force but does not go into a death spiral  

for my guns the muzzle flash will not instantiate
---code image
The time to destroy the muzzle flash does not need to be with Time.DeltaTime. The flash was instantiating as a child of the disabled muzzle flash i had in the game. Since the parent object is diabled the flashes were too. So create a prefab of the flashes and reference them to the Gun script. The muzzle flash now work but the direction of the flashes is off. to solve this open the prefab and change the rotation of the objects in it.

for the player controller the character has to be referenced even when the script is on the character itself the horizontal view is locked  the mouse script cannot be without the character reference   add images 
public class Mouse : MonoBehaviour
{
    public Transform Player;
    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }
}
from mouse script and replace 

public class Mouse : MonoBehaviour
{
    public Transform Player;
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        Quaternion vertical = Quaternion.Euler(xRotation, 0f, 0f);
        Quaternion horizontal = Quaternion.Euler(0f, yRotation, 0f);
        transform.localRotation = horizontal * vertical;
    }
}

