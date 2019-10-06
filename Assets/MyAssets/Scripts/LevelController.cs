// Jacob Faulk
// This file reads in all the level info from a file and creates new Level objects based on that data.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController
{
    public List<Level> levels; // The list of all levels
    private const int NUM_LEVELS = 20; // The total number of levels in the game
    private int currentReadLine; // the current value of the line being read in from the file

    // Reads in all the level data from "LevelData.txt", and creates the new level data based on that
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

    // gets the Level object corresponding to a certain level number
    public Level getLevel(int levelNum)
    {
        return levels[levelNum - 1];
    }
}
