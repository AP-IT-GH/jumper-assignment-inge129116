using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class CubeAgent : Agent
{
    public Transform Obstacle;
    public Transform Obstacle2;
    public float jumpForce = 10f;
    Rigidbody rb;
    private int vooruit;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
       
    }

    public override void OnEpisodeBegin()
    {
       
        transform.localPosition = new Vector3(4f, 0.55f, 0f);
        transform.localRotation = Quaternion.identity;
        Obstacle.localPosition = new Vector3(-4f, Obstacle.localPosition.y, Obstacle.localPosition.z);
        Obstacle2.localPosition = new Vector3(4f, Obstacle2.localPosition.y, -10f);
        Obstacle2.localRotation = Quaternion.identity;
        Obstacle.localRotation = Quaternion.identity;
        vooruit = 6; //Random.Range(0, 9);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent positie
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(Obstacle.localPosition);
        sensor.AddObservation(Obstacle2.localPosition);
    }

    
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float jumpAction = actionBuffers.ContinuousActions[0];

        if (jumpAction > 0.5f && transform.position.y <= 0.6)
        {
            SetReward(-0.5f);

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if(transform.position.y >= 3)
            {

                rb.AddForce(-Vector3.up * jumpForce, ForceMode.Impulse);
                if (transform.position.y <= 0)
                {
                    rb.AddForce(Vector3.up*0, ForceMode.Impulse);


                }
            }
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);


        }

        if(transform.position.y <= -1 )
        {
            transform.localPosition = new Vector3(4f, 0.55f, 0f);
        }
        if (vooruit < 5)
        {
            float obstacleSpeed = Random.Range(1, 6); ;
            Obstacle.localPosition += Vector3.right * obstacleSpeed * Time.deltaTime;

            // Reset the obstacle's position if it reaches the end of its path
            if (Obstacle.localPosition.x >= 5f)
            {
                Obstacle.localPosition = new Vector3(-4f, Obstacle.localPosition.y, Obstacle.localPosition.z);
                SetReward(2.0f);
                EndEpisode();
            }
        }
        else
        {
            float obstacleSpeed2 = Random.Range(1, 6); ;

            Obstacle2.localPosition += Vector3.forward * obstacleSpeed2 * Time.deltaTime;

            // Reset the obstacle's position if it reaches the end of its path
            if (Obstacle2.localPosition.z >= 5f)
            {
                Obstacle2.localPosition = new Vector3(4f, Obstacle2.localPosition.y, -5);
                SetReward(1.0f);
                EndEpisode();
            }
        }
        //beloning
        if (Vector3.Distance(this.transform.localPosition, Obstacle.localPosition) < 1f || Vector3.Distance(this.transform.localPosition, Obstacle2.localPosition) < 1f)

        {
            SetReward(-1.0f);
            EndEpisode();
        }

        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1f : 0f;
    }

}
