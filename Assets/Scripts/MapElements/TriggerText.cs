using TMPro;
using UnityEngine;

public class TriggerText : TriggerBase
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI text;
    TextFade fade;

    void Start()
    {
        fade = gameObject.AddComponent<TextFade>();
        fade.text = text;

        fade.SetToVal(0.0f);
    }

    void Update()
    {
        
    }

    public override void DoAction()
    {
        if (activationMode == ModeOfActivation.OnEnter)
        {
            activationMode = ModeOfActivation.OnExit;
            StartCoroutine(fade.FadeText(fade.text.color.a, 1.0f));
        }
        else if (activationMode == ModeOfActivation.OnExit)
        {
            activationMode = ModeOfActivation.OnEnter;
            StartCoroutine(fade.FadeText(fade.text.color.a, 0.0f));
        }
        
    }
}
