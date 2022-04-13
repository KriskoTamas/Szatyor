using System;
using System.Collections.Generic;

[Serializable]
public class RecordList
{
    public List<Record> elements = new List<Record>();

    [Serializable]
    public class Record
    {
        public string playerName;
        public int highScore;
    }
}