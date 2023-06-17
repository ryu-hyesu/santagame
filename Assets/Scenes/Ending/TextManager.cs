using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public Dictionary<int, string[]> textData;

    void Awake()
    {
        textData = new Dictionary<int, string[]>();
        Generate();
    }

    void Generate()
    {
        textData.Add(200, new string[] { 
            "모든 아이에겐 산타가 필요하다.",
            "비록 할아버지를 별로 좋아하지 않는 산타의 손자일지라도…….",
            "오늘은 크리스마스 이브.",
            "그리고 할아버지가 돌아가신 지 벌써 삼 년 째 되는 날이다.",
            "할아버지는 선물 썰매에 떨어져 돌아가셨다.",
            "할아버지의 유일한 가족이었던 나는 할아버지의 일을 이어 받아\n매년 크리스마스에 남의 집 굴뚝에 들어가 선물을 주고 있었다.",
            "매번 남을 먼저 생각하는 할아버지.",
            "정작 손자가 어떻게 되든 신경도 안 썼던 할아버지의 일을…….",
            "…….",
            "…나도 내가 왜 할아버지의 일을 아직까지 대신하고 있는 줄 모르겠다.",
            "할아버지를 생각하면 늘 원망스러웠다.",
            "산타 그게 뭐라고…… 나는 항상 뒷전이었는데…….",
            "오늘도 그런 날이었다.",
            "할아버지를 원망하면서 작년처럼 아이들에게 나누어 줄 선물을 나누어 주기 위해…….",
            "창고를 열었는데…….",
            "……없다?",
            "이게 어떻게 된 일이지?",
            "허탈한 마음으로 집 안을 들어가자\n아까는 보이지 않던 황당한 쪽지가 놓여있었다.",
            "[네 선물은 내가 가져간다!]",
            "동화책만 꽂힌 책장이었다.",
            "할아버지가 아이들을 위해 매년 수집하셨던 동화책들……",
            "책에서 미약한 빛이 새어나왔다.",
            "……한두 개가 아니다.",
            "빌어먹을. 이 중에서 어떤 새끼가 가져간 거지?",
            "어차피 선물 주는 건 내 할 일도 아니지만…….",
            "불현듯 할아버지의 미운 얼굴이 눈 앞에 그려졌다.",
            "아이들이 기뻐한다면 어떤 일도 마다하지 않겠다던 의지에 찬 목소리까지.",
            "……쓸모 없는 의무감이 생겨나는 것 같았다.",
            "나는 입술을 질겅질겅 씹으며 반짝이는 동화책들 중 하나를 꺼내들었다."
            });
    }

    public string GetStory(int id, int textIndex)
    {
        if (textData.ContainsKey(id) && textIndex < textData[id].Length)
            return textData[id][textIndex];
        else
            return null;
    }
}
