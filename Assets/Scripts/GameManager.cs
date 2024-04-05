using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject delivery;
    private Animator deliveryAnimator;

    public Canvas healthBarCanvas;

    [SerializeField] private GameObject orderMenu;
    [SerializeField] private GameObject openOrderMenuButton;
    [SerializeField] private GameObject closeOrderMenuButton;
    [SerializeField] private GameObject selectionUI;

    public enum GameState
    {
        Defend,
        Order
    }
    public GameState state;

    private void Awake()
    {
        AudioManager.instance.Play("Music");
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        deliveryAnimator = delivery.GetComponent<Animator>();
        deliveryAnimator.Play("PackageLeaves");
        AudioManager.instance.Play("PhaseEnd");
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.Defend:
                break;

            case GameState.Order:

                break;
        }
    }

    public void StartOrderPhase()
    {
        deliveryAnimator.Play("PackageLeaves");
        AudioManager.instance.Play("PhaseEnd");
        state = GameState.Order;
        openOrderMenuButton.SetActive(true);
        closeOrderMenuButton.SetActive(false);
        selectionUI.SetActive(true);

        OrderManager.instance.OpenDeliveredTowers();
    }

    public void StartDefensePhase()
    {
        deliveryAnimator.Play("PackageEnters");
        AudioManager.instance.Play("PhaseEnd");
        state = GameState.Defend;
        openOrderMenuButton.SetActive(false);
        closeOrderMenuButton.SetActive(false);
        orderMenu.SetActive(false);
        selectionUI.SetActive(true);

        WaveManager.instance.StartWave();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
