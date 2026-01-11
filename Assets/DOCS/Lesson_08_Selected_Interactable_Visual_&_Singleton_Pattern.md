> The **Singleton design pattern in Unity** ==ensures that only one instance of a specific class exists throughout the game, providing a global access point for other scripts==. It is commonly used for "manager" scripts like `AudioManager`, `GameManager`, or `UIManager` that need to be accessible from anywhere without requiring direct references.
> Pros and Cons in Unity

| ***Pros***                                                                                                                                                                            | **Cons**                                                                                                                                                                |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Global Access** Provides an easy, global access point to methods and variables, avoiding the need for `GetComponent()` or manual reference dragging in the Inspector.               | **Tight Coupling** Can lead to tight coupling, making the code harder to change, extend, or refactor later.                                                             |
| **Guaranteed Single Instance** Ensures only one object of its kind exists, which is perfect for systems like Audio Managers or Save Systems.                                          | **Difficult to Unit Test** Testing classes that depend on singletons can be difficult because the global state is hard to isolate or mock.                              |
| **Persistence Across Scenes** The `DontDestroyOnLoad()` method allows singletons to maintain their state across scene changes (e.g., holding the player's score from level to level). | **Dependency Hiding** It's not immediately obvious what dependencies a class has just by looking at its signature, as the singleton calls can be hidden within methods. |

1. First duplicate the prefab visual and set different material to the duplicated prefab visual which will be the selected visual, also increase the size to very small amount of the parent duplicated visual prefab so it doesn't cause rendering issue. and then disable the visual component or child object of the new selected visual. 
2. Now Create an script for `SelectedVisual` .
3. but before editing anything in selected visual we need to know which object is being selected, which we can do in `player.cs` or later `Handleinteraction.cs`,
4. Now we just need to store our selected object details so we can send it to selected visual script.
5. To do that create a variable to store selected object `private ClearCounter selectedCounter`.
> 	Note: - **Selection** → continuous, passive, frame-based
>	- **Interaction** → discrete, intentional, input-based

6. Now we just check if the object hit by raycast is Interactable, ==(We will learn Interfaces in future)== and has a `clearCounter` script which we get out  using `out` keyword,
7. Then set the `selectedVisual = clearCounter` else set `selectedVisual` to null,
8. if nothing is hit then also set `selectedVisual` to null.
9. Now we run this every frame and so if we get in range with selected visual it sets the `selectedVisual`.
10. And we check if `selectedVisual` is not null before calling the interact function, e.g.`if (selectedCounter != null)  selectedCounter.Interact(gameObject);`
11. Later we can generalize so `selectedVisual` can be any type of interactable object.
12. now there are many approaches for enabling selected visual object but to separate visual from logic we will use an event,
13. when `selectedVisual` is not null we fire the event and for every change of the `selectedVisual` we fire the event.
14. This approach is costly as every Interactable will listen to the event, we will try to solve this issue later.
15. Now lets make the event we need to fire `using System` unity package, which has `EventHandler` and make it public so other can listen to it. ex. `public event EventHandler OnSelectedCounterChanged;`
16. To extend the information which is being sent by C# events we can extend this by creating a class named same as `EventHandler` with `EventArgs` and using `system.EventArgs` ex. 
```
    public class SelectedCounterChangedEventArgs : EventArgs{
    public ClearCounter selectedCounter;
}
```
17. And on `EventHandler` we use an version that takes a ***Generic*** and we pass in our `eventArgs`.
18. ex. `public event EventHandler<SelectedCounterChangedEventArgs> OnSelectedCounterChangedWithArgs`
> We will Learn Generics Later in Detail
> it is C# one of the C# most powerful feature.
19. Now we just need to fire the event, We do this whenever we set the value of `selectedVisual` Using `?.Invoke` and Set our custom `eventArgs` to `slectedVisual` ex. 
```OnSelectedCounterChangedWithArgs?.Invoke(this, new SelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
```
20. To avoid Code duplication we can make a new function named `SetVisual(ClearCounter gameObject)` and call this whenever we need to set `selectedVisual`.
21. Now we use a Singleton pattern to define our player instance property so for the entire game we just have single player Instance by using `static` Keyword and getter and setter.
22. And now we set our Instance to `this.player` in `Awake` also as this is a singleton we first check if there is not more then one instance of player if there is then we just send a `Debug.LogError` to avoid crash.
23. Now the `slectedVisual` just need to listen to this event an change status of their Visual.