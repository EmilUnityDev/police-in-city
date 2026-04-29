using UnityEngine;
using UnityEngine.UI;

public class DetainButtonController : MonoBehaviour
{
    [SerializeField] private Button _detainButton;
    [SerializeField] private GameObject _finger;
    [SerializeField] private GameObject _background;

    public void Init()
    {
        _detainButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _detainButton.gameObject.SetActive(true);
        _finger.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
        _finger.gameObject.SetActive(false);
    }
}