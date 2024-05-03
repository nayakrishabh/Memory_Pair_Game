using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int pairCount;
    public int scorecount;
    public int lastid,newid;
    public Transform panelContainer;
    public Transform winPopup;
    public Transform pausemenu;
    public Button restartButton;
    public Vector2 gridSize;
    public GameObject lastObject,newObject;
    public List<GameObject> itemList;
    public List<GameObject> resetitems;
    List<GameObject> selectedItems = new List<GameObject>();
    List<GameObject> shuffledItems = new List<GameObject>();
    List<GameObject> pairItems = new List<GameObject>();

    // Start is called before the first frame update
    private void Awake() {
        instance = this;
    }
    void Start()
    {
        GetItems();
        StartCoroutine(MakePairList());
        //Debug.LogError(MakePairList().Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }
    /// <summary>
    /// it returns list of items based on pairCount
    /// </summary>
    /// <returns></returns>
    List<GameObject> GetItems() {
        for(int i = 0; i < pairCount; i++) {
            //number = Random.Range(0,itemList.Count);
            selectedItems.Add(itemList[i]);
        }
        return selectedItems;
    }
    
    IEnumerator MakePairList() {
        for(int i = 0; i < 2; i++) {
            for(int j= 0; j < pairCount; j++) {
                pairItems.Add(GameObject.Instantiate<GameObject>(selectedItems[j]));
                yield return null;
            }
        }
        shuffledItems = Shufflelist(pairItems);
        SetObjects();
    }
    List<GameObject> Shufflelist(List<GameObject> pairItems) {
        for (int i = 0; i < (pairItems.Count-1); i++) {
            var r = Random.Range(i, pairItems.Count);
            var temp = pairItems[i];
            pairItems[i] = pairItems[r];
            pairItems[r] = temp; 
        }
        return pairItems;
    }
    void SetObjects() {
        for(int i = 0; i < shuffledItems.Count; i++) {
            shuffledItems[i].transform.SetParent(panelContainer);
            shuffledItems[i].GetComponent<PairItems>().anim.SetBool("flip", true);
        }
    }

    public void PairDetection() {
        PairItems pr1 = lastObject.GetComponent<PairItems>();
        PairItems pr2 = newObject.GetComponent<PairItems>();
        lastid = pr1.uid;
        newid = pr2.uid;

            if (lastid == newid) {
            DisablePairs();
                Debug.LogError("Pairs Matched");
                scorecount++;
            } else {
                Debug.LogError("Pairs not Matched");
                pr1.ResetAnimation();
                pr2.ResetAnimation();

            }

        lastObject = null;
        newObject = null;
        if(scorecount == pairCount) {
            winPopup.gameObject.SetActive(true);
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    void DisableItems() {
        Debug.LogError(lastObject.name);
        StartCoroutine(DelayCallback(2f,DisablePairs));
    }
    void DisablePairs() {
        Debug.LogError(lastObject.name);
        lastObject.GetComponent<CanvasGroup>().alpha = 0;
        lastObject.GetComponent<PairItems>().enabled = false;
        lastObject.GetComponent<Button>().enabled = false;
        newObject.GetComponent<CanvasGroup>().alpha = 0;
        newObject.GetComponent<PairItems>().enabled = false;
        newObject.GetComponent<Button>().enabled = false;
    }
    /*
    void ResetAnimation()
    {
        lastObject.GetComponent<PairItems>().anim.SetBool("flip", false);
        newObject.GetComponent<PairItems>().anim.SetBool("flip", false);
    }
    void ResetAnimation1()
    {
        lastObject.GetComponent<PairItems>().anim.SetBool("return", true);
        newObject.GetComponent<PairItems>().anim.SetBool("return", true);
    }
    */
    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void PauseGame() {
        Time.timeScale = 0.0f;
        pausemenu.gameObject.SetActive(true);
    }
    IEnumerator DelayCallback(float delay, UnityEngine.Events.UnityAction callback) {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}