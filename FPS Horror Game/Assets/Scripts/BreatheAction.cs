using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BreatheAction : MonoBehaviour
{
   public abstract void OnFocusEnemy();

   public abstract void OnLoseFocusEnemy();
   
   public void Awake()
   {
      gameObject.layer = 10;
   }
}
