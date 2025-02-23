using System;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private Boolean valid = true;
    private CheckpointController controller;
    private MeshRenderer render;

    void Start(){
        controller = GetComponentInParent<CheckpointController>();
        render = GetComponentInChildren<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other){
        CarAgent carAgent = other.GetComponent<CarAgent>();
        
        if(valid){
            SetInvalid();
            controller.CheckpointTriggered();
            carAgent.AddReward(5f);
        }else
            carAgent.AddReward(-5f);
        
    }

    void SetInvalid(){
        valid = false;
        gameObject.tag = "InvalidCheckpoint";
        render.enabled = false;
    }
    public void Restart(){
        valid = true;
        gameObject.tag = "Checkpoint";
        render.enabled = true;
    }
}
