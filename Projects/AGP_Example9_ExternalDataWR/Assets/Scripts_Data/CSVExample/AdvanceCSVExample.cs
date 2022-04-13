using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Jitbit.Utils;

public class AdvanceCSVExample : MonoBehaviour
{
    string currentAvailableName;

    void Awake()
    {
        WriteCsv(Application.streamingAssetsPath + "/CSV/PlayerNameAdvance.csv");

        string dataString = Read(Application.streamingAssetsPath + "/CSV/PlayerNameAdvance.csv");

        string[] stringSeparators = new string[] { "PlayerName,", ",Health" };
        string health = dataString.Split(stringSeparators, System.StringSplitOptions.None)[1];

        Debug.Log(health);
    }


    private void WriteCsv(string path)
    {
        StreamWriter stream = new StreamWriter(path);

        var myExport = new CsvExport();

        myExport.AddRow();
        myExport["PlayerName"] = "Bobby";
        myExport["Age"] = 12;
        myExport["Health"] = 1200;
        myExport["Any other comments?"] = "Good at archery";

        myExport.AddRow();
        myExport["PlayerName"] = "Abby";
        myExport["Age"] = 33;
        myExport["Health"] = 8000;
        myExport["Any other comments?"] = "She is a lancer";

        stream.WriteLine(myExport.Export());

        stream.Close();
        stream.Dispose();
    }

    private string Read(string path)
    {
        return new StreamReader(path).ReadToEnd();
    }
}
