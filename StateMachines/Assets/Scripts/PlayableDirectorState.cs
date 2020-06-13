using UnityEngine;
using UnityEngine.Playables;

public sealed class PlayableDirectorState : StateMachineBehaviour
{
    public enum Action
    {
        None,
        Play,
        Stop,
        Pause,
        Resume,
    }

    [SerializeField] private PlayableAsset playableAsset;
    [SerializeField] private Action onEnter, onExit;

    private Playable playable;

    void OnEnable()
    {
        var graph = PlayableGraph.Create("JonasWasHere");
        var owner = new GameObject("Owner");
        playable = playableAsset.CreatePlayable(graph, owner);
    }

    void OnStateEnter()
    {
        HandleAction(onEnter);
    }

    void OnStateExit()
    {
        HandleAction(onExit);
    }

    private void HandleAction(Action action)
    {
        switch (action)
        {
            case Action.None: break;
            case Action.Play: playable.Play(); break;
            case Action.Stop: playable.Pause(); break;
            case Action.Pause: playable.Pause(); break;
            case Action.Resume: playable.Play(); break;
            default: Debug.LogErrorFormat("Unhandled action {0}", action); break;
        }
    }
}
