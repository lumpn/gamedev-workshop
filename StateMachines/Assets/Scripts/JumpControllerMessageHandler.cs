using UnityEngine;

public sealed class JumpControllerMessageHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpVelocity;

    private bool startJump, stopJump;

    void StartJump()
    {
        startJump = true;
    }

    void StopJump()
    {
        stopJump = true;
    }

    void FixedUpdate()
    {
        if (startJump)
        {
            startJump = false;
            var downVelocity = Mathf.Min(rb.velocity.y, 0);
            var deltaVelocity = new Vector3(0, jumpVelocity - downVelocity, 0);
            rb.AddForce(deltaVelocity, ForceMode.VelocityChange);
        }
        if (stopJump)
        {
            stopJump = false;
            var upVelocity = Mathf.Max(rb.velocity.y, 0);
            var deltaVelocity = new Vector3(0, -upVelocity, 0);
            rb.AddForce(deltaVelocity, ForceMode.VelocityChange);
        }
    }
}
