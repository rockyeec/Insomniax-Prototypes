using System.Collections;
using TMPro;
using UnityEngine;

public class PoppingNames : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmp = null;
    private string text = string.Empty;
    private float durationBetweenLetters = 0.05f;
    private bool isTyped = false;



    private void Start()
    {
        text = tmp.text;
        tmp.text = string.Empty;
        TimeHandler.Enable();
    }

    public void TriggerTypeText()
    {
        if (isTyped)
            return;

        isTyped = true;
        StopAllCoroutines();
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        float time = Time.time + durationBetweenLetters;
        foreach (char item in text)
        {
            tmp.text += item;

            while (Time.time < time)
            {
                yield return null;
            }
            time = Time.time + durationBetweenLetters;
        }
    }

    private void FixedUpdate()
    {
        tmp.ForceMeshUpdate();
        Vector3[] vertices = tmp.mesh.vertices;
        int charCount = tmp.textInfo.characterCount;
        for (int i = 0; i < charCount; i++)
        {
            TMP_CharacterInfo charInfo = tmp.textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            int vertexIndex = charInfo.vertexIndex;
            Matrix4x4 matrix = Matrix4x4.TRS(
                (Vector3.right * TimeHandler.CosTime + Vector3.up * TimeHandler.SinTime) * ((i % 2) * 0.01f + 0.005f),
                Quaternion.Euler(0.0f, 0.0f, TimeHandler.CosTime * 1.337f),
                Vector3.one + Vector3.one * (0.01f) * TimeHandler.SinTime);

            for (int j = 0; j < 4; j++)
            {
                vertices[vertexIndex + j] = matrix.MultiplyPoint3x4(vertices[vertexIndex + j]);
                tmp.mesh.vertices = vertices;
            }
        }
    }
}
