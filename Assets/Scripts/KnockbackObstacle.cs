using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 knockbackVelocity;
    [SerializeField] private float knockbackDuration = 1;
    [SerializeField] private bool doesPlayerDropCollectables = true;
    [SerializeField] private int collectablesToDrop = 3;
    [SerializeField] private float dropForce = 5;

    [SerializeField] private GameObject collectablePrefab;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            // TODO could be greatly refactored.
            PlayerMovement player = collision.rigidbody.gameObject.GetComponent<PlayerMovement>();
            collision.rigidbody.AddForce(knockbackVelocity, ForceMode.Impulse);
            if(doesPlayerDropCollectables)
            {
                player.Stun(knockbackDuration);

                int dropCount = Mathf.Min(Collectable.PlayerCollectableCount, collectablesToDrop);
                Collectable.PlayerCollectableCount -= dropCount;

                for(int i = 0; i < dropCount; i++)
                {
                    GameObject newPickup = Instantiate(collectablePrefab);
                    newPickup.transform.position = player.transform.position;
                    newPickup.transform.eulerAngles = Vector3.up * 360 * ((float)i / dropCount);
                    newPickup.GetComponent<Rigidbody>().velocity = Vector3.up + newPickup.transform.forward * dropForce;
                    newPickup.GetComponent<Collectable>().MarkDissapearing(7);
                }

            }
        }
    }

}
