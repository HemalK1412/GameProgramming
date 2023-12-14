
# learning Journal

### 
> For Mac users, the scripts do not open right away in Visual Studio.
>> The default option to open scripts is Visual Studio. But even changing it to VS Code did not solve the issue. A temporary fix is to locate the script in the File Explorer and open it in VS code.

###
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

###
> For my realistic character controller(first-person controller) the added force component from the grenade makes the character fly and keep rotating.
>>  So the character has a ground check object that looks for the ground plane in the Ground layer and resets the velocity gained form falling repeatedly and since the character is in air this cannot take place. To fix this add a Rigidbody component to the character controller and constraint all rotation for the character so he still takes the force but does not go into a death spiral.

![Rotation Constraints](https://github.com/HemalK1412/GameProgramming/blob/11e86503f7488a9a6c879937bd971d5a92075e18/Tutorials/Images(Tutorials)/Journal/Character%20controller%20freeze%20rotation.png)

###
> While gun switching I would scroll up and down but the weapons would only move down the list.
>> This is because I copied and pasted a part of the code to move the weapons down the list  but forgot to change the signs so it moves up the list too.

##
> The grenade will apply force to the boxes but not to the destroyed pieces. 
>> So the force is being applied to the crates as intended but when the scraps appear they just fall. So the grenade would need to scan nearby colliders again and apply force one more. Just copy the code in the function except the destroy(gameObject) and paste it once more before you destroy the gameobject.

```.cs

    void Explode()
    {
        // Show some effect

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius); 
            }
        }

        Destroy(gameObject);
    }
```

###
> I made my map in Blender and exported it as an FBX file.
>> My player would pass through the ground even if I had a box collider on it. Even tried adding the layer and everything set it up as before. It seems to be some kind of error I could not pinpoint but I did solve it by just making the ground plane within Unity itself. (This plane had the same components as the one from Blender and worked just fine).

###
> Also my map is one single mesh and adding a mesh collider will give you the error that the object needs to have a rigid body or set the collider to a convex collider.
>> A convex collider is just a box that envelops the entire object and upon starting the playthrough my Player would jitter inside the map and be thrown out of the map.
  To solve don't tick convex in the collider Just add a rigid body and tick off "Use gravity" and enable "Is Kinematic". just to be safe freeze the position and rotation for the map object from the rigidbody component in the inspector.

![Map Inspector](https://github.com/HemalK1412/GameProgramming/blob/db7a12889a80f9e356af39e6902f0025a80164a0/Tutorials/Images(Tutorials)/Journal/Map%20Inspector.png)

###
> For my gun script the muzzle flash will not instantiate.
>> At first I was multiplying the Destroy(Flash, 0.2f * Time.DeltaTime). The flash was instantiated as a child of the disabled muzzleflashPosition object I had in the game. since the parent object is disabled the child objects are too. So create a prefab (Drag the object from the Hierarchy into the projects panel) and reference that to the Gun Script. Also multiplying with Time.DeltaTime  will destroy the flash very quickly so remove it. Now the flashes work but the direction is off. This can be solved by just opening the prefab and changing the rotation in it.

```.cs

    void Shoot()
    {
        RaycastHit hit;
        
        GameObject Flash = Instantiate(MuzzleFlash, MuzzleFlashPosition.transform);
        Destroy (Flash, 0.2f);
        

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDmg(damage);
            }
        }

    }

```

###
> The character could not jump (the code had zero errors and the logic was perfect)(For the script refer to the Character Controller tutorial)
>> So the problem was I set the ground check value to be very low and will overwrite the jump velocity value to 0 very quickly.

###
> Teleport setup was affecting the entire map and making it spiral out of control
>> This is related to the issue of colliders not working unless they are convex or have rigidbody attached to them. To solve attach rigidbody to the Portal gameobject and freeze position and rotation.

###
> The character won't teleport(move) until physics.SyncTransfroms(); (Reference script in the teleport tutorial)
>> So when using a Character controller component to move your player. The teleport values are assigned when you set the player transform equal to the destination transform. But it then needs to be updated by the character controller component to change position.<br>
>> This sometimes does not work so you need to Disable the player, change the position and syncTransforms() then enable the player back again.

###
> Unity has fully merged with Text Mesh Pro and the object reference I was trying to set up was throwing errors.
>> You need to add an extra namespace "using TMpro;" at the top.

###
> Update Ui using different Methods.
>> I know the entire point of code is to reduce the compute the game has to perform. Both methods used by me in my game could have been done in a single script and by directly referencing the UI element.
>> But I wanted to explore different ways to achieve the same tasks and the use cases for the different methods.

###
> I know that I have done most of my Component fetching in the Start method which is to be done in the Awake method but it does not make that much of a difference.
>> I have found it to be more useful if the game has a Game Manager script controlling everything and has multiple people working on the same game. If it is a solo project it does not make much of a difference.

## 13/12/2023
> Github push and pull
>> So while making the tutorials I was trying to upload the Images folder through github desktop and it was no success. So I did a force push which did the job but reset all the tutorial files to what they were before. So I had to Re-edit them.
