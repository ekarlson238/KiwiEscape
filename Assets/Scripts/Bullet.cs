using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float knockbackForce = 5;

    private KnockbackObstacle knockback;

    [SerializeField]
    private float lifeTime = 5;

    private float lifeTimer = 0;

    private void Start()
    {
        knockback = GetComponent<KnockbackObstacle>();

        knockback.knockbackVelocity = new Vector3(transform.forward.x, 0.5f, transform.forward.z) * knockbackForce;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeTimer += Time.deltaTime;

        if (lifeTimer > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
