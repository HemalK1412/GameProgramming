
# Update UI

This Tutorial will show you two ways to Update UI.

1. Using UnityEvents
2. Directly updating it

## Using Unity Events

### Setup
A canvas with 2 text boxes 1 with the description of the collectable object and the other empty or a 0.

!2 images
___________________
A collectable that the player will pick up.

The Player will have his inventory(script).

### Script

The collectable I have gone for is keys.

#### Player inventory

```.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

```

We will need to add an extra namespace "using UnityEngine.Events;

```.cs

public class PlayerInventory : MonoBehaviour
{
    
    public int NumberOfKeys { get; private set;}
    public UnityEvent<PlayerInventory> OnKeyCollected;
    
    public void KeyCollected()
    {
        NumberOfKeys++;
        OnKeyCollected.Invoke(this);
    }

}

```

First, we create a property called NumberOfKeys.
Then a Unity Event from <this script> called OnKeyCollected.

Then we write the function to update the NumberOfKeys value called KeyCollected().<br>
Which is just NumberOfKeys++.

When the function is called it will Invoke the Event we created;

#### Collectable

This script goes on the collectable in my case my Key.

```.cs
public class KeyCollectable : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            playerInventory.KeyCollected();
            gameObject.SetActive(false);
        }
    }
}
```

This is just a standard Trigger Enter Function which looks for PlayerInventory script on the objects it collides with.

If the object has a PlayerInventory script it calls the KeyCollected function(which will update the key count) and disable the key from the game space.

#### Counter(UI)

This script will go on the text box that displays the key count.

```.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

```

An additional namespace is needed since Unity Merged with TextMeshPro called "using TMPro";

```.cs

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI KeyCount;
    

    void Start()
    {
        KeyCount = GetComponent<TextMeshProUGUI>();
        
    }

```

We make a reference to the text box that is to be updated.
Then grab the TextMeshProUGUI component from it.

```.cs

    public void UpdateKeyCount(PlayerInventory playerInventory)
    {
        KeyCount.text = playerInventory.NumberOfKeys.ToString();
    }
    
}

```

This function will Update KeyCount according to the values sent from the PlayerInventory script.
So it takes the TextMesh reference we set earlier and sets the value to be the Number of Keys value from the PlayerInventory script.

Also the value is an integer value so we need to convert it to a string variable which is done by adding ".ToString()" and if you want to convert from string to integer the function is int.TryParse("string value").

#### Final Setup

After the scripts are attached to the corresponding objects.

The Player who has the PlayerInventory script and extra box has opened for the event.<br>
Place the the key count text box here which has the InventoryUI script use the drop-down menu to select the UpdateKeyCount method. 

#### Script Flow

1. > The character picks up a key.
   >> The collectable scripts update the Player Inventory.
2. > This invokes the counter in our UI.
   >> Which then grabs the latest value updated in the Inventory and updates the counter.

## Directly updating it

This script is on a gun and counts target hits.

### Setup

A canvas setup like this.

#### Script

```.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

```

The same namespace will need to be added here.

```.cs

public class TargetShooter : MonoBehaviour
{
    [SerializeField] Camera cam;
    public TextMeshProUGUI ScoreCounter;
    private int Score = 0;
```

The variables needed are a reference to the Text Box and a Score integer.

```.cs

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Target target = hit.collider.gameObject.GetComponent<Target>();
                if(target != null)
                {
                    target.Hit();
                    Score++;
                    ScoreCounter.text = Score.ToString();
                }
            }
        }
    }
}

```
This function on the Mouse left click casts a ray and if it hits an object with the Target scripts increments the Hit variable.
Then through direct reference to the Score display on the UI, it updates the display string with the new score value.

