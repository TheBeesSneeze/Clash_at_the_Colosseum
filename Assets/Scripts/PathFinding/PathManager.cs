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

        public Path GetPathToPlayer(Cell navigator)
        {
            if (CellManager.PlayerCell == null)
                return null;

            return Navigate(navigator, CellManager.PlayerCell);
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
                Path next = new Path(cell, startNode, navigator);
                if (/*!cell.Solid && */ nextCells.IndexOf(next)== -1)
                    nextCells.Add(next);
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
                    if(!exploredCells.Contains(cell) /*&& !cell.Solid*/)
                        nextCells.Add(new Path(cell, next, navigator));
                }
                nextCells.Sort();

            }

            return null;
        }
   
    }
}

