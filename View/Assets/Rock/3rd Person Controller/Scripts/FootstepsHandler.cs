using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsHandler : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] string[] clipnames = null;
    [SerializeField] float maxInterval = 0.05f;
    float time = 0.0f;
    
    public void PlayFootstep()
    {
        if (animator.GetFloat("vertical") < 0.08f)
            return;

        if (Time.time < time)
            return;

        time = Time.time + maxInterval;

        AudioManager.instance.PlaySfx(clipnames.GetRandom());
    }
}
