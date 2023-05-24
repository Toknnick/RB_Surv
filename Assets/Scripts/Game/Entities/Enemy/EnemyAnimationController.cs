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
        animator.CrossFade(run, 0.1f);
    }

    public void SetAttack()
    {
        animator.CrossFade(attack, 0.1f);
    }

    public void SetDie()
    {
        GetComponent<Enemy>().Off();
        animator.CrossFade(die, 0.1f);
        StartCoroutine(Destroy());
    }

    public void StopAnim()
    {
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
