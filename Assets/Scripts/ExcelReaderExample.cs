using UnityEngine;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Text;
using System.Linq;

public class ExcelReaderExample : MonoBehaviour
{
    private class Player
    {
        public string name;
        public float goalPercent;
    }
    [SerializeField] private TMP_Dropdown dropdownPercentage;
    [SerializeField] private TMP_Text textRes;
    [SerializeField] private TMP_InputField iFBestPlayers;
    [SerializeField] private TMP_InputField iFBadPlayers;
    [SerializeField] private TMP_InputField iFBestPlayersDiscription;
    [SerializeField] private TMP_InputField iFBadPlayersDiscription;
    [SerializeField] private Toggle toggleBestPlayers;
    [SerializeField] private Toggle toggleBadPlayers;
    
    public void ReadColumnNames(string path)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
            var config = new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true // ⚠️ Используем первую строку как заголовки колонок
                }
            };

            var dataSet = reader.AsDataSet(config);

            DataTable table = dataSet.Tables[0]; // Первая таблица (Sheet1)

            Debug.Log("Имена колонок:");

            foreach (DataColumn column in table.Columns)
            {
                Debug.Log(column.ColumnName);
            }
        }
    }
    public void LoadAndDisplayTopPlayers(string path)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
            var config = new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true // Используем первую строку как заголовки
                }
            };

            var dataSet = reader.AsDataSet(config);
            var table = dataSet.Tables[0];
            textRes.text = "";
            
            // Индексы нужных колонок
            int nameCol = table.Columns.IndexOf("Name");
            int percentCol = table.Columns.IndexOf("Goal Percentage (Hunt)");

            if (nameCol == -1 || percentCol == -1)
            {
                Debug.LogError("Не найдены нужные колонки: 'Name' или 'Goal Percentage'");
                return;
            }

            // Парсим игроков
            var players = new List<Player>();

            foreach (DataRow row in table.Rows)
            {
                string name = row[nameCol].ToString();
                string percentRaw = row[percentCol].ToString().Trim();

                float percent = 0;

                if (percentRaw.Contains("%"))
                {
                    percentRaw = percentRaw.Replace("%", "").Trim();
                    float.TryParse(percentRaw, out percent);
                }
                else
                {
                    float.TryParse(percentRaw, out percent);
                    percent *= 100f;
                }

                players.Add(new Player { name = name, goalPercent = percent });
            }

            // Сортируем
            int top = int.Parse(iFBestPlayers.text);
            int worst = int.Parse(iFBadPlayers.text);
            var top5 = players.OrderByDescending(p => p.goalPercent).Take(top).ToList();
            var worst10 = players.OrderBy(p => p.goalPercent).Take(worst).ToList();

            if (toggleBestPlayers.isOn)
            {
                textRes.text += iFBestPlayersDiscription.text + '\n' + '\n';
                Debug.Log("🔝 Лучшие игроки по Goal Percentage:");
                foreach (var p in top5)
                {
                    string res = $"{p.name} - {Mathf.FloorToInt(p.goalPercent)}%";
                    Debug.Log(res);
                    textRes.text += res + '\n';
                }
              
            }
            if (toggleBadPlayers.isOn)
            {
                if (toggleBestPlayers.isOn)
                    textRes.text += '\n';
                textRes.text += iFBadPlayersDiscription.text + '\n' + '\n';
                Debug.Log("🔻 Худшие игроки по Goal Percentage:");
                foreach (var p in worst10)
                {
                    string res = $"{p.name} - {Mathf.FloorToInt(p.goalPercent)}%";
                    Debug.Log(res);
                    textRes.text += res + '\n';
                }
            }
           
             
        }
    }
}
