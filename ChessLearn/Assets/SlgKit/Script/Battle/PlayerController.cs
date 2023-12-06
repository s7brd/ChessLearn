using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //移动力
    public int moveRange = 5;

    Animator animator;

    public bool canTraverseWater = false;

    public void Awake()
    {
        animator = this.GetComponent<Animator>();

        StartCoroutine(TestUpdate());
    }

    IEnumerator TestUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            ShowMoveRange();
        }
    }

    [ContextMenu("移动范围测试")]
    void Test()
    {
        ShowMoveRange();
    }

    public void ShowMoveRange(System.Action<List<GraphNode>> callback = null)
    {
        GetMovePath( (Path path) =>
            {
                GridMeshManager.Instance.ShowPath(path.path);
                if (callback != null) callback.Invoke(path.path);
            }
            );
    }

    /// <summary>
    /// 获取移动路径
    /// </summary>
    /// <param name="OnPathSerchOkCallBack"></param>
    public void GetMovePath(System.Action<Path> OnPathSerchOkCallBack)
    {
        var moveGScore = this.moveRange * 1000* 3;
      
        var SerchPath = MoveRangConStantPath.Construct(this.transform.position, moveGScore, canTraverseWater,
        (Path path) =>
        {
            path.path = (path as MoveRangConStantPath).allNodes;
            OnPathSerchOkCallBack.Invoke(path);

        }

        );
        //异步返回搜索结果
        AstarPath.StartPath(SerchPath, true);
    }
}
