using UnityEngine;

public sealed class Player : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;

    [Header("Stats")]
    [SerializeField] private BoundedFloat health;
    [SerializeField] private BoundedFloat mana;

    void Update()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var deltaZ = Input.GetAxis("Vertical");
        var forward = new Vector3(deltaX, 0, deltaZ);

        if (forward.sqrMagnitude > 0.01f)
        {
            transform.localRotation = Quaternion.LookRotation(forward, Vector3.up);
        }

        controller.SimpleMove(forward * speed);
    }
}
