using System;

using UnityEngine;


public class player : MonoBehaviour
{
    public static player Instance;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 30f;
    [SerializeField] private LayerMask layer_Wal;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject brickPrefab2;
    [SerializeField] private GameObject brickGround;
    [SerializeField] private GameObject listBricksReceive;
    [SerializeField] private Transform parentPositon;
    [SerializeField] private Transform playerVisual;

    private Vector3 mouseDownPosition;
    private Vector3 mouseUpPosition;
    private Vector3 stopPos;
    private Vector3 direction;
    private Vector3 directionPush;
    private Vector3 currentPosCast;
    private Vector3 lastPosition;
    private bool isStopPos = true;
    private bool isControl = true;
    private bool ispush = false;
    public bool isWin = false;
    public bool isloss = false;
    private int countBrickPlayer = 1;
    private float brickPlaceUp = 0;

    public Action WinAction;
    public Action LossAction;
    private void OnEnable()
    {
        Instance = this;
    }
    private void Start()
    {
        stopPos = transform.position;
    }
    void Update()
    {
        MoveController();
        SetDirection();
        Push();
    }

    public void AddBrick()
    {
        AddBrickGround();
        AddBrickPlayer();
    }

    public void AddBrickPlayer()
    {
        countBrickPlayer++;
    }
    public void RemoveBrick()
    {
        countBrickPlayer--;
    }
    public void ChangPositionPlayerVisual(float distance)
    {
        playerVisual.localPosition += new Vector3(0, distance, 0);
    }
    public void AddBrickGround()
    {
        brickGround = Instantiate(brickPrefab, parentPositon);
        Vector3 brickSize = brickGround.GetComponent<Renderer>().bounds.size;
        brickGround.transform.localPosition = new Vector3(0, brickPlaceUp + brickSize.y, 0);
        brickPlaceUp += brickSize.y;
        ChangPositionPlayerVisual(brickSize.y);

    }
    public void RemoveBrickGround()
    {
        int countBrickReceive = listBricksReceive.transform.childCount;
        if (countBrickReceive > 0)
        {
            GameObject brickPlayer = listBricksReceive.transform.GetChild(countBrickReceive - 1).gameObject;
            Vector3 bricksize = brickPlayer.GetComponent<Renderer>().bounds.size;
            Destroy(brickPlayer);
            brickPlaceUp -= bricksize.y;
            ChangPositionPlayerVisual(-bricksize.y);
        }
    }

    public void Win()
    {
        Destroy(listBricksReceive);
        playerVisual.localPosition = new Vector3(0, 0, 0);
        isWin = true;
        WinAction?.Invoke();
    }
    private void Loss()
    {
        if (isloss == true)
        {
            return;
        }
        LossAction?.Invoke();
    }
    public void SetDirection()
    {
        if (isControl == false)
        {
            Debug.Log("Khong vuot duoc");
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isStopPos = false;
            mouseUpPosition = Input.mousePosition;
            Vector3 swipeDirection = mouseUpPosition - mouseDownPosition;

            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                if (swipeDirection.x > 0)
                {
                    // Vuốt sang phảis
                    direction = Vector3.right;

                }
                else
                {
                    // Vuốt sang trái
                    direction = Vector3.left;

                }
            }
            else
            {
                if (swipeDirection.y > 0)
                {
                    // Vuốt lên trên
                    direction = Vector3.forward;

                }
                else
                {
                    // Vuốt xuống dưới
                    direction = Vector3.back;

                }
            }
            currentPosCast = transform.position;
            isStopPos = false;
        }
    }

    public void MoveController()
    {
        if (isStopPos == false)
        {
            //    StartRaycat startRay = Instantiate(startRaycatPrifab);
            //transform.position = currentPosCast;//vị trí tia ray được tạo ra
            isStopPos = IsFindWall(new Vector3(0, -1, 0));

            if (isStopPos == false)
            {
                stopPos = currentPosCast;
                currentPosCast += direction;//được cộng dần vị trí theo hướng đó           
            }
        }
        if (countBrickPlayer > 0)
        {
            if (MenuManager.Instance.Playing == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, stopPos, speed * Time.deltaTime);
            }

            if (lastPosition != transform.position)
            {
                isControl = false;
            }
            else
            {
                isControl = true;
            }
            lastPosition = transform.position;
        }
        else
        {
            rb.velocity = Vector3.zero;
            isStopPos = true;
            Loss();
            Debug.LogError("Thua");
        }

    }
    private void Push()
    {


    }
    public bool IsFindWall(Vector3 direction)
    {
        Ray ray = new Ray(currentPosCast, direction);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 3, Color.red, 1000f);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 3))
        {
            //Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject.tag == "Wall" || hitInfo.collider.gameObject.tag == "Chest")
            {
                return true;
            }
        }
        return false;
    }
    //public void ChangAnim(string animName)
    //{
    //    Debug.Log(currentAnim + " " + animName);
    //    if (currentAnim != animName)
    //    {
    //        anim.ResetTrigger(currentAnim);
    //        currentAnim = animName;
    //        anim.SetTrigger(currentAnim);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "unBrick")
        {
            if (countBrickPlayer > 0)
            {
                RemoveBrickGround();
                RemoveBrick();
            }
            GameObject addbrik = Instantiate(brickPrefab2);
            addbrik.transform.position = other.transform.position + new Vector3(0, 0.5f, 0);
        }
        if (other.gameObject.tag == "Win")
        {
            Win();
        }
        if (other.CompareTag("Push"))
        {
            Quaternion rotation = other.transform.rotation;

            // Tính toán hai hướng vuông góc bằng cách quay vector đơn vị
            Vector3 forwardDirection = rotation * Vector3.forward;           

            // Tính toán hai hướng trước mặt vuông góc bằng cách quay vector đơn vị 90 độ
            Vector3 perpendicularDirectionx = Quaternion.Euler(0, -90, 0) * forwardDirection;           


            currentPosCast = transform.position;
            isStopPos = false;

            if ((Mathf.Abs(perpendicularDirectionx.x) > Mathf.Abs(perpendicularDirectionx.y)))
            {
                if (perpendicularDirectionx.x > 0  )
                {
                    direction = Vector3.right;
                }
                else
                {
                    direction = Vector3.left;
                }
            }
            else
            {
                if (perpendicularDirectionx.y > 0 )
                {
                    direction = Vector3.forward;
                }
                else
                {
                    direction = Vector3.back;
                }
            }                       
        }
    }
}

