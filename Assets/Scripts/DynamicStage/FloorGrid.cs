using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGrid : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private int length = 10;
    [SerializeField] private int width = 10;
    public Color color1 = Color.white;
    public Color color2 = Color.gray;
    public GameObject[,] GroundGrid;
    void Start()
    {
        GroundGrid = new GameObject[length, width];
        for (int i = 0; i < length; i++) {
            for (int j = 0; j < length; j++)
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
    void Update()
    {
        
    }
}
