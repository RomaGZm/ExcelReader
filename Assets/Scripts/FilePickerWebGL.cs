
using System;
using System.IO;
using TMPro;
using UnityEngine;

public class FilePickerWebGL : MonoBehaviour
{
    public ExcelReaderExample excelReaderExample;
    private string selPath;
    [SerializeField] private TMP_Text ifResult;
    public PanelHellp panelHellp;
    public TMP_Text debug_text;
    void Start()
    {




    }

    public void OnBtnOpenFileClick()
    {

        try
        {
         //   WebGLFileUploadManager.PopupDialog(null, "Select xlsx file (.xlsx)");
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
