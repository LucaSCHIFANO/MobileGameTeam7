using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPattern : MonoBehaviour
{
    private panelType[,] pattern;

    public enum panelType // differents type de case
    {
        GRASS,
        PATH,
        FOREST,
        WATER,
        WALL,
        BRIDGE,
    }

    public panelType[,] createPattern(int id) // les patterns
    {

        switch (id)
        {
            case 0:
                pattern = new panelType[,]
                {
                    {panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.PATH,panelType.PATH,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,},
                    {panelType.FOREST,panelType.FOREST,panelType.FOREST,panelType.GRASS,panelType.PATH,panelType.PATH,panelType.GRASS,panelType.FOREST,panelType.FOREST,panelType.FOREST,},
                    {panelType.WATER,panelType.WATER,panelType.WATER,panelType.WATER,panelType.BRIDGE,panelType.BRIDGE,panelType.WATER,panelType.WATER,panelType.WATER,panelType.WATER,},
                    {panelType.WATER,panelType.WATER,panelType.WATER,panelType.WATER,panelType.BRIDGE,panelType.BRIDGE,panelType.WATER,panelType.WATER,panelType.WATER,panelType.WATER,},
                    {panelType.FOREST,panelType.FOREST,panelType.FOREST,panelType.GRASS,panelType.PATH,panelType.PATH,panelType.GRASS,panelType.FOREST,panelType.FOREST,panelType.FOREST,},
                    {panelType.GRASS,panelType.GRASS,panelType.PATH,panelType.PATH,panelType.PATH,panelType.PATH,panelType.PATH,panelType.PATH,panelType.GRASS,panelType.GRASS,},
                    {panelType.GRASS,panelType.PATH,panelType.PATH,panelType.FOREST,panelType.FOREST,panelType.FOREST,panelType.FOREST,panelType.PATH,panelType.PATH,panelType.GRASS,},
                    {panelType.GRASS,panelType.PATH,panelType.PATH,panelType.FOREST,panelType.WALL,panelType.WALL,panelType.FOREST,panelType.PATH,panelType.PATH,panelType.GRASS,},
                    {panelType.GRASS,panelType.PATH,panelType.PATH,panelType.FOREST,panelType.WALL,panelType.WALL,panelType.FOREST,panelType.PATH,panelType.PATH,panelType.GRASS,},
                    {panelType.GRASS,panelType.PATH,panelType.PATH,panelType.FOREST,panelType.FOREST,panelType.FOREST,panelType.FOREST,panelType.PATH,panelType.PATH,panelType.GRASS,},

                };
                break;
            default:
                break;
        }

        return pattern;
    }
}
