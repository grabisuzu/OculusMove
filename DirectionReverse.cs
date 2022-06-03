using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionReverse : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform CenterEyeAnchor;
    float eyeAngle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        eyeAngle = CenterEyeAnchor.transform.localEulerAngles.y;
        Vector3 changeRotate = new Vector3(0f, -eyeAngle, 0f);
        this.transform.localEulerAngles = changeRotate;
    }
    //-eyeangle‚ª”{—Ê“®‚­’¼‚·
}
