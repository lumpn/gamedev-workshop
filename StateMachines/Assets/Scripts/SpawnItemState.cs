using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SpawnItemState : StateMachineBehaviour
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public Transform attachPoint;
}
