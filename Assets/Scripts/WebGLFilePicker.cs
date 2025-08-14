using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class WebGLFilePicker : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenXLSXFile(string callbackMethod);
    public ExcelReaderExample excelReaderExample;
    private string fileType;
    private string selPath;
    [SerializeField] private TMP_Text ifResult;
    public PanelHellp panelHellp;
    // Вызываем из Unity

    void Start()
    {
      //  fileType = ConvertExtensionToFileType("xlsx");
      
    }
    public void PickFile()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OpenXLSXFile("OnFileLoaded");
#else
        Debug.LogWarning("File picker работает только в WebGL билде.");
#endif
    }
    public void OnFileLoaded(string csvData)
    {
        Debug.Log("Получен CSV:\n" + csvData);
        // Здесь можно парсить данные
        ifResult.text = csvData;
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
    public static string ConvertExtensionToFileType(string extension)
    {
        if (string.IsNullOrEmpty(extension))
            return null;

        if (extension.IndexOf('*') >= 0)
        {
            // So many users try to do this that it's now necessary to throw an exception for this particular scenario
            throw new ArgumentException("See: https://github.com/yasirkula/UnityNativeFilePicker#faq");
        }

#if !UNITY_EDITOR && UNITY_ANDROID
		return AJC.CallStatic<string>( "GetMimeTypeFromExtension", extension.ToLowerInvariant() );
#elif !UNITY_EDITOR && UNITY_IOS
		return _NativeFilePicker_ConvertExtensionToUTI( extension.ToLowerInvariant() );
#else
        return extension;
#endif
    }
}
