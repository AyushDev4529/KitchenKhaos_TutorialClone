### Adding Character Controller to player

## Goal
Refactor custom player movement to Unity’s CharacterController
to achieve stable collision handling, gravity, and animation state.


>Problem : Movement logic was using custom Player Controller Logic for movement and using Custom logic for Collision Detection, Due to which several bugs were there like we talked in previous lesson. So to solve that Using Unity's built-in Character Controller.

1. Add `CharacterController` Component to player Game Object. (Imp : Parent not the Visual)
2. Adjust the Y value and Radius to match the Asset.
3. Now we can Refactor the player movement in `Player.cs` file.
4. First add a Character Controller Reference `CharacterController characterController`.
5. Then either cache the `characterController` or add a `[SerializeField]` and manually drag the component in editor.
6. Now to change the movement logic in `PlayerMovement()` so it uses `characterController.Move(movementDirection * movementDistance)`.
7. `characterController.Move()` is used for 
	- **Collision Handling:** The `Move` function automatically handles collisions and allows the character to slide along surfaces like walls or slopes.
	- **Absolute Delta:** You must provide an absolute movement delta (the exact distance to move this frame). This is why multiplying by `Time.deltaTime` is essential for consistent speed across different frame rates.
	- **Return Value:** The function returns `CollisionFlags`, which you can use to check if the character is hitting the ground, ceiling, or sides during that specific move.
	- **Best Practice:** It is recommended to call `Move` only once per frame.
8. Now we can also separate the Horizontal Velocity from Vertical Velocity, using `characterController.velocity.x` and `characterController.velocity.z` and set y to 0, ex. `Vector3 horizontalVelocity = new Vector3(characterController.velocity.x, 0, characterController.velocity.z)`
9. Using this new `horizontalVelocity.magnitude` we can check if player is walking or not, ex `isWalking = horizontalVelocityMagnitude > smallThreshold`.
10. Using this `horizontalVelocity.magnitude` we can also check if player needs to rotate, ex. `if (horizontalVelocityMagnitude > smallThreshold)   PlayerRotation();`
11. To Calculate Vertical Velocity we can use `characterController.isGrounded` which returns true if on Ground and false otherwise, with this we can implement a method so when plyer is above ground their vertical Velocity increases over  time, ex. `verticalVelocity += Physics.gravity.y * Time.deltaTime`
12. Now we Can add our vertical Velocity to our `playerMovement`, ex. `characterController.Move(movementDirection * moveDistance + new Vector3(0, HandleGravity() * Time.deltaTime, 0))`
