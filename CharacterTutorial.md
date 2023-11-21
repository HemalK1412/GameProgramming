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
