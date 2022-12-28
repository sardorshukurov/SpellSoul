using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // simple logic to follow player
    public Transform followTarget;
    Vector3 offset = new Vector3(0, 8, -3);
    
    void Update()
    {
        transform.position = followTarget.position + offset;
    }
}
