using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueTrigger : MonoBehaviour
{
    [SerializeField] string saveEntry = string.Empty;
    [SerializeField] float delay = 0.0f;
    [SerializeField] string[] monologue = null;
    [SerializeField] bool isSave = true;
    [SerializeField] bool isRepeatedEveryGame = false;

    private void Start()
    {
        if (SaveSystem.GetBool(saveEntry))
        {
            KillSelf();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            if (other.gameObject.name != "Player")
                return;
            
            if (isSave)
                other.GetComponent<SaveManager>().SavePlayer();

            StartCoroutine(StartSpeech());
        }
    }

    IEnumerator StartSpeech()
    {
        while (Time.timeScale == 0.0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(delay);
        Queue<string> q = new Queue<string>();
        foreach (var item in monologue)
        {
            q.Enqueue(item);
        }
        MonologueScript.TriggerText(q);
        SaveSystem.SetBool(saveEntry, !isRepeatedEveryGame);
        KillSelf();
    }

    void KillSelf()
    {
        Destroy(transform.parent.gameObject);
    }
}
