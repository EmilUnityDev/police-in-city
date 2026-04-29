using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    public Image[] slots; 
    public Sprite[] symbols; 
    public GameObject _miniGamePanel; 
    public float spinDuration = 2f; 

    public void StartSlot()
    {
        StartCoroutine(SpinSlots());
    }

    private IEnumerator SpinSlots()
    {
        
        for (int i = 0; i < slots.Length; i++)
        {
            StartCoroutine(SpinSlot(slots[i]));
            yield return new WaitForSeconds(spinDuration / slots.Length);
        }

        
        CheckWin();
    }

    private IEnumerator SpinSlot(Image slot)
    {
        float elapsedTime = 0f;
        while (elapsedTime < spinDuration)
        {
            
            slot.sprite = symbols[Random.Range(0, symbols.Length)];
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
    }

    private void CheckWin()
    {
        
        bool win = true;
        Sprite firstSymbol = slots[0].sprite;
        for (int i = 1; i < slots.Length; i++)
        {
            if (slots[i].sprite != firstSymbol)
            {
                win = false;
                break;
            }
        }
        Invoke("ClosePanel",2.5f);
    }

    public void ClosePanel()
    {
        _miniGamePanel.transform.DOScale(Vector3.zero, 0.3f);
    }
}
