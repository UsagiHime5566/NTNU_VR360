using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OscJack;
using UnityEngine.Video;

public class MaManager : MonoBehaviour
{
    public List<Text> TXT_Debug_Resolution;
    public List<InputField> INP_Ips;
    public List<Camera> Proj_Cameras;
    public List<OscJack.OscConnection> oscSetting;
    public List<OscJack.OscPropertySender> oscSender;

    public Button BTN_Inline;
    public Button BTN_Square;
    public List<GameObject> ModeInLine;
    public List<GameObject> ModeSquare;

    public RawImage GroundTarget;
    public VideoPlayer videoPlayer;

    [Header("Component")]
    public CameraRotate cameraRotate;
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

        BTN_Inline.onClick.AddListener(BeModeInLine);

        BTN_Square.onClick.AddListener(BeModeSquare);

        if(SystemConfig.Instance.GetData<string>("Mode","") == "InLine"){
            BeModeInLine();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace)){
            foreach (var item in TXT_Debug_Resolution)
            {
                item.enabled = !item.enabled;
            }
        }

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

        if(Input.GetKeyDown(KeyCode.A)){
            BeModeInLine();
        }
        if(Input.GetKeyDown(KeyCode.S)){
            BeModeSquare();
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.L)){
            var orect = GroundTarget.uvRect;
            GroundTarget.uvRect = new Rect(0, 0, orect.width + 0.01f, 1);
        }

        if(Input.GetKeyDown(KeyCode.K)){
            var orect = GroundTarget.uvRect;
            GroundTarget.uvRect = new Rect(0, 0, orect.width - 0.01f, 1);
        }

        if(Input.GetKeyDown(KeyCode.Delete)){
            if(cameraRotate.isRunning()){
                cameraRotate.ResetRotate();

                oscSender[0].Send("reset");
                oscSender[1].Send("reset");
            } else {
                cameraRotate.StartRotate();
                oscSender[0].Send("start");
                oscSender[1].Send("start");
            }
        }

        if(Input.GetKeyDown(KeyCode.P)){
            videoPlayer.Stop();
            videoPlayer.Play();
            oscSender[0].Send("play");
            oscSender[1].Send("play");
        }
    }

    void BeModeInLine(){
        foreach (var item in ModeInLine)
        {
            item.SetActive(true);
        }
        foreach (var item in ModeSquare)
        {
            item.SetActive(false);
        }
        SystemConfig.Instance.SaveData("Mode","InLine");
    }

    void BeModeSquare(){
        foreach (var item in ModeInLine)
        {
            item.SetActive(false);
        }
        foreach (var item in ModeSquare)
        {
            item.SetActive(true);
        }
        SystemConfig.Instance.SaveData("Mode","Square");
    }

    void Config_F1(){
        Proj_Cameras[0].targetDisplay = 1;
        Proj_Cameras[1].targetDisplay = 2;
        Proj_Cameras[2].targetDisplay = 3;
        SystemConfig.Instance.SaveData("Display", 0);
    }
    void Config_F2(){
        Proj_Cameras[0].targetDisplay = 0;
        Proj_Cameras[1].targetDisplay = 2;
        Proj_Cameras[2].targetDisplay = 3;
        SystemConfig.Instance.SaveData("Display", 1);
    }
    void Config_F3(){
        Proj_Cameras[0].targetDisplay = 1;
        Proj_Cameras[1].targetDisplay = 0;
        Proj_Cameras[2].targetDisplay = 3;
        SystemConfig.Instance.SaveData("Display", 2);
    }
    void Config_F4(){
        Proj_Cameras[0].targetDisplay = 1;
        Proj_Cameras[1].targetDisplay = 2;
        Proj_Cameras[2].targetDisplay = 0;
        SystemConfig.Instance.SaveData("Display", 3);
    }

    public void OSC_Order(string cmd){
        if(cmd == "start"){
            cameraRotate.StartRotate();
        }
        if(cmd == "reset"){
            cameraRotate.ResetRotate();
        }
        if(cmd == "play"){
            videoPlayer.Stop();
            videoPlayer.Play();
        }
    }
}
