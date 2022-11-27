using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


public class ProjectorDrone : MonoBehaviour
{
    // projector parts
    private Transform projectPoint;
    private RectTransform projectCanvas; 
    private CanvasGroup imageGroup;
    private RawImage projectorCanvasMask;
    private RawImage projectImage;
    private Light2D projectLight;

    private Tweener lightIntensityTween, imageColorTween;

    
    private void Awake()
    {
        projectPoint = transform.Find("Project Point").transform;
        projectCanvas = projectPoint.Find("Project Screen").GetComponent<RectTransform>();
        
        projectLight = projectPoint.GetComponentInChildren<Light2D>();
        imageGroup = projectCanvas.GetComponent<CanvasGroup>();
        projectorCanvasMask = projectCanvas.GetComponentInChildren<RawImage>();
        projectImage = projectorCanvasMask.transform.Find("Projector Image").GetComponent<RawImage>();
    }


    public void SetProjectorComponent(Vector2 projectScale)
    {
        projectCanvas.sizeDelta = new Vector2(projectScale.x, projectScale.y);
        projectLight.pointLightOuterRadius = Mathf.Abs(projectCanvas.anchoredPosition.y) + projectScale.y / 2;
        projectLight.pointLightOuterAngle = 2 * Mathf.Atan((projectScale.x / 2) / Mathf.Abs(projectCanvas.anchoredPosition.y) )  * Mathf.Rad2Deg;
    }


    public void SetProjectImage(Texture image)
    {
        projectImage.texture = image;
    }
    
    
    public void DOProjectorAnimation(float lightIntensity, float groupAlpha, float during)
    {
        var imageColor = projectImage.color;
        imageColor.a = groupAlpha;
        lightIntensityTween?.Kill();
        imageColorTween?.Kill();
        
        lightIntensityTween = DOTween.To(()=> projectLight.intensity, x=> projectLight.intensity = x, lightIntensity, during);
        imageColorTween = DOTween.To(() => imageGroup.alpha, x => imageGroup.alpha = x, groupAlpha, during);
        
        lightIntensityTween.Play();
        imageColorTween.Play();
    }


    public Vector2 GetProjectorCanvasPos()
    {
        return projectImage.transform.position;   
    }
}
