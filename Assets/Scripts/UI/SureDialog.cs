using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SureDialog : MonoBehaviour
{
    public delegate void SureDialogResult(int result);
    SureDialogResult DialogResult;
    public TextMeshProUGUI ControlObject;
    public TextMeshProUGUI Action;

    public void ShowDialog(string controlObject, string action, SureDialogResult dialogResult)
    {
        DialogResult = dialogResult;
        ControlObject.text = controlObject;
        Action.text = action;
        
        gameObject.SetActive(true);
    }
    void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().SetAsLastSibling();
    }
    
    public void OkPressed()
    {
        gameObject.SetActive(false);
        DialogResult.Invoke(1);
    }
    public void CancelPressed()
    {
        gameObject.SetActive(false);
        DialogResult.Invoke(0);
    }
    public void ClosePressed()
    {
        gameObject.SetActive(false);
        DialogResult.Invoke(0);
    }
}
