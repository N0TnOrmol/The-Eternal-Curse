using UnityEngine;

public class ThrowingBomb : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform throwPoint;
    public float throwForce = 15f;

    private bool hasThrown = false;

    public void Throw()
    {
        if (hasThrown || bombPrefab == null || throwPoint == null) return;

        GameObject thrownBomb = Instantiate(bombPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = thrownBomb.GetComponent<Rigidbody>();
        Debug.Log("works");
        if (rb)
        {
            rb.linearVelocity = throwPoint.forward * throwForce;
        }

        hasThrown = true;
    }

    public void ResetThrow()
    {
        hasThrown = false;
    }
}
