using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private int maxHealthPoints = 100;  // int or float?

    private int currentHealthPoints;
    private Animator animator;

    void Start()
    {
        currentHealthPoints = maxHealthPoints;
        animator = GetComponentInChildren<Animator>();

    }

    private IEnumerator Die()
    {
        int dieHash = Animator.StringToHash("DeathFromFront");

        // Play 

        animator.SetTrigger("Die");
        // Wait until animation starts
        while (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != dieHash)
        {
            Debug.Log("Waiting for " + dieHash + " current Hash " + animator.GetCurrentAnimatorStateInfo(0).shortNameHash);
            yield return null;
        }

        float counter = 0;
        float waitTime = animator.GetCurrentAnimatorStateInfo(0).length;

        //Now, Wait until the current state is done playing
        while (counter < (waitTime))
        {
            counter += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        Debug.Log("DESTROY");
        yield return null;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealthPoints <= 0)
        {
            return;
        }
        currentHealthPoints -= damage;
        if (currentHealthPoints <= 0)
        {

            StartCoroutine(Die());
        }
    }
}
