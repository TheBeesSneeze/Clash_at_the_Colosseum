using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using NaughtyAttributes;
using Utilities;
using System;

namespace PathFinding
{
    public class Cell : MonoBehaviour
    {
        [OnValueChanged("UpdateIfSolid")]
        [SerializeField] public bool Solid = false;
        [SerializeField] bool autoRenameCell = false;

        private float segmentSize = 0.5f;

        public List<Cell> SideNeighbors { get => _sideNeighbors; }
        private List<Cell> _sideNeighbors = new List<Cell>();
        public List<Cell> VerticalNeighbors { get => _verticalNeighbors; }
        private List<Cell> _verticalNeighbors = new List<Cell>();

        [HideInInspector] public Vector3 PathPosition {  get => new Vector3(_center.x, topPoint+0.1f, _center.z); }
        private Vector3 _center;
        private float topPoint;

        private LayerMask _cellLM;
        private Collider _collider;

        // should ve called in cellmanager MAYBE
        void Awake()
        {
            _collider = GetComponent<Collider>();
            _center = _collider.bounds.center;
            topPoint = _collider.bounds.max.y; 
            int empty = LayerMask.NameToLayer("Empty Cell");
            int fill = 
            _cellLM = LayerMask.GetMask(new String[] { "FilL Cell", "Default" });
            //_cellLM |= (1 << LayerMask.NameToLayer("Default"));
            //_cellLM |= (1 << LayerMask.NameToLayer("Fill Cell"));

            GetNeighbors();
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider other)
        {
            if (Solid)
                return;

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
        
        public void GetNeighbors()
        {
            _sideNeighbors.Clear();
            raycastSideNeighbors();

            /*
            boxcastSideNeighbor(new Vector3(1, 0,0));
            boxcastSideNeighbor(new Vector3(-1,0,0));
            boxcastSideNeighbor(new Vector3(0,0, 1));
            boxcastSideNeighbor(new Vector3(0,0,-1)); 
            */
            //raycastVerticalNeighbor(new Vector3(0, 1, 0));
            //raycastVerticalNeighbor(new Vector3(0, -1, 0));
        }

        private Cell raycastSideNeighbor(Vector3 direction, float distance = 0.1f)
        {

            direction = new Vector3(_collider.bounds.extents.x * direction.x, 0, _collider.bounds.extents.z * direction.z);
            distance = direction.magnitude;
            RaycastHit hit;
            float yScale = transform.lossyScale.y;
            Vector3 boxSize = new Vector3(0.1f, yScale, 0.1f);
            if(Physics.BoxCast(_collider.bounds.center, boxSize, direction, out hit, Quaternion.identity))
            {
                Cell c = hit.transform.GetComponent<Cell>();
                return c;
            }
            return null;

            /*
            direction = new Vector3( _collider.bounds.extents.x * direction.x, 0, _collider.bounds.extents.z * direction.z);
            distance = direction.magnitude;
            RaycastHit[] hits;
            float yScale = transform.lossyScale.y;
            Vector3 boxSize = new Vector3(0.1f, yScale, 0.1f);
            hits = Physics.BoxCastAll(_collider.bounds.center, boxSize,direction, Quaternion.identity);
            return hits;
            */
        }



        private void raycastSideNeighbors()
        {
            for (float x = -1; x <= 1; x+= segmentSize)
            {
                for (float y = -1; y <= 1; y+=segmentSize)
                {
                    if (x == 0 && y==0) continue;

                    Cell neighbor = raycastSideNeighbor(new Vector3(x, 0, y));

                    if (neighbor == null)
                        continue;

                    if (!_sideNeighbors.Contains(neighbor))
                    {
                        _sideNeighbors.Add(neighbor);
                    }

                    /*
                    RaycastHit[] neighbors = raycastSideNeighbor(new Vector3(x, 0, y));

                    foreach(RaycastHit hit in neighbors)
                    {
                        Cell neighbor = hit.transform.gameObject.GetComponent<Cell>();

                        if (neighbor == null)
                            continue;

                        if ( !_sideNeighbors.Contains(neighbor))
                        {
                            _sideNeighbors.Add(neighbor);
                        }
                    }
                    */

                }
            }
        }

        
#if UNITY_EDITOR


        private void OnDrawGizmos()
        {
            if(Selection.activeGameObject != null)
            {
                if (Selection.activeGameObject.GetComponent<StageBuilderTool>() != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(_center, PathPosition);
                }
            }
            
            
            if (gameObject.GetComponent<MeshCollider>() != null)
                GetComponent<MeshCollider>().convex = true;

            DebugDrawNeighbors();
        }

        public void DebugDrawNeighbors()
        {

            for (int i = 0; i < _sideNeighbors.Count; i++)
            {
                Cell c = _sideNeighbors[i];
                //Gizmos.DrawWireCube(c.transform.position, c.transform.lossyScale);
                //Gizmos.DrawWireMesh(c.GetComponent<MeshFilter>().sharedMesh, c.transform.position, transform.rotation, transform.lossyScale);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(PathPosition, c.PathPosition);
            }

            
        }

        
#endif

        #region obsolete
        [Obsolete]
        private void boxcastSideNeighbor(Vector3 direction, float distance = 0.1f)
        {
            RaycastHit[] hits;
            hits = Physics.BoxCastAll(transform.position, transform.lossyScale / 2.1f, direction, transform.rotation, distance);

            foreach (RaycastHit hit in hits)
            {
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();
                if (cell != null)
                {
                    _sideNeighbors.Add(cell);
                }
            }
        }

        [Obsolete]
        private void raycastVerticalNeighbor(Vector3 direction, float distance = 0.1f)
        {
            RaycastHit[] hits;
            hits = Physics.BoxCastAll(transform.position, transform.lossyScale / 2.1f, direction, transform.rotation, distance);

            foreach (RaycastHit hit in hits)
            {
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();
                if (cell != null)
                {
                    _verticalNeighbors.Add(cell);
                }
            }
        }

#if UNITY_EDITOR
        [Obsolete]
        private void DrawSelfGizmos()
        {
            if (Selection.activeGameObject == null)
                return;

            if (Selection.activeGameObject.GetComponent<StageBuilderTool>() == null)
                return;

            Gizmos.color = Color.blue;

            if (GetComponent<MeshFilter>() != null)
                Gizmos.DrawWireMesh(GetComponent<MeshFilter>().sharedMesh, transform.position, transform.rotation, transform.lossyScale);
            else
                Gizmos.DrawWireCube(transform.position, transform.lossyScale);
        }
#endif

        [Obsolete]
        private void UpdateIfSolid()
        {
            GetComponent<Collider>().isTrigger = !Solid;
            GetComponent<MeshRenderer>().enabled = Solid;
            gameObject.layer = Solid ? 0 : 6;

            if (autoRenameCell && !Application.isPlaying)
                gameObject.name = Solid ? "Fill Cell" : "Air Cell";
        }

        [Obsolete]
        private void DebugDrawBoxCasts()
        {
            _collider = GetComponent<Collider>();
            for (float x = -1; x <= 1; x += segmentSize)
            {
                for (float y = -1; y <= 1; y += segmentSize)
                {
                    if (x == 0 && y == 0) continue;

                    float yScale = transform.lossyScale.y;
                    Vector3 boxSize = new Vector3(0.1f, yScale, 0.1f);
                    //Vector3 direction = new Vector3(x, 0, y);
                    Vector3 direction = new Vector3(_collider.bounds.extents.x * x, 0, _collider.bounds.extents.z * y);
                    float distance = direction.magnitude;
                    DebugUtilities.DrawBoxCastBox(_collider.bounds.center, boxSize, direction, Quaternion.identity, distance, Color.yellow);


                }
            }
        }

        #endregion


    }

}

