using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderState{ Closed, Open, Colliding }

public class Hitbox : MonoBehaviour
{
    public ColliderState state;
    public LayerMask layerMask;

    public Color collidingColor;
    public Color closedColor;
    public Color openColor;

    private IHitboxResponder responder = null;

    public Vector3 boxSize;
    private Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = boxSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == ColliderState.Closed) { return; }

        float rotation = transform.parent.eulerAngles.z;
        Vector3 position = gameObject.transform.position;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, boxSize, rotation, layerMask);

        foreach(Collider2D coll in colliders)
        {
            if(coll.gameObject.tag != responder.getResponderTag())
            {
                state = ColliderState.Colliding;
                responder?.collisionedWith(coll);
            }
        }
    }

    public void startCheckingCollision()
    {
        state = ColliderState.Open;
    }

    public void stopCheckingCollision()
    {
        state = ColliderState.Closed;
    }

    public void setResponder(IHitboxResponder responder)
    {
        this.responder = responder;
    }

    private void OnDrawGizmos()
    {
        switch(state)
        {
            case ColliderState.Closed:
                Gizmos.color = closedColor;
                break;
            case ColliderState.Colliding:
                Gizmos.color = collidingColor;
                break;
            case ColliderState.Open:
                Gizmos.color = openColor;
                break;
            default:
                Gizmos.color = openColor;
                break;
        }

        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube(transform.position, boxSize);

    }
}
