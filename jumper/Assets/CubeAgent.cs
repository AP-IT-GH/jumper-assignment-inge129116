using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class CubeAgent : Agent
{
    public Transform Obstacle;
    public Transform gift;
    public float jumpForce = 10f;
    Rigidbody rb;
    private int vooruit;
    private float obstacleSpeed;

  
   
        
       
    

    public override void OnEpisodeBegin()
    {
        rb = GetComponent<Rigidbody>();
        transform.localPosition = new Vector3(20f, 0.55f, 0f);

        startNewObstacle(Obstacle);


    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent positie
        sensor.AddObservation(this.transform.localPosition);

        
    }

    
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        int jumpAction = actionBuffers.DiscreteActions[0];
        //Debug.Log("jumpAction: " + jumpAction); (als dit niet werkt discrete actie instellen in unity)
        if (jumpAction == 1 && transform.position.y <= 0.6f)
        {
            SetReward(-0.2f);
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);


        }

        if(transform.position.y <= -1 )
        {
            transform.localPosition = new Vector3(4f, 0.55f, 0f);
        }
        if (vooruit < 5)
        {
            
            Obstacle.localPosition += Vector3.right * obstacleSpeed * Time.deltaTime;

            if (Obstacle.localPosition.x >= (5f + transform.localPosition.x))
            {
                
                SetReward(7.0f);
                startNewObstacle(Obstacle);
            }
        }
        else
        {

            gift.localPosition += Vector3.right * obstacleSpeed * Time.deltaTime;

            if (gift.localPosition.x >= (5f + transform.localPosition.x))
            {

                startNewObstacle(gift);
            }
        }



    }
    private void startNewObstacle(Transform Obstacles)
    {
        Obstacles.localPosition = new Vector3(-4f, Obstacles.localPosition.y, Obstacles.localPosition.z);
        vooruit = Random.Range(0, 9);
        obstacleSpeed = Random.Range(3, 6);
    }

    private void OnTriggerEnter(Collider obst)

    {

        
        if (obst.tag == "hindernis")
        {
            SetReward(-7.0f);
            EndEpisode();

        }
        if (obst.tag == "beloning")
        {
            SetReward(7f);

        }
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;

        // Als de spatiebalk wordt ingedrukt, stel actie 1 in (springen), anders actie 0 (geen actie)
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

}
