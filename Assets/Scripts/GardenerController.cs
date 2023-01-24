using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GardenerController : MonoBehaviour
{
    // get input action ref to read screen position from
    [SerializeField] private Camera _camera;
    // Get screen current position
    private Vector3 _currentScreenPosition;
    private List<Vector3> _destinations = new List<Vector3>();
    private float _dist;

    private void Update()
    {
        // we get the agent
        NavMeshAgent _agent = gameObject.GetComponent<NavMeshAgent>();

        if (Input.GetMouseButtonDown(0))
        {
            // we add the position to the pool
            PositionPool();
        }

        MoveAgent(_agent);
    }

    // stores the position in a pool
    private void PositionPool()
    {
        // we get position
        Vector3 transformInCameraSpace = _camera.transform.InverseTransformPoint(transform.position);
        Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transformInCameraSpace.z);
        _currentScreenPosition = _camera.ScreenToWorldPoint(screenPoint);

        // add position to pool
        _destinations.Add(_currentScreenPosition);
    }

    private float GetDistance(Vector3 from,Vector3 to)
    {
        // do not use the _currentScreenPosition but the real position of click
        _dist = Vector3.Distance(to, from);
        return _dist;
    }

    private void MoveAgent(NavMeshAgent agent)
    {
        // we move the agent to the destination using the pool
        if (_destinations.Count > 0)
        {
            agent.SetDestination(_destinations[0]);

            if (GetDistance(_destinations[0], transform.position) < 0.5f)
            {
                _destinations.RemoveAt(0);
            }
        }
    }
}
