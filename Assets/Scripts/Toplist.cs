using TMPro;
using UnityEngine;

public class Toplist : MonoBehaviour
{

    [SerializeField]
    private GameObject row;
    public static RecordList records;
    bool init = false;
    // Start is called before the first frame update
    void Start()
    {
        LoadFromJson();
    }

    // Update is called once per frame
    void Update()
    {
        if (!init)
        {
            init = true;
            //LoadFromJson();
        }
    }

    public static int getHighScore(string playerName)
    {
        if (records == null || playerName == "") return 0;
        RecordList.Record record = records.elements.Find(x => x.playerName == playerName);
        if(record == null) return 0;
        return record.highScore;
    }

    public static void LoadFromJson()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "RecordData.json");
        string data = System.IO.File.ReadAllText(filePath);

        records = JsonUtility.FromJson<RecordList>(data);
        // DisplayRecords();
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
