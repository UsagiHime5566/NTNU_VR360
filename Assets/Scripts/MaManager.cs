using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaManager : MonoBehaviour
{
    public List<Text> TXT_Debug_Resolution;
    public List<Camera> Proj_Cameras;
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
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace)){
            foreach (var item in TXT_Debug_Resolution)
            {
                item.enabled = !item.enabled;
            }
        }

        if(Input.GetKeyDown(KeyCode.F1)){
            Config_F1();
        }

        if(Input.GetKeyDown(KeyCode.F2)){
            Config_F2();
        }

        if(Input.GetKeyDown(KeyCode.F3)){
            Config_F3();
        }

        if(Input.GetKeyDown(KeyCode.F4)){
            Config_F4();
        }
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
}
