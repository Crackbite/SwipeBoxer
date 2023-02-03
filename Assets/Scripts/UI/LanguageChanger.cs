using System.Collections;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

#pragma warning disable CS0162
        if (YandexGamesSdk.IsInitialized == false)
        {
            yield break;
        }

        string lang = YandexGamesSdk.Environment.i18n.lang switch
        {
            "en" => "English",
            "tr" => "Turkish",
            _ => "Russian"
        };

        LeanLocalization.SetCurrentLanguageAll(lang);
#pragma warning restore CS0162
    }
}