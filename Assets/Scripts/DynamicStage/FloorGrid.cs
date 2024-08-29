using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using PathFinding;

public class FloorGrid : MonoBehaviour
{
    [SerializeField] private GameObject cell;
    [SerializeField] private int length = 10;
    [SerializeField] private int width = 10;
    [SerializeField] private Color color1 = Color.white;
    [SerializeField] private Color color2 = Color.gray;

    [Button]
    void CreateStage()
    {
        for (int i = 0; i < length; i++) {
            for (int j = 0; j < length; j++)
            {
                GameObject newCell = Instantiate(cell, new Vector3(i, 0, j), Quaternion.identity);
                Cell c = newCell.GetComponent<Cell>();
                Renderer tileRenderer = newCell.GetComponent<Renderer>();

                if ((i + j) % 2 == 0){
                    tileRenderer.material.color = color1;
                }else{
                    tileRenderer.material.color = color2;
                }
            }
        }
    }
}
