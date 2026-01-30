
using System;
using System.IO;
using TMPro;
using UnityEngine;
using WebGLFileUploader;

public class FilePickerWebGL : MonoBehaviour
{
    public ExcelReaderExample excelReaderExample;
    private string selPath;
    [SerializeField] private TMP_Text ifResult;
    public PanelHellp panelHellp;
    public TMP_Text debug_text;
    void Start()
    {

            Debug.Log("WebGLFileUploadManager.getOS: " + WebGLFileUploadManager.getOS);
            Debug.Log("WebGLFileUploadManager.isMOBILE: " + WebGLFileUploadManager.IsMOBILE);
            Debug.Log("WebGLFileUploadManager.getUserAgent: " + WebGLFileUploadManager.GetUserAgent);

            WebGLFileUploadManager.SetDebug(true);
            if ( 
#if UNITY_WEBGL && !UNITY_EDITOR
                    WebGLFileUploadManager.IsMOBILE 
#else
            Application.isMobilePlatform
#endif
            ) {
                WebGLFileUploadManager.Show (false);
                WebGLFileUploadManager.SetDescription("Select xlsx files (.xlsx)");

            }else{
                WebGLFileUploadManager.Show (true);
                WebGLFileUploadManager.SetDescription("Drop image files (Select xlsx files (.xlsx)) here");
            }

            WebGLFileUploadManager.SetAllowedFileName("\\.(xlsx)$");
            WebGLFileUploadManager.SetImageShrinkingSize(1280 ,960);
            WebGLFileUploadManager.onFileUploaded += OnFileUploaded;


    }
    private void OnFileUploaded(UploadedFileInfo[] result)
    {
        if (result.Length == 0)
        {
            Debug.Log("File upload Error!");
        }
        else
        {
            Debug.Log("File upload success! (result.Length: " + result.Length + ")");
        }

        foreach (UploadedFileInfo file in result)
        {
            if (file.isSuccess)
            {
                Debug.Log("file.filePath: " + file.filePath + " exists:" + File.Exists(file.filePath));

                selPath = file.filePath;
                excelReaderExample.LoadAndDisplayTopPlayers(selPath);

                break;
            }
        }
    }

    public void OnBtnOpenFileClick()
    {

        try
        {
            WebGLFileUploadManager.PopupDialog(null, "Select xlsx file (.xlsx)");
            excelReaderExample.LoadAndDisplayTopPlayers(selPath);
        }
        catch (Exception ex)
        {
            debug_text.text = ex.Message;          // Short error message
            Debug.LogError(ex);               // Full info in Unity Console
        }

    }

    public void OnBtnUpdateClick()
    {
        
        if (File.Exists(selPath))
        {
            excelReaderExample.LoadAndDisplayTopPlayers(selPath);
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
}
