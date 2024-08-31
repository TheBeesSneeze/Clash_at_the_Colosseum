using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;
using Utilities;
using Unity.VisualScripting;

namespace PathFinding
{
    public class Cell : MonoBehaviour
    {
        public bool Solid = false;
        private List<Cell> _sideNeighbors = new List<Cell>();
        public List<Cell> SideNeighbors { get => _sideNeighbors; }

        private List<Cell> _verticalNeighbors = new List<Cell>();
        public List<Cell> VerticalNeighbors { get => _verticalNeighbors; }

        public LayerMask _cellLM;

        // should ve called in cellmanager MAYBE
        void Awake()
        {
            int empty = LayerMask.NameToLayer("Empty Cell");
            int fill = LayerMask.NameToLayer("Fill Cell");
            gameObject.layer = Solid ? fill : empty;
            _cellLM = new LayerMask();
            _cellLM |= (1 << empty);
            _cellLM |= (1 << fill);

            GetNeighbors();

            GetComponent<Collider>().isTrigger = !Solid;
            GetComponent<MeshRenderer>().enabled = Solid;
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider other)
        {
            if (Solid)
                return;

            Debug.Log("collide");
            if(other.gameObject.GetComponent<PlayerBehaviour>() != null)
            {
                CellManager.Instance.PlayerCellUpdate(this);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (Solid)
                return;
            
            Debug.Log("collide");
            if (collision.gameObject.GetComponent<PlayerBehaviour>() != null)
            {
                CellManager.Instance.PlayerCellUpdate(this);
            }
        }

        public void GetNeighbors()
        {
            _sideNeighbors.Clear();
            raycastSideNeighbor(new Vector3(1, 0,0), transform.lossyScale.x);
            raycastSideNeighbor(new Vector3(-1,0,0), transform.lossyScale.x);
            raycastSideNeighbor(new Vector3(0,0, 1), transform.lossyScale.z);
            raycastSideNeighbor(new Vector3(0,0,-1), transform.lossyScale.z);
            raycastVerticalNeighbor(new Vector3(0, 1, 0), transform.lossyScale.y);
            raycastVerticalNeighbor(new Vector3(0, -1, 0), transform.lossyScale.y);
        }

        private void raycastSideNeighbor(Vector3 direction, float distance)
        {
            //idk if boxcast better yet
            /*
            
            RaycastHit hit;
            if(Physics.Raycast(transform.position, direction, out hit, distance, ~6))
            {
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();
                if (cell != null)
                {
                    _neighbors.Add(cell);
                }
            }
            */
            
            distance = 0.1f;
            RaycastHit[] hits;
            hits = Physics.BoxCastAll(transform.position, transform.lossyScale * 0.4f, direction, transform.rotation, distance);
            
            foreach (RaycastHit hit in hits )
            {
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();
                if (cell != null)
                {
                    _sideNeighbors.Add(cell);
                }
            }
        }
        private void raycastVerticalNeighbor(Vector3 direction, float distance)
        {
            /*
            
            RaycastHit hit;
            if(Physics.Raycast(transform.position, direction, out hit, distance, ~6))
            {
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();
                if (cell != null)
                {
                    _neighbors.Add(cell);
                }
            }
            */

            distance = 0.1f;
            RaycastHit[] hits;
            hits = Physics.BoxCastAll(transform.position, transform.lossyScale * 0.4f, direction, transform.rotation, distance);

            foreach (RaycastHit hit in hits)
            {
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();
                if (cell != null)
                {
                    _verticalNeighbors.Add(cell);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (Selection.activeGameObject != transform.gameObject)
            {
                return;
            }

            Debug.Log("drawing");

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.lossyScale);

            Gizmos.color= Color.yellow;
            for (int i = 0; i < _sideNeighbors.Count; i++)
            {
                Cell c = _sideNeighbors[i];
                Gizmos.DrawWireCube(c.transform.position, c.transform.lossyScale);
            }
        }

        private void OnDrawGizmos()
        {
            GetComponent<Collider>().isTrigger = !Solid;
            GetComponent<MeshRenderer>().enabled = Solid;
            gameObject.layer = Solid ? 0 : 6;
        }
    }
}

