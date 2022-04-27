using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    
    [SerializeField]
    GameObject skeleton;
    [SerializeField]
    GameObject greenSkeleton;
    [SerializeField]
    GameObject redSkeleton;
    [SerializeField]
    GameObject player;
    int count = 0;
    Vector3 spawnPosition;
    int delay;
    
    
    

    void Start()
    {
        StartCoroutine(SpawnSkeletons());
    }

    IEnumerator SpawnSkeletons()
    {
        while(true)
        {
            Debug.Log("Spawn");
            count = Random.Range(1,3);
            delay = Random.Range(120,180);
            yield return new WaitForSeconds(delay);
            spawnPosition = new Vector3(player.transform.position.x + Random.Range(-12,12), player.transform.position.y, player.transform.position.z+Random.Range(-12,12));
            for(int i = 0; i < count; i++)
            {
                Instantiate(skeleton, spawnPosition, Quaternion.identity);
            }
            Instantiate(greenSkeleton,spawnPosition,Quaternion.identity);
            
        }
        
    }
}
