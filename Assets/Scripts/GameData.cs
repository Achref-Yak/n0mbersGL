using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{

    public int currentActiveLevel = 0;
    public int level = 0;
    public int hints = 0;
    public int bestTime = 0;
    public int soundState = 0;
    public string theme = "dark";
    public int premium = 0;


    public GameData()
    {
 
    }

    public int nextLevel()
    {
        level++;
        return level;
    }

    public int prevLevel()
    {
        level--;
        return level;
    }

    public void reset()
    {
        level = 0;
    }

    
}

[System.Serializable]
public class LevelWins
{
    public int level;
    public string time;

    public LevelWins(int level,string time)
    {
        this.level = level;
        this.time = time;
    }
}
