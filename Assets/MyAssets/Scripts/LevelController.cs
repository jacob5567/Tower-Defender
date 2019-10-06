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

    // Hardcoded level data
    public LevelController()
    {
        levels = new List<Level>();
        levels.Add(new Level(1, 3, 200, 400));
        levels.Add(new Level(2, 5, 300, 300));
        levels.Add(new Level(3, 10, 400, 200));
        levels.Add(new Level(4, 10, 500, 150));
        levels.Add(new Level(5, 1, 2000, 1000));
        levels.Add(new Level(6, 15, 600, 150));
        levels.Add(new Level(7, 15, 750, 150));
        levels.Add(new Level(8, 20, 900, 150));
        levels.Add(new Level(9, 15, 1000, 150));
        levels.Add(new Level(10, 1, 5000, 1000));
        levels.Add(new Level(11, 20, 1250, 150));
        levels.Add(new Level(12, 20, 1500, 100));
        levels.Add(new Level(13, 15, 1750, 150));
        levels.Add(new Level(14, 20, 1750, 100));
        levels.Add(new Level(15, 1, 10000, 1000));
        levels.Add(new Level(16, 20, 2000, 150));
        levels.Add(new Level(17, 25, 1500, 50));
        levels.Add(new Level(18, 15, 2500, 150));
        levels.Add(new Level(19, 1, 30000, 1000));
        levels.Add(new Level(20, 30, 2500, 100));
    }

    // gets the Level object corresponding to a certain level number
    public Level getLevel(int levelNum)
    {
        return levels[levelNum - 1];
    }
}
