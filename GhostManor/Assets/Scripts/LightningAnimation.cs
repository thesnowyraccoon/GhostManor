using System.Collections;
using UnityEngine;

public class LightningAnimation : MonoBehaviour
{
    private Animator animator;

    public float minDelay = 2f;
    public float maxDelay = 5f;

    public float activeDuration = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(RandomTrigger());
    }

    private IEnumerator RandomTrigger()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            animator.SetBool("isActive", true);

            if (activeDuration > 0)
            {
                yield return new WaitForSeconds(activeDuration);

                animator.SetBool("isActive", false);
            }
        }
    }
}
