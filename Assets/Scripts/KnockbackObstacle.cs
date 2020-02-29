using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackObstacle : MonoBehaviour
{
    public Vector3 knockbackVelocity;
    [SerializeField] private float knockbackDuration = 1;
    [SerializeField] private bool doesPlayerDropCollectables = true;
    [SerializeField] private int collectablesToDrop = 3;
    [SerializeField] private float dropForce = 5;

    [SerializeField] private GameObject collectablePrefab;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            // TODO could be greatly refactored.
            PlayerMovement player = collider.gameObject.GetComponent<PlayerMovement>();
            collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collider.gameObject.GetComponent<Rigidbody>().AddForce(knockbackVelocity, ForceMode.Impulse);
            if (doesPlayerDropCollectables)
            {
                player.Stun(knockbackDuration);

                int dropCount = Mathf.Min(Collectable.PlayerCollectableCount, collectablesToDrop);
                Collectable.PlayerCollectableCount -= dropCount;

                for (int i = 0; i < dropCount; i++)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            // TODO could be greatly refactored.
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            collision.rigidbody.velocity = Vector3.zero;
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
    
    private void ApplyKnockback(PlayerMovement player, Rigidbody body)
    {
        player.Stun(knockbackDuration);
        AudioManager.PlaySoundEffect(SoundEffect.KiwiHurt);
        body.AddForce(knockbackVelocity, ForceMode.Impulse);
    }
}
