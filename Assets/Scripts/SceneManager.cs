using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SceneManager : MonoBehaviour
{
    List<int> premises = new List<int>();
     //어떤 상황에 게임을 끝낼지에 대한 대전제 ex)hp 자원이 0이 되면 게임오버.
    List<Branch> branches = new List<Branch>();
    public int currentBranchId;
    public GameObject CardPrefab;
    public GameObject card1, card2, card3;
    public Text content;
    public Sprite defaultSprite;

    void Awake()//개발자 디버그용 브랜치 생성
    {
        Branch defaultBranch = new Branch(0, "기본 브랜치 입니다.", "",
        new Destination(1, 0, 0, ""),
        new Destination(1, 0, 0, ""));
        
        Branch branch = new Branch(1, "게임 시작, 승리하시겠습니까?", "",
        new Destination(2, 0, 0, "예"),
        new Destination(3, 0, 0, "아니오"));

        Branch branch1 = new Branch(2, "승리하셨습니다. 다시하시겠습니까?", "",
        new Destination(1, 0, 0, "yes"),
        new Destination(0, 0, 0, "no"));

        Branch branch2 = new Branch(3, "패배하셨습니다. 다시하시겠습니까?", "",
        new Destination(1, 0, 0, "Yeah!!!!"),
        new Destination(0, 0, 0, "Never!!!!"));

        branches.Add(defaultBranch);
        branches.Add(branch);
        branches.Add(branch1);
        branches.Add(branch2);
    }
    void Start(){
        currentBranchId = 0; // 0으로 시작
        SetUI(true);
    }

    //json 파싱
    IEnumerator ServerInit(string uri)
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if(webRequest.isNetworkError)
            {
                Debug.Log("Networking Error : " + webRequest.error);
            }
            else
            {
                Debug.Log("Networking Ok : "+webRequest.downloadHandler.text);
            }
        }
    }
    Branch GetBranch(int branchId){
        foreach (Branch i in branches)
        {
            if (i.id == branchId)
            {
                return i;
            }
        }
        return null;
    }

    public void SetUI(bool isLeft){
        Branch branch = GetBranch(currentBranchId);
        if(isLeft)
        {
            currentBranchId = branch.destinFirst.branchId;
        }
        else
        {
            currentBranchId = branch.destinSecond.branchId;
        }
        branch = GetBranch(currentBranchId);
        

        OnCardDragged cardDragged1 = card1.GetComponent<OnCardDragged>();
        OnCardDragged cardDragged2 = card2.GetComponent<OnCardDragged>();
        OnCardDragged cardDragged3 = card3.GetComponent<OnCardDragged>();
        
        
        if(cardDragged1.GetFocus()){
            //card1이 바깥으로 밀어졌을 때
            
            card1.GetComponentInChildren<Text>().text = "";

            card1.transform.SetAsFirstSibling();

            if(branch.img.Equals("")){
                content.text = "";
                StartCoroutine(TurnAndChangeImage(2, branch, true));
            }else{
                content.text = branch.text;
                StartCoroutine(TurnAndChangeImage(2, branch, false));
                //이미지 set
            }
            cardDragged2.leftText.GetComponentInChildren<Text>().text = branch.destinFirst.text;
            cardDragged2.rightText.GetComponentInChildren<Text>().text = branch.destinSecond.text;
        }
        else if(cardDragged2.GetFocus())
        {
            //card2이 바깥으로 밀어졌을 때    
            card2.GetComponentInChildren<Text>().text = "";
            card2.transform.SetAsFirstSibling();

            if(branch.img.Equals("")){
                content.text = "";
                StartCoroutine(TurnAndChangeImage(3, branch, true));
            }else{
                content.text = branch.text;
                StartCoroutine(TurnAndChangeImage(3, branch, false));
                //이미지 set
            }
            cardDragged3.leftText.GetComponentInChildren<Text>().text = branch.destinFirst.text;
            cardDragged3.rightText.GetComponentInChildren<Text>().text = branch.destinSecond.text;
        }else
        {
            //card3이 바깥으로 밀어졌을 때
            
            card3.GetComponentInChildren<Text>().text = "";

            card3.transform.SetAsFirstSibling();

            if(branch.img.Equals("")){
                content.text = "";
                StartCoroutine(TurnAndChangeImage(1, branch, true));
            }else{
                content.text = branch.text;
                StartCoroutine(TurnAndChangeImage(1, branch, false));
                //이미지 set
            }
            cardDragged1.leftText.GetComponentInChildren<Text>().text = branch.destinFirst.text;
            cardDragged1.rightText.GetComponentInChildren<Text>().text = branch.destinSecond.text;
        }
    }

    IEnumerator TurnAndChangeImage(int cardCode, Branch branch, bool isDefaultSprite){
        switch(cardCode){
            case 1:
                card1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                while(card1.transform.rotation.y < 0.7){
                    card1.transform.Rotate(new Vector3(0, 3f, 0));
                    yield return new WaitForEndOfFrame();
                }
                //이미지 변경, 텍스트 입력 
                if(isDefaultSprite)
                {
                    card1.GetComponentInChildren<Text>().text = branch.text;
                    card1.GetComponent<Image>().sprite = defaultSprite;
                }else
                {
                    //이미지 변경, 이미지가 있을 때
                }

                while(card1.transform.rotation.y > 0){
                    card1.transform.Rotate(new Vector3(0, -3f, 0));
                    yield return new WaitForEndOfFrame();
                }
                card1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                card1.GetComponent<OnCardDragged>().SetFocus(true);
                card2.GetComponent<OnCardDragged>().SetFocus(false);
                card3.GetComponent<OnCardDragged>().SetFocus(false);
                break;
            case 2:
                while(card2.transform.rotation.y < 0.7){
                    card2.transform.Rotate(new Vector3(0, 3f, 0));
                    yield return new WaitForEndOfFrame();
                }
                //이미지 변경, 텍스트 입력 
                if(isDefaultSprite)
                {
                    card2.GetComponentInChildren<Text>().text = branch.text;
                    card2.GetComponent<Image>().sprite = defaultSprite;
                }else
                {
                    //이미지 변경, 이미지가 있을 때
                }

                while(card2.transform.rotation.y > 0){
                    card2.transform.Rotate(new Vector3(0, -3f, 0));
                    yield return new WaitForEndOfFrame();
                }
                card2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                card1.GetComponent<OnCardDragged>().SetFocus(false);
                card2.GetComponent<OnCardDragged>().SetFocus(true);
                card3.GetComponent<OnCardDragged>().SetFocus(false);
                break;
            case 3:
                while(card3.transform.rotation.y < 0.7){
                    card3.transform.Rotate(new Vector3(0, 3f, 0));
                    yield return new WaitForEndOfFrame();
                }
                //이미지 변경, 텍스트 입력 
                if(isDefaultSprite)
                {
                    card3.GetComponentInChildren<Text>().text = branch.text;
                    card3.GetComponent<Image>().sprite = defaultSprite;
                }else
                {
                    //이미지 변경, 이미지가 있을 때
                }

                while(card3.transform.rotation.y > 0){
                    card3.transform.Rotate(new Vector3(0, -3f, 0));
                    yield return new WaitForEndOfFrame();
                }
                card3.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                card1.GetComponent<OnCardDragged>().SetFocus(false);
                card2.GetComponent<OnCardDragged>().SetFocus(false);
                card3.GetComponent<OnCardDragged>().SetFocus(true);
                break;
        }
    }
}
