using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;

namespace PathFinding
{
    public class Cell : MonoBehaviour
    {
        public bool Solid = false;
        //[Header("Debug")]
        private List<Cell> _neighbors = new List<Cell>();
        public List<Cell> Neighbors { get => _neighbors; }

        // should ve called in cellmanager MAYBE
        void Awake()
        {
            GetNeighbors();

            GetComponent<Collider>().isTrigger = !Solid;
            GetComponent<MeshRenderer>().enabled = Solid;
            gameObject.layer = Solid ? 0 : 6;
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
            _neighbors.Clear();
            raycastNeighbor(new Vector3(1, 0,0), transform.lossyScale.x);
            raycastNeighbor(new Vector3(-1,0,0), transform.lossyScale.x);
            raycastNeighbor(new Vector3(0, 1,0), transform.lossyScale.y);
            raycastNeighbor(new Vector3(0,-1,0), transform.lossyScale.y);
            raycastNeighbor(new Vector3(0,0, 1), transform.lossyScale.z);
            raycastNeighbor(new Vector3(0,0,-1), transform.lossyScale.z);
        }

        private void raycastNeighbor(Vector3 direction, float distance)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, direction, out hit, distance, ~6))
            {
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();
                if (cell != null)
                {
                    _neighbors.Add(cell);
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
            for (int i = 0; i < _neighbors.Count; i++)
            {
                Cell c = _neighbors[i];
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

