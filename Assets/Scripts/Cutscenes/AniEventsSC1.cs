using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Animation Events script. Shows 0 references bc the Animation Window doesnt get counted.
public class AniEventsSC1 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mainText;
    Dictionary<int, string> dialogueScript = new Dictionary<int, string>();

    // Start is called before the first frame update
    void Start()
    {
        SetupDialogueScript();
    }

    void SetupDialogueScript() {
        dialogueScript.Add(1000, "STARTING BOOT SEQUENCE...");
        dialogueScript.Add(1001, "SYNCHRONIZING CONTAINERS...");
        dialogueScript.Add(1002, "ENGAGING COOLANT...");
        dialogueScript.Add(1003, "ARMING COMBAT MODULES...");
        dialogueScript.Add(1004, "ALLOCATING SPACE FOR NEW MEMORY...");
        dialogueScript.Add(1005, "LOADING SENTIENCE MODULE...");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallDialogueStep(int step) {
        Debug.Log("Hello!");
        StartCoroutine(UpdateText(dialogueScript.GetValueOrDefault(step)));
    }

    private IEnumerator UpdateText(string text) {
        mainText.text = "";
        foreach (char c in text) {
            mainText.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
