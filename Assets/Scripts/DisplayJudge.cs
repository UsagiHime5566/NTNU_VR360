using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayJudge : MonoBehaviour
{
    [Header("ㄇ字區域")]
    public Camera originCameraMALeft;
    string s_MAfovleft = "MAfovleft";
    public Camera originCameraMA;
    string s_MAfov = "MAfov";
    public RawImage MAImg;
    string s_MAScale = "MAScale";
    public Camera originCameraMARight;
    string s_MAfovright = "MAfovright";
    public List<RawImage> MATwImg;
    string s_MATwScale = "MATwScale";

    [Header("地板")]
    public Transform ground;
    string s_groundScale = "GDScale";
    string s_groundPos = "GDPos";
    public Camera groundCamera;
    string s_groundFov = "GDFov";

    [Header("背面")]
    public Camera originCameraBK;
    string s_BKfov = "BKFov";
    public RawImage BKPos;
    string s_BKPos = "BKPos";
    
    void Start()
    {
        float mafov;
        mafov = SystemConfig.Instance.GetData<float>(s_MAfovleft, 16);
        originCameraMALeft.fieldOfView = mafov;
        mafov = SystemConfig.Instance.GetData<float>(s_MAfov, 90);
        originCameraMA.fieldOfView = mafov;
        mafov = SystemConfig.Instance.GetData<float>(s_MAfovright, 16);
        originCameraMARight.fieldOfView = mafov;

        var sc = SystemConfig.Instance.GetData<Vector3>(s_MAScale, new Vector3(1, 1, 1));
        MAImg.transform.localScale = sc;

        sc = SystemConfig.Instance.GetData<Vector3>(s_MATwScale, new Vector3(1, 0.6f, 1));
        foreach (var item in MATwImg)
        {
            item.transform.localScale = sc;
        }

        var v = SystemConfig.Instance.GetData<Vector3>(s_groundScale, new Vector3(1, 1, 1));
        ground.localScale = v;
        var p = SystemConfig.Instance.GetData<Vector3>(s_groundPos, new Vector3(0, 0, 0));
        ground.localPosition = p;
        var gv = SystemConfig.Instance.GetData<float>(s_groundFov, 156);
        groundCamera.fieldOfView = gv;

        mafov = SystemConfig.Instance.GetData<float>(s_BKfov, 60);
        originCameraBK.fieldOfView = mafov;

        var r = SystemConfig.Instance.GetData<Rect>(s_BKPos, new Rect(0, 0.52f, 1, 0.48f));
        BKPos.uvRect = r;


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            originCameraMALeft.fieldOfView -= 0.1f;
            SystemConfig.Instance.SaveData(s_MAfovleft, originCameraMALeft.fieldOfView);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            originCameraMALeft.fieldOfView += 0.1f;
            SystemConfig.Instance.SaveData(s_MAfovleft, originCameraMALeft.fieldOfView);
        }
        if(Input.GetKeyDown(KeyCode.E)){
            originCameraMA.fieldOfView -= 0.1f;
            SystemConfig.Instance.SaveData(s_MAfov, originCameraMA.fieldOfView);
        }
        if(Input.GetKeyDown(KeyCode.R)){
            originCameraMA.fieldOfView += 0.1f;
            SystemConfig.Instance.SaveData(s_MAfov, originCameraMA.fieldOfView);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            MAImg.transform.localScale = MAImg.transform.localScale + new Vector3(-0.01f, 0, 0);
            SystemConfig.Instance.SaveData(s_MAScale, MAImg.transform.localScale);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            MAImg.transform.localScale = MAImg.transform.localScale + new Vector3(+0.01f, 0, 0);
            SystemConfig.Instance.SaveData(s_MAScale, MAImg.transform.localScale);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            foreach (var item in MATwImg)
            {
                item.transform.localScale = item.transform.localScale + new Vector3(0, -0.01f, 0);
                SystemConfig.Instance.SaveData(s_MATwScale, item.transform.localScale);
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            foreach (var item in MATwImg)
            {
                item.transform.localScale = item.transform.localScale + new Vector3(0, +0.01f, 0);
                SystemConfig.Instance.SaveData(s_MATwScale, item.transform.localScale);
            }
        }


        if(Input.GetKeyDown(KeyCode.T)){
            originCameraMARight.fieldOfView -= 0.1f;
            SystemConfig.Instance.SaveData(s_MAfovright, originCameraMARight.fieldOfView);
        }
        if(Input.GetKeyDown(KeyCode.Y)){
            originCameraMARight.fieldOfView += 0.1f;
            SystemConfig.Instance.SaveData(s_MAfovright, originCameraMARight.fieldOfView);
        }

        if(Input.GetKeyDown(KeyCode.A)){
            ground.localScale = ground.localScale + new Vector3(0, -0.01f, 0);
            SystemConfig.Instance.SaveData(s_groundScale, ground.localScale);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            ground.localScale = ground.localScale + new Vector3(0, 0.01f, 0);
            SystemConfig.Instance.SaveData(s_groundScale, ground.localScale);
        }
        if(Input.GetKeyDown(KeyCode.G)){
            groundCamera.fieldOfView -= 0.1f;
            SystemConfig.Instance.SaveData(s_groundFov, groundCamera.fieldOfView);
        }
        if(Input.GetKeyDown(KeyCode.H)){
            groundCamera.fieldOfView += 0.1f;
            SystemConfig.Instance.SaveData(s_groundFov, groundCamera.fieldOfView);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            ground.localPosition = ground.localPosition - new Vector3(0, 5, 0);
            SystemConfig.Instance.SaveData(s_groundPos, ground.localPosition);
        }
        if(Input.GetKeyDown(KeyCode.F)){
            ground.localPosition = ground.localPosition + new Vector3(0, 5, 0);
            SystemConfig.Instance.SaveData(s_groundPos, ground.localPosition);
        }

        if(Input.GetKeyDown(KeyCode.J)){
            originCameraBK.fieldOfView -= 0.1f;
            SystemConfig.Instance.SaveData(s_BKfov, originCameraBK.fieldOfView);
        }
        if(Input.GetKeyDown(KeyCode.K)){
            originCameraBK.fieldOfView += 0.1f;
            SystemConfig.Instance.SaveData(s_BKfov, originCameraBK.fieldOfView);
        }
        if(Input.GetKeyDown(KeyCode.B)){
            var orect = BKPos.uvRect;
            BKPos.uvRect = new Rect(0, orect.y - 0.01f, 1, orect.height + 0.01f);
            SystemConfig.Instance.SaveData(s_BKPos, BKPos.uvRect);
        }

        if(Input.GetKeyDown(KeyCode.N)){
            var orect = BKPos.uvRect;
            BKPos.uvRect = new Rect(0, orect.y + 0.01f, 1, orect.height - 0.01f);
            SystemConfig.Instance.SaveData(s_BKPos, BKPos.uvRect);
        }

        if(Input.GetKeyDown(KeyCode.F12))
        {
            ScreenCapture.CaptureScreenshot("screenshot.png");
            Debug.Log("A screenshot was taken!");
        }
    }
}
