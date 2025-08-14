using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class FilePicker : MonoBehaviour
{
 /*   public ExcelReaderExample excelReaderExample;
    private string fileType;
    private string selPath;
    [SerializeField] private TMP_Text ifResult;
    public PanelHellp panelHellp;
    void Start()
    {
        fileType = NativeFilePicker.ConvertExtensionToFileType("xlsx");
        Debug.Log("pdf's MIME/UTI is: " + fileType);
    }

   public void OnBtnOpenFileClick()
    {
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
    }*/
}
