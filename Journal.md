
## Journal

> For Mac users, the scripts do not open right away in Visual Studio.
>> The default option to open scripts is Visual Studio. But even changing it to VS Code did not solve the issue. A temporary fix is to locate the script in the File Explorer and open it in VS code.

> For the gun shooting using ray cast, the gun kept shooting without mouse input.
>> I tried restarting Unity and copying the code to a new file which did not solve it. The problem was with the line that checks if the button is pressed or not. So it was an If statement that checks wether the player clicked the left mouse button or not ad i put a semicolon at the end of it. This made the code skip the check and call the shoot() function every loop.

```.cs
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }
```

> For my realistic character controller(first-person controller) the added force component from the grenade makes the character fly and keep rotating.
>>  So the character has a ground check object that looks for the ground plane in the Ground layer and resets the velocity gained form falling repeatedly and since the character is in air this cannot take place. To fix this add a Rigidbody component to the character controller and constraint all rotation for the character so he still takes the force but does not go into a death spiral.

![Rotation Constraints](https://github.com/HemalK1412/GameProgramming/blob/11e86503f7488a9a6c879937bd971d5a92075e18/Tutorials/Images(Tutorials)/Journal/Character%20controller%20freeze%20rotation.png)

while gun switching I would scroll up and down but the weapons would only move down the list.
  This is because I copied and pasted a part but forgot to change the signs so it moves up the list too.

the grenade will apply force to the boxes but no the destroyed pieces 
    Scan for colliders twice and apply the force twice.

I made my map in blender and exported as an fbx file.
My player woujld pass through the ground even if I had a box collider on it. but it was solved when i created the ground plane within Unity itself.

Also my map is one single mesh and adding mesh collider will give you the error that the object needs to have a rigidbody or set the collider to a convex collider.
  A convex collider is just a box that envelops the entire objects and upon starting the playthrough my PLayer would jitter inside the map and be thrown out of the map.
  To solve dont tick convex in the collider Just add a rigidbody and tick off "Use gravity" and enable "Is Kinematic". just to be safe freeze the position and rotation for the map object form the rigidbody component in the inspector. 

for my guns the muzzle flash will not instantiate
---code image
The time to destroy the muzzle flash does not need to be with Time.DeltaTime. The flash was instantiating as a child of the disabled muzzle flash i had in the game. Since the parent object is diabled the flashes were too. So create a prefab of the flashes and reference them to the Gun script. The muzzle flash now work but the direction of the flashes is off. to solve this open the prefab and change the rotation of the objects in it.


Teleporting while moving does funky stuff

The character could not jump (the code had zero errors and the logic was perfect)
  The problem was if you set the ground distance value very low it will overwrite the jump velocity value and reset jump velocity to 0 very quickly.

Teleport setup was affecting the entire map and making it spiral out of control
  Thsi is related to issue of colliders not working unless they are convex or have rigidbody attached with them. To solve attach rigidbody and freeze position and rotation.

The character wont teleport move until physics.SyncTransfroms();

Unity has fully merged with Text Mesh Pro and the object reference I was trying to setup was throwing errors. You need to add an extra namespace "using TMpro;" at the top.
Update Ui using different Methods

Event System and direct update


I know that I have done most of my Component fetching in the Start method which is to be done in the Awake method but it does not make that much of a difference.
