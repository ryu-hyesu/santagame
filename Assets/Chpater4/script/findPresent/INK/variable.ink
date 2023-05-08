INCLUDE globalVar.ink

{next == 0 : -> text00 | {next == 1 : -> text01 | {next == 2: -> text02 | {next==3 : ->text03 | {next==4 : -> text04 |{next==5 : -> text05}}}}}}

=== text00 ==
닥쳐 이 머저리 같은 놈아. #npc:1
날아가지 못하게\\n아이들을 모두 묶었나?#npc:1
당연하죠!#npc:2
이게 무슨 소리지?#npc:0
~ next = 1
-> END

=== text01 ==
너희 여섯 명은 오늘 밤 뱃전에\\n내민 널빤지를 걷게 될 거야.#npc:1
하지만 선실 자리가 비는데,\\n누구 지원 할 사람 없나?#npc:1
그게 무슨 소리예요?#npc:3
너희들 중 두 명은\\n살려주겠다는 의미다.#npc:1
대신, 해적으로서.#npc:1
~ next = 2
-> END

=== text02 ==
난 해적이 되지 않을 거예요!#npc:3
난 마지막 기회를 줬어.\\n #npc:1
명예롭게 살 수 있는 마지막 기회.#npc:1
저 미친 선장은 아직도\\n제정신이 아니네.#npc:0
~ next = 3
-> END

=== text03 ==
널빤지를 걷기 전에\\n채찍 맛 좀 보겠어?#npc:1
싫어요! 살려주세요!#npc:3
빨리 여기서 나가야 해.#npc:0
~ next = 4
-> END

=== text04 ==
여자애를 묶어라!#npc:1
~ next = 5
-> END

=== text05 ==
피터! 와줬구나!#npc:3
쉿, 조용히 해.#npc:4
저 녀석들은 아직\\n내가 여기에 온 걸 몰라.#npc:4
~ next = 6
-> END

=== text06 ==

-> END