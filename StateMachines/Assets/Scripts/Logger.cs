using UnityEngine;

public sealed class Logger : MonoBehaviour
{
    public void LogMessage(string message)
    {
        Debug.Log(message, this);
    }
}
