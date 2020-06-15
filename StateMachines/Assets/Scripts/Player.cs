using UnityEngine;

public sealed class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float walkSpeed;

    private Vector3 targetVelocity;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        targetVelocity = new Vector3(horizontal * walkSpeed, 0, 0);
    }

    void FixedUpdate()
    {
        var velocity = rb.velocity;
        var deltaVelocity = targetVelocity - velocity;
        deltaVelocity.y = 0;
        rb.AddForce(deltaVelocity, ForceMode.VelocityChange);
    }
}
