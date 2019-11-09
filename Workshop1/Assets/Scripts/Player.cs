using UnityEngine;

public sealed class Player : MonoBehaviour
{
    public static Player main { get; private set; }

    void Start()
    {
        main = this;
    }

    [Header("Setup")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;

    [Header("Health")]
    [SerializeField] public float maxHealth;
    [SerializeField] public float health;

    [Header("Mana")]
    [SerializeField] public float maxMana;
    [SerializeField] public float mana;

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
