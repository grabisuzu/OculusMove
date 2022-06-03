using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private string groundTag = "Ground";
    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    //接地判定を返すメソッド
    //物理判定の更新毎に呼ぶ必要がある
    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    private void OnTriggerEnter(Collider collision)
    {
      
        //triggerは反応してるtagが問題かも
        if (collision.tag == groundTag)
        {
            
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        
        if (collision.tag == groundTag)
        {
            
            isGroundStay = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        
        if (collision.tag == groundTag)
        {
            
            isGroundExit = true;
        }
    }
}
