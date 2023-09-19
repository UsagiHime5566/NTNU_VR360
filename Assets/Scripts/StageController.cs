using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageController : HimeLib.SingletonMono<StageController>
{
    [Header("System Params")]
    public AudioSource sceneBGM;
    public CanvasGroup toFadeGroup;
    public float fadeTime = 0.7f;
    public float LoadingBuffTime = 1;

    [Header("Auto Mode")]
    public bool AutoPlay;
    public bool PresantationMode = false;
    public float AutoPlayDelay = 30;

    [Header("Scene Params")]
    public string Scene_0;
    public string Scene_1;
    public string Scene_2;
    public string Scene_3;
    public List<string> stage_names;
    public List<float> stage_times;

    [Header("Timer List")]
    public List<TimePack> openList;
    bool isStagePlay = false;

    public bool isServer;
    public Toggle TG_IsServer;

    void Start()
    {
        TG_IsServer.onValueChanged.AddListener(x => {
            isServer = x;
            SystemConfig.Instance.SaveData("IsServer", x);
        });
        TG_IsServer.isOn = SystemConfig.Instance.GetData<bool>("IsServer");

        if(!AutoPlay)
            return;
            
        StartCoroutine(EnterScene_0());
    }

    IEnumerator EnterScene_0(){
        isStagePlay = true;
        yield return new WaitForSeconds(3);
        SystemManager.instance.SynchoCmd("act0");
        yield return SceneManager.LoadSceneAsync(Scene_0);
        yield return null;
        isStagePlay = false;

        Debug.Log($"Wait {AutoPlayDelay} second for play...");
        yield return new WaitForSeconds(AutoPlayDelay);

        if(!isServer)
            yield break;

        StartCoroutine(InfinityLoopPlay());
        Debug.Log("Ready to Play Stages");

        // if(!PresantationMode){
        //     StartCoroutine(TimeTick());
        //     Debug.Log("Ready to Play Stages");
        // }
    }

    IEnumerator TimeTick(){
        while (true)
        {
            System.DateTime date = System.DateTime.Now;
            int hour = date.Hour;
            int minute = date.Minute;
            System.DayOfWeek dayType = date.DayOfWeek;
            int month = date.Month;
            int day = date.Day;

            foreach (var pack in openList)
            {
                if(isStagePlay)
                    break;

                if(hour == pack.hour && minute == pack.minute){
                    Debug.Log($"Do Play at :{date}");
                    StartStagePlay();
                    break;
                }
            }
            
            //Debug.Log($"now is {day}day , {hour}/{minute}");
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator InfinityLoopPlay(){
        while(true){
            StartStagePlay();
            yield return new WaitForSeconds(1);
        }
    }

    void StartStagePlay(){
        if(isStagePlay)
            return;
        
        stage_names = new List<string>(){Scene_1, Scene_2, Scene_3};
        isStagePlay = true;
        StartCoroutine(DoStagePlay());
    }

    IEnumerator DoStagePlay(){
        int SceneIndex = 0;
        yield return null;
        // if(!PresantationMode){
        //     sceneBGM.Play();
        // }
        float playTime = Time.realtimeSinceStartup;

        for (int i = 0; i < stage_names.Count; i++)
        {
            SceneIndex = i;
            GoStage(stage_names[SceneIndex]);
            SystemManager.instance.SynchoCmd(stage_names[SceneIndex]);
            
            Debug.Log($"Play Stage ({SceneIndex}) and wait {stage_times[SceneIndex]} seconds to next.");
            yield return new WaitForSeconds(stage_times[SceneIndex]);
        }

        float endTime = Time.realtimeSinceStartup;

        Debug.Log($"Total play seceonds : {endTime - playTime}");

        //GoStage(Scene_0);
        yield return new WaitForSeconds(1);
        isStagePlay = false;
    }

    async void GoStage(string sceneName){
        await FireFadeEffect(() => {
            SceneManager.LoadScene(sceneName);
        });
    }

    async Task FireFadeEffect(System.Action doThing){
        toFadeGroup.DOFade(1, fadeTime);
        await Task.Delay(Mathf.FloorToInt(fadeTime * 1000));
        
        doThing?.Invoke();

        await Task.Delay(Mathf.FloorToInt(LoadingBuffTime * 1000));

        toFadeGroup.DOFade(0, fadeTime);
    }

    public void GoStageSyncho(string sceneName){
        GoStage(sceneName);
    }

    void Update()
    {
        
    }
}


[System.Serializable]
public class TimePack
{
    public int hour;
    public int minute;
}