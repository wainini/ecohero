using System.Collections.Generic;

[System.Serializable]
public class GameSaveData
{
    public List<string> ListUnlockedItem;
    public List<LevelData> ClearedLevelData;

    public GameSaveData()
    {
        this.ListUnlockedItem = new List<string>();
        this.ClearedLevelData = new List<LevelData>();
    }
}

[System.Serializable]
public class LevelData
{
    // Level berapa
    public int Level;
    public int BestScore;
}