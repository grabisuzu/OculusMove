using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private string groundTag = "Ground";
    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    //�ڒn�����Ԃ����\�b�h
    //��������̍X�V���ɌĂԕK�v������
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
      
        //trigger�͔������Ă�tag����肩��
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
