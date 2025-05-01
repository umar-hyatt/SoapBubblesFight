using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField] public bool IsWalking = false;
    [SerializeField] private float ForwardSpeed = 1.0f;
    [SerializeField] private float StrafeSpeed = 1.0f;
    [SerializeField] private float ClampX = 3.0f;
    [SerializeField] private float RotationSpeed = 2.5f;

    private InputControls Inputs = null;
    private Transform Self = null;
    public Animator PlayerAnim;
    private Rigidbody RB = null;
    private Collider Col = null;
    private float InputX = 0.0f;
    private Vector3 MovePos = Vector3.zero;
    private Quaternion targetRotation;

    public BubbleController handBubble;

    private void Start() => Initialize();

    private void Initialize()
    {
        Self = transform;
        RB = GetComponent<Rigidbody>();
        Col = GetComponent<Collider>();
        Inputs = GetComponentInChildren<InputControls>();
    }

  private void Update()
{
    if (UIManager.instance.gameState != GameState.GamePlay)
        return;

    // Get horizontal input from input component
    InputX = Inputs.Horizontal;

    // Detect walking
    IsWalking = Input.GetMouseButton(0);
    PlayerAnim.SetBool("isRunning", IsWalking);
}

private void FixedUpdate()
{
    if (!IsWalking || UIManager.instance.gameState != GameState.GamePlay)
    {
        RB.linearVelocity = Vector3.zero;
        return;
    }

    // Calculate target movement
    float moveX = InputX * StrafeSpeed;
    float moveZ = ForwardSpeed;

    Vector3 movement = new Vector3(moveX, 0, moveZ) * Time.fixedDeltaTime;

    // Apply movement
    Vector3 targetPos = transform.position + movement;
    targetPos.x = Mathf.Clamp(targetPos.x, -ClampX, ClampX);

    Vector3 velocity = (targetPos - transform.position) / Time.fixedDeltaTime;
    RB.linearVelocity = new Vector3(velocity.x, RB.linearVelocity.y, velocity.z);

    PlayerRotation();
}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameManager.instance.LevelComplete();
            UIManager.instance.gameState = GameState.LevelComplete;
            IsWalking = false;
            PlayerAnim.SetBool("isRunning", false);
        }
    }

    private void PlayerRotation()
    {
        float currentRotation = Mathf.Atan(InputX / 1) * Mathf.Rad2Deg;
        currentRotation = Mathf.Clamp(currentRotation, -30, 30);

        targetRotation = Quaternion.Euler(Vector3.up * currentRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
    }
}
