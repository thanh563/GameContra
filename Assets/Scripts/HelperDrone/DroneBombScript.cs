using UnityEngine;

using System.Collections;

public class DroneBombScript : MonoBehaviour
{
    private Animator animator; // Assign in Inspector
    public float destroyDelay = 0.5f; // Adjust to match explosion animation length

    private bool hasExploded = false; // Prevent multiple triggers

    private void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground")) && !hasExploded)
        {
            hasExploded = true;
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        if (animator != null)
        {
            animator.SetTrigger("Explode");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Wait for animation to finish
        }
        Destroy(gameObject);
    }
}

