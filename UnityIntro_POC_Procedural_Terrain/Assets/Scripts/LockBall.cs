using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBall : MonoBehaviour
{
    Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(originalPosition.x, transform.position.y, originalPosition.z);
    }
}
