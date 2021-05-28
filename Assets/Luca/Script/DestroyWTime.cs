using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWTime : MonoBehaviour
{
    public float timeToDestroy;

    void Start()
    {
        StartCoroutine("wait");
    }

    private IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(timeToDestroy * 1.5f); 
        Destroy(gameObject);
    }
}
