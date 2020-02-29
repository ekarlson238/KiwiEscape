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
    private Transform targetPosition;
    private Vector3 startScale;

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
            startScale = transform.localScale;
            targetPosition = other.transform;
            StartCoroutine(ObtainCollectable());
        }
    }

    private IEnumerator ObtainCollectable()
    {
        Debug.Log("coroutine");
        float interpolant = (Time.time - pickupStartTime) / pickupDuration;

        if(interpolant < 1)
        {
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, interpolant);
            transform.position = new Vector3(
                Mathf.Lerp(startPosition.x, targetPosition.position.x, interpolant),
                targetPosition.position.y + pickupArcHeight * (1 -Mathf.Pow(2*interpolant - 1,2)),
                Mathf.Lerp(startPosition.z, targetPosition.position.z, interpolant)
                );
        }
        else
        {
            Destroy(gameObject);
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }
}
