using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    public GameObject targetVisual;
    GameObject instantiatedTargetVisual;
    public void EnableTargetedVisual(bool show)
    {
        if (show)
        {
            instantiatedTargetVisual = Instantiate(targetVisual);
            instantiatedTargetVisual.transform.position = gameObject.transform.position + new Vector3(0,0,0.01f);
        } else
        {
            Destroy(instantiatedTargetVisual);
        }
    }
    public virtual void WhenSelectedBy(DiceMob selector)
    {

    }
}
