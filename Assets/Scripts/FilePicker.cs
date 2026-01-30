using SFB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FilePicker : MonoBehaviour
{
    public ExcelReaderExample excelReaderExample;
    private string fileType;
    private string selPath;
    [SerializeField] private TMP_Text ifResult;
    public PanelHellp panelHellp;
    public TMP_Text debug_text;
    void Start()
    {
#if !UNITY_WEBGL
        fileType = NativeFilePicker.ConvertExtensionToFileType("xlsx");
        Debug.Log("pdf's MIME/UTI is: " + fileType);
#endif
    }

    public void OnBtnOpenFileClick()
    {
#if UNITY_WEBGL

        try
        {
            // Open file with filter
            var extensions = new[] {
             new ExtensionFilter("xlsx Files", "xlsx")};

            selPath = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true)[0];
        }
        catch (Exception ex)
        {
            debug_text.text = ex.Message;          // Short error message
            Debug.LogError(ex);               // Full info in Unity Console
        }


#else
        NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
                Debug.Log("Operation cancelled");
            else
            {
                excelReaderExample.LoadAndDisplayTopPlayers(path);
                selPath = path;
                Debug.Log("Picked file: " + path);
            }
               
        }, new string[] { fileType });
#endif
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
