using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


public enum Direct 
{
    None = 0,
    Right = 1,
    Forward = 2,
    Left = 3,
    Back = 4
}
public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask moveLayer;    
    [SerializeField] private LayerMask lineLayer;    
    [SerializeField] private float speed;
    [SerializeField] private Transform playerSkin;
    [SerializeField] private LayerMask pushLayer;
    [SerializeField] private Transform boxBrick;
    [SerializeField] private Animator Anim;
    [SerializeField] private Transform PointRay;

    private List<Brick> playerBricks = new List<Brick>();
    private int currentAnim;

    private Vector3 mouseDown, mouseUp;    
    private Vector3 movePoint;
    public Vector3 MovePoint
    {
        get { return movePoint; } 
        set
        {
            movePoint = value;
        }
    }

    private bool isMove;
    private bool isControl;
    private Direct currentDirect;
    private Vector3 currentDir;

    public Level level;

    // Start is called before the first frame update

    private void Start()
    {
        OnInit();
    }
    public  void OnInit()
    {
        isMove = false;
        isControl= false;
        movePoint = transform.position;
        ResetAnim();
        ClearBrick();

    }



    // Update is called once per frame
    void Update()
    {
        if (!isMove)
        {
            HandleMouseInput();
        }
        else
        {
            Move();
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseUp = Input.mousePosition;
            currentDirect = GetDirect(mouseDown, mouseUp);
            if (currentDirect != Direct.None)
            {
                movePoint = GetNextPoint(currentDirect);
                isMove = true;
            }
        }
    }

    private void Move()
    {
        
        if (Vector3.Distance(transform.position, movePoint) < 0.1f)
        {
            isMove = false;
            if (Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, 10f, pushLayer))
            {
                currentDirect = GetPushDirect(currentDirect);
                movePoint = GetNextPoint(currentDirect);

            }

        }
        transform.position = Vector3.MoveTowards(transform.position, movePoint, Time.deltaTime * speed);
    }
    private Direct GetDirect(Vector3 mouseDown, Vector3 mouseUp)
    {
        float delayX = mouseUp.x - mouseDown.x;
        float delayY = mouseUp.y - mouseDown.y;
        if (Vector3.Distance(mouseDown, mouseUp) < 100)
        {
            return Direct.None;
        }
        else
        {
            if (Mathf.Abs(delayY) > Mathf.Abs(delayX))
            {
                if (delayY > 0)
                {
                    return Direct.Forward;
                }
                else
                {
                    return Direct.Back;
                }
            }
            else
            {
                if (delayX > 0)
                {
                    return Direct.Right;
                }
                else
                {
                    return Direct.Left;
                }
            }
        }
    }

    private Vector3 GetNextPoint(Direct direct)
    {

        RaycastHit hit;
        Vector3 nextPoint = transform.position;
        Vector3 dir = Vector3.zero;

        switch (direct)
        {
            case Direct.Forward:
                dir = Vector3.forward; break;
            case Direct.Back:
                dir = Vector3.back; break;
            case Direct.Left:
                dir = Vector3.left; break;
            case Direct.Right:
                dir = Vector3.right; break;
            case Direct.None:
                break;
            default:
                break;
        }

        
        for (int i = 1; i < 100; i++)
        {
            Debug.DrawRay(transform.position + dir * i + Vector3.up * 2, Vector3.down,  Color.red,10f);
            if (Physics.Raycast(transform.position + dir * i + Vector3.up * 2, Vector3.down, out hit, 10f, lineLayer))
            {
                Line line = hit.collider.GetComponent<Line>();
                if (!line.isCollect && playerBricks.Count <= 0)
                {
                    return nextPoint;
                }
            }
            if (Physics.Raycast(transform.position + dir*i + Vector3.up * 2, Vector3.down, out hit, 10f, moveLayer))
            {
                nextPoint = hit.collider.transform.position;
            }
            else
            {
                break;
            }
            
        }
        return nextPoint;

    }

    public void Stop()
    {

        movePoint = transform.position;
    }

    private Direct GetPushDirect(Direct Direct)
    {
        int dir = (int)Direct;
        float angle = 360f / 4;
        for(int i = 1; i <=4; i++)
        {
            if(i == dir || Mathf.Abs(dir -i)==2)
            {
                continue;
            }
            float radian = angle *(i-1) * Mathf.Deg2Rad;
            Vector3 check = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian)).normalized;
            if (Physics.Raycast(transform.position + check + Vector3.up *2, Vector3.down, 10f, moveLayer))
            {
                isMove = true;
                return (Direct)i;
            }
        }
        return Direct.None;

    }

    public void AddBrick()
    {
        ChangAnim(1);
        LevelManager.Instance.totalBrick++;
        int index = playerBricks.Count;
        Brick brick = SimplePool.Spawn<Brick>(PoolType.BrickPlayer, this.transform.position, Quaternion.Euler(-90f,0,-180f));
        brick.transform.localPosition = (index + 1) * Constant.HEIGHT_BRICK * Vector3.up;
        playerBricks.Add(brick);
        if (LevelManager.Instance.totalBrick >1)
        {
            playerSkin.localPosition = playerSkin.localPosition + Vector3.up * Constant.HEIGHT_BRICK;
        }
        Invoke(nameof(ResetAnim), 0.3f);
    }

    public void RemoveBrick()
    {
        LevelManager.Instance.totalBrick--;
        int index = playerBricks.Count - 1;
        if (index >= 0)
        {
            Brick brick = playerBricks[index];
            playerBricks.RemoveAt(index);
            brick.OnDespawn();
            if (LevelManager.Instance.totalBrick >0)
            {
                playerSkin.localPosition = playerSkin.localPosition -Vector3.up * Constant.HEIGHT_BRICK;
            }        
        }

    }
    public void ClearBrick()
    {
        for(int i = 0; i < playerBricks.Count; i++)
        {
            playerBricks[i].OnDespawn();
        }
        playerBricks.Clear();
    }
    public void ChangAnim(int newAnim)
    {
        Anim.SetInteger("swap", newAnim);
    }
    public void ResetAnim() 
    {
        ChangAnim(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_FINISH))
        {
            ChangAnim(2);
            playerSkin.localPosition -= Vector3.up * playerBricks.Count * Constant.HEIGHT_BRICK;
            ClearBrick();
            level.WinGame();
        }
        if (other.CompareTag(Constant.TAG_DIAMOND))
        {
            other.gameObject.SetActive(false);    
        }
    }

}





