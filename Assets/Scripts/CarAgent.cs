using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Sensors.Reflection;
using Unity.MLAgents.Demonstrations;
using Unity.MLAgents.Policies;

public class CarAgent : Agent
{
    private CarController carController;
    private CheckpointController checkpointController;

    public override void Initialize()
    {
        carController = GetComponent<CarController>();
        checkpointController = GetComponent<CheckpointController>();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Datos normalizados para mayor estabilidad en el entrenamiento
        Vector3 norm_speed = carController.speed/carController.maxSpeed;
        sensor.AddObservation(transform.forward);
        sensor.AddObservation(norm_speed);
        sensor.AddObservation(carController.slipAngle/180-1);
        sensor.AddObservation(carController.steerAngle/180-1);
        sensor.AddObservation((float)carController.durability/10);



        //  Recompensar llegar antes y tener velocidad
        AddReward(-0.0005f);
        AddReward(Mathf.Lerp(0.001f,0.0005f,norm_speed.magnitude));
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
        checkpointController.Restart();
        carController.InitPosition();
    }
}
