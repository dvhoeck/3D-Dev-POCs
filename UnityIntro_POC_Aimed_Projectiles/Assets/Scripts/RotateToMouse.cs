using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera Camera;
    public float MaximumLength;

    private GameObject objectHit;
    private Ray rayMouse;
    private Vector3 position;
    private Vector3 normal;
    private Vector3 direction;
    private Quaternion rotation;

    // Start is called before the first frame update
    private void Update()
    {
        if (Camera == null)
        {
            Debug.Log("Cam is null");
            return;
        }

        // get position of object under cursor
        Vector3 mousePos = Input.mousePosition;
        rayMouse = Camera.ScreenPointToRay(mousePos);
        position = rayMouse.GetPoint(MaximumLength);

        // try to get the normal of the point we hit if we hit an Enemy
        if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out RaycastHit hit, MaximumLength * 100) && hit.rigidbody?.gameObject?.tag == "Enemies")
        {
            Debug.Log(hit.rigidbody?.gameObject?.tag);

            RotateToMouseDirection(gameObject, hit.point);

            objectHit = hit.rigidbody.gameObject;
            position = hit.point;
            normal = hit.normal;
        }
        else
        {
            RotateToMouseDirection(gameObject, position);

            normal = position;
            objectHit = null;
        }
    }

    /// <summary>
    /// Rotate this object to a destination
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="destination"></param>
    private void RotateToMouseDirection(GameObject gameObject, Vector3 destination)
    {
        direction = destination - gameObject.transform.position;
        rotation = Quaternion.LookRotation(direction);
        gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, 1);
    }

    public Quaternion GetRotation()
    {
        return rotation;
    }

    public Vector3 GetHitObjectPosition()
    {
        return position;
    }

    public Vector3 GetHitObjectNormal()
    {
        return normal;
    }

    public GameObject GetHitObject()
    {
        return objectHit;
    }
}