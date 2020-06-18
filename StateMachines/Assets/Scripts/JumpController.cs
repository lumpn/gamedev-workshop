using UnityEngine;

public sealed class JumpController : MonoBehaviour
{
    private static readonly int onGroundId = Animator.StringToHash("OnGround");
    private static readonly int jumpButtonId = Animator.StringToHash("JumpButton");

    private static readonly string[] groundLayers = { "Terrain" };
    private const QueryTriggerInteraction groundTriggerInteraction = QueryTriggerInteraction.Ignore;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider capsule;

    private bool onGround;
    private int groundLayerMask;

    void Start()
    {
        groundLayerMask = LayerMask.GetMask(groundLayers);
    }

    void Update()
    {
        animator.SetBool(onGroundId, onGround);
        animator.SetBool(jumpButtonId, Input.GetButton("Jump"));
    }

    void FixedUpdate()
    {
        RaycastHit hitInfo;
        var center = rb.position + capsule.transform.localPosition;
        var radius = capsule.radius;
        var direction = Vector3.down;
        var distance = capsule.height / 2 + 0.05f;
        onGround = Physics.SphereCast(center, radius, direction, out hitInfo, distance, groundLayerMask, groundTriggerInteraction);
    }
}
