using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class VariableSave : MonoBehaviour
{
    // public GameObject target;
    // public string compName;
    // public string variableName;
    // public string toSet;

    // public string testStr;

    
    // public void FindVariable(){
    //     var q = target.GetComponent(compName);
    //     Debug.Log(q);
        
    //     Debug.Log(q.GetType());
    //     Debug.Log(q.GetType().GetField(variableName));
    //     q.GetType().GetField(variableName).SetValue(target, toSet);
    // }

    // public Camera originCameraMALeft;

    // [ContextMenu("Set Name")]
    // public void InitValue(){
        

    //     //var val = SystemConfig.Instance.GetData<t>

    //     Type targetType = originCameraMALeft.fieldOfView.GetType();
    //     var r = typeof(SystemConfig).GetMethod("GetData").MakeGenericMethod(targetType).Invoke(SystemConfig.Instance, new object[]{"ttkey", 5.0f});
    //     Debug.Log(r.GetType());



    //     //Type valueType = typeof(int);
    //     //object val = Activator.CreateInstance(typeof(List<>).MakeGenericType(valueType));
    //     //Console.WriteLine(val.GetType() == typeof(List<int>)); // "True" - it worked!


    // }

    // void Update()
    // {
    //     foreach (var k in keys)
    //     {
    //         if(Input.GetKeyDown(k))
    //         {
    //             originCameraMALeft.fieldOfView -= 0.1f;
    //             SystemConfig.Instance.SaveData(s_MAfovleft, originCameraMALeft.fieldOfView);
    //         }
    //     }
        
    // }

    // public List<KeyCode> keys;

    // public List<SaveField> saveFields;

    // [Serializable]
    // public class SaveField
    // {
    //     public GameObject target;
    //     public Action 
    // }
}
