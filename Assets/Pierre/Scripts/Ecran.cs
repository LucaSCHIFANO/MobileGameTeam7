using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ecran : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            wait();
        }
    }

    private void wait()
    {
        Destroy(gameObject);
    }
}
