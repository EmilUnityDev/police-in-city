using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private string _privacyPolicyUrl;
    [SerializeField] private string _touUrl;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private RectTransform _referenceRectTransform;
    [SerializeField] private RectTransform _touRt;

    private UniWebView _uniWebView;
    private UniWebView _touWebView;

    private void Awake()
    {
        _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    public void OpenPrivacyPolicyScreenFromButton()
    {
        StartCoroutine(OpenPrivacyPolicy());
    }

    public void OpenTOU()
    {
        StartCoroutine(OpenTou());
    }

    private IEnumerator OpenTou()
    {
        LoadTOU();
        yield return new WaitForSeconds(1f);
        _touWebView.Show();
    }

    private IEnumerator OpenPrivacyPolicy()
    {
        LoadPrivacyPolicy();
        yield return new WaitForSeconds(1f);
        _uniWebView.Show();
    }

    private void OnConfirmButtonClicked()
    {
        PlayerPrefs.SetInt("start", 1);
        PlayerPrefs.Save();

        if (_uniWebView != null)
        {
            Destroy(_uniWebView.gameObject);
        }
        if (_touWebView != null)
        {
            Destroy(_touWebView.gameObject);
        }
    }

    private void LoadPrivacyPolicy()
    {
        _uniWebView = new GameObject().AddComponent<UniWebView>();
        _uniWebView.transform.SetParent(_referenceRectTransform);
        _uniWebView.Load(_privacyPolicyUrl);
        _uniWebView.ReferenceRectTransform = _referenceRectTransform;
        _uniWebView.transform.SetAsFirstSibling();
        _uniWebView.SetShowToolbar(false);
    }

    private void LoadTOU()
    {
        _touWebView = new GameObject().AddComponent<UniWebView>();
        _touWebView.transform.SetParent(_touRt);
        _touWebView.Load(_touUrl);
        _touWebView.ReferenceRectTransform = _touRt;
        _touWebView.transform.SetAsFirstSibling();
        _touWebView.SetShowToolbar(false);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}