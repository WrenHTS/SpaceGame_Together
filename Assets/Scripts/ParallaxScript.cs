using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Tooltip("Use positive values (0.1 to 0.9) for background. Use negative values (-0.1 to -0.8) for foreground.")]
    [SerializeField] private float parallaxFactor = -0.3f;

    private Transform camTransform;
    private Vector3 previousCamPos;

    private void Start()
    {
        camTransform = Camera.main.transform;
        previousCamPos = camTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = camTransform.position - previousCamPos;
        
        // Move the layer based on camera movement and the parallax factor
        transform.position += new Vector3(deltaMovement.x * parallaxFactor, deltaMovement.y * parallaxFactor, 0f);
        
        previousCamPos = camTransform.position;
    }
}
