using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed; 

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
