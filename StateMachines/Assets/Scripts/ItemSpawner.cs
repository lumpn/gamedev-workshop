using UnityEngine;

public sealed class ItemSpawner : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public Transform attachPoint;

    public void Spawn()
    {
        Spawn(prefab, attachPoint);
    }

    public void Spawn(GameObject prefab)
    {
        Spawn(prefab, attachPoint);
    }

    public void SpawnAt(Transform attachPoint)
    {
        Spawn(prefab, attachPoint);
    }

    public void Spawn(GameObject prefab, Transform attachPoint)
    {
        Object.Instantiate<GameObject>(prefab, attachPoint);
    }
}
