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
    public void ShowItem(ItemSO item,bool isEqui = false, bool isShop = false) {
        toolShowItem.ShowItem(item, isEqui, isShop);
        toolShowItem.gameObject.SetActive(true);
    }
}
