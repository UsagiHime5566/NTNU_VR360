using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieSyncho : MonoBehaviour
{
    public List<VideoPlayer> vp;
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
        foreach (var item in vp)
        {
            item.Play();
        }
    }
    void AddStopCallback(){
        foreach (var item in vp)
        {
            item.Stop();
            item.time = 0;
        }
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(prepareTime);

        SystemManager.instance.InvokeSynchoPlay();
        SystemManager.instance.SynchoCmd("play");
    }
}
