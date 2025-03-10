using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private readonly int isIdle = Animator.StringToHash("IsIdle");
    private readonly int isAttack = Animator.StringToHash("IsAttack");
    private readonly int isUlti = Animator.StringToHash("IsUlti");
    private readonly int isDeath = Animator.StringToHash("IsDead");
    private readonly int isWin = Animator.StringToHash("IsWin");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetIdleAnimation(bool value) {
        animator.SetBool(isIdle, value);
    }

    public void SetAttackAnimation(bool value) {
        animator.SetBool(isAttack, value);
    }

    public void SetUltiAnimation(bool value) {
        animator.SetBool(isUlti, value);
    }

    public void SetDeadAnimation(bool value) {
        animator.SetBool(isDeath, value);
    }

    public void SetWinAnimation(bool value) {
        animator.SetBool(isWin, value);
    }

    public void ResetAnimation() {
        animator.SetBool(isIdle, false);
        animator.SetBool(isAttack, false);
        animator.SetBool(isUlti, false);
        animator.SetBool(isDeath, false);
        animator.SetBool(isWin, false);
    }
}