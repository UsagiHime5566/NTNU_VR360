using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraRotate : MonoBehaviour
{
    public float rotateSpeed = 60;
    Tweener tweener;

    void Start()
    {
        //tweener = transform.DORotate(new Vector3(0, 360, 0), rotateSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Delete)){
        //     if(tweener == null){
        //         tweener = transform.DORotate(new Vector3(0, 360, 0), rotateSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        //     } else {
        //         tweener.Kill();
        //         tweener = null;
        //         transform.localEulerAngles = new Vector3(0, 0, 0);
        //     }
        // }
    }

    public bool isRunning(){
        return tweener != null;
    }

    public void StartRotate(){
        if(tweener == null){
            tweener = transform.DORotate(new Vector3(0, 360, 0), rotateSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        }
    }
    public void ResetRotate(){
        if(tweener != null){
            tweener.Kill();
            tweener = null;
        }
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
