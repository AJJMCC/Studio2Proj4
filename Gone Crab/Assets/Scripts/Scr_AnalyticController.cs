using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AnalyticController : MonoBehaviour {

    struct ChunkData{
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
    #endregion

    #region Private Fields
    private float ChunkDataDumpTimer; // The period between when dump data is collected.
    private List<ChunkData> chunkDataList;
    private int NumberOfNewShells = 0;
    private int NumberOfNewTightShells = 0;
    private int NumberOfNewAverageShells = 0;
    private int NumberOfNewLooseShells = 0;
    private int NewShellsThisChunk = 0;
    #endregion

    #region Public Fields
    public float PlaySessionLength;
    public bool DidPlayerFinish;
    public int TimesTakenFallDamage;
    public int TimesOutgrownShell;
    public float MaxTimeSpentWithoutShell;
    #endregion

    // Use this for initialization
    void Start () {
        Analytics = this;
        CrabRef = FindObjectOfType<Scr_PlayerCrab>();
        ChunkDataDumpTimer = ChunkDataDumpInterval;
    }
	
	// Update is called once per frame
	void Update () {
        ChunkDataUpdate();
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
        // OverallData

        // PlaySessionLength
        // DidPlayerFinish
        // NumberOfNewShells
        // NumberOfNewTightShells
        // NumberOfNewAverageShells
        // NumberOfNewLooseShells
        // TimesTakenFallDamage
        // TimesOutgrownShell
        // MaxTimeSpentWithoutShell

        // Chunk Data
    }

    public void ReportOnShell(string ShellState)
    {
        NumberOfNewShells++;
        if(ShellState == "Tight") NumberOfNewTightShells++;
        else if (ShellState == "Average") NumberOfNewAverageShells++;
        else if (ShellState == "Spacious") NumberOfNewLooseShells++;
        NewShellsThisChunk++;
    }
}