using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Toplist : MonoBehaviour
{

    [SerializeField]
    private GameObject row;
    public static RecordList records;
    bool init = false;
    // Start is called before the first frame update
    void Start()
    {
        //print(row);
        LoadFromJson();
    }

    // Update is called once per frame
    void Update()
    {
        if (!init)
        {
            init = true;
            LoadFromJson();
        }
    }

    public void LoadFromJson()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "RecordData.json");
        string data = System.IO.File.ReadAllText(filePath);

        records = JsonUtility.FromJson<RecordList>(data);
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

    public void WriteToJson()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "RecordData.json");
        string json = JsonUtility.ToJson(records);
        System.IO.File.WriteAllText(filePath, json);
    }
}
