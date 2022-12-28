    using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    // Movement
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    bool isMovementPressed;
    bool isRunPressed;

    float rotationFactorPerFrame = 10.0f;
    float movementSpeed = 5.0f;
    float runMultiplier = 10.0f;

    int isWalkingHash;
    int isRunningHash;

    // Attack
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    // Cooldown
    private float fireRate = 0.5f;
    private float canFire = -1f;

    // Block
    public Transform blockSpawnPoint;
    public GameObject blockPrefab;
    private float blockLength = 3f;

    // Cooldown
    private float blockRate = 3f;
    private float canBlock = -1f;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // saved the name of boolean here to change it later if needed
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        // when the Move keys are pressed (in input system) this method will be called
        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;

        // when the Run key is pressed (in input system) this method will be called
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;

        // when the Attack key is pressed (in input system) this method will be called
        playerInput.CharacterControls.Attack.performed += Attack;

        // when the Block key is pressed (in input system) this method will be called
        playerInput.CharacterControls.Block.performed += Block;
    }

    // method where movement logic is stored
    void onMovementInput (InputAction.CallbackContext context)
    {
        // reads input
        currentMovementInput = context.ReadValue<Vector2>();

        // changes position when movement keys are pressed
        currentMovement.x = currentMovementInput.x * movementSpeed;
        currentMovement.z = currentMovementInput.y * movementSpeed;

        // canges position when run key is pressed
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void OnRun(InputAction.CallbackContext context)
    {
        // reads input
        isRunPressed = context.ReadValueAsButton();
    }

    void Attack(InputAction.CallbackContext context)
    {     
        // checks whether cooldown is ready
        if (Time.time > canFire)
        {
            canFire = Time.time + fireRate;

            // instantiates projectile (shoots)
            var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectileSpawnPoint.forward * projectileSpeed;
        }
    }

    void Block(InputAction.CallbackContext context)
    {
        // checks whether cooldown is ready
        if (Time.time > canBlock)
        {
            canBlock = Time.time + blockRate;

            // instantiates block prefab and destroys it after blockLength time lasts
            Destroy(Instantiate(blockPrefab, blockSpawnPoint.position, blockSpawnPoint.rotation), blockLength);
        }
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        // if player moves
        if (isMovementPressed)
        {
            // changes target rotation to the position player looks at
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);

            // transforms rotation of the player
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
        
    }
    void HandleAnimation()
    {
        // boolean variables of isWalking and isRunning
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        //checks wheter the conditions are met and sets corresponded booleans to true or false
        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if(!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void HandleGravity()
    {
        // checks wheter player is standing on the ground
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            // if he is no standing on the ground, it makes him fall
            float gravity = -9.8f;
            currentMovement.y += gravity * Time.deltaTime;
            currentRunMovement.y += gravity * Time.deltaTime;
        }
    }

    void Update()
    {
        // all needed methods are called here
        HandleGravity();
        HandleRotation();
        HandleAnimation();

        // checks wheter player presses the run button and calls corresponded function
        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        
    }

    // OnEnable and OnDisable is needed to enable new Input System
    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
