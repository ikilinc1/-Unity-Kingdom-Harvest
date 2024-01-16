using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnAnimationStarter : MonoBehaviour
{
    public Animation endTurnAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void PlayAnimation()
    {
        if (endTurnAnimation)
        {
            endTurnAnimation.Play("EndTurnTextAnim");
        }
    }
}
