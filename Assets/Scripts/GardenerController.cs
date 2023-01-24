using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GardenerController : MonoBehaviour
{
    // get input action ref to read screen position from
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask raycastMask;
    // Get screen current position
    private Vector3 _currentScreenPosition;
    private Queue<Command> commands = new Queue<Command>();
    private Command currentCommand = null;
    private float _dist;
    private NavMeshAgent agent;

    private void Start()
    {
        // we get the agent
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ProcessCommand();
        ExecuteCommand();

    }

    private void ProcessCommand()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // we add the position to the pool
            AddMoveCommand();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            AddTextCommand();

        }
    }

    private void AddTextCommand()
    {
        commands.Enqueue(new TextCommand());
    }

    // stores the position in a pool
    private void AddMoveCommand()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, raycastMask))
        {
            Vector3 point = hit.point;
            MoveCommand moveCommand = new MoveCommand(agent,hit.point);
            commands.Enqueue(moveCommand);
        }

        ////Version sans raycast
        //// we get position
        //Vector3 transformInCameraSpace = _camera.transform.InverseTransformPoint(transform.position);
        //Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transformInCameraSpace.z);
        //_currentScreenPosition = _camera.ScreenToWorldPoint(screenPoint);

        //// add position to pool
        //_destinations.Add(_currentScreenPosition);
    }


    private void ExecuteCommand()
    {
        //Si j'ai une commande en cours je fais rien
        if (currentCommand != null && !currentCommand.IsFinished() ) return;
        
        // Si je n'ai pas de commandes à exécuter je fais rien
        if (commands.Count == 0) return;

        // Sinon
        currentCommand = commands.Dequeue();
        currentCommand.Execute();
    }
}

public abstract class Command
{
    public abstract void Execute();
    public abstract bool IsFinished();
}

public class MoveCommand : Command
{
    private NavMeshAgent agent;
    private Vector3 destination;

    public MoveCommand(NavMeshAgent agent, Vector3 destination)
    {
        this.agent = agent;
        this.destination = destination;
    }

    public override void Execute()
    {
        agent.destination = destination;
    }

    public override bool IsFinished()
    {
        return agent.remainingDistance < 0.1f;
    }
}

public class TextCommand : Command
{
    public override void Execute()
    {
        Debug.Log("SALUT!");
    }
    public override bool IsFinished()
    {
        return true;
    }
}
