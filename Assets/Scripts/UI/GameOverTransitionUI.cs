using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class GameOverTransitionUI : MonoBehaviour
{
    [SerializeField] private Image gameOverPanel;
    [SerializeField] private Sprite[] gameOverSprites;
    [SerializeField] private float fadeImageTime;
    [SerializeField] private float imageStayTime;
    
    private void OnEnable()
    {
        KeyEventManager.OnGameOver += ImageTransitionPerform;
    }


    private void OnDisable()
    {
        KeyEventManager.OnGameOver -= ImageTransitionPerform;
    }


    private void Start()
    {
        var panelColor = gameOverPanel.color;
        panelColor.a = 0;
        gameOverPanel.color = panelColor;
        
        gameOverPanel.gameObject.SetActive(false);
    }
    
    

    private async void ImageTransitionPerform()
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
        }
    }
}
