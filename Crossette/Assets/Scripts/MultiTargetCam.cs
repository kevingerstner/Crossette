using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetCam : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    public List<Transform> followTargets;
    private Vector3 velocity;

    public Vector3 lowerBound;
    public Vector3 upperBound;

    public Vector3 offset;
    public float padding;
    public float smoothTime = 1f;

    private void LateUpdate()
    {
        if (followTargets.Count == 0) return;

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        newPosition.x = Mathf.Clamp(newPosition.x, lowerBound.x, upperBound.x);
        newPosition.y = Mathf.Clamp(newPosition.y, lowerBound.y, upperBound.y);

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private Vector3 GetCenterPoint()
    {
        if (followTargets.Count == 1)
            return followTargets[0].position;
        Bounds bounds = new Bounds(followTargets[0].position, Vector3.zero);
        for(int i = 0; i < followTargets.Count; i++)
        {
            bounds.Encapsulate(followTargets[i].position);
        }
        bounds.Expand(padding);

        return bounds.center;
    }
}
