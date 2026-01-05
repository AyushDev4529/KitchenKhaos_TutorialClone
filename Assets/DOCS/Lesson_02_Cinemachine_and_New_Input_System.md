### Cinemachine Camera

- Using Unity Package manager install Cinemachine.
- then add a Cinemachine virtual camera in Hierarchy.
- Change the Position, Rotation and FOV in Virtual Camera now, as we can't directly change in main camera now.
> For now this will suffice for still camera, We will look this in depth when making a customized Cinemachine camera for our other games.

### Refactoring to New Input System

- First we decouple the input from player movement, by creating a separate class `GameInput` which will take input.
> Rules to follow for Decoupling this is that Player Input is ==**Intent**== and Player movement is the ==**Action**== both should be separate also if we later want to increase input support or other platforms we can easily change in the  `GameInput` file without changing `Player` Script. 
- Put Input Script in `GameInput` Class ex. ```public class GameInput : MonoBehaviour
{
    public Vector2 GetMovementVector()
    {
        //Input Script
        return input; // not normalized
    }
}
- Step 1 : In unity 6 enable both Input manager in *Project setting -> player -> Other -> Active Input Handling.*
- Step 2 : Create Input Actions in Project Assets window, Double click to open it.
- Step 3 : In this input Action there are three Columns.
	- Action Maps is for different Scenarios for Ex, Normal walking Driving a car etc.
	- Actions are the individual actions in each Action Map, Ex, Movement, Jump, Fire, Fly etc. 
	
	> **Note : ***Create a Composite for WASD movement*
	
	- Action Properties are for Assigning the Input Keys like Button, Key Press, Controller Button etc.
- Check this ex image for more clarity![[Action Map Window.png]](Images/Action-Map-Window.png)
- Step 4: After Creating this Input Actions ==Save it!==
- Step 5: Now with Input Actions Selected in Inspector Generate a C# script.![[Genrate CS.png]](Images/Genrate-CS.png)
- Now in `GameInput` Script Create an `Awake()` and Construct Script class that was auto generated. ex. `PlayerInputActions inputActions = new PlayerInputActions();`
- Then Enable the Input Actions in `Awake` `ActionMap` ex. `inputActions.Player.Enable();` 
- In `GetMovementVector()` get the `Vector2` from `inputActions.Player.Move` with the help of `ReadValue<Vector2>()`, ex. `Vector2 input = inputActions.Player.Move.ReadValue<Vector2>();`
- Now return the `Vector2 input`
- Done, Now We have Decoupled this code from from movement and refactored with new input system.
- Now You Can Add any new Action Binding to your `Player` Action Map and everything should work the same. ex.
![[Player Action Map Ex.png]](Images/Player-Action-Map-Ex.png)
> Note : to calculate `movementDeltaVector` we subtract current position of player `transform.position` with previous player position `playerPos`
> ![[Vector Visualisation.png]](Images/Vector-Visualisation.png)
> `movementDeltaVector = transform.position - playerPos`
> To move from "A" to "B" point vector points down