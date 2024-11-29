using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using Enemy;
using Player;

namespace PathFinding
{
    public class PathManager
    {
        private Transform _player;
        private List<GroundedEnemyMovement> aliveGroundedEnemies = new List<GroundedEnemyMovement>();
        private int groundMask;
        public static Cell PlayerCell;

        private PlayerPath playerPath; // the last node of every path chain
        private int pathUpdateIndex = 0;

        public PathManager() 
        {
            _player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
            playerPath = new PlayerPath(_player);
            groundMask = LayerMask.NameToLayer("Default");

            PublicEvents.OnEnemySpawned  += OnEnemySpawn; 
            PublicEvents.OnAnyEnemyDeath += OnEnemyDeath; //dude this shit is so poetic im going to cry
        }

        //public GameObject player;

        public Path GetPathToPlayer(Cell navigator, bool canGoDown=true)
        {
            if (PlayerCell == null)
            {
                Debug.LogWarning("no playercell");
                return null;
            }

            Path path = Navigate(navigator, PlayerCell, canGoDown);
            //path.nextPath = playerPath;
            return path;
        }

        //doesnt account for if start / end are right next to each other
        // or if start == end
        // or if start cant navigate to end
        public Path Navigate(Cell navigator, Cell target, bool canGoDown = true)
        {
            if (target == null || navigator == null)
            {
                return null;
            }

            List<Path> nextCells = new List<Path>();
            List<Cell> exploredCells = new List<Cell>();
            //Path startNode = new Path(target, navigator);
            PlayerPath startNode = new PlayerPath(_player);

            foreach (Cell cell in target.SideNeighbors)
            {
                if (!canGoDown && cell.PathPosition.y + 0.1f < target.PathPosition.y)
                    continue;

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
   

        private void OnEnemySpawn(EnemyStats enemy)
        {
            if(enemy.TryGetComponent<GroundedEnemyMovement>(out GroundedEnemyMovement move))
            {
                aliveGroundedEnemies.Add(move); 
            }
        }
        private void OnEnemyDeath(EnemyStats enemy)
        {
            if (enemy.TryGetComponent<GroundedEnemyMovement>(out GroundedEnemyMovement move))
            {
                aliveGroundedEnemies.Remove(move);
            }
        }

        //called in gamemanager
        public void Update()
        {
            UpdatePlayerPosition();

            //fuck it. only do it every other frame
            if (Time.frameCount % 2 == 1) return;

            if (aliveGroundedEnemies.Count == 0) return;

            pathUpdateIndex = (pathUpdateIndex + 1) % aliveGroundedEnemies.Count;

            if (aliveGroundedEnemies[pathUpdateIndex] == null)
            {
                aliveGroundedEnemies.RemoveAt(pathUpdateIndex);
                return;
            }

            Path p = Navigate(aliveGroundedEnemies[pathUpdateIndex].GetCurrentCell(), PlayerCell, aliveGroundedEnemies[pathUpdateIndex].canGoDown); 
            if(p != null)
                aliveGroundedEnemies[pathUpdateIndex].SetPath(p);
        }

        private void UpdatePlayerPosition()
        {
            if (Physics.Raycast(_player.transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, ~groundMask))
            {
                if (hit.transform.TryGetComponent<Cell>(out Cell c))
                {
                    PlayerCell = c;
                }
            }
        }
    }
}

