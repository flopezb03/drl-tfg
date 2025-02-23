using UnityEngine;

public class Wall : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        Debug.Log("XXX");
        CarAgent agent = other.GetComponent<CarAgent>();

        agent.AddReward(-20f);
        agent.EndEpisode();
    }
}
