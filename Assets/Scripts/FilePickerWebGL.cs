
using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class FilePickerWebGL : MonoBehaviour
{
    public ExcelReaderExample excelReaderExample;
    private string selPath;
    [SerializeField] private TMP_Text ifResult;
    public PanelHellp panelHellp;
    public TMP_Text debug_text;
    public byte[] fileBytes;

#if UNITY_WEBGL
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

     // Called from browser
    public void OnFileUpload(string url) {
        StartCoroutine(OutputRoutine(url));
    }



    public void OnBtnOpenFileClick()
    {
        try
        {
            UploadFile(gameObject.name, "OnFileUpload", ".xlsx", false);
           
        }
        catch (Exception ex)
        {
            debug_text.text = ex.Message;          // Short error message
            Debug.LogError(ex);               // Full info in Unity Console
        }

    }
    private IEnumerator OutputRoutine(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Download failed: " + request.error);
            yield break;
        }
        fileBytes = request.downloadHandler.data;
       // debug_text.text = loader.text;
        excelReaderExample.LoadWWWAndDisplayTopPlayers(fileBytes);
    }
    public void OnBtnUpdateClick()
    {
        
        if (fileBytes.Length > 0)
        {
            excelReaderExample.LoadWWWAndDisplayTopPlayers(fileBytes);
            Debug.Log("Файл найден!");
        }
        else
        {
            Debug.Log("Файл НЕ найден.");
        }
    }
    public void OnBtnCopyClick()
    {
        GUIUtility.systemCopyBuffer = ifResult.text;
    }
    public void OnBtnHelpClick()
    {
        panelHellp.Show();
    }
 
    public void OnBtnExitClick()
    {
        Application.Quit();
    }
#endif
}
