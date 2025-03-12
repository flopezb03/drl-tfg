using UnityEngine;

public class Wall : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        CarAgent agent = other.GetComponent<CarAgent>();

        agent.AddReward(-1f);
        agent.EndEpisode();
    }
}
