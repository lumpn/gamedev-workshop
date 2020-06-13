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

    private PlayableGraph graph;
    private Playable playable;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HandleAction(onEnter, animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HandleAction(onExit, animator);
    }

    private void HandleAction(Action action, Animator animator)
    {
        if (action == Action.None) return;

        // ensure playable
        if (!playable.IsValid())
        {
            graph = PlayableGraph.Create();
            playable = playableAsset.CreatePlayable(graph, animator.gameObject);
        }

        switch (action)
        {
            case Action.Play: graph.Play(); break;
            case Action.Stop: graph.Stop(); break;
            case Action.Pause: playable.Pause(); break;
            case Action.Resume: playable.Play(); break;
            default: Debug.LogErrorFormat("Unhandled action {0}", action); break;
        }
    }
}
