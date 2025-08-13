using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Component))]
public class AutoSaveUIElement : MonoBehaviour
{
    [Tooltip("Если не задан, будет использован уникальный GUID")]
    public string customKey = "";

    private string prefsKey;

    private TMP_InputField inputField;
    private TMP_Text tmpText;
    private Toggle toggle;

    void Awake()
    {
        prefsKey = string.IsNullOrEmpty(customKey) ? GenerateDefaultKey() : customKey;

        inputField = GetComponent<TMP_InputField>();
        tmpText = GetComponent<TMP_Text>();
        toggle = GetComponent<Toggle>();

        if (inputField != null)
        {
            inputField.onValueChanged.AddListener(SaveInputField);
        }

        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(SaveToggle);
        }
    }

    void Start()
    {
        if (inputField != null)
        {
            inputField.text = PlayerPrefs.GetString(prefsKey, inputField.text);
        }
        else if (tmpText != null)
        {
            tmpText.text = PlayerPrefs.GetString(prefsKey, tmpText.text);
        }
        else if (toggle != null)
        {
            toggle.isOn = PlayerPrefs.GetInt(prefsKey, toggle.isOn ? 1 : 0) == 1;
        }
    }

    private void SaveInputField(string value)
    {
        PlayerPrefs.SetString(prefsKey, value);
        PlayerPrefs.Save();
    }

    private void SaveToggle(bool value)
    {
        PlayerPrefs.SetInt(prefsKey, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Генерирует уникальный ключ на основе пути объекта в сцене
    /// </summary>
    private string GenerateDefaultKey()
    {
        return gameObject.scene.name + "_" + gameObject.name + "_" + GetType().Name;
    }
}
