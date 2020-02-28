using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private float spinRate = 90;

    private float pickupDuration = 2;
    private float pickupStartTime;
    private float pickupArcHeight = 2;
    private Vector3 startPosition;

    void Update()
    {
        transform.eulerAngles += Vector3.up * spinRate * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            pickupStartTime = Time.time;
            startPosition = transform.position;
            StartCoroutine(ObtainCollectable());
        }
    }

    private IEnumerator ObtainCollectable()
    {
        float interpolant = (Time.time - pickupStartTime) / pickupDuration;

        if(interpolant < 1)
        {

        }
        else
        {

        }

        yield return new WaitForEndOfFrame();
    }
}
