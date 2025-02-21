using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Sensors.Reflection;
using Unity.MLAgents.Demonstrations;
using Unity.MLAgents.Policies;

public class CarAgent : Agent
{
    private Rigidbody rb;
    private CarController carController;

    public override void Initialize()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        carController = gameObject.GetComponent<CarController>();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float gasIn = actions.ContinuousActions[0];
        float steeringIn = actions.ContinuousActions[1];
        

        carController.getInput(steeringIn,gasIn);



    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> actions = actionsOut.ContinuousActions;
        actions[0] = Input.GetAxis("Vertical");
        actions[1] = Input.GetAxis("Horizontal");
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }
}
