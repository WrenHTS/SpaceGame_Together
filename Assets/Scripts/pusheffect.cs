using UnityEngine;

public class PushZone : MonoBehaviour
{
    public float reducedPushStrength = 2f; // Strength inside the zone
    private float originalPushStrength;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the Player
        if (other.CompareTag("Player"))
        {
            // Access your player's movement/push script
            PlayerPush playerScript = other.GetComponent<PlayerPush>();

            if (playerScript != null)
            {
                originalPushStrength = playerScript.pushForce;
                playerScript.pushForce = reducedPushStrength;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPush playerScript = other.GetComponent<PlayerPush>();

            if (playerScript != null)
            {
                // Restore original strength when leaving
                playerScript.pushForce = originalPushStrength;
            }
        }
    }
}