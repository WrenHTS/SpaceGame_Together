using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GravityAttractor2D : MonoBehaviour
{
    [Header("Gravity Settings")]
    [Tooltip("Strength of the gravitational pull.")]
    public float gravityStrength = 25f;

    [Tooltip("Maximum distance at which objects are pulled.")]
    public float pullRadius = 10f;

    [Tooltip("Minimum distance used in force calculation to avoid division-by-zero spikes.")]
    public float minDistance = 0.5f;

    [Tooltip("If true, force stays constant instead of dropping off with distance.")]
    public bool constantForce = false;

    [Tooltip("Layers affected by this gravitational pull.")]
    public LayerMask affectedLayers = ~0;

    private CircleCollider2D _pullTrigger;

    private void Awake()
    {
        _pullTrigger = GetComponent<CircleCollider2D>();
        _pullTrigger.isTrigger = true;
        _pullTrigger.radius = pullRadius;
    }

    private void OnValidate()
    {
        pullRadius = Mathf.Max(0.1f, pullRadius);
        minDistance = Mathf.Max(0.05f, minDistance);

        if (_pullTrigger != null)
        {
            _pullTrigger.isTrigger = true;
            _pullTrigger.radius = pullRadius;
        }
    }

    private void FixedUpdate()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, pullRadius, affectedLayers);

        foreach (Collider2D hit in hits)
        {
            if (hit.attachedRigidbody == null || hit.gameObject == gameObject)
                continue;

            Rigidbody2D rb = hit.attachedRigidbody;
            if (rb.bodyType != RigidbodyType2D.Dynamic)
                continue;

            Vector2 toAttractor = (Vector2)transform.position - rb.position;
            float distance = toAttractor.magnitude;

            if (distance <= 0.0001f || distance > pullRadius)
                continue;

            Vector2 direction = toAttractor / distance;
            float clampedDistance = Mathf.Max(distance, minDistance);

            float forceMagnitude = constantForce
                ? gravityStrength * rb.mass
                : (gravityStrength * rb.mass) / (clampedDistance * clampedDistance);

            rb.AddForce(direction * forceMagnitude, ForceMode2D.Force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, pullRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}