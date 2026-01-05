### Naming Rules
##### Type of Cases
	* PascalCase
	* camelCase
	* snake_case

- Functions - **PascalCase**
- Function Parameters - **camelCase**
- Fields - **camelCase**
- Properties - **PascalCase**
- Constants - **UPPERCASE snake_case**
- for array - arr in front
- list - list in front
- Interface - I in front

> *Spend time dedding on a proper name !
	Don't be afraid to rename things
	Don't use single letter names
	Don't use acronyms or abbreviations*
### Unity Shortcuts

- Ctrl + Shift + F  - to align Scene Camera to the screen Position


> Setup Post Processing Study tomorrow

### Unity Input
#### Old Input - for Quick Prototyping
- `GetKey` vs `Get Key down`

| `GetKey`                                           | `GetKeyDown`                             |
| -------------------------------------------------- | ---------------------------------------- |
| Returns True for Single Frame When Key is pressed. | Returns true as long as key is held Down |
| Use case : Jump, Fire, etc.                        | Use Case : Movement, Driving, etc.       |

- The Character moves in X & Y plane so `Vector2` input will suffice
- We add and subtract the input to the `Vector2` based on the Key Pressed
	- W is for Forward -> y += 1
	- D is for Right -> x += 1 etc.
- We normalize the vector using `normalized` before applying so it only retain direction information not speed.
- Applying vector2 to `transform.position`  to move the object. ==Temporary later we will use Physics based system==*
- `transform.position` takes a vector3 input so we convert it to `Vector3` and store it in new variable `Vector3 movementDirection = new Vector3(movementInput.x, 0, movementInput.y);`
- To make it frame independent multiplying it by `Time.deltaTime`.
- And to increase its movement speed by some amount adding a `moveSpeed` variable which is a `[SerializedField]` to make it accessible from Editor.

> Separated to different functions for later refactoring

> `transform.position` vs `transform.Translate`
> For general object movement not involving physics, both methods can achieve the same results, but `transform.Translate()` offers a built-in handling of local space movement that can be more convenient. Neither method should be used for movement of `Rigidbody` components that require physics interactions (use `Rigidbody.MovePosition()` or `rigidbody.velocity` instead).

- To rotate the player so it faces move direction we can use multiple methods one of them is  `transform.forward = movementDirection`.
- To smooth out rotation using **Lerp** , for rotation use **Slerp** for position use **Lerp**, in this case use `Vector3.Slerp`
> 	We will learn that in deep later in ==`Splines`==.


### Animator
- Adding Animation to the player,
- Add `animator` component to the player visual.
- Create a animation Controller and name it,
- Open Animation window using Ctrl + 6.
- Record the animation using record button ![[Animation Recording Button.png]](Images/Animation-Recording-Button.png)
- Record basic animations for Idle and Walk,
- in Animator window make transition from Idle to Walking with a parameter of bool type named `IsWalking` and make condition in transition for is waking set to true for walking animation. ![[Transition for Animation.png]]
> We will learn animation later in Great details for now understanding the basics that this function exists in unity.

### Animating Player Script

- We can Get the Component by either `[SerializedField] Animator animator` and dragging the animator component from editor or by using  `GetComponent<Animator>();`
- Use `animator.SetBool` to set bool value of `IsWalking` to false  
- Also `SetBool` takes `string` & `true`or`false` as parameter, So, to reduce chances of error use constant `string`set to the string used as parameter in Animator ex. `const string IS_WALKING = "IsWalking"` 
- Then use the **const** in place of pure string `animator.SetBool(IS_WALKING, false)`.
- To check if player is moving in player movement script create a function that returns if player is moving eg `public bool IsWalking(){... return isWalking;}` store it in an bool variable `isWalking = player.IsWalking()`.
- Then in Update set the bool value of `SetBool` to false e.g. '`animator.SetBool(IS_WALKING, isWalking)`.