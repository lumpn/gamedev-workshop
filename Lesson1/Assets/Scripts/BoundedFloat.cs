using UnityEngine;

[CreateAssetMenu]
public sealed class BoundedFloat : ScriptableObject
{
    public float maxValue;
    public float value;
}
