using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
   private void FixedUpdate()
   {
      transform.rotation = CameraManager.Instance.MainCamera.transform.rotation;
   }
}
