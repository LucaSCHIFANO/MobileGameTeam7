using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime;
    public Vector3 Offset;
    public Vector3 randomizeIntensify;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-randomizeIntensify.x, randomizeIntensify.x),
            Random.Range(-randomizeIntensify.y, randomizeIntensify.y),
            Random.Range(-randomizeIntensify.z, randomizeIntensify.z));
    }


}
