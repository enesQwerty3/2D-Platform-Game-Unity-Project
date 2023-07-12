using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    private Vector3 zAxisOffset = new Vector3(0f, 0f, -10f);  
    private Vector3 cameraVelocity = Vector3.zero;
    [Range(0f, 0.5f)]
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Transform playerTransform;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //Update is called once per frame
    void Update()
    {
        // lock camera somewhere near the spawn point if player pass this point camera followes player 
        Vector3 cameraLockPos = new Vector3(4f, playerTransform.position.y, -10f);
        Vector3 playerPosition = playerTransform.position + zAxisOffset; //player position + z dimension -10f vector to be able to see game scence
        
        if(playerTransform.position.x>=cameraLockPos.x)
            transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref cameraVelocity, smoothTime);

        else
            transform.position = Vector3.SmoothDamp(transform.position, cameraLockPos, ref cameraVelocity, smoothTime);

        //Debug.Log(transform.position);
        //Debug.Log("Camera velocity is:" + cameraVelocity);    
    }
}
