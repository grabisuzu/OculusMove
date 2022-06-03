using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class move : MonoBehaviour
{
    public GameObject CenterEyeAnchor;
    public float limitedSpeed;
    public float directionSpeed = 30.0f;
    public GroundCheck ground;

    float eyeAngle;
    float stickRTime = 0f;
    float stickRTimeX = 0f;
    float stickRTimeY = 0f;
    int jumpCount = 0;
    bool buttonOne = false;
    bool isGround = false;

    void LateUpdate()
    {
        Move();
        //MoveFoward();
        //使用するコントローラーによってどちらかをコメントアウト
        //DualChangeDirection();
        eyeDirection();
        Jump();
        //transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

    void Move()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        //右ジョイスティックの情報取得
        Vector2 stickL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        float DualstickLhorizontal = Input.GetAxis("Horizontal Stick-L");
        float DualstickLverticall = Input.GetAxis("Vertical Stick-L");
        stickL = new Vector2(DualstickLhorizontal, -DualstickLverticall);
        //Vector3 changePosition = new Vector3(stickL.x * 10, 0, stickL.y * 10);
        rb.AddForce(transform.forward * stickL.y * 30, ForceMode.Force);
        rb.AddForce(transform.right * stickL.x * 30, ForceMode.Force);
        if(rb.velocity.magnitude >= limitedSpeed || rb.velocity.magnitude <= -limitedSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x / 1.2f, rb.velocity.y, rb.velocity.z / 1.2f);
        }
        if(stickL.x==0 && stickL.y == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x / 1.2f, rb.velocity.y, rb.velocity.z / 1.2f);
        }
    }

    void MoveFoward()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        //右ジョイスティックの情報取得
        
        float DualstickLhorizontal = Mathf.Abs(Input.GetAxis("Horizontal Stick-L"));
        float DualstickLvertical = Mathf.Abs(Input.GetAxis("Vertical Stick-L"));
        Vector2 stickL = new Vector2(DualstickLhorizontal, -DualstickLvertical);
        float stickPower = DualstickLhorizontal + DualstickLvertical;
        //Vector3 changePosition = new Vector3(stickL.x * 10, 0, stickL.y * 10);
        rb.AddForce(transform.forward * stickPower * 30, ForceMode.Force);
        
        if (rb.velocity.magnitude >= limitedSpeed || rb.velocity.magnitude <= -limitedSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x / 1.2f, rb.velocity.y, rb.velocity.z / 1.2f);
        }
        if (stickL.x == 0 && stickL.y == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x / 1.2f, rb.velocity.y, rb.velocity.z / 1.2f);
        }
    }

    void Jump()
    {
        isGround = ground.IsGround();
        Rigidbody rb = this.GetComponent<Rigidbody>();
        buttonOne = OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch);
        buttonOne = Input.GetButtonDown("Fire_1");
        if (isGround)
        {
            jumpCount = 1;
        }
        if (buttonOne && jumpCount>0)
        {
            //Debug.Log(jumpCount);
            rb.velocity = new Vector3(0.0f, 5.0f, 0.0f);
            jumpCount -= 1;
        }
    }

    void DualChangeDirection()
    {
        float DualstickRhorizontal = Input.GetAxis("Horizontal Stick-R");
        float DualstickRverticall = Input.GetAxis("Vertical Stick-R");
        Vector2 stickR = new Vector2(DualstickRhorizontal, DualstickRverticall);
        bool buttonR2 = Input.GetButtonDown("Fire_R2");
        stickRTimeX += stickR.x * directionSpeed * Time.deltaTime;
        stickRTimeY += stickR.y * directionSpeed * Time.deltaTime;
        Vector3 changeRotate = new Vector3(stickRTimeY, stickRTimeX, 0f);
        this.transform.eulerAngles = changeRotate;
        if (buttonR2)
        {
            stickRTimeY = 0.0f;
        }
        
    }

    void eyeDirection()
    {
        Vector2 stickR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        float DualstickRhorizontal = Input.GetAxis("Horizontal Stick-R");
        eyeAngle = CenterEyeAnchor.transform.localEulerAngles.y;
        stickRTime += stickR.x * Time.deltaTime * directionSpeed;
        stickRTime += DualstickRhorizontal * Time.deltaTime * directionSpeed;
        Vector3 changeRotate = new Vector3(0f, eyeAngle+stickRTime, 0f);
        this.transform.eulerAngles = changeRotate;
    }
}
//移動、スティック回転時視界がブレる
//子オブジェクトの追従が1F遅れているorフレームレートが垂直同期していない？
//壁に跳ね返り機能をつける、壁接触時摩擦0に変更等で壁張り付きに対応