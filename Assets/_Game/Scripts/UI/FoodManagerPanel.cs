using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FoodManagerPanel : MonoBehaviour {

    public Button openPanelButton;
    public GameObject panel;

    void Start() {
        var tr = (RectTransform) panel.transform;
        tr.gameObject.SetActive(false);
    }
    
    public void OpenPanel() {
        
        var tr = (RectTransform) panel.transform;
        tr.gameObject.SetActive(true);
        tr.DOLocalMoveX(100+tr.rect.width, 0.2f);
        
    }

    public void ClosePanel() {
        var tr = (RectTransform) panel.transform;
        tr.DOLocalMoveX(100+2*tr.rect.width, 0.2f)
            .OnComplete(() => {
                tr.gameObject.SetActive(false);
            });
    }
    

}