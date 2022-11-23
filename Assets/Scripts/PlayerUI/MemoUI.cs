using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MemoUI : MonoBehaviour
{
    private Player player;
    private AlbumBook albumBook;
    
    [SerializeField] private RectTransform memoPage;
    [SerializeField] private RectTransform bookMemoArea;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text contents;

    [SerializeField] private Button ChangeToMemoBtn;
    
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    
    
    private void OnEnable()
    {
        player.OnPropEquipped += RegisterMemoUI;
    }


    private void OnDisable()
    {
        player.OnPropEquipped -= RegisterMemoUI;
    }


    private void RegisterMemoUI(IPlayerProp prop)
    {
        if(!(prop is AlbumBook)) return;
        
        albumBook = (AlbumBook) prop;

        albumBook.OnAlbumBookToggleEnable += MemoUISet;
        albumBook.OnAlbumPageTypeChange += ChangeAlbumTypePage;
        albumBook.OnAlbumChangeCurrentMemo += ChangeCurrentMemo;
        
        ChangeToMemoBtn.onClick.AddListener(() => albumBook.SetCurrentPage(AlbumPage.Memo));
    }


    private void MemoUISet(bool enable)
    {
        var firstMemo = albumBook.GetCurrentChooseMemo();
        
        bookMemoArea.gameObject.SetActive(firstMemo);
        ChangeCurrentMemo(firstMemo);
    }


    private void ChangeAlbumTypePage(AlbumPage targetPage)
    {
        memoPage.gameObject.SetActive(targetPage == AlbumPage.Memo);
    }


    private void ChangeCurrentMemo(MemoData currentMemo)
    {
        if(!currentMemo) return;
        
        title.text = currentMemo.Title;
        contents.text = currentMemo.Content;
    }
}
