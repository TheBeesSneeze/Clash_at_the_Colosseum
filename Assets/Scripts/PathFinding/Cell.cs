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
        public bool Slope = false;
        [OnValueChanged("UpdateIfSolid")]
        private bool Solid = false;
        [SerializeField] bool autoRenameCell = false;

        [Header("Boss Phase 3:")]
        [Tooltip("On boss phase 3, seconds until stage piece falls. -1 to not fall")]
        [SerializeField] float boss3PieceFallDelay = -1;

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

        private ParticleSystem ps;

        // should ve called in cellmanager MAYBE
        void Awake()
        {
            _collider = GetComponent<Collider>();
            _center = _collider.bounds.center;
            topPoint = _collider.bounds.max.y; 
            int empty = LayerMask.NameToLayer("Empty Cell");
            int fill = 
            _cellLM = LayerMask.GetMask(new String[] { "FilL Cell", "Default" });

            GetNeighbors();
            PublicEvents.OnStageTransitionFinish.AddListener(GetNeighbors);
            //PublicEvents.OnBossPhaseThreeStart.AddListener(OnBossPhase3Start);

            ps = GetComponent<ParticleSystem>();
        }

        private void LateUpdate()
        {
            //get rid of this
            _center = _collider.bounds.center;
            topPoint = _collider.bounds.max.y;
        }

        private void OnBossPhase3Start()
        {
            if (boss3PieceFallDelay < 0)
                return;
            
            StartCoroutine(StageFall());
        }

        // Update is called once per frame
        public void GetNeighbors()
        {
            _center = _collider.bounds.center;
            topPoint = _collider.bounds.max.y;

            _verticalNeighbors.Clear();
            _sideNeighbors.Clear();
            raycastSideNeighbors();

            return;

            /*
            Vector3 pathpos = PathPosition;

            foreach(Cell neighbor in _verticalNeighbors)
            {
                float difference = pathpos.y - neighbor.PathPosition.y;

                //if(difference < 1 && difference > -1)
                //if(difference < 2 && difference > -2)
                    _sideNeighbors.Add(neighbor);
            }*/
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
                        //_verticalNeighbors.Add(neighbor);
                        _sideNeighbors.Add(neighbor);
                    }
                }
            }
        }

        private Cell raycastSideNeighbor(Vector3 direction, float distance = 0.1f)
        {

            direction = new Vector3(_collider.bounds.extents.x * direction.x, 0, _collider.bounds.extents.z * direction.z);
            distance = direction.magnitude;
            RaycastHit hit;
            float yScale = transform.lossyScale.y;
            Vector3 boxSize = new Vector3(0.1f, yScale, 0.1f);
            if (Physics.BoxCast(_collider.bounds.center, boxSize, direction, out hit, Quaternion.identity))
            {
                Cell c = hit.transform.GetComponent<Cell>();
                return c;
            }
            return null;
        }

        private IEnumerator StageFall()
        {
            if (ps != null)
                   ps.Play();
            yield return new WaitForSeconds(boss3PieceFallDelay);
            float timeElapsed = 0;
            Vector3 startPos = transform.position;
            Vector3 endpos = new Vector3(transform.position.x, transform.position.y - CellManager.cellFallDistance, transform.position.z);
            while (timeElapsed < CellManager.cellFallTime * (1/5))
            {
                float t = timeElapsed / CellManager.cellFallTime;
                t = Mathf.Pow(t, 5);


                transform.position = Vector3.Lerp(startPos, endpos, t);

                yield return null;
                timeElapsed += Time.deltaTime;
            }

            yield return new WaitForSeconds(1);

            //this is the same code as above but i am too tired + stressed to make it a function
            while (timeElapsed < CellManager.cellFallTime)
            {
                float t = timeElapsed / CellManager.cellFallTime;
                t = Mathf.Pow(t, 5);


                transform.position = Vector3.Lerp(startPos, endpos, t);

                yield return null;
                timeElapsed += Time.deltaTime;
            }

            Debug.Log("Destroying " + gameObject.name);
            Destroy(gameObject);
        }

#if UNITY_EDITOR


        private void OnDrawGizmos()
        {
            if(TryGetComponent<ParticleSystem>(out ParticleSystem ps))
            {
                _collider = GetComponent<Collider>();
                _center = _collider.bounds.center;
                topPoint = _collider.bounds.max.y;
                var shape = ps.shape;
                shape.position = PathPosition - (Vector3.down * -1.5f);
            }

            if (Selection.activeGameObject != null)
            {
                if (Selection.activeGameObject.GetComponent<StageBuilderTool>() != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(_center, PathPosition);
                }
            }
            
            
            if (gameObject.GetComponent<MeshCollider>() != null)
                GetComponent<MeshCollider>().convex = false;

            DebugDrawNeighbors();
        }

        public void DebugDrawNeighbors()
        {
            for (int i = 0; i < _sideNeighbors.Count; i++)
            {
                Cell c = _sideNeighbors[i];
                //Gizmos.DrawWireCube(c.transform.position, c.transform.lossyScale);
                //Gizmos.DrawWireMesh(c.GetComponent<MeshFilter>().sharedMesh, c.transform.position, transform.rotation, transform.lossyScale);

                float difference = PathPosition.y - c.PathPosition.y;

                if (difference > -1)
                    Gizmos.color = Color.blue;
                else
                    Gizmos.color = Color.cyan;
                    
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

