using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextArchitect
{

    private TextMeshProUGUI tmpro_ui;
    private TextMeshPro tmpro_word;
    public TMP_Text tmpro()
    {
        if (tmpro_ui != null)
        { return tmpro_ui; }
        else
        { return tmpro_word; }
    }
    public string currentText() { return tmpro_word.text; }
    private string targetText = "";
    public string getTargetText() { return targetText; }
    private string preText = "";
    public string getPreText() { return preText; }
    private int preTextIndex = 0;

    public string fullTargetText() { return preText + targetText; }
    public enum BuildMethod { instant, typewriter, fade }

    private BuildMethod buildMethod = BuildMethod.typewriter;
    public void setBuildMethod(BuildMethod buildMethod) { this.buildMethod = buildMethod; }
    public Color textColor { get { return tmpro().color; } set { tmpro().color = value; } }
    public float speed { get { return baseSpeed * speedMultiplier; } set { speedMultiplier = value; } }
    private float baseSpeed = 1;
    private float speedMultiplier = 1;

    public int characterPerCycle { get { return speed <= 2f ? characterMultiplier : speed <= 2.5f ? characterMultiplier * 2 : characterMultiplier * 3; } }
    private int characterMultiplier = 1;

    private bool hurryUp = false;
    public bool getHurryUp() { return hurryUp; }

    public TextArchitect(TextMeshProUGUI tmpro_ui)
    {
        this.tmpro_ui = tmpro_ui;
    }
    public TextArchitect(TextMeshPro tmpro_word)
    {
        this.tmpro_word = tmpro_word;
    }
    public Coroutine Build(string text)
    {
        preText = "";
        targetText = text;
        Stop();
        buildProcess = tmpro().StartCoroutine(Building());
        return buildProcess;
    }
    public Coroutine Append(string text)
    {
        preText = tmpro().text;
        targetText = text;
        Stop();
        buildProcess = tmpro().StartCoroutine(Building());
        return buildProcess;
    }

    private Coroutine buildProcess = null;
    public bool isBuilding() { return buildProcess != null; }
    public void Stop()
    {
        if (!isBuilding()) { return; }
        tmpro().StopCoroutine(buildProcess);
        buildProcess = null;
    }
    IEnumerator Building()
    {
        Prepare();
        switch(buildMethod)
        {
            case BuildMethod.typewriter:
                yield return Buid_Typewriter();
                break;
            case BuildMethod.fade:
                yield return Buid_Fade();
                break;
        }
        OnConplete();
    }
    private void OnConplete()
    {
        buildProcess = null;
    }
    private void Prepare()
    {
        switch (buildMethod)
        {
            case BuildMethod.instant:
                yield return Prepare_Instant();
                break;
            case BuildMethod.typewriter:
                yield return Prepare_Typewriter();
                break;
            case BuildMethod.fade:
                yield return Prepare_Fade();
                break;
        }
        yield return null;
    }
    private void Prepare_Instant()
    {
        tmpro().color = tmpro().color;
        tmpro().text = fullTargetText();
        tmpro().ForceMeshUpdate();
        tmpro().maxVisibleCharacters=tmpro().textInfo.characterCount;
    }
    private void Prepare_Typewriter()
    {

    }
    private void Prepare_Fade()
    {

    }
    private IEnumerator Buid_Typewriter()
    {
        yield return null;
    }
    private IEnumerator Buid_Fade()
    {
        yield return null;
    }
}