using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch
{
    public int id; //id가 0이라면 게임 엔드
    public string text; //카드 위의 텍스트, img가 null이라면 카드 안의 텍스트
    public string img; //카드 배경 이미지, 이미지 파일 이름 ex) sample.jpg, img가 null이라면 only text branch
    public Destination destinFirst;
    public Destination destinSecond;

    public Branch(int id, string text, string img, Destination destin1, Destination destin2){
        this.id = id;
        this.text = text;
        this.img = img;
        this.destinFirst = destin1;
        this.destinSecond = destin2;
    }
}
