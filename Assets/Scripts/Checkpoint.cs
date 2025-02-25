using System;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{


    void OnTriggerEnter(Collider other){
        other.GetComponent<CheckpointController>().CheckpointTriggered(this);
        
    }

}
