using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using TMPro;
using System.Runtime.InteropServices;

[RequireComponent(typeof(Button))]
public class OpenFileXls : MonoBehaviour, IPointerDownHandler
{
    public ExcelReaderExample excelReaderExample;
    public RawImage output;
    public TMP_Text tMP_Text;
#if UNITY_WEBGL 
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload",".xlsx", false);
    }

    // Called from browser
    public void OnFileUpload(string url) {
        StartCoroutine(OutputRoutine(url));
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "xlsx", false);
        if (paths.Length > 0)
        {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
#endif

    private IEnumerator OutputRoutine(string url)
    {
        var loader = new WWW(url);
        yield return loader;
        // output.texture = loader.texture;
        tMP_Text.text = loader.bytes.Length.ToString();
        excelReaderExample.LoadWWWAndDisplayTopPlayers(loader.bytes);
    }
}