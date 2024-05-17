using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFolow : MonoBehaviour
{

    public Transform target; // Đối tượng mà camera sẽ theo dõi
    public Vector3 offset; // Khoảng cách giữa camera và đối tượng
    public float speed = 10;

   
    private void Start()
    {
        if (LevelSolection.instance != null)
        {           
            LevelSolection.instance.ActionCamraFoloww -= Fintagert;
            LevelSolection.instance.ActionCamraFoloww += Fintagert;
        }
    }
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
        }

    }
    private void Fintagert()
    {
        if (target == null)
        {
            target = LevelSolection.instance.currentPlayer.transform;
        }
    }
    
}
