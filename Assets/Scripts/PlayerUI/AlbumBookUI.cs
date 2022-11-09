using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumBookUI : MonoBehaviour
{
    private Player player;
    private AlbumBook albumBook;

    public RectTransform bookPanel;
    public Button albumBtn;
    
    public Button closeAlbumBtn;
    public Button LastPageBtn;
    public Button NextPageBtn;
    [SerializeField] private RectTransform choosingPhotoTrans;
    
    // Instantiate prefab
    [SerializeField] private RectTransform pagePrefab;
    [SerializeField] private RectTransform photoPrefab;

    // Panel data
    public float PhotoWidth;
    public float PhotoHeight;
    public float XSpacing;
    public float YSpacing;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
}
