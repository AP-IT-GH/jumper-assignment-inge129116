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
    public float obstacleMaxY = 0.55f;
    public float obstacleMinY = 0.55f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
       
    }

    public override void OnEpisodeBegin()
    {
       
        transform.localPosition = new Vector3(4f, 0.55f, 0f);
        transform.localRotation = Quaternion.identity;
        Obstacle.localPosition = new Vector3(-4f, Obstacle.localPosition.y, Obstacle.localPosition.z);
    
        Obstacle.localRotation = Quaternion.identity;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent positie
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(Obstacle.localPosition);
    }

    
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float jumpAction = actionBuffers.ContinuousActions[0];

        if (jumpAction > 0.5f && transform.position.y <= 0.6)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if(transform.position.y >= 2)
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
        float obstacleSpeed = 2f;
        Obstacle.localPosition += Vector3.right * obstacleSpeed * Time.deltaTime;

        // Reset the obstacle's position if it reaches the end of its path
        if (Obstacle.localPosition.x >= 5f)
        {
            Obstacle.localPosition = new Vector3(-4f, Obstacle.localPosition.y, Obstacle.localPosition.z);
            SetReward(1.0f);
            EndEpisode();
        }

        Obstacle.localPosition = new Vector3(
            Obstacle.localPosition.x,
            Mathf.Clamp(Obstacle.localPosition.y, obstacleMinY, obstacleMaxY),
            0f); // Keep the Z position constant at 0

        //beloning
        if (Vector3.Distance(this.transform.localPosition, Obstacle.localPosition) < 1f)
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
