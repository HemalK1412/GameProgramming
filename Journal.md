# Journal 
## Journal
For Mac users, the scripts do not open right away in Visual Studio so locate the file in Explorer and open it from there.



For the gun shooting using ray cast, the gun kept shooting without mouse input.  Tried restarting Unity and copied the code to another file    but the line that checks if the button is pressed had a semi-colon so it went straight inside the if statement (Add images)


For my realistic character controller, first-person the added force component from the grenade makes the character fly and keep rotating. 
  So the character has a ground check object that looks for a ground object in the Ground layer and resets the velocity gained from falling and since the character is rotating this cannot take place. So in the character controllers' rigid body you can constraint all rotation for the characters so he still takes the force but does not go into a death spiral  

for my guns the muzzle flash will not instantiate

