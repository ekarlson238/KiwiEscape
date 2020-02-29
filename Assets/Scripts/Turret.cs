using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int FieldOfView = 45;
    public int ViewDistance = 20;
    private Transform playerTrans;

    [SerializeField]
    private float rotateSpeed;
    
    [SerializeField]
    private float scanRotateSpeed;

    [SerializeField]
    private float fireSpeed = 3;
    private float fireTime = 0;

    [SerializeField]
    private float lostTargetTime = 2;
    private float lostTargetTimer = 0;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform cannonEndTransform;

    private void Start()
    {
        //Find player position
        playerTrans =
        GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void Rotate()
    {
        var q = Quaternion.LookRotation(playerTrans.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
    }

    //Detect perspective field of view for the AI Character
    void DetectPlayer()
    {
        RaycastHit hit;

        Ray ray = new Ray(cannonEndTransform.position, playerTrans.position - transform.position);

        if ((Vector3.Angle(ray.direction, transform.forward)) < FieldOfView && Vector3.Distance(transform.position, playerTrans.position) < ViewDistance)
        {
            Rotate();
            lostTargetTimer = Time.time + lostTargetTime;

            Shoot();
        }
        else
        {
            if (Time.time > lostTargetTimer)
            {
                Vector3 rot = transform.rotation.eulerAngles;
                rot += new Vector3(0, scanRotateSpeed * Time.deltaTime, 0);
                transform.rotation = Quaternion.Euler(rot);
            }
        }
    }

    private void Shoot()
    {
        if (Time.time > fireTime)
        {
            Instantiate(bulletPrefab, cannonEndTransform.position, this.transform.rotation);
            fireTime = Time.time + fireSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isEditor || playerTrans == null) return;
        Debug.DrawLine(cannonEndTransform.position, transform.position + transform.forward * ViewDistance, Color.red);
    }
}
