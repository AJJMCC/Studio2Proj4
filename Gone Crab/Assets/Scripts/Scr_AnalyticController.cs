using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Scr_AnalyticController : MonoBehaviour {

    public struct ChunkData{
        public Vector3 playerPos;
        public float CurrentShellSize;
        public int NumShellsLost;

        public ChunkData(Vector3 p, float s, int n)
        {
            playerPos = p;
            CurrentShellSize = s;
            NumShellsLost = n;
        }
    };

    // Singleton Declaration
    public static Scr_AnalyticController Analytics;

    #region External References
    private Scr_PlayerCrab CrabRef; // The period between when dump data is collected.
    #endregion

    #region Serialized Fields
    [SerializeField]
    private float ChunkDataDumpInterval = 15.0f; // The period between when dump data is collected.
    [SerializeField]
    private string DumpFile = "/Dump/AnalyticsDump"; // The period between when dump data is collected.
    private string DumpFileExtension = ".csv"; // The period between when dump data is collected.
    #endregion

    #region Private Fields
    private float ChunkDataDumpTimer; // The period between when dump data is collected.
    private List<ChunkData> chunkDataList;
    private int NumberOfNewShells = 0;
    private int NumberOfNewTightShells = 0;
    private int NumberOfNewAverageShells = 0;
    private int NumberOfNewLooseShells = 0;
    private int NewShellsThisChunk = 0;
    private float MaxTimeSpentWithoutShell = 0.0f;
    #endregion

    #region Public Fields
    public float PlaySessionLength = 0.0f;
    public bool DidPlayerFinish = false;
    public int TimesTakenFallDamage = 0;
    public int TimesOutgrownShell = 0;
    public float CurrentTimeSpentWithoutShell = 0.0f;
    #endregion

    // Use this for initialization
    void Start () {
        Analytics = this;
        CrabRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Scr_PlayerCrab>();
        ChunkDataDumpTimer = ChunkDataDumpInterval;
    }
	
	// Update is called once per frame
	void Update () {
        ChunkDataUpdate();

        if (Input.GetKey(KeyCode.F1))
            WriteDataToCSV();
	}

    void ChunkDataUpdate()
    {
        ChunkDataDumpTimer -= Time.deltaTime;
        if(ChunkDataDumpTimer <= 0.0f)
        {
            ChunkDataDumpTimer = ChunkDataDumpInterval;

            // RecordInfoHere
            chunkDataList.Add(new ChunkData(CrabRef.gameObject.transform.position, CrabRef.GetShellSize(), NewShellsThisChunk));

            NewShellsThisChunk = 0;
        }
    }

    void WriteDataToCSV()
    {
        // Getting the Path
        string dataPath = Application.dataPath + DumpFile;
        int num = 0;
        while(Directory.Exists(dataPath + num.ToString() + DumpFileExtension))
        {
            num++;
        }

        dataPath = dataPath + num.ToString() + DumpFileExtension;
        StreamWriter writer = new StreamWriter(dataPath);

        // OverallData
        writer.WriteLine("#OverallData");
        // PlaySessionLength
        writer.WriteLine("PlaySessionLength," + PlaySessionLength.ToString());
        // DidPlayerFinish
        writer.WriteLine("DidPlayerFinish," + DidPlayerFinish.ToString());
        // NumberOfNewShells
        writer.WriteLine("NumberOfNewShells," + NumberOfNewShells.ToString());
        // NumberOfNewTightShells
        writer.WriteLine("NumberOfNewTightShells," + NumberOfNewTightShells.ToString());
        // NumberOfNewAverageShells
        writer.WriteLine("NumberOfNewAverageShells," + NumberOfNewAverageShells.ToString());
        // NumberOfNewLooseShells
        writer.WriteLine("NumberOfNewLooseShells," + NumberOfNewLooseShells.ToString());
        // TimesTakenFallDamage
        writer.WriteLine("TimesTakenFallDamage," + TimesTakenFallDamage.ToString());
        // TimesOutgrownShell
        writer.WriteLine("TimesOutgrownShell," + TimesOutgrownShell.ToString());
        // MaxTimeSpentWithoutShell
        writer.WriteLine("MaxTimeSpentWithoutShell," + MaxTimeSpentWithoutShell.ToString());

        writer.WriteLine("");

        // Chunk Data
        writer.WriteLine("#OverallData");

        int i = 0;

        foreach(ChunkData cd in chunkDataList)
        {
            writer.WriteLine("#Chunk " + i.ToString());
            writer.WriteLine("Position," + cd.playerPos.x.ToString() + "," + cd.playerPos.z.ToString());
            writer.WriteLine("CurrentShellSize," + cd.CurrentShellSize.ToString());
            writer.WriteLine("NumShellsLost," + cd.NumShellsLost.ToString());
        }

        writer.Flush();
        writer.Close();
    }

    public void ReportOnShell(string ShellState)
    {
        NumberOfNewShells++;
        if(ShellState == "Tight") NumberOfNewTightShells++;
        else if (ShellState == "Average") NumberOfNewAverageShells++;
        else if (ShellState == "Spacious") NumberOfNewLooseShells++;
        NewShellsThisChunk++;
    }

    public void CheckTimeSpentNaked()
    {
        if (CurrentTimeSpentWithoutShell > MaxTimeSpentWithoutShell)
        {
            MaxTimeSpentWithoutShell = CurrentTimeSpentWithoutShell;
            CurrentTimeSpentWithoutShell = 0.0f;
        }
    }
}