using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(timer());
    }

    private IEnumerator timer()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
