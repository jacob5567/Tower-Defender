using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController
{
    public List<Level> levels;
    private const int NUM_LEVELS = 20;
    private int currentReadLine;
    public LevelController()
    {
        levels = new List<Level>();
        //read input from level file
        currentReadLine = 0;
        try
        {
            using (StreamReader sr = new StreamReader("LevelData.txt"))
            {
                do
                {
                    currentReadLine++;
                    string line = sr.ReadLine();
                    switch (currentReadLine)
                    {
                        case 1:
                            levels.Add(new Level(Int32.Parse(line)));
                            break;
                        case 2:
                            levels[levels.Count - 1].numEnemies = Int32.Parse(line);
                            break;
                        case 3:
                            levels[levels.Count - 1].enemyHealth = Int32.Parse(line);
                            break;
                        case 4:
                            levels[levels.Count - 1].spawnRate = Int32.Parse(line);
                            currentReadLine = 0;
                            break;
                        default:
                            Debug.Log("ERROR IN READING FILE");
                            break;
                    }
                } while (!sr.EndOfStream);
            }
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
        }
    }

    public Level getLevel(int levelNum)
    {
        return levels[levelNum - 1];
    }
}
