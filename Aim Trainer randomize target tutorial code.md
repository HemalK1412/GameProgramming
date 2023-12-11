This tutorial is for spawning objects at random positions.
And will explore 2 ways of achieving this.
1 (Technically not spawning)Move the object after being hit and update counters as in an Aim trainer.
2 Actually spawning random objects at random locations.
The script for the random positions will be the same so we will do that first and then go ahead with the separate methods.

To create the bounding box on which the objects will spawn Create an Empty object in unity and add a box Collider to it.
________Collider edit inspector image.

Then edit the box collider size by clicking the button marked below. Clicking the button will enable you to change the collider dimensions by clicking and dragging the dots
that have appeared on the collider faces in the scene view.
Now if you have already placed the Gameobject where you need it and just need to expand the collider pressing Alt + adjusting the collider will make it expand on opposite sides
simultaneously.
________Collider image

Then disable the box collider in the inspector. We only need the dimensions of the collider and do not need collision physics enabled on it.

Then add the script for random spawn you can name it anything you want. I have gone with TargetBounds.
_______3 coordinate system image

If the origin marks the center of our collider then we need to find the bounding faces of our collider.
Which for the x-axis would be -x to origin + origin to X same for Y and Z axes.

So to get the min value of X (minX) we can 

```.cs

    [SerializeField] BoxCollider col;
```
An reference to the Box Collider which is to be drag and dropped from the inspector to this script.
```.cs

    public Vector3 GetRandomPosition()
    {
        Vector3 centre = col.center + transform.position;

        float minX = centre.x - col.size.x / 2f;
        float maxX = centre.x + col.size.x / 2f;

        float minY = centre.y - col.size.y / 2f;
        float maxY = centre.y + col.size.y / 2f;

        float minZ = centre.z - col.size.z / 2f;
        float maxZ = centre.z + col.size.z / 2f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
        return randomPosition;
    }
}
```



```.cs
public class TargetBounds : MonoBehaviour
{
    public static TargetBounds Instance;
    private void Awake()
    {
        Instance = this;
    }

```

