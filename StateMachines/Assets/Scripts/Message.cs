using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "Data/Message")]
public sealed class Message : ScriptableObject
{
    private static readonly List<IMessageReceiver> emptyList = new List<IMessageReceiver>();

    private readonly Dictionary<GameObject, List<IMessageReceiver>> receivers = new Dictionary<GameObject, List<IMessageReceiver>>();

    public void Send(GameObject gameObject)
    {
        var list = receivers.GetOrFallback(gameObject, emptyList);
        foreach (var receiver in list)
        {
            receiver.OnMessage(this);
        }
    }

    public void Register(IMessageReceiver receiver)
    {
        var list = receivers.GetOrAddNew(receiver.gameObject);
        list.Add(receiver);
    }

    public void Deregister(IMessageReceiver receiver)
    {
        var list = receivers.GetOrFallback(receiver.gameObject, emptyList);
        list.RemoveUnordered(receiver);
    }
}
