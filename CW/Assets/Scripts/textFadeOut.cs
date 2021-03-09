using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Fades the selected text out over a set time

class textFadeOut : MonoBehaviour
{
    public float fadeOutTime;   // Fade time in seconds
    public void FadeOut()
    {
        //Debug.Log("Im being called");
        StartCoroutine(FadeOutRoutine());
    }
    private IEnumerator FadeOutRoutine()
    {
        Text text = GetComponent<Text>();
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }
}