using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 knockbackVelocity;
    [SerializeField] private float knockbackDuration;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            ApplyKnockback(
                collider.gameObject.GetComponent<PlayerMovement>(),
                collider.attachedRigidbody);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            ApplyKnockback(
                collision.rigidbody.gameObject.GetComponent<PlayerMovement>(),
                collision.rigidbody);
        }
    }

    private void ApplyKnockback(PlayerMovement player, Rigidbody body)
    {
        player.Stun(knockbackDuration);
        AudioManager.PlaySoundEffect(SoundEffect.KiwiHurt);
        body.AddForce(knockbackVelocity, ForceMode.Impulse);
    }
}
