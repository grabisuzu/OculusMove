using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class JoyStickMove : MonoBehaviour
{
    public float directionSpeed = 30.0f;
    //public GameObject move;
    float eyeAngle;
    float stickRTimeY = 0f;
    bool resetDirection = false;
    float resetTime = 0f;
    void Update()
    {
        //Movement();
        //ChangeDirection();
        DualChangeDirection();
        //transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

    void Movement()
    {
        //this.transform.position = new Vector3 (move.transform.position.x, transform.position.y, move.transform.position.z);
        //HMDのY軸の角度取得
        //Vector3 changeRotation = new Vector3(0, InputTracking.GetLocalRotation(XRNode.Head).eulerAngles.y, 0);
        //OVRCameraRigの位置変更
        //this.transform.position += this.transform.rotation * (Quaternion.Euler(changeRotation) * changePosition);

    }
    //void ChangeDirection()
    //{
        //eyeAngle = CenterEyeAnchor.transform.localEulerAngles.y;
        //Vector3 changeRotate = new Vector3(0, -eyeAngle, 0);
        //Vector3 headRotation = new Vector3(0, InputTracking.GetLocalRotation(XRNode.Head).eulerAngles.y, 0);
        //Vector3 changeRotate2 = new Vector3(0.0f, 0.0f, 0.0f);
        //if (headRotation != changeRotate2)
        //{
        //    if(headRotation.y > changeRotate2.y)
        //    {
        //        changeRotate2.y = headRotation.y+5;
        //        Debug.Log(headRotation + "head");
        //        Debug.Log(changeRotate2+"chan");
        //    }

        //}
        //transform.rotation = Quaternion.Euler(0, headRotation.y, 0);
        //this.transform.eulerAngles = (changeRotate);

    //}
        void DualChangeDirection()
    {
        
        float DualstickRvertical = Input.GetAxis("Vertical Stick-R");
        Vector2 stickR = new Vector2(0f, DualstickRvertical);
        bool buttonR2 = Input.GetButtonDown("Fire_R2");
        float stickTime = stickR.y * directionSpeed * Time.deltaTime;
        if (stickRTimeY + stickTime > 90f)
        {
            stickRTimeY = 90f;
        }
        else if (stickRTimeY + stickTime < -90f)
        {
            stickRTimeY = -90f;
        }
        else
        {
            stickRTimeY += stickTime;
        }
        
        if (buttonR2)
        {
            resetDirection = true;
            float resetDirectionRotate = stickRTimeY;
        }
        else if (DualstickRvertical != 0)
        {
            resetDirection = false;
            resetTime = 0f;
        }
        if (resetDirection)
        {
            resetTime += 0.005f;
            stickRTimeY = Mathf.Lerp(stickRTimeY, 0, resetTime);
        }
        //座標や角度をローカル指定するときは単語の先頭にlocalをつける
        Vector3 changeRotate = new Vector3(stickRTimeY, 0f, 0f);
        this.transform.localEulerAngles = changeRotate;
    }
}