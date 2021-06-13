using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPattern : MonoBehaviour
{
    private panelType[,] pattern;
    public TutoGrid gridreftuto;

    public enum panelType // differents type de case
    {
        GRASS, // 1 deplacement
        PATH,   // 1 deplacement
        FOREST, // 3 deplacement
        WATER,  // 4 deplacement
        WALL,   // 255 deplacement
        BRIDGE, // 1 deplacement
        HOLE, // 255 deplacement mais peut tirer a travers
        CHEST, // 1 depl mais coffre
        POISON, // 3 deplacement + degats 
    }

    public panelType[,] createPattern(int id) // les patterns
    {
            pattern = new panelType[,]
            {
                {panelType.FOREST,panelType.FOREST,panelType.FOREST,panelType.PATH,panelType.PATH,panelType.PATH,panelType.FOREST,panelType.FOREST,panelType.FOREST},
                {panelType.FOREST,panelType.GRASS,panelType.GRASS,panelType.PATH,panelType.PATH,panelType.PATH,panelType.GRASS,panelType.GRASS,panelType.FOREST},
                {panelType.FOREST,panelType.GRASS,panelType.GRASS,panelType.PATH,panelType.PATH,panelType.PATH,panelType.GRASS,panelType.GRASS,panelType.FOREST},
                {panelType.FOREST,panelType.GRASS,panelType.GRASS,panelType.PATH,panelType.PATH,panelType.PATH,panelType.GRASS,panelType.GRASS,panelType.FOREST},
                {panelType.FOREST,panelType.GRASS,panelType.GRASS,panelType.PATH,panelType.PATH,panelType.PATH,panelType.GRASS,panelType.GRASS,panelType.FOREST},
                {panelType.FOREST,panelType.GRASS,panelType.GRASS,panelType.PATH,panelType.PATH,panelType.PATH,panelType.GRASS,panelType.GRASS,panelType.FOREST},
                {panelType.FOREST,panelType.GRASS,panelType.GRASS,panelType.PATH,panelType.PATH,panelType.PATH,panelType.GRASS,panelType.GRASS,panelType.FOREST},
                {panelType.FOREST,panelType.GRASS,panelType.GRASS,panelType.PATH,panelType.PATH,panelType.PATH,panelType.GRASS,panelType.GRASS,panelType.FOREST},
                {panelType.FOREST,panelType.FOREST,panelType.FOREST,panelType.PATH,panelType.PATH,panelType.PATH,panelType.FOREST,panelType.FOREST,panelType.FOREST},


            };
            gridreftuto.height = 9;
            gridreftuto.width = 9;

            gridreftuto.locationEnemy.Add(new Vector2(4, -3));


            gridreftuto.playerSpawn.Add(new Vector2(6, -6));
            gridreftuto.playerSpawn.Add(new Vector2(2, -6));

            Debug.Log("fire");
   
        return pattern;
    }
}

