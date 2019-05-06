using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public int Speed = 5;
    public float FireRate = 20;
    public float WeaponDamage = 1.0f;

    public GameObject MuzzleFlashPrefab;
    public GameObject HitPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        if (MuzzleFlashPrefab != null)
        {
            // spawn muzzle flash and face it forward
            GameObject MuzzleFlash = Instantiate(MuzzleFlashPrefab, transform.position, Quaternion.identity);
            MuzzleFlash.transform.forward = gameObject.transform.forward;

            // get duration of muzzle flash particle system...
            ParticleSystem muzzleFlashParticleSystem = MuzzleFlash.GetComponent<ParticleSystem>();
            float  timeToLive = 0.0f;
            if (muzzleFlashParticleSystem != null)
                timeToLive = muzzleFlashParticleSystem.main.duration;
            else
                timeToLive = MuzzleFlash.transform.GetChild(0).GetComponent<ParticleSystem>().main.duration;

            // ...and despawn muzzle flash after duration has expired
            Destroy(MuzzleFlash, timeToLive);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider);

        if (collider.gameObject.tag == "Enemies")
        {
            // get the point of collision
            Vector3 contactPoint = collider.ClosestPointOnBounds(transform.position);
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contactPoint);

            if (HitPrefab != null)
            {
                // spawn hit effect
                GameObject hit = Instantiate(HitPrefab, contactPoint, rotation);

                // get duration of hit particle system...
                ParticleSystem hitParticleSystem = hit.GetComponent<ParticleSystem>();
                float timeToLive = 0.0f;
                if (hitParticleSystem != null)
                    timeToLive = hitParticleSystem.main.duration;
                else
                    timeToLive = hit.transform.GetChild(0).GetComponent<ParticleSystem>().main.duration;

                // ...and despawn hit effect after duration has expired
                Destroy(hit, timeToLive);
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Speed > 0)
            transform.position += transform.forward * (Speed * Time.deltaTime); // forward is relative to the fire point's rotation
    }
}