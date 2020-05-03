using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -1)
        {
            Destroy(gameObject);
            // TODO place back on top of playground instead
        }
    }
}
