using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private const float TIMEOUT = 180f;
    private CarAgent carAgent;

    private float time = TIMEOUT;
    private int checkpointsNumber;
    private Dictionary<Checkpoint,Boolean> cpDict = new Dictionary<Checkpoint, bool>();

    void Awake(){
        carAgent = GetComponent<CarAgent>();
        
        foreach(Checkpoint cp in GameObject.Find("Checkpoints").GetComponentsInChildren<Checkpoint>()){
            cpDict.Add(cp, true);
        }
        checkpointsNumber = cpDict.Keys.Count;
    }

    void Update(){
        time -= Time.deltaTime;
        
        if (time <= 0){
            time = TIMEOUT;
            carAgent.AddReward(-1f);
            carAgent.EndEpisode();
        }
    }

    public void Restart(){
        foreach(Checkpoint cp in cpDict.Keys.ToList()){
            cpDict[cp] = true;
        }
        checkpointsNumber = cpDict.Keys.Count;
        time = TIMEOUT;
    }
    public void CheckpointTriggered(Checkpoint cp){
        if(cpDict[cp]){
            carAgent.AddReward(0.2f);

            cpDict[cp] = false;
            checkpointsNumber--;
            if(checkpointsNumber <= 0)
                carAgent.EndEpisode();
        }else{
            carAgent.AddReward(-1f);
            carAgent.EndEpisode();
        }
    }
}
