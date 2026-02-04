## 유니티 게임 제작 숙련 팀 프로젝트 5조

## 소개

### 게임 소개
게임 이름 : 탈출! 공장 5호<br>
게임 장르 : 러닝 게임<br>
게임 소개 : 업무에 지친 인공지능 로봇 5호가 공장에서 탈출하기 위해 쫒아오는 감독관 로봇으로부터 도망가는 게임입니다.<br>
           별을 모아 점수를 획득하고 아이템 효과를 이용해 최대한 높은 점수를 얻는 것을 목표로 합니다.

### 팀 소개          
팀 이름 : 5늘의 TIL은 뭘까요?<br>
팀원 : 박범근(팀장), 구민지, 김주원, 백성현, 정성재

### 팀원 별 역할 분담
박범근 : 팀장, 장애물, 아이템 생성, 맵 디자인, 발표자료 준비, 발표<br>
구민지 : 캐릭터 움직임, 애니메이션, 플레이어 체력, 능력, 펫 시스템, 발표자료 준비<br>
김주원 : 매니저 작성, UI 제작, 타이틀 씬 제작, 업적 제작<br>
백성현 : 카메라 이동, 백미러 카메라 제작, 발표자료 준비<br>
정성재 : 게임 매니저 및 게임 흐름 작성, 발표자료 준비<br>



## 게임 설명
(에디터에서는 ManagerScene에서 시작해주세요!)<br>

### 조작
캐릭터를 키보드 W,A,S,D 입력을 통해 조작합니다.<br>
W : 점프<br>
<img width="325" height="458" alt="화면 캡처 2025-11-21 124131" src="https://github.com/user-attachments/assets/bb263015-235a-448e-904b-483b15ea59da" />

S : 슬라이딩<br>
<img width="321" height="461" alt="화면 캡처 2025-11-21 124149" src="https://github.com/user-attachments/assets/c50ce58b-9c49-4c75-a8f1-27f8ca63bb5b" />

A,D : 좌우 이동<br>
<img width="319" height="462" alt="화면 캡처 2025-11-21 124220" src="https://github.com/user-attachments/assets/a0ccfa3b-4fe8-4184-a8a7-e118165d520d" />





### UI
타이틀 화면에서 하단 텍스트를 클릭하여 게임을 시작할 수 있습니다.<br>
<img width="815" height="455" alt="화면 캡처 2025-11-21 124422" src="https://github.com/user-attachments/assets/0aff52f0-79da-4b43-a499-73fdd36fd09d" />

타이틀 좌하단의 펫 버튼을 통해 플레이어와 같이 도망치는 펫을 선택할 수 있습니다.<br>
<img width="817" height="458" alt="화면 캡처 2025-11-21 123641" src="https://github.com/user-attachments/assets/b81116c2-99c5-49b0-8d63-ce950545cfe7" />

세팅 UI를 통해 Bgm, 효과음의 볼륨 조절, 업적을 확인, 타이틀로 이동, 재시작을 선택할 수 있습니다.<br>
<img width="439" height="422" alt="화면 캡처 2025-11-21 124443" src="https://github.com/user-attachments/assets/95608efc-ec80-49f3-95e7-7195d6296486" />
<img width="631" height="374" alt="화면 캡처 2025-11-21 124453" src="https://github.com/user-attachments/assets/04f40b17-e785-4c9c-808f-f644113cb4dc" />


게임오버 UI에서 현재 점수와 최고점수를 확인하고 타이틀 이동, 재시도를 선택할 수 있습니다.<br>
<img width="411" height="438" alt="화면 캡처 2025-11-21 124515" src="https://github.com/user-attachments/assets/51a50189-00ba-41ab-b2ca-2537ac6e58b4" />


### 핵심 기능 설명

1. 장애물, 배경, 아이템 랜덤 스폰 및 오브젝트 풀을 통한 관리<br>
   <img width="543" height="306" alt="화면 캡처 2025-11-21 122403" src="https://github.com/user-attachments/assets/54941c75-09df-4f6a-9b27-b2fd21cc99d5" />
   <img width="239" height="323" alt="화면 캡처 2025-11-21 122422" src="https://github.com/user-attachments/assets/2a1802f1-a7c8-46fd-bfed-ab2c36cdaf07" />
   <img width="601" height="375" alt="화면 캡처 2025-11-21 122850" src="https://github.com/user-attachments/assets/6f8a8874-c4b0-41f0-b744-9f63ce1986c3" />

   반복되는 맵 프리팹 단위에 장애물, 아이템, 별의 생성 위치를 지정하여 확률에 따라 생성되도록 제작하였습니다.<br>
   장애물 등장확률은 게임의 진행에 따라 점점 증가하며 별은 프리팹 단위 당 한줄에서만 등장합니다.<br>
   장애물 등장 위치를 세분화하여 여러개의 프리팹 중 하나가 랜덤으로 적용되도록 하였습니다.<br>

2. 랜더 텍스쳐를 이용한 백미러 구현
    <img width="819" height="455" alt="화면 캡처 2025-11-21 123116" src="https://github.com/user-attachments/assets/ac7e045a-3c81-4efc-8456-ee3a1c27efff" />
    플레이어가 즉사 장애물 이외의 장애물에 최초로 부딪히면 후방에서 플레이어를 따라오는 감독관 로봇의 거리가 가까워지며 화면 죄상단에 가까워진 로봇의 모습이 백미러의 형태로 보여지게 됩니다.<br>
   UI Image에 후방 카메라를 통해 얻은 렌더 텍스쳐를 로우 이미지 컴포넌트를 통해 적용하여 구현하였습니다.<br>
   또한 마스크 이미지를 씌워 백미러 모양의 이미지 위에만 텍스쳐가 표시되도록 하였습니다.<br>

4. 펫을 통한 캐릭터 커스터마이징
  <img width="817" height="458" alt="화면 캡처 2025-11-21 123641" src="https://github.com/user-attachments/assets/468909d0-eab2-4f36-b2ca-a9d06bb3efc6" />
  <img width="816" height="458" alt="화면 캡처 2025-11-21 123712" src="https://github.com/user-attachments/assets/bc291430-d709-44d9-9b6a-0087613e88cd" />
   펫 UI를 통해 3종류의 펫 중 하나를 선택할 수 있습니다.<br>
   펫은 플레이어의 장애물 충돌을 1회 막아주며 이때 회전하는 애니메이션과 함께 효과가 연출됩니다.<br>

## 트러블 슈팅
문제 : 점프 직후 플레이어가 땅에 붙어있는지 확인하는 Landed 파라미터가 정상적으로 반영이 되지 않음<br>
        점프 애니메이션이 점프 직후 달리기 애니메이션으로 변경됨<br>
        
원인 분석 : Unity 물리 엔진은 AddForce 적용 후 실제 Rigidbody 이동이 한 프레임 뒤에 반영됨<br>
            점프 직후에도 IsGrounded() 가 true로 판정 -> landed가 곧바로 true로 설정됨<br>
            
시도 : 코루틴으로 0.9초 후 landed = true 처리<br>
        단점: 점프 높이나 속도에 따라 착지 타이밍 불일치<br>
        
해결 : jumpStartTime 기록 후 Ground 체크를 일정 시간 지연<br>
        FixedUpdate에 Time.time - jumpStartTime >0.1f 조건 추가<br>
        

## Licence
Pet Asset : Copyright (c) 2024-present Tibo




   









