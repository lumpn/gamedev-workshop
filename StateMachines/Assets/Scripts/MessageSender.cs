using UnityEngine;

public sealed class MessageSender : StateMachineBehaviour
{
    [SerializeField] private Message onEnter;
    [SerializeField] private Message onExit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!onEnter) return;
        onEnter.Send(animator.gameObject);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!onExit) return;
        onExit.Send(animator.gameObject);
    }
}
