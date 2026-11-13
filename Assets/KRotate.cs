using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class KRotate : MonoBehaviour
{
  public float rotationSpeed = 0f;

    private bool shouldRotate = true;

    // called once per frame
    private void Update()
{
    if (shouldRotate)
    {
        // rotate x axis by rotationSpeed degrees per second
        transform.Rotate(UnityEngine.Vector3.right, rotationSpeed * Time.deltaTime);
    }
}

// called when this object's collider is clicked
private void OnMouseDown()
{
    // invert shouldRotate
    shouldRotate = !shouldRotate;
}
}