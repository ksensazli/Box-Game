using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
   public eZoneType ZoneType;
    private void OnEnable()
    {
        BoxController.OnBoxCollected += OnBoxCollected;
    }
    public float radius = 5.0F;
    public float power = 10.0F;

 
    private void OnBoxCollected(eZoneType obj)
    {
        if (ZoneType!=obj)
        {
            return;
        }
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        { 

            if (!hit.CompareTag(nameof(eTags.box)))
            {
                continue;
            }
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
    }
}
