using TMPro;
using UnityEngine;

public class Toplist : MonoBehaviour
{

    [SerializeField]
    private GameObject row;
    public static RecordList records;

    void Start()
    {
        LoadFromJson();
        DisplayRecords();
    }

    void Update()
    {

    }

    public static int getHighScore(string playerName)
    {
        if (records == null || playerName == "") return 0;
        RecordList.Record record = records.elements.Find(x => x.playerName == playerName);
        if (record == null) return 0;
        return record.highScore;
    }

    public static void LoadFromJson()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "RecordData.json");
        string data = System.IO.File.ReadAllText(filePath);

        records = JsonUtility.FromJson<RecordList>(data);
        records.elements.Sort((a, b) => b.highScore.CompareTo(a.highScore));
    }

    public static void WriteToJson()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "RecordData.json");
        string json = JsonUtility.ToJson(records);
        System.IO.File.WriteAllText(filePath, json);
    }

    public void DisplayRecords()
    {
        foreach (Transform child in UIManager.toplistView.transform)
        {
            if (child.gameObject.name != "Header")
                Destroy(child.gameObject);
        }

        foreach (var item in records.elements)
        {
            row.transform.Find("PlacementText").GetComponent<TextMeshProUGUI>().text = "#" + UIManager.toplistView.childCount;
            row.transform.Find("UsernameText").GetComponent<TextMeshProUGUI>().text = item.playerName;
            row.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = item.highScore.ToString();
            Instantiate(row, UIManager.toplistView);
        }
    }
}
