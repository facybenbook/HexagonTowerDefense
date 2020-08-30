﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool isRagdoll = false;

    // The object that is the parent of all Character joints. Default: self
    public GameObject targetRagdollRoot;
    public float forceMultiplier = 50f;

    [SerializeField] SkinnedMeshRenderer[] skinnedMeshRenderers;
    Collider[] colliders;
    Rigidbody[] rigidbodies;

    void Awake()
    {
        if (!GetComponent<Animator>())
            Debug.LogWarning("There are no Animator attached to the GameObject. " +
                "Please make you you added this script to a GameObject with an Animator component");

        skinnedMeshRenderers = targetRagdollRoot.GetComponentsInChildren<SkinnedMeshRenderer>();
        colliders = targetRagdollRoot.GetComponentsInChildren<Collider>();
        rigidbodies = targetRagdollRoot.GetComponentsInChildren<Rigidbody>();

        Deactivate();
    }

    //private bool prevRagdollState = false;
    //private void Update()
    //{
    //    if (isRagdoll != prevRagdollState)
    //    {
    //        if (isRagdoll)
    //        {
    //            Activate();
    //            var mult = forceMultiplier;
    //            var force = new Vector3(Random.Range(-1f, 1f)*mult, Random.Range(1f, 10f)*mult, Random.Range(-1f, 1f));
    //            foreach(var comp in targetRagdollRoot.GetComponentsInChildren<Rigidbody>())
    //            {
    //                comp.AddForce(force);
    //            }
    //            targetRagdollRoot.GetComponentInChildren<Rigidbody>().AddForce(force*0.3f);
    //        }
    //        else
    //            Deactivate();

    //    }
    //    prevRagdollState = isRagdoll;
    //}

    public void Activate() => SetActive(true);
    public void Deactivate() => SetActive(false);

    public void SetActive(bool active)
    {
        // Make the ragdolls visible when it is blasted far away from its origin
        foreach (var smr in skinnedMeshRenderers)
            smr.updateWhenOffscreen = active;

        GetComponent<Animator>().enabled = !active;
        foreach (var collider in colliders)
        {
            collider.enabled = active;
        }
        foreach (var rb in rigidbodies)
        {
            rb.detectCollisions = active;
            rb.useGravity = active;
            rb.isKinematic = !active;
        }
        if (gameObject.GetComponent<Rigidbody>())
        {
            gameObject.GetComponent<Rigidbody>().detectCollisions = !active;
            gameObject.GetComponent<Rigidbody>().useGravity = !active;
            gameObject.GetComponent<Rigidbody>().isKinematic = active;
        }
        if (gameObject.GetComponent<Collider>())
            gameObject.GetComponent<Collider>().enabled = !active;
    }

    public void ApplyForce(Vector3 force)
    {
        Vector3 multipliedForce = force * forceMultiplier;
        foreach (var comp in targetRagdollRoot.GetComponentsInChildren<Rigidbody>())
        {
            comp.AddForce(multipliedForce);
        }
        // The hip should move a little more. Making it look more real.
        GetComponentInChildren<Rigidbody>().AddForce(force * 0.3f);
    }


}
