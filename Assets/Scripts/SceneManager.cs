using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    List<int> premises = new List<int>();
     //어떤 상황에 게임을 끝낼지에 대한 대전제 ex)hp 자원이 0이 되면 게임오버.
    List<Branch> branches = new List<Branch>();
    int currentBranchId;
    public GameObject CardPrefab;
    public Image card;
    public Text content, cardText;

    void Awake()//개발자 디버그용 브랜치 생성
    {
        branches.Add(new Branch(1,
        "게임이 시작되었습니다. 시작하시겠습니까?",
        "images.png",
        new Destination(2, 0, 0, "예"),
        new Destination(3, 0, 0, "아니요")));

        branches.Add(new Branch(2,
        "승리하였습니다. 재시작하시겠습니까?",
        null,
        new Destination(1, 0, 0, "예"),
        new Destination(0, 0, 0, "아니요")));

        branches.Add(new Branch(3,
        "패배하였습니다. 재시작하시겠습니까?",
        null,
        new Destination(1, 0, 0, "예"),
        new Destination(0, 0, 0, "아니요")));
    }
    void Start(){
        currentBranchId = 1; // 1: 시작 브랜치 아이디
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

    void SetUI(){
        Branch branch = GetBranch(currentBranchId);

        GameObject card = GameObject.Instantiate(CardPrefab);
        if(branch.img == null){
            cardText = card.GetComponentInChildren<Text>();
            cardText.text = branch.text;
            
        }else{

        }
    }
}
