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
        
        if(valid){
            Debug.Log("Check");
            SetInvalid();
            controller.CheckpointTriggered();
        }else
            other.GetComponent<CarAgent>().AddReward(-5f);
        
    }

    void SetInvalid(){
        valid = false;
        render.enabled = false;
        //  Desactivar ser visible por raycasts
    }
    public void Restart(){
        valid = true;
        render.enabled = true;
        //  Activar ser visible por raycasts
    }
}
