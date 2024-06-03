# 아쿠아리움 육성 게임, 스카이 아쿠아리움

해양생물을 키워 더 많은 골드를 획득하고, 그 골드로 아쿠아리움을 꾸미고 발전시키는 캐주얼 게임입니다.

# 개발 기간 및 개발 환경
개발 기간: 2024.01.03 ~ 2024.02.26

개발 인원: 1명

개발 환경

- C#
- Visual Studio
- Unity 2021.3.15f1
-GitHub

<br>

# 주요 기능

<div align=center>
  <img width="719" src="https://github.com/Junghs-cloud/SkyAquarium/assets/77110178/62c43228-0b25-40d2-a339-0a02ac9c50a3">
</div>
<br>

- 골드를 이용하여 해양 생물과 해양생물의 먹이를 생산하는 빌딩, 아쿠아리움을 장식할 수 있는 데코레이션 등의 아이템을 구입할 수 있습니다.
- MANAGE 버튼을 눌러 꾸미기 버튼에 진입하면, 데코레이션 아이템들을 배치하고, 판매하고, 인벤토리에 저장하고 꺼낼 수 있습니다.
- 빌딩에서부터 먹이를 생산하면, 해양생물에게 먹이를 주고 레벨업 시킬 수 있습니다. 레벨업하면 더 많은 골드를 획득할 수 있습니다.

<br>

## 상점

<div align = center>
  <img width="400" src="https://github.com/Junghs-cloud/SkyAquarium/assets/77110178/9ceb5778-a030-413f-a708-c431af414a7d">
  <img width="400"  src="https://github.com/Junghs-cloud/SkyAquarium/assets/77110178/7ce07930-219e-4f71-8c50-1754780bcb14">
</div>

<br>

각종 아이템을 구입할 수 있는 상점입니다.

상속을 사용하여 중복을 줄이려 노력하였습니다.
- 부모 클래스: 아이템을 구매하는 함수, 구매 가능 여부를 확인하는 함수, 아이템 이름을 가져오는 함수 등 구현.
- 자식 클래스: 해양 생물 섹션, 데코레이션 아이템 섹션, 기타 아이템 섹션 등의 자식 클래스를 두어 부모 클래스의 함수들을 오버라이딩하는 방식으로 구현하였습니다.


<div align=center>
  <img width="808" alt="shop" src="https://github.com/Junghs-cloud/SkyAquarium/assets/77110178/ee2121b7-991b-44df-a118-baa8c6671500">
  <br>
  <img width="1115" alt="shop1" src="https://github.com/Junghs-cloud/SkyAquarium/assets/77110178/9117b0ee-79a6-4123-9dd5-a3d3f62abc95">
</div>
