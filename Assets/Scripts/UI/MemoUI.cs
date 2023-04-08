using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MemoUI : MonoBehaviour
{
    private AlbumBook albumBook;
    
    [SerializeField] private RectTransform memoPage;
    [SerializeField] private RectTransform bookMemoArea;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text contents;

    [SerializeField] private Button ChangeToMemoBtn;


    public void RegisterMemoUI(IPlayerProp prop)
    {
        if(prop is not AlbumBook book) return;
        
        albumBook = book;

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
