using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Button btnMenu;
    [SerializeField] GameObject goMenu;
    [SerializeField] Button btnMenuContinue;
    [SerializeField] Button btnMenuNewGame;
    [SerializeField] Button btnMenuQuit;

    [Header("\n----- Infor ----")]
    [SerializeField] Button btnInfor;
    [SerializeField] GameObject goInfor;
    [SerializeField] Button btnInforQuit;
    [SerializeField] ToolShowItem toolShowItem;
    public UiShowInforPlayer uiShowInforPlayer;
    // Start is called before the first frame update
    void Start()
    {
        btnMenu.onClick.AddListener(() =>
        {
            goMenu.SetActive(true);
            GameManager.instance.PauseGame(true);
        });
        btnInfor.onClick.AddListener(() =>
        {
            goInfor.SetActive(true);
            //goInfor.transform.GetComponentInChildren<UiShowInforPlayer>().UpdateData(PlayerManager.instance.player.stats);
            GameManager.instance.PauseGame(true);
        });

        btnMenuContinue.onClick.AddListener(() =>
        {
            goMenu.SetActive(false);
            GameManager.instance.PauseGame(false);
        });
        btnMenuNewGame.onClick.AddListener(() =>
        {
            GameManager.instance.PauseGame(false);
            int indexScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(indexScene);
        });
        btnMenuQuit.onClick.AddListener(() =>
        {
            GameManager.instance.PauseGame(false);
            SceneManager.LoadScene(1);
        });

        btnInforQuit.onClick.AddListener(() =>
        {
            GameManager.instance.PauseGame(false);
            goInfor.SetActive(false);
        });

        goMenu.SetActive(false);
        goInfor.SetActive(false);
        toolShowItem.gameObject.SetActive(false);
    }
    public void ShowItem(Vector3 p, ItemSO item,bool isEqui = false, bool isShop = false) {
        toolShowItem.ShowItem(item, isEqui, isShop);

        // Calculate half of the width and height of the RectTransform
        RectTransform rectTransform = toolShowItem.GetComponent<RectTransform>();
        Vector2 size = rectTransform.sizeDelta;

        Vector2 screenCenter = new Vector2(Screen.width - size.x, Screen.height - size.y);
        // Determine the offset based on the position relative to the center
        float xOffset = (p.x > screenCenter.x) ? -1 : 1;
        float yOffset = (p.y > screenCenter.y) ? -1 : 1;

        // Correctly position the item considering the anchor and pivot
        Vector3 positionOffset = new Vector3(xOffset * size.x / 1.8f, yOffset * size.y / 1.8f, 0);
        toolShowItem.transform.position = p + positionOffset;

        toolShowItem.gameObject.SetActive(true);
    }
}
