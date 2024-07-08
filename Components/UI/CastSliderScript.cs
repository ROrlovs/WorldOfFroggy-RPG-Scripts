using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastSliderScript : MonoBehaviour
{

    Slider slider;
    GameObject parent;
    TextMeshProUGUI tmp;

void Awake()
{
    tmp = GetComponentInChildren<TextMeshProUGUI>();
    parent = transform.parent.gameObject;
    slider = GetComponent<Slider>();
}

private void Start()
{
    PlayerCastManager.Instance.onPlayerStartCast += StartSlider;
    parent.SetActive(false);
}

private void StartSlider(float castTime, string abilityName)
{
    //Debug.Log("start slider");
    parent.SetActive(true);
    tmp.text = abilityName;
    //Debug.Log("starting slidercasting coroutine with "+castTime);
    StartCoroutine(SliderCasting(castTime));
}

private IEnumerator SliderCasting(float castTime)
{
    //Debug.Log($"started casting with {castTime} casttime and is casting {PlayerCastManager.Instance.isCasting}");
    float timePassed = 0.1f;
    while(/* timePassed!>=castTime && */ PlayerCastManager.Instance.isCasting)
    {
        float scaledTimePassed = Mathf.Clamp(timePassed/castTime,0f,castTime);
        slider.value = scaledTimePassed;
        timePassed+=0.1f;
        //Debug.Log($"current cast time {timePassed}");
        yield return new WaitForSecondsRealtime(0.1f);
    }
    
    ResetSlider();

}

private void ResetSlider()
{
    //Debug.Log("reset slider");
    slider.value = 0f;
    parent.SetActive(false);
}


}
