using UnityEngine;
using System.Collections;
public class ThrowingBomb : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float throwCooldown = 1.5f;

    private bool canThrow = true;
    public Animator animator;

    public void Throw()
    {
        if (!canThrow || bombPrefab == null || throwPoint == null)
        {
            Debug.LogWarning("ThrowingBomb: Missing required references.");
            return;
        }

        // Trigger the throw animation
        if (animator != null)
        {
            animator.SetTrigger("IsLaunching_Bomb");
        }

        // Delay the actual throw to match animation
        StartCoroutine(DelayedThrow(0.5f)); // Adjust delay to match throw moment in animation
        canThrow = false;
        StartCoroutine(StopShootingAnimation("IsLaunching_Bomb"));
        Invoke(nameof(ResetThrow), throwCooldown);
    }
    IEnumerator StopShootingAnimation(string animationBool)
    {
        yield return new WaitForSecondsRealtime(0.2f);
        animator.SetBool(animationBool, false);
    }

    private IEnumerator DelayedThrow(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject bombInstance = Instantiate(bombPrefab, throwPoint.position, Quaternion.identity);
        bombInstance.transform.SetParent(null);

        Rigidbody rb = bombInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            Vector3 throwDir = throwPoint.forward;
            rb.AddForce(throwDir * throwForce, ForceMode.Impulse);

            Debug.Log("Bomb thrown!");
        }
        else
        {
            Debug.LogWarning("Bomb prefab has no Rigidbody!");
        }
    }

    public void ResetThrow()
    {
        canThrow = true;
    }
}
