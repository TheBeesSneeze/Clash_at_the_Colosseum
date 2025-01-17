using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using TMPro;
using NaughtyAttributes;
using Utilities;
using Enemy;
using Player;

namespace PathFinding
{
    [RequireComponent(typeof(EnemyStats))]
    public class GroundedEnemyMovement : MonoBehaviour
    {
        private float _turningSpeed = 1;
        private float _stoppingSpeed = 1;
        private float _sightDistance = 10;
        [HideInInspector] public bool canGoDown = false;
        private EnemyStats _enemyStats;
        private EnemyTakeDamage _takeDamage;

        private static Transform _player;
        public bool debug = true;
        private Rigidbody rb;
        private Collider _collider;

        private Path path;
        private int _playerLayer = 6;
        private LayerMask groundlm;
        public ContactFilter2D ContactFilter;
        [ReadOnly][SerializeField] private Cell currentCell;
        private float _colliderHeight;

        private void Start()
        {
            if (_player == null)
                _player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
            _playerLayer = LayerMask.NameToLayer("Player");
            groundlm = LayerMask.NameToLayer("Default");
            rb = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _enemyStats = GetComponent<EnemyStats>();
            _takeDamage = GetComponent<EnemyTakeDamage>();

            _colliderHeight = _collider.bounds.size.y;

            if (_enemyStats == null) return;
            _turningSpeed = _enemyStats.TurningSpeed;
            _stoppingSpeed = _enemyStats.StopSpeed;
            _sightDistance = _enemyStats.SightDistance;
            canGoDown = _enemyStats.CanPathfindDown;
        }

        private void Update()
        {
            if (_takeDamage.IsDead)
            {
                rb.velocity = Vector3.zero;
                return;
            }

            ApplyGravity();
            RotateColliderTowardsDirection(rb.velocity); //it changes direction after this, but theres so many ways that it can move that its scary

            if (AttemptBeelinePlayer()) return;
            if (path == null) return;
            if (path.nextPath == null) return;

            bool navigating = NavigatePath();
            if (!navigating)
            {
                float y = rb.velocity.y;
                rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(0, y, 0), Time.deltaTime * _stoppingSpeed);
            }
        }



        private void ApplyGravity()
        {
            rb.AddForce(0, -_enemyStats.gravity, 0, ForceMode.Acceleration);
        }

        private void RotateColliderTowardsDirection(Vector3 direction)
        {

            direction.y = 0;
            if (direction.magnitude < 0.01f)
                return;

            Quaternion targetRoation = Quaternion.LookRotation(direction);
            rb.transform.rotation = Quaternion.Lerp(transform.rotation, targetRoation, Time.deltaTime * _turningSpeed);
        }

        private bool AttemptBeelinePlayer()
        {
            bool canBeeline = false;
            if (path == null)
                canBeeline = false;
            // if the enemy is close enough to the player (and it doesnt need to jump)
            else if (!path.IsLengthGreaterThan(3) && Mathf.Abs(transform.position.y - path.position.y) < 1f)
                canBeeline = true;

            if (!canBeeline)
                return false;

            // bee line time
            Vector3 direction = _player.transform.position - transform.position;
            direction.y = 0;
            direction = direction.normalized * _enemyStats.MoveSpeed;
            direction.y = rb.velocity.y;
            rb.velocity = Vector3.Lerp(rb.velocity, direction, 12 * Time.deltaTime);

            return true;
        }

        /// <summary>
        /// returns true if it can navigate to player
        /// </summary>
        private bool NavigatePath()
        {
            //if (Vector3.Distance(transform.position, _player.transform.position) < _enemyStats.StopDistanceToPlayer)
            //    return false;

            float distance = Vector3.Distance(new Vector3(path.position.x, 0, path.position.z), new Vector3(transform.position.x, 0, transform.position.z));

            if (distance < 0.1f)
                path = path.nextPath;

            float y = rb.velocity.y;
            Vector3 targetPosition = getTargetPosition();
            Vector3 direction = targetPosition - transform.position;

            direction.y = 0;
            direction = direction.normalized * _enemyStats.MoveSpeed;
            Debug.DrawLine(transform.position, transform.position + direction, Color.red);
            bool jump = ShouldJump(direction);
            direction.y = y; //reapply gravity
            rb.velocity = Vector3.Lerp(rb.velocity, direction, 8 * Time.deltaTime);

            if (jump)
                Jump(); //if jump Jump

            return true;
        }

        private void Jump()
        {
            //rb.velocity = new Vector3(rb.velocity.x, _enemyStats.JumpForce, rb.velocity.z);
            rb.AddForce(Vector3.up * _enemyStats.JumpForce, ForceMode.Force);
        }

        private Vector3 getTargetPosition()
        {
            if (path == null)
                return _player.position;

            return path.position;

            if (path.nextPath == null)
                return path.position;

            if (Mathf.Abs(path.nextPath.position.y - path.position.y) > 0.1f)
                return path.position;

            return (path.position + path.nextPath.position) / 2;
        }

        private bool ShouldJump(Vector3 direction)
        {
            if (!isGrounded()) return false;

            //if (path.nextPath == null)
            return (path.position.y > transform.position.y);

        }

        private bool isGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, _colliderHeight * 0.6f);
        }

        /// <summary>
        /// called in PathManager
        /// </summary>
        public void SetPath(Path newPath)
        {
            path = newPath;

            if (path == null) return;

            if (path.nextPath != null)
                path = path.nextPath;
        }

        public Cell GetCurrentCell()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, ~groundlm))
            {
                if (hit.transform.TryGetComponent<Cell>(out Cell c))
                    return c;
            }
            return null;
            /*
            //Ray r = new Ray(transform.position - (Vector3.up * transform.lossyScale.y), Vector3.up );
            RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down,5 * transform.lossyScale.y);
            foreach (RaycastHit hit in hits)
            {
                if(hit.transform.TryGetComponent<Cell>( out Cell c))
                {
                    return c;
                }
            }
            return null;
            */
        }
        private void OnDrawGizmos()
        {
            if (!debug) return;

            if (path == null)
            {
                //Debug.LogWarning("no path");
                return;
            }
            if (path.nextPath == null)
                return;

            path.DrawPath();

            return; //top ten most necessary returns
        }

    }
}