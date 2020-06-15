using UnityEngine;

public sealed class SendMessageState : StateMachineBehaviour
{
    [SerializeField] private string onEnter;
    [SerializeField] private string onExit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!string.IsNullOrEmpty(onEnter))
        {
            animator.SendMessage(onEnter, SendMessageOptions.RequireReceiver);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!string.IsNullOrEmpty(onExit))
        {
            animator.SendMessage(onExit, SendMessageOptions.RequireReceiver);
        }
    }
}
