using Agava.WebUtility;
using UnityEngine;

public class InBackgroundMonitor : MonoBehaviour
{
    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        EnableAudio(hasFocus);
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        EnableAudio(inBackground == false);
    }

    private void EnableAudio(bool value)
    {
        AudioListener.pause = value == false;
        AudioListener.volume = value ? 1f : 0f;
    }
}