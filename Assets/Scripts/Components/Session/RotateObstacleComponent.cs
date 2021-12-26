using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacleComponent : MonoBehaviour
{

    public float rotateSpeed;
    public float timeToStartRotate;
    private bool canRotate;

    // Start is called before the first frame update
    void Start()
    {
        canRotate = false;
        StartCoroutine(TimeStartCor(timeToStartRotate));
    }

    // Update is called once per frame
    void Update()
    {
        if(canRotate & transform.rotation.z < 1f) transform.Rotate(new Vector3(0, 0, rotateSpeed));
        //transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 0, rotateSpeed),1);
    }

    IEnumerator TimeStartCor(float _time)
    {
        while (_time > 0)
        {
            _time -= Time.deltaTime;
            yield return null;
        }
        canRotate = true;
    }
}
