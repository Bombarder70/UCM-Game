using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Player;
    public float TargetDistance;
    public float AllowedDistance = 3;
    public GameObject follower;
    public float FollowSpeed;
    public RaycastHit Shot;

    
    void Update()
    {
        transform.LookAt(Player.transform);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
        {
            TargetDistance = Shot.distance;

            if (TargetDistance >= AllowedDistance)
            {
                FollowSpeed = 0.1f;
            //    follower.GetComponent<Animation>().Play("running");
                Vector3 xxx = new Vector3 (0,1.7f,0);
                xxx += Player.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, xxx , FollowSpeed);
            }
            else
            {
                FollowSpeed = 0;
             //   follower.GetComponent<Animation>().Play();
            }
        }
    }
}
