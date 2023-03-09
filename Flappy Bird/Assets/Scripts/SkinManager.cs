using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer selectionSprite;
    public List<Sprite> skins = new List<Sprite>();
    private int selectedIdx = 0;

    public void Next()
    {
        selectedIdx += 1;
        if (selectedIdx == skins.Count)
        {
            selectedIdx = 0;
        }
        selectionSprite.sprite = skins[selectedIdx];
    }

    public void Prev()
    {
        selectedIdx -= 1;
        if (selectedIdx < 0)
        {
            selectedIdx = skins.Count - 1;
        }
        selectionSprite.sprite = skins[selectedIdx];
    }

    public void Play()
    {
        SelectionController.instance.CharIndex = selectedIdx;
        AudioManager.instance.PlaySFX("Swoosh");
        SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
    }
}
