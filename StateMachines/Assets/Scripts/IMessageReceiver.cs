using UnityEngine;

public interface IMessageReceiver
{
    GameObject gameObject { get; }

    void OnMessage(Message message);
}
