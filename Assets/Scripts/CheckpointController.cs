using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    private  float timeout;
    private CarAgent carAgent;

    private float time;
    private int checkpointsNumber;
    private Dictionary<Checkpoint,Boolean> cpDict = new Dictionary<Checkpoint, bool>();

    void Awake(){
        carAgent = GetComponent<CarAgent>();
        Transform circuit = transform.parent.parent;
        Transform checkpoints = circuit.Find("Checkpoints");
        
        foreach(Checkpoint cp in checkpoints.GetComponentsInChildren<Checkpoint>()){
            cpDict.Add(cp, true);
        }
        checkpointsNumber = cpDict.Keys.Count;


        if(SceneManager.GetActiveScene().name == "Forward")
            timeout = 120f;
        else if(SceneManager.GetActiveScene().name == "Reorientation")
            timeout = 30;
        else if(SceneManager.GetActiveScene().name == "CurvesLarge")
            timeout = 20;
        else if(SceneManager.GetActiveScene().name == "CurvesMedium")
            timeout = 15f;
        else if(SceneManager.GetActiveScene().name == "Curves" || SceneManager.GetActiveScene().name == "CurvesD1")
            timeout = 15f;
        else if(SceneManager.GetActiveScene().name == "Circuit_1")
            timeout = 15f;
        else if(SceneManager.GetActiveScene().name == "Hills")
            timeout = 10f;
        else if(SceneManager.GetActiveScene().name == "Circuit_2")
            timeout = 15f;

        time = timeout;
    }

    void Update(){
        time -= Time.deltaTime;
        
        if (time <= 0){
            time = timeout;
            carAgent.AddReward(-1f);
            carAgent.EndEpisode();
        }
    }

    public void Restart(){
        foreach(Checkpoint cp in cpDict.Keys.ToList()){
            cpDict[cp] = true;
        }
        checkpointsNumber = cpDict.Keys.Count;
        time = timeout;
    }
    public void CheckpointTriggered(Checkpoint cp){
        if(cpDict[cp]){
            if(SceneManager.GetActiveScene().name == "Reorientation")
                carAgent.AddReward(1f);
            else
                carAgent.AddReward(0.2f);

            time = timeout;
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
