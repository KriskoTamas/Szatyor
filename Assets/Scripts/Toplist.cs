using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Toplist : MonoBehaviour
{

    [SerializeField]
    private GameObject row;
    bool init = false;
    // Start is called before the first frame update
    void Start()
    {
        //print(row);
    }

    // Update is called once per frame
    void Update()
    {
        if (!init)
        {
            init = true;
            for (int i = 0; i < 10; i++)
            {
                row.transform.Find("PlacementText").GetComponent<TextMeshProUGUI>().text = "#" + UIManager.toplistView.childCount;
                Instantiate(row, UIManager.toplistView);
            }
        }
    }

    public void AddRow()
    {

    }
}
