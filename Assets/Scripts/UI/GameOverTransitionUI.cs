using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GameOverTransitionUI : MonoBehaviour
{
    [SerializeField] private Button exitBtn;
    [SerializeField] private Image gameOverPanel;
    [SerializeField] private Sprite[] gameOverSprites;
    [SerializeField] private float fadeImageTime;
    [SerializeField] private float imageStayTime;
    

    private void Start()
    {
        var panelColor = gameOverPanel.color;
        panelColor.a = 0;
        gameOverPanel.color = panelColor;
        
        gameOverPanel.gameObject.SetActive(false);
        exitBtn.gameObject.SetActive(false);
    }
    
    

    public async void ImageTransitionPerform()
    {
        gameOverPanel.gameObject.SetActive(true);
        var panelColor = gameOverPanel.color;
        for (var i = 0; i < gameOverSprites.Length; i++)
        {
            gameOverPanel.sprite = gameOverSprites[i];
            //fade in color alpha
            while (panelColor.a < 0.99f)
            {
                panelColor.a += 1 / fadeImageTime * Time.deltaTime;
                gameOverPanel.color = panelColor;
                await Task.Yield();
            }

            await Task.Delay((int) (imageStayTime * 1000f));
            
            if(i == gameOverSprites.Length -1) return;
            
            while (panelColor.a > 0.01f)
            {
                panelColor.a -= 1 / fadeImageTime * Time.deltaTime;
                gameOverPanel.color = panelColor;
                await Task.Yield();
            }
            
            // Show exit button
            exitBtn.gameObject.SetActive(true);
        }
    }


    public void BindExitBtn(UnityAction onClick)
    {
        exitBtn.onClick.AddListener(onClick);
    }
}
