eThis tutorial is for spawning objects at random positions.
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

minX = center - (size of the X axis)/2.
maxX = center + (size of the X axis)/2.

So if the origin is at 0 and the distance of Origin to X = 1, then origin to -x = 1 as they are equidistant from the origin.
Then size of X axis would be (-x,X) = 1 + 1 =2.

min X = 0(center) - 2(size of X axis)/2
        = 0 - 1
            -1
max X = 0(center) + 2(size of X axis)/2
        = 0 + 1
            1
The range of the x-axis would be (minX, maxX) = (-1, 1)

Same for the Y and Z axes.

```.cs
public class TargetBounds : MonoBehaviour
{
    public static TargetBounds Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] BoxCollider col;
```
Create an public instance of the script so the spawn objects can access the GetRandomPosition function.
An reference to the Box Collider which is to be drag and dropped from the inspector to this script.
```.cs

    public Vector3 GetRandomPosition()
    {
        Vector3 centre = col.center + transform.position;
```
This is to ensure the accuracy even if the position of the collider moves.
```.cs

        float minX = centre.x - col.size.x / 2f;
        float maxX = centre.x + col.size.x / 2f;

        float minY = centre.y - col.size.y / 2f;
        float maxY = centre.y + col.size.y / 2f;

        float minZ = centre.z - col.size.z / 2f;
        float maxZ = centre.z + col.size.z / 2f;
```
**This is where we implement our calculations.

```.cs
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
        return randomPosition;
    }
}
```
So after we have our ranges for different axes we have the code select a random point on each axis using those minimum and maximum values.
  The randomX coordinate is a random point between minX and maxX.
  The randomY coordinate is a random point between minY and maxY.
  The randomZ coordinate is a random point between minZ and maxZ.

We store it in a new Vector 3 with the 3 coordinates as the values for x,y and z axis.


