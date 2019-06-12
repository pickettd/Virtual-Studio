using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class A_PaintScaler : MonoBehaviour {

    // paint_brush script
    A_PaintBrush pB;

    //size
    public float width;

	void Start () {
        pB = this.transform.parent.GetComponent<A_PaintBrush>();
	}

    void Update()
    {
        if (pB.controller != null)
        {
            if (pB.controller.GetPress(SDK_BaseController.ButtonTypes.Touchpad) && pB.controller.GetAxis(SDK_BaseController.ButtonTypes.Touchpad).y >= -0.4f && pB.controller.GetAxis(SDK_BaseController.ButtonTypes.Touchpad).y <= 0.4f)
            {
                Vector2 touchpad = (pB.controller.GetAxis(SDK_BaseController.ButtonTypes.Touchpad));
                //pB.SizeX = touchpad.x + 1;
                //  print("Pressing Touchpad" + touchpad.y * 10 + " x:" + touchpad.x * 10);
                pB.SizeSelection(touchpad.x + 1, "XYZ");
            }
            
        }
    }
 }

