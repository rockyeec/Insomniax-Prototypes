using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueTrigger : MonoBehaviour
{
    [SerializeField] float delay = 0.0f;
    [SerializeField] string[] monologue = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            if (other.gameObject.name != "Player")
                return;

            Invoke("StartSpeech", delay);
        }
    }

    void StartSpeech()
    {
        Queue<string> q = new Queue<string>();
        foreach (var item in monologue)
        {
            q.Enqueue(item);
        }
        MonologueScript.TriggerText(q);
        Destroy(transform.parent.gameObject);
    }
}
