using UnityEngine;
using UnityEngine.Events;

public sealed class MessageReceiver : MonoBehaviour, IMessageReceiver
{
    [System.Serializable]
    public struct MessageHandler
    {
        public Message message;
        public UnityEvent @event;
    }

    [SerializeField] private MessageHandler[] handlers;

    void OnEnable()
    {
        foreach (var handler in handlers)
        {
            handler.message.Register(this);
        }
    }

    void OnDisable()
    {
        foreach (var handler in handlers)
        {
            handler.message.Deregister(this);
        }
    }

    public void OnMessage(Message message)
    {
        foreach (var handler in handlers)
        {
            if (handler.message == message)
            {
                handler.@event.Invoke();
            }
        }
    }
}
