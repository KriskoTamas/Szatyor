using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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