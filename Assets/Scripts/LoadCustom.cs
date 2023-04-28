using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Video;

public class LoadCustom : MonoBehaviour
{
    [Header("要貼到的目標 Mesh")] public MeshRenderer targetMesh;
    [Header("要貼到的目標 RawImage")] public RawImage targetRawImage;
    [Header("檔案名稱(*.jpg , *.png)")] public string fileName = @"background";
    public VideoPlayer videoPlayer;
    public bool imidiatePlay = true;
    string fileSuffixJPG = @".jpg";
    string fileSuffixPNG = @".png";
    string fileSuffixMP4 = @".mp4";
    Texture2D targetTex;

    string useUrl;

    void Start()
    {
        string root = Application.dataPath + "/../";
        useUrl = root + fileName + fileSuffixPNG;
        if (!System.IO.File.Exists(useUrl))
            useUrl = root + fileName + fileSuffixJPG;

        if (!System.IO.File.Exists(useUrl))
            useUrl = root + fileName + fileSuffixMP4;

        if (!System.IO.File.Exists(useUrl)){
            Debug.LogError("No Background file found in : " + useUrl);
            return;
        }

        Debug.Log("Use Background file : " + useUrl);
        DoLoadBackground();

        SetupVideo(useUrl);

        //ScriptsCamera.OnBackgroundWrited += DoLoadBackground;
    }

    public void DoLoadBackground(){
        StartCoroutine(DownloadImage(useUrl, LoadImageToMesh));
    }

    IEnumerator DownloadImage(string MediaUrl, System.Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else {
            callback(((DownloadHandlerTexture)request.downloadHandler).texture);
        }
    }

    void LoadImageToMesh(Texture2D tex){
        if(targetMesh)
            targetMesh.material.mainTexture = tex;
        if(targetRawImage)
            targetRawImage.texture = tex;
    }

    void SetupVideo(string MediaUrl)
    {
        videoPlayer.url = MediaUrl;
        videoPlayer.Prepare();
        videoPlayer.loopPointReached += delegate
        {

        };
        videoPlayer.prepareCompleted += delegate
        {
            Debug.Log($"Get video size: {videoPlayer.texture.width}x{videoPlayer.texture.height}");

        };

        if (!imidiatePlay)
            return;

        videoPlayer.Play();
    }
}