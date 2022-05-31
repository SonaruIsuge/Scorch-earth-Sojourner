using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowViewControl : MonoBehaviour
{
    [SerializeField] private Transform backImage;
    [SerializeField] private Transform middleImage;
    [SerializeField] private Transform forwardImage;
    
    [Range(0, 50)][SerializeField] private float backImageSpeed;
    [Range(0, 50)][SerializeField] private float middleImageSpeed;
    [Range(0, 50)][SerializeField] private float forwardImageSpeed;

    private float imageLeftSide;
    private float imageRightSide;

    private Camera windowCamera;
    private float cameraHalfHeight;
    private float cameraHalfWidth;

    void Awake()
    {
        windowCamera = GetComponentInChildren<Camera>();
        cameraHalfHeight = windowCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * windowCamera.aspect;

        var imageSprite = backImage.GetComponent<SpriteRenderer>().sprite;
        imageLeftSide = transform.position.y - imageSprite.rect.width / imageSprite.pixelsPerUnit / 2;
        imageRightSide = transform.position.y + imageSprite.rect.width / imageSprite.pixelsPerUnit / 2;
    }

    public void UpdateViews()
    {
        updateImagePos(backImage, backImageSpeed);
        updateImagePos(middleImage, middleImageSpeed);
        updateImagePos(forwardImage, forwardImageSpeed);
    }
    
    private void updateImagePos(Transform imageTrans, float speed)
    {
        imageTrans.Translate(speed * Time.deltaTime, 0, 0);
        if(imageTrans.localPosition.x >= imageRightSide - cameraHalfWidth) imageTrans.localPosition = new Vector3(imageLeftSide + cameraHalfWidth,0,0);
    }
}
