using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination
{
    public int branchId;
    public int condition; //선택지를 선택하기위한 조건, 없을 경우 이동 불가
    public int detail; //선택지를 선택할 경우 발생하는 자원에 관련된 이벤트
    public string text;
    public Destination(int branchId, int condition, int detail, string text){
        this.branchId = branchId;
        this.condition = condition;
        this.text = text;
        this.detail = detail;
    }
}
