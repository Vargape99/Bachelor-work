using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : Interactive
{

    Vector3 startLocation;
    Animator anim;
    Movement playerMove;

    private void Awake()
    {
        firstParent = transform.parent;
        anim = GetComponent<Animator>();
        startLocation = transform.position;
    }

    public override void Interact(Movement other)
    {
        if (IsInRange()) {
            Vector3 otherpos = other.GetPosition(); 
            if (WillFit(otherpos)) {
                Vector3 newpos = findPosition(otherpos);
                newpos.y = other.transform.position.y;
                float newangle = findAngle(otherpos);
                Quaternion newquat = Quaternion.Euler(0, newangle, 0);
                other.SetPosition(newpos);
                other.SetRotation(newquat);
                transform.parent = other.transform;
                playerMove = other;
                other.setPushing(true);
            }
        }
    }

    public override void StopInteracting()
    {
        transform.parent = firstParent;
        playerMove = null;
    }

    //check if there is enough space for the player to fit in
    public bool WillFit(Vector3 position) {
        RaycastHit[] hiter = Physics.BoxCastAll(transform.position + new Vector3(0, 0.8f, 0), new Vector3(0.15f, 0.7f, 0.15f), findDirection(position), transform.rotation, 1f, mask);
        bool willFit = true;
        foreach (RaycastHit obj in hiter)
        {
            if (!(obj.transform.IsChildOf(transform) || obj.transform.tag == "Player" || obj.transform.gameObject == transform.gameObject))
            {
                willFit = false;
            }
        }

        return willFit;
    }

    public void StartResetAnimation()
    {
        anim.SetBool("Reset", true);
    }

    public void EndResetAnimation()
    {
        transform.position = startLocation;
        transform.parent = firstParent;
        if (playerMove != null)
        {
            playerMove.setPushing(false);
        }
        anim.SetBool("Reset", false);
    }

    //find where the player should be snapped to
    public Vector3 findPosition(Vector3 position)
    {
        Vector3 trans = transform.InverseTransformPoint(position);
        if (Mathf.Abs(trans.x) > Mathf.Abs(trans.z))
        {
            if (trans.x > 0)
            {
                return transform.position + transform.right * distanceModifier;
            }
            else
            {
                return transform.position - transform.right * distanceModifier;
            }
        }
        else
        {
            if (trans.z > 0)
            {
                return transform.position + transform.forward * distanceModifier;
            }
            else
            {
                return transform.position - transform.forward * distanceModifier;
            }

        }
    }
    // find which direction is the player interacting from
    public Vector3 findDirection(Vector3 position)
    {
        Vector3 trans = transform.InverseTransformPoint(position);
        if (Mathf.Abs(trans.x) > Mathf.Abs(trans.z))
        {
            if (trans.x > 0)
            {
                return transform.right;
            }
            else
            {
                return -transform.right;
            }
        }
        else
        {
            if (trans.z > 0)
            {
                return transform.forward;
            }
            else
            {
                return -transform.forward;
            }

        }
    }
    //finds under what angle the player is interacting from
    public float findAngle(Vector3 position)
    {
        Vector3 trans = transform.InverseTransformPoint(position);
        if (Mathf.Abs(trans.x) > Mathf.Abs(trans.z))
        {
            if (trans.x > 0)
            {
                return transform.rotation.eulerAngles.y - 90;
            }
            else
            {
                return transform.rotation.eulerAngles.y + 90;
            }
        }
        else
        {
            if (trans.z > 0)
            {
                return transform.rotation.eulerAngles.y - 180;
            }
            else
            {
                return transform.rotation.eulerAngles.y;
            }

        }

    }

}
