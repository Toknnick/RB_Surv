using System.Collections;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private int run = Animator.StringToHash("Run");
    private int attack = Animator.StringToHash("Attack");
    private int die = Animator.StringToHash("Die");
    private Animator animator;

    public void SetRun()
    {
        if(animator != null)
        animator.CrossFade(run, 0.1f);
    }

    public void SetAttack()
    {
        if (animator != null)
            animator.CrossFade(attack, 0.1f);
    }

    public void SetDie()
    {
        GetComponent<Enemy>().Off();

        if (animator != null)
            animator.CrossFade(die, 0.1f);

        StartCoroutine(Destroy());
    }

    public void StopAnim()
    {
        if (animator != null)
            animator.StopPlayback();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSecondsRealtime(2);
        Destroy(gameObject);
    }
}
