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

        ControlV360Cam.instance.UpdateView(viewPos, viewRotate);

        yield return new WaitForSeconds(prepareTime);

        SystemManager.instance.InvokeSynchoPlay();
        SystemManager.instance.SynchoCmd("play");
    }
}
