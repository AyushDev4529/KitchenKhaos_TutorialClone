using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    Animator animator;
    bool isWalking = false;
    const string IS_WALKING = "IsWalking";
    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
        animator.SetBool(IS_WALKING, false);// Initialize the IsWalking parameter to false
    }

    private void Update()
    {
        isWalking = playerMovement.IsWalking(); // Check if the player is walking using the Player script
        animator.SetBool(IS_WALKING, isWalking); // Update the Animator parameter based on the player's movement state
    }

}
