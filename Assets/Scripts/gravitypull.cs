using UnityEngine;

public class PointGravity2D : MonoBehaviour
{
    [Header("Gravity Settings")]
    public float pullRadius = 5f;
    public float pullForce = 20f;
    public bool useInverseSquareLaw = false;
    public LayerMask affectedLayers = ~0; // Defaults to Everything

    private Rigidbody2D selfRb;

    void Awake()
    {
        selfRb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Find all 2D colliders within the pull radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pullRadius, affectedLayers);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D targetRb = col.attachedRigidbody;

            // Skip objects without a Rigidbody2D or the attractor itself
            if (targetRb == null || targetRb == selfRb) continue;

            // Direction pointing from the target to this object
            Vector2 direction = (Vector2)transform.position - targetRb.position;
            float distance = direction.magnitude;

            // Prevent division by zero or erratic forces at the exact center
            if (distance < 0.05f) continue;

            float forceMagnitude = pullForce;

            if (useInverseSquareLaw)
            {
                // Realistic fall-off: stronger when closer, weaker when farther
                forceMagnitude = pullForce / (distance * distance);
            }

            targetRb.AddForce(direction.normalized * forceMagnitude, ForceMode2D.Force);
        }
    }

}