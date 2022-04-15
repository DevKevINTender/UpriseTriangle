using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveDoTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       Invoke("test",2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void test()
    {
        transform.DOScale(new Vector3(2, 2, 2), 1f).SetEase(Ease.InOutBack);
    }
}
