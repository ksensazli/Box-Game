using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaterialMover : MonoBehaviour
{
   [SerializeField] private MeshRenderer _meshRenderer;

   private void FixedUpdate()
   {
      var UVoffset = _meshRenderer.material.GetTextureOffset("_AlbedoMap");
      UVoffset+= Vector2.down*Time.fixedDeltaTime * 2 ;

      _meshRenderer.material.SetTextureOffset("_AlbedoMap", UVoffset);
   }
}
