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
        [OnValueChanged("UpdateIfSolid")]
        [SerializeField] public bool Solid = false;
        [SerializeField] bool autoRenameCell = false;

        public List<Cell> SideNeighbors { get => _sideNeighbors; }
        private List<Cell> _sideNeighbors = new List<Cell>();
        public List<Cell> VerticalNeighbors { get => _verticalNeighbors; }
        private List<Cell> _verticalNeighbors = new List<Cell>();

        private LayerMask _cellLM;

        // should ve called in cellmanager MAYBE
        void Awake()
        {
            int empty = LayerMask.NameToLayer("Empty Cell");
            int fill = LayerMask.NameToLayer("Fill Cell");
            UpdateIfSolid();
            _cellLM = new LayerMask();
            _cellLM |= (1 << empty);
            _cellLM |= (1 << fill);

            GetNeighbors();
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider other)
        {
            if (Solid)
                return;

            Debug.Log("collide");
            if(other.gameObject.GetComponent<PlayerBehaviour>() != null)
            {
                GameManager.pathManager.PlayerCellUpdate(this);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (Solid)
                return;
            
            Debug.Log("collide");
            if (collision.gameObject.GetComponent<PlayerBehaviour>() != null)
            {
                GameManager.pathManager.PlayerCellUpdate(this);
            }
        }
        private void UpdateIfSolid()
        {
            GetComponent<Collider>().isTrigger = !Solid;
            GetComponent<MeshRenderer>().enabled = Solid;
            gameObject.layer = Solid ? 0 : 6;

            if (autoRenameCell && !Application.isPlaying)
                gameObject.name = Solid ? "Solid Cell" : "Air Cell";
        }
        public void GetNeighbors()
        {
            _sideNeighbors.Clear();
            raycastSideNeighbor(new Vector3(1, 0,0));
            raycastSideNeighbor(new Vector3(-1,0,0));
            raycastSideNeighbor(new Vector3(0,0, 1));
            raycastSideNeighbor(new Vector3(0,0,-1));
            raycastVerticalNeighbor(new Vector3(0, 1, 0));
            raycastVerticalNeighbor(new Vector3(0, -1, 0));
        }

        private void raycastSideNeighbor(Vector3 direction, float distance=0.1f)
        { 
            RaycastHit[] hits;
            hits = Physics.BoxCastAll(transform.position, transform.lossyScale/2.1f, direction, transform.rotation,distance);
            
            foreach (RaycastHit hit in hits )
            {
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();
                if (cell != null)
                {
                    _sideNeighbors.Add(cell);
                }
            }
        }
        private void raycastVerticalNeighbor(Vector3 direction, float distance=0.1f)
        {
            RaycastHit[] hits;
            hits = Physics.BoxCastAll(transform.position, transform.lossyScale/2.1f, direction, transform.rotation, distance);

            foreach (RaycastHit hit in hits)
            {
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();
                if (cell != null)
                {
                    _verticalNeighbors.Add(cell);
                }
            }
        }

        private void OnDrawGizmos()
        {
            UpdateIfSolid();

            DrawSelfGizmos();

            // Draw Neighbors
            if (Selection.activeGameObject != this.transform.gameObject)
                return;

            GetNeighbors();
            Gizmos.color = Color.yellow;
            for (int i = 0; i < _sideNeighbors.Count; i++)
            {
                Cell c = _sideNeighbors[i];
                Gizmos.DrawWireCube(c.transform.position, c.transform.lossyScale);
            }
        }

        private void DrawSelfGizmos()
        {
            if (Selection.activeGameObject == null)
                return;

            if (Selection.activeGameObject.GetComponent<StageBuilderTool>() == null)
                return;
            
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position, transform.lossyScale);
        }
    }
}

