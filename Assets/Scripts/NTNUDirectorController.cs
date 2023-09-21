using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NTNUDirectorController : MonoBehaviour
{
    public PlayableDirector mainDirector;
    public AudioSource bgm;
    public string objToFind = "360ViewPosition";
    public Vector3 viewPos;
    public Vector3 viewRotate;
    public float prepareTime = 3;

    public bool RequireV360Data = false;

    private void OnEnable() {
        SystemManager.instance.OnSynchroPlay += AddPlayCallback;

        SystemManager.instance.OnSynchroStop += AddStopCallback;
    }
    private void OnDisable() {
        SystemManager.instance.OnSynchroPlay -= AddPlayCallback;

        SystemManager.instance.OnSynchroStop -= AddStopCallback;
    }

    void AddPlayCallback(){
        if(mainDirector != null)
            mainDirector.Play();
        if(bgm != null)
            bgm.Play();
    }
    void AddStopCallback(){
        if(mainDirector != null)
        {
            mainDirector.Stop();
            mainDirector.time = 0;
        }
        if(bgm != null)
        {
            bgm.Stop();
            bgm.time = 0;
        }
    }

    IEnumerator Start()
    {
        // var cams = GameObject.Find(objToFind);

        // if(cams){
        //     cams.transform.position = viewPos;
        //     cams.transform.eulerAngles = viewRotate;
        // }
        var v_p = viewPos;
        var v_r = viewRotate;

        if(RequireV360Data){
            v_p = SystemConfig.Instance.GetData<Vector3>("v360pos", viewPos);
            v_r = SystemConfig.Instance.GetData<Vector3>("v360rot", viewRotate);
        }

        ControlV360Cam.instance.UpdateView(v_p, v_r);

        yield return new WaitForSeconds(prepareTime);

        SystemManager.instance.InvokeSynchoPlay();
        SystemManager.instance.SynchoCmd("play");
    }

    void Update(){
        if(Input.GetKey(KeyCode.LeftAlt))
        {
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                ControlV360Cam.instance.UpdateRot(RotDir.Up);
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                ControlV360Cam.instance.UpdateRot(RotDir.Down);
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                ControlV360Cam.instance.UpdateRot(RotDir.Left);
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                ControlV360Cam.instance.UpdateRot(RotDir.Right);
            }
        } else if(Input.GetKey(KeyCode.LeftControl)) 
        {
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                ControlV360Cam.instance.UpdateDepth(Depth.Forward);
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                ControlV360Cam.instance.UpdateDepth(Depth.Back);
            }
        } else {
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                ControlV360Cam.instance.UpdatePos(PosDir.Up);
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                ControlV360Cam.instance.UpdatePos(PosDir.Down);
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                ControlV360Cam.instance.UpdatePos(PosDir.Left);
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                ControlV360Cam.instance.UpdatePos(PosDir.Right);
            }
        }
    }
}
