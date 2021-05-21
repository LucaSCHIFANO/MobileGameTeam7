using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPattern : MonoBehaviour
{
    private panelType[,] pattern;
    public Grid gridref;

    public enum panelType // differents type de case
    {
        GRASS, // 1 deplacement
        PATH,   // 1 deplacement
        FOREST, // 3 deplacement
        WATER,  // 4 deplacement
        WALL,   // 255 deplacement
        BRIDGE, // 1 deplacement
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
                gridref.height = 10; // taille de la grille 
                gridref.width = 10;

                gridref.locationEnemy.Add(new Vector2(4, -2)); // position des ennemies
                gridref.locationEnemy.Add(new Vector2(5, -2));

                gridref.playerSpawn.Add(new Vector2(1, -7)); // possible position du player
                gridref.playerSpawn.Add(new Vector2(8, -7));

                Debug.Log("normal");
                break;

            case 1:
                pattern = new panelType[,]
                {
                    {panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.PATH},
                    {panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.WALL,panelType.WALL,panelType.WALL},
                    {panelType.WALL,panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.GRASS},
                    {panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS},
                    {panelType.PATH,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.WALL,panelType.GRASS},
                    {panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,},
                    {panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.PATH,panelType.GRASS,panelType.GRASS,},

                };
                gridref.height = 7;
                gridref.width = 7;

                gridref.locationEnemy.Add(new Vector2(1, -2));
                gridref.locationEnemy.Add(new Vector2(5, -3));

                gridref.playerSpawn.Add(new Vector2(0, -4));
                gridref.playerSpawn.Add(new Vector2(6, -0)); 
                gridref.playerSpawn.Add(new Vector2(4, -6));

                Debug.Log("green");
                break;

            case 2:
                pattern = new panelType[,]
                {
                    {panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS},
                    {panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.WALL,panelType.GRASS},
                    {panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS},
                    {panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.GRASS},
                    {panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WALL},
                    {panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.GRASS,},
                    {panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,},
                    {panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,},

                };
                gridref.height = 8;
                gridref.width = 7;

                gridref.locationEnemy.Add(new Vector2(4, 0));
                gridref.locationEnemy.Add(new Vector2(2, -7));

                gridref.playerSpawn.Add(new Vector2(0, -1));
                gridref.playerSpawn.Add(new Vector2(6, -6));

                Debug.Log("water");
                break;

            case 3:
                pattern = new panelType[,]
                {
                    {panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.WALL,panelType.WALL},
                    {panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.WALL},
                    {panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS},
                    {panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WATER,panelType.GRASS},
                    {panelType.GRASS,panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WATER,panelType.WATER},
                    {panelType.WATER,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WATER,},
                    {panelType.WATER,panelType.WATER,panelType.WATER,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WATER,},
                    {panelType.WATER,panelType.WATER,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,},
                    {panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS},
                    {panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,},
                    {panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,},
                    {panelType.WALL,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.GRASS,panelType.WALL,panelType.WALL,},

                };
                gridref.height = 12;
                gridref.width = 7;

                gridref.locationEnemy.Add(new Vector2(1, -2));
                gridref.locationEnemy.Add(new Vector2(6, -8));

                gridref.playerSpawn.Add(new Vector2(2, -11));

                Debug.Log("fire");
                break;
            default:
                break;
        }

        return pattern;
    }
}
