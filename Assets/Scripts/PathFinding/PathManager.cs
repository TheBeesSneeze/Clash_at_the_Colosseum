using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

namespace PathFinding
{
    public class PathManager
    {
        //public GameObject player;
        private Cell playerCell;

        public void PlayerCellUpdate(Cell cell)
        {
           playerCell = cell;
        }

        public Path GetPathToPlayer(Cell navigator)
        {
            if (playerCell == null)
                return null;

            return Navigate(navigator, playerCell);
        }

        //doesnt account for if start / end are right next to each other
        // or if start == end
        // or if start cant navigate to end
        public Path Navigate(Cell navigator, Cell target)
        {
            List<Path> nextCells = new List<Path>();
            List<Cell> exploredCells = new List<Cell>();
            Path startNode = new Path(target, navigator);

            foreach (Cell cell in target.SideNeighbors)
            {
                if(!cell.Solid)
                    nextCells.Add(new Path( cell, startNode, navigator));
            }
            nextCells.Sort();

            exploredCells.Add(target);

            bool done = false;
            while(!done && nextCells.Count > 0)
            {
                Path next = nextCells[0];
                nextCells.RemoveAt(0);
                exploredCells.Add(next.cell);
                
                if(next.cell == navigator)
                    return next;

                foreach (Cell cell in next.cell.SideNeighbors)
                {
                    if(!exploredCells.Contains(cell) && !cell.Solid)
                        nextCells.Add(new Path(cell, next, navigator));
                }
                nextCells.Sort();

            }

            return null;
        }
   
    }
}
