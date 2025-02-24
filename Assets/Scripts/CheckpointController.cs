using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public CarAgent carAgent;
    private List<Checkpoint> checkpoints;

    private float time = 60f;
    private int checkpointsNumber;

    void Start(){
        checkpoints = new List<Checkpoint>(GetComponentsInChildren<Checkpoint>());
        checkpointsNumber = checkpoints.Count;
    }

    void Update(){
        time -= Time.deltaTime;

        if (time <= 0){
            time = 60f;
            carAgent.AddReward(-20f);
            carAgent.EndEpisode();
        }
    }

    public void Restart(){
        checkpointsNumber = checkpoints.Count;
        foreach(Checkpoint c in checkpoints)
            c.Restart();
    }
    public void CheckpointTriggered(){
        checkpointsNumber--;
        if(checkpointsNumber <= 0)
            carAgent.EndEpisode();
    }
}
