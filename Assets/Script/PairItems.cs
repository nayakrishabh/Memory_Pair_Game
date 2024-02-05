using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PairItems : MonoBehaviour {
    public int uid;
    public Image animalImg;
    public CanvasGroup canvasG;
    public Button clickButton;
    public GameObject animalObj;
    public Animator anim;

    public PairItems(PairItems obj) {
        uid = obj.uid;
        animalImg = obj.animalImg;
        canvasG = obj.canvasG;

    }
    // Start is called before the first frame update
    void Start() {
        clickButton.onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update() {
    }
    void click(){
        if (GameManager.instance.lastObject == null) {
            animalImg.GetComponent<Image>().enabled = false;
            anim.SetBool("flip", false);
            GameManager.instance.lastObject = animalObj;
        } else {
            if (animalObj == GameManager.instance.lastObject)
                return;
            animalImg.GetComponent<Image>().enabled = false;
            anim.SetBool("flip", false);
            GameManager.instance.newObject = animalObj;
        }
        if(GameManager.instance.lastObject != null && GameManager.instance.newObject != null) {
            GameManager.instance.PairDetection();
        }
    }

    public void ResetAnimation() {
        StartCoroutine(DelayCallback(1f, () => anim.SetBool("flip", true)));
    }
    

    IEnumerator DelayCallback(float delay, UnityEngine.Events.UnityAction callback) {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}