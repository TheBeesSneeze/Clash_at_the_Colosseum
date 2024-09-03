/*******************************************************************************
 * File Name :         FloorGrid.cs
 * Author(s) :         Tyler
 * Creation Date :     8/29/2024
 *
 * Brief Description : Spawns in a grid of tiles for the ground.
 *****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FloorGrid : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private int XLength = 10;
    [SerializeField] private int Zlength = 10;
    public Color color1 = Color.white;
    public Color color2 = Color.gray;
    public GameObject[,] GroundGrid;
    void Start()
    {
        GroundGrid = new GameObject[XLength, Zlength];
        for (int i = 0; i < XLength; i++) {
            for (int j = 0; j < Zlength; j++)
            {
                GroundGrid[i, j] = Instantiate(tile, new Vector3(i, 0, j), Quaternion.identity);
                Renderer tileRenderer = GroundGrid[i, j].GetComponent<Renderer>();
                if ((i + j) % 2 == 0){
                    tileRenderer.material.color = color1;
                }else{
                    tileRenderer.material.color = color2;
                }
            }
        }
    }
}
