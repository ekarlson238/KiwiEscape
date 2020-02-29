using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float lifeTime = 5;

    private float lifeTimer = 0;

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
