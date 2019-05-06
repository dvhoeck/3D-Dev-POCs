using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject FirePoint;
    public List<GameObject> Vfx = new List<GameObject>();
    public RotateToMouse RotateToMouse;

    [SerializeField]
    float maximumLength = 100;

    private GameObject fire01EffectToSpawn;

    private float timeToFire = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        fire01EffectToSpawn = Vfx[0];
    }

    // Update is called once per frame
    private void Update()
    {
        // fire 1
        if (Input.GetMouseButton(0) && Time.time >= timeToFire) // create a delay between shots
        {
            timeToFire = Time.time + 1 / fire01EffectToSpawn.GetComponent<ProjectileController>().FireRate;
            FireWeapon01();
        }
    }

    /// <summary>
    /// Fire weapon 1
    /// </summary>
    private void FireWeapon01()
    {
        GameObject effect;

        // get position of object under cursor
        Vector3 mousePos = Input.mousePosition;
        Ray rayMouse = Camera.main.ScreenPointToRay(mousePos);
        Vector3 position = rayMouse.GetPoint(maximumLength);

        // check if we target an enemy
        if (FirePoint != null && (Physics.Raycast(rayMouse.origin, rayMouse.direction, out RaycastHit hit, maximumLength) && hit.rigidbody?.gameObject?.tag == "Enemies"))
        {
            // create a projectile, aimed at the target. ProjectileController script will move the shot and handle collisions
            effect = Instantiate(fire01EffectToSpawn, FirePoint.transform.position, Quaternion.identity);
            if (RotateToMouse != null)
                effect.transform.localRotation = RotateToMouse.GetRotation();

            // determine when the muzzle FX ends so it can be destroyed
            float timeToLive = 4.0f;
            Destroy(effect, timeToLive);
        }
    }

}