using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PedestrianDescriptionPanel : MonoBehaviour
{
    [SerializeField] private Text _nameText, _descriptionText;
    [SerializeField] private Image _backGroundPanel, _descrPanelBigBG, _descriptionFullPanel;
    [SerializeField] private Color[] _notGuiltyColors, _arrestColors, _shockColors;
    [SerializeField] private Sprite _notGuiltyDescrSprite, _arrestDescrSprite, _shockDescrSprite;

    private Shadow _shadow;
    private Dictionary<CrimeType, Color[]> _crimeColors;
    private Dictionary<CrimeType, Sprite> _crimeSprites;
    private bool _isInit;

    private void Init()
    {
        if (_isInit) return;

        _crimeColors = new Dictionary<CrimeType, Color[]>()
        {
            [CrimeType.NotGuilty] = _notGuiltyColors,
            [CrimeType.ArrestWorthy] = _arrestColors,
            [CrimeType.ShockWorthy] = _shockColors
        };

        _crimeSprites = new Dictionary<CrimeType, Sprite>()
        {
            [CrimeType.NotGuilty] = _notGuiltyDescrSprite,
            [CrimeType.ArrestWorthy] = _arrestDescrSprite,
            [CrimeType.ShockWorthy] = _shockDescrSprite
        };

        if (_descriptionText != null)
        {
            _shadow = _descriptionText.GetComponent<Shadow>();
        }
        _isInit = true;
    }

    public virtual void FeedPedestrianData(PedestrianData data)
    {
        if (!_isInit) Init();
        if (_nameText != null)
            _nameText.text ="Name: " + data.Name;

        if (_descriptionText != null)
        {
            _descriptionText.text = data.Description;
            _descriptionText.color = _crimeColors[data.CrimeType][1];
            if (_shadow != null)
            {
                _shadow.effectColor = _crimeColors[data.CrimeType][0];
            }
        }
            
        
        if (_backGroundPanel != null)
            _backGroundPanel.color = _crimeColors[data.CrimeType][1];

        if (_descrPanelBigBG != null)
            _descrPanelBigBG.color = _crimeColors[data.CrimeType][0];

        if (_descriptionFullPanel != null)
        {
            Sprite s = _crimeSprites[data.CrimeType];
            if (s == null) return;
            _descriptionFullPanel.sprite = s;
        }

    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }
}