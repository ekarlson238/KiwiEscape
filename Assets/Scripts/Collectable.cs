using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // This could be put in its own static class:
    private static int playerCollectableCount;
    public static int PlayerCollectableCount
    {
        get
        {
            return playerCollectableCount;
        }
        set
        {
            playerCollectableCount = value;
            OnCollectableCountChanged?.Invoke(value);
        }
    }
    public delegate void ValueChangedHandler(int newValue);
    public static event ValueChangedHandler OnCollectableCountChanged;


    [SerializeField] private Transform pickupRenderedChild;

    private float unTouchableTime = 2;
    private float spawnTime;
    private float spinRate = 90;

    private float pickupDuration = 0.6f;
    private float pickupStartTime;
    private float pickupArcHeight = 2;
    private Vector3 startPosition;
    private Transform targetPosition;
    private Vector3 startScale;

    //sound
    [SerializeField]
    private AudioClip collectSound;
    private AudioSource cherryAudio;

    void Start()
    {
        spawnTime = Time.time;
        cherryAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        pickupRenderedChild.eulerAngles += Vector3.up * spinRate * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && Time.time - spawnTime > unTouchableTime)
        {
            cherryAudio.PlayOneShot(collectSound, 1.0f);
            playerCollectableCount++;
            pickupRenderedChild.GetComponent<SphereCollider>().enabled = false;
            pickupStartTime = Time.time;
            startPosition = transform.position;
            startScale = transform.localScale;
            targetPosition = other.transform;
            StartCoroutine(ObtainCollectable());
        }
    }

    // Animation for when the collectable is gotten by the player.
    private IEnumerator ObtainCollectable()
    {
        while(true)
        {
            float interpolant = (Time.time - pickupStartTime) / pickupDuration;

            if (interpolant < 1)
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
                break;
            }
            yield return null;
        }
    }

    private float dissapearDuration;
    private float dissapearStartTime;
    public void MarkDissapearing(float timeToDissapear)
    {
        dissapearDuration = timeToDissapear;
        dissapearStartTime = Time.time;
        StartCoroutine(BlinkOut());
    }
    private IEnumerator BlinkOut()
    {
        while (true)
        {
            float interpolant = (Time.time - dissapearStartTime) / dissapearDuration;

            if (interpolant < 1)
            {
                // Fading function gotten from desmos
                if(Mathf.Floor(0.07f - (Mathf.Sin(50 * interpolant) * 0.05f + 0.1f * interpolant)) == 0)
                {
                    pickupRenderedChild.gameObject.SetActive(true);
                }
                else
                {
                    pickupRenderedChild.gameObject.SetActive(false);
                }
            }
            else
            {
                Destroy(gameObject);
                break;
            }
            yield return null;
        }
    }
}
