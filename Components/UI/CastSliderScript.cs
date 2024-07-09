using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastSliderScript : MonoBehaviour
{

    Slider slider;
    GameObject parent;
    TextMeshProUGUI tmp;
    [SerializeField] Image abilityImage;

    void Awake()
    {
        //Debug.Log(abilityImage);
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        parent = transform.parent.gameObject;
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        PlayerCastManager.Instance.onPlayerStartCast += StartSlider;
        parent.SetActive(false);
    }

    private void StartSlider(Ability ability)
    {
        if(ability.castingTime>0.1f)
        {
            //Debug.Log("start slider");
            parent.SetActive(true);
            tmp.text = ability.name;
            abilityImage.sprite = ability.sprite;
            //Debug.Log("starting slidercasting coroutine with "+castTime);
            StartCoroutine(SliderCasting(ability.castingTime));
        }

    }

    private IEnumerator SliderCasting(float castTime)
    {
        //Debug.Log($"started casting with {castTime} casttime and is casting {PlayerCastManager.Instance.isCasting}");
        float timePassed = 0.3f;
        while(/* timePassed!>=castTime && */ PlayerCastManager.Instance.isCasting)
        {
            float scaledTimePassed = Mathf.Clamp(timePassed/castTime,0f,castTime);
            slider.value = scaledTimePassed;
            timePassed+=0.01f;
            //Debug.Log($"current cast time {timePassed}");
            yield return new WaitForSecondsRealtime(0.01f);
        }
        
        ResetSlider();

    }

    private void ResetSlider()
    {
        //Debug.Log("reset slider");
        abilityImage.sprite = null;
        slider.value = 0f;
        parent.SetActive(false);
    }


}
