using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SystemManager : HimeLib.SingletonMono<SystemManager>
{
    public List<InputField> INP_Ips;
    public List<Camera> Proj_Cameras;
    public List<OscJack.OscConnection> oscSetting;
    public List<OscJack.OscPropertySender> oscSender;
    public AudioListener mainListener;

    public System.Action OnSynchroPlay;
    public System.Action OnSynchroStop;

    public System.Action<int> OnSwitchScreen;

    [Header("暫定以KL鍵調整")]
    public RawImage GroundEdgeAdjust;

    public int currentDisplay;

    void Start()
    {
        int lastDisplay = SystemConfig.Instance.GetData<int>("Display", 0);
        switch (lastDisplay)
        {
            case 0:
                Config_F1();
                 break;
            case 1:
                Config_F2();
                break;
            case 2:
                Config_F3();
                break;
            case 3:
                Config_F4();
                break;
        }

        INP_Ips[0].onValueChanged.AddListener(x => {
            oscSetting[0].host = x;
            SystemConfig.Instance.SaveData("Host0", x);
        });
        INP_Ips[0].text = SystemConfig.Instance.GetData<string>("Host0", "127.0.0.1");

        INP_Ips[1].onValueChanged.AddListener(x => {
            oscSetting[1].host = x;
            SystemConfig.Instance.SaveData("Host1", x);
        });
        INP_Ips[1].text = SystemConfig.Instance.GetData<string>("Host1", "127.0.0.1");

        oscSender[0].gameObject.SetActive(true);
        oscSender[1].gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            Config_F1();
        }

        if(Input.GetKeyDown(KeyCode.X)){
            Config_F2();
        }

        if(Input.GetKeyDown(KeyCode.C)){
            Config_F3();
        }

        if(Input.GetKeyDown(KeyCode.V)){
            Config_F4();
        }
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.L)){
            var orect = GroundEdgeAdjust.uvRect;
            GroundEdgeAdjust.uvRect = new Rect(0, 0, orect.width + 0.01f, 1);
        }

        if(Input.GetKeyDown(KeyCode.K)){
            var orect = GroundEdgeAdjust.uvRect;
            GroundEdgeAdjust.uvRect = new Rect(0, 0, orect.width - 0.01f, 1);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            OnSynchroPlay?.Invoke();
            oscSender[0].Send("play");
            oscSender[1].Send("play");
        }

        if(Input.GetKeyDown(KeyCode.Backspace)){
            OnSynchroStop?.Invoke();
            oscSender[0].Send("stop");
            oscSender[1].Send("stop");
        }
        if(Input.GetKeyDown(KeyCode.U)){
            SceneManager.LoadScene("Scene Calibration");
            SynchoCmd("act0");
        }

        if(Input.GetKeyDown(KeyCode.I)){
            StageController.instance.GoStageSyncho("Act1");
            SynchoCmd("Act1");
        }

        if(Input.GetKeyDown(KeyCode.O)){
            StageController.instance.GoStageSyncho("Act2");
            SynchoCmd("Act2");
        }

        if(Input.GetKeyDown(KeyCode.P)){
            StageController.instance.GoStageSyncho("Act3");
            SynchoCmd("Act3");
        }
    }

    void Config_F1(){
        Proj_Cameras[0].targetDisplay = 1;
        Proj_Cameras[1].targetDisplay = 2;
        Proj_Cameras[2].targetDisplay = 3;
        SystemConfig.Instance.SaveData("Display", 0);
        currentDisplay = 0;
        OnSwitchScreen?.Invoke(0);
        mainListener.enabled = true;
    }
    void Config_F2(){
        Proj_Cameras[0].targetDisplay = 0;
        Proj_Cameras[1].targetDisplay = 2;
        Proj_Cameras[2].targetDisplay = 3;
        SystemConfig.Instance.SaveData("Display", 1);
        currentDisplay = 1;
        OnSwitchScreen?.Invoke(1);
        mainListener.enabled = true;
    }
    void Config_F3(){
        Proj_Cameras[0].targetDisplay = 1;
        Proj_Cameras[1].targetDisplay = 0;
        Proj_Cameras[2].targetDisplay = 3;
        SystemConfig.Instance.SaveData("Display", 2);
        currentDisplay = 2;
        OnSwitchScreen?.Invoke(2);
        mainListener.enabled = false;
    }
    void Config_F4(){
        Proj_Cameras[0].targetDisplay = 1;
        Proj_Cameras[1].targetDisplay = 2;
        Proj_Cameras[2].targetDisplay = 0;
        SystemConfig.Instance.SaveData("Display", 3);
        currentDisplay = 3;
        OnSwitchScreen?.Invoke(3);
        mainListener.enabled = false;
    }

    public void OSC_RecieveMsgCallback(string cmd){
        if(cmd == "play"){
            OnSynchroPlay?.Invoke();
        }
        if(cmd == "stop"){
            OnSynchroStop?.Invoke();
        }
        if(cmd == "act0"){
            SceneManager.LoadScene("Scene Calibration");
        }
        if(cmd == "Act1"){
            StageController.instance.GoStageSyncho("Act1");
        }
        if(cmd == "Act2"){
            StageController.instance.GoStageSyncho("Act2");
        }
        if(cmd == "Act3"){
            StageController.instance.GoStageSyncho("Act3");
        }
        if(cmd.Contains("v360:")){
            try {
                var vals = cmd.Split(":")[1];
                var val = vals.Split(",");
                float x, y, z, rx, ry, rz;
                float.TryParse(val[0], out x);
                float.TryParse(val[1], out y);
                float.TryParse(val[2], out z);
                float.TryParse(val[3], out rx);
                float.TryParse(val[4], out ry);
                float.TryParse(val[5], out rz);
                ControlV360Cam.instance.UpdateView(new Vector3(x, y, z), new Vector3(rx, ry, rz));
                ControlV360Cam.instance.SaveView();
            } catch {}
        }
    }

    public void InvokeSynchoPlay(){
        OnSynchroPlay?.Invoke();
    }

    public void InvokeSynchoStop(){
        OnSynchroStop?.Invoke();
    }

    public void SynchoCmd(string cmd){
        oscSender[0].Send(cmd);
        oscSender[1].Send(cmd);
    }
}
