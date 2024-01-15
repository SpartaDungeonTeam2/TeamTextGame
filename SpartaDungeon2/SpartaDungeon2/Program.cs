using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Xml;
using System.Runtime.InteropServices.ComTypes;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.Metadata;

namespace SpartaDungeon2
{
    internal class Program
    {

        private static PlayerStat playerStat;
private static ItemData itemData;
private static EnemyStat Minion;
private static EnemyStat VoidInsect;
private static EnemyStat CanonMinion;

static List<ItemData> itemsInDatabase = new List<ItemData>();
static List<ItemData> playerEquippedItems = new List<ItemData>();
static List<EnemyStat> FirstStageEnemy = new List<EnemyStat>();
static List<EnemyStat> EncounteredEnemy = new List<EnemyStat>();

static void ItemsDatabase()
{
    itemsInDatabase.Add(new ItemData(0, " 수련자 갑옷      ", 0, 5, 0, "수련에 도움을 주는 갑옷입니다.                    ", 1000, false));
    itemsInDatabase.Add(new ItemData(1, " 무쇠 갑옷        ", 0, 9, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.                ", 1500, true));
    itemsInDatabase.Add(new ItemData(2, " 스파르타의 갑옷  ", 0, 15, 0, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다. ", 3500, false));
    itemsInDatabase.Add(new ItemData(3, " 낡은 검          ", 2, 0, 0, "쉽게 볼 수 있는 낡은 검 입니다.                   ", 600, true));
    itemsInDatabase.Add(new ItemData(4, " 청동 도끼        ", 5, 0, 0, "어디선가 사용됐던거 같은 도끼입니다.              ", 1500, false));
    itemsInDatabase.Add(new ItemData(5, " 스파르타의 창    ", 7, 0, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.   ", 3500, true));
}
public class ItemData
{
    public int ItemId;
    public string ItemName;
    public int ItemAtk;
    public int ItemDef;
    public int ItemHp;
    public int ItemPrice;
    public string Description;
    public bool IsItemEquipped;
    public bool IsPlayerOwned;

    public ItemData(int _itemId, string _itemName, int _itemAtk, int _itemDef, int _itemHp, string _description, int _itemPrice, bool _isPlayerOwned)
    {
        ItemId = _itemId;
        ItemName = _itemName;
        ItemAtk = _itemAtk;
        ItemDef = _itemDef;
        ItemHp = _itemHp;
        Description = _description;
        ItemPrice = _itemPrice;
        IsItemEquipped = false;
        IsPlayerOwned = _isPlayerOwned;
    }
}
public class PlayerStat
{
    public string Name;
    public string PlayerClass;
    public int Level;
    public int AtkValue;
    public int DefValue;
    public int HpValue;
    public int Gold;
    public int BaseAtkValue;
    public int BaseDefValue;
    public int BaseHpValue;
    public int FullHpValue;

    public PlayerStat(string _name, string _playerClass, int _level, int _atkValue, int _defValue, int _hpValue, int _gold)
    {
        Name = _name;
        PlayerClass = _playerClass;
        Level = _level;
        AtkValue = _atkValue;
        DefValue = _defValue;
        HpValue = _hpValue;
        FullHpValue = HpValue;
        Gold = _gold;

        BaseAtkValue = _atkValue;
        BaseDefValue = _defValue;
        BaseHpValue = _hpValue;

    }
}

public class EnemyStat // 적 클래스 생성
{
    public string Name;
    public int Level;
    public int HpValue;
    public int AtkValue;
    public bool isAlive;

    public EnemyStat(string _name, int _level, int _hpValue, int _atkValue, bool _isAlive = true)
    {
        Name = _name;
        Level = _level;
        HpValue = _hpValue;
        AtkValue = _atkValue;
        isAlive = _isAlive;
    }
}

public static void EnemyPhase(int Level, string Name, int AtkValue) // 적의 공격화면 출력 메서드
{

    Random AtkDamage = new Random();                            // 적 공격력 랜덤 설정용 랜덤 생성
    int atkDamage = AtkDamage.Next(AtkValue - 1, AtkValue + 2); // 적 공격력 오차범위 ± 1로 설정

    Console.Clear();
    Console.WriteLine(" 적의 차례입니다.");
    Thread.Sleep(500); // 잠깐 멈췄다가 등장시키는 기능, 불필요한 기능이므로 삭제해도 무방
    Console.WriteLine();
    Console.WriteLine($" Lv. {Level} {Name} 의 공격!");
    Console.WriteLine($" {playerStat.Name} 을(를) 맞췄습니다.    [데미지 : {atkDamage}]"); // 공격력 오차범위 적용을 위한 atkDamage 변수 사용
    Console.WriteLine();
    Console.WriteLine($" Lv.{playerStat.Level} {playerStat.Name}");
    Console.WriteLine($" HP {playerStat.HpValue} -> {playerStat.HpValue - atkDamage}");
    playerStat.HpValue -= atkDamage; // 표기상으로만 체력이 깎인게 아니라 실제로 체력이 깎여야하므로 HpValue - atkDamage 함
    Console.WriteLine();
    Console.WriteLine(" 아무키나 누르면 다음으로 넘어갑니다.");
    Console.Write(" >> ");
    Console.ReadKey();
}

static void EnemyAttack_Minion() // 적이 미니언일때 발동하는 메서드
{
    EnemyPhase(Minion.Level, Minion.Name, Minion.AtkValue);
}

static void EnemyAttack_Void() // 적이 공허충일때 발동하는 메서드
{
    EnemyPhase(VoidInsect.Level, VoidInsect.Name, VoidInsect.AtkValue);
}

static void EnemyAttack_Canon() // 적이 대포미니언일때 발동하는 메서드
{
    EnemyPhase(CanonMinion.Level, CanonMinion.Name, CanonMinion.AtkValue);
}
static void EnemyDataSet() // 적 데이터 생성
{
    Minion = new EnemyStat("미니언", 2, 15, 5);
    VoidInsect = new EnemyStat("공허충", 3, 10, 9);
    CanonMinion = new EnemyStat("대포미니언", 5, 25, 8);
}

static void PlayerDataSet()
{
    playerStat = new PlayerStat("Chad", "전사", 1, 10, 5, 100, 1500);
}

static void Main()
{
    PlayerDataSet();
    ItemsDatabase();
    EnemyDataSet(); // 게임 시작할때 적 데이터 세팅
    MainScene();
}

private static int CheckValidInput(int min, int max) // 입력값을 처리하는 메서드(개인과제 해설 참조)
{
    int keyInput; // 올바른 입력값용 변수
    bool result; // 입력값을 int로 TryParse하기 위해 사용하는 변수
    do
    {
        Console.WriteLine(" 원하시는 행동을 입력해주세요.");
        Console.Write(" >> ");
        result = int.TryParse(Console.ReadLine(), out keyInput); // 입력값을 int로 변경시도, 변경되면 keyInput으로 out
    } while (result == false || CheckIfValid(keyInput, min, max) == false); // do-while문을 써서, 입력값이 int가 안되거나, 
                                                                            // CheckIfValid가 true가 아니라면 반복하게 함
    return keyInput;
}

private static bool CheckIfValid(int keyInput, int min, int max) // 입력값이 올바른 int 값인지 확인하는 메서드
{
    if (min <= keyInput && keyInput <= max) // CheckValidInput에서 설정한 값에 맞다면?
    {
        return true; // CheckIfValid의 bool값이 true이므로 CheckValidInput에서 keyInput을 return한다.
    }
    else // 올바르지 않은 값을 입력했을 경우
    {
        Console.WriteLine(" 잘못된 입력입니다."); 
        return false; // 잘못된 입력입니다 출력 후 false를 return 하므로 CheckValidInput의 do-while문을 다시 실행함
    }

}
static void MainScene()
{
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine(" 스파르타 마을에 오신 여러분 환영합니다.\n 이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
    Console.WriteLine();
    Console.WriteLine(" 1. 상태보기\n 2. 전투 시작");
    Console.WriteLine();
    Console.WriteLine(" 원하시는 행동을 입력해주세요.");
    Console.Write(" >> ");
    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            Status();
            break;

        //case "2":
        //    Inventory();
        //    break;

        // case "3":
        //     Store();
        //     break;

        case "2":
            Battle();
            break;

        default:
            AnyKey();
            break;
    }
}

public static void Battle() // 본격적인 배틀페이즈 시작이전 적 생성 페이즈
{
    FirstStageEnemy.Clear();
    FirstStageEnemy.Add(Minion);
    FirstStageEnemy.Add(VoidInsect);
    FirstStageEnemy.Add(CanonMinion); // 밑의 생성파트에서 사용할 FirstStageEnemy 리스트에 적 정보 추가

    EncounteredEnemy.Clear(); // 전투창과 메인창 여러번 왔다갔다 할때 몬스터가 계속해서 쌓이는 

    Random EnemyCnt = new Random();
    int RandCnt = EnemyCnt.Next(1, 5); // 랜덤한 수의 적 생성을 위한 랜덤 변수

    for (int i = 0; i < RandCnt; i++)
    {
        Random rnd = new Random();
        int randIndex = rnd.Next(FirstStageEnemy.Count);
        EnemyStat Enemy = FirstStageEnemy[randIndex]; // 미니언, 공허충, 대포미니언 중 랜덤한 적을 생성하게 해줌
        EncounteredEnemy.Add(Enemy); // 생성된 적을 리스트로 저장함
    }
    PlayerTurn(); // 적 생성 완료 후 플레이어의 턴으로 넘김
}


private static void PlayerTurn() // 플레이어턴 메서드
{
    Console.Clear();
    Console.WriteLine(" 플레이어의 차례입니다.");
    Console.WriteLine();
    for (int i = 0; i < EncounteredEnemy.Count; i++) // 생성된 적들의 정보를 표기해주는 for 반복문
    {
        EnemyStat Enemy = EncounteredEnemy[i]; // EncounteredEnemy의 i번째 index를 가진 적을 Enemy로 할당
        Console.Write($" - Lv.{Enemy.Level} ");
        Console.Write($"{Enemy.Name} ");
        Console.Write($" HP {Enemy.HpValue}\n");
    }
    Console.WriteLine();
    Console.WriteLine(" [내정보]");
    Console.WriteLine($" Lv. {playerStat.Level} {playerStat.Name} ({playerStat.PlayerClass})");
    Console.WriteLine($" Hp {playerStat.HpValue}/{playerStat.FullHpValue}");
    Console.WriteLine();
    Console.WriteLine(" 1. 공격 \n 2. 마을로 돌아가기");
    Console.WriteLine();
    switch (CheckValidInput(1, 2)) // 개인과제 해설영상에 올라온 입력값 처리 메서드를 활용하여 다음 메서드로 넘어가게 함
    {
        case 1:
            PlayerTurn_Attack();
            break;
        case 2:
            MainScene(); // 이건 테스트할때 원활하게 할 용도로 만든 것, 제출 전 수정요망
            break;
    }
}

private static void PlayerTurn_Attack() // 플레이어 공격 화면 메서드
{
    // 플레이어 공격 부분에서 문제가 있습니다. 여러 적이 나타날 시 중복해서 나타나게 되는 경우가 많은데 (미니언 2, 공허충 1 과 같은 방식으로)
    // 이때 플레이어가 중복된 적(미니언 2, 공허충 1에서 미니언 아무거나 한마리)을 공격했을때 중복된 적 모두의 HP가 감소하게 됩니다.
    // 이에 대한 문제 해결 방식을 모색해봤지만 해결하지 못했습니다..
    // 근데 제 파트도 아니고 관철님이 잘 만들어주실 거라고 생각합니다. (@.@) 그냥 이사람은 이렇게 했구나 정도로만 봐주시면 좋을 것 같습니다.

    int minDamage = (int)(playerStat.AtkValue * 0.9); // 플레이어 공격 오차범위 최솟값
    int maxDamage = (int)(playerStat.AtkValue * 1.1); // 플레이어 공격 오차범위 최댓값
    Random PlayerAttack = new Random();
    int playerAttack = PlayerAttack.Next(minDamage, maxDamage + 1); // 공격 오차범위 랜덤적용

    Random Critical = new Random(); // 크리티컬 적용을 위한 랜덤 생성
    int critical = Critical.Next(0, 11); 
    int CriticalDamage = (int)(playerAttack * 1.5); // 크리티컬 대미지 적용 (150%) 

    Console.Clear();
    Console.WriteLine(" 플레이어의 차례입니다.");
    Console.WriteLine();
    for (int i = 0; i < EncounteredEnemy.Count; i++) // 적 정보 표기
    {
        EnemyStat selectedEnemy = EncounteredEnemy[i];
        Console.Write(" - ");                           // 플레이어 턴 메서드에서 추가된 요소
        HighlightedColor($"{i + 1}", ConsoleColor.Red); // HighlightedColor 메서드 활용, 적 번호에 색깔 입힘
        Console.Write($" Lv.{selectedEnemy.Level} ");
        Console.Write($"{selectedEnemy.Name} ");
        Console.Write($" HP {selectedEnemy.HpValue}\n");
    }
    Console.WriteLine();
    Console.WriteLine(" [내정보]");
    Console.WriteLine($" Lv. {playerStat.Level} {playerStat.Name} ({playerStat.PlayerClass})");
    Console.WriteLine($" Hp {playerStat.HpValue}/{playerStat.FullHpValue}");
    Console.WriteLine();
    Console.WriteLine(" 적의 번호. 적 공격하기 \n 0. 마을로 돌아가기");
    Console.WriteLine();
    Console.WriteLine(" 원하시는 행동을 입력해주세요.");
    Console.Write(" >> ");
    int input = int.Parse(Console.ReadLine());
    if (input != 0 && input <= EncounteredEnemy.Count) // 올바른 값(적 번호)이 입력 됐을 시 실행되는 조건문
    {
        Console.Clear();
        EnemyStat selectedEnemy = EncounteredEnemy[input - 1]; // index는 0부터 시작하고 input은 1부터 시작하므로 input - 1
        Console.WriteLine($" {selectedEnemy.Name} 을 공격하셨습니다.");
        if (critical >= 10) // 10% 확률로 치명타 구현
        {
            Console.Clear();
            Console.WriteLine($" 치명타! {CriticalDamage} 만큼의 데미지를 입혔습니다!");
            selectedEnemy.HpValue -= CriticalDamage;
        }
        else // 치명타 아닌 일반공격
        {
            Console.WriteLine($" {playerAttack} 만큼의 데미지를 입혔습니다.");
            selectedEnemy.HpValue -= playerAttack;
        }

        if (selectedEnemy.HpValue <= 0) // 처치했을시 출력되는 메시지
        {
            Console.WriteLine($" {selectedEnemy.Name} 을 처치하셨습니다.");
            selectedEnemy.isAlive = false;
        }
        else // 남은 체력 출력
        {
            Console.WriteLine($" {selectedEnemy.Name} 의 남은 체력 : {selectedEnemy.HpValue}");
        }

        Console.WriteLine();
        Console.WriteLine($" {playerStat.Name} 의 턴을 종료합니다.");
        Console.WriteLine($" 아무 키나 누르면 적의 턴으로 넘어갑니다."); //
        Console.Write(" >> ");                                        // AnyKey 메서드에 활용한 '아무키나..'
        Console.ReadKey(true);                                        //
        EnemyTurn();
    }
    else if (input == 0) // 원활한 테스트를 위해 작성한 내용, 수정필요
    {
        MainScene();
    }
}

public static void EnemyTurn() // 적 차례, 위에 있는 EnemyPhase와 EnemyAttack_Name 메서드를 활용하였음
{
    
    for (int i = 0; i < EncounteredEnemy.Count; i++) // 적의 수만큼 적이 공격하는 것을 반복
    {
        EnemyStat Enemy = EncounteredEnemy[i]; // EncounteredEnemy의 index가 늘어야 첫번째 적으로 적 종류가 통일되는 것이 방지됨

        if (Enemy.HpValue <= 0) // 처치당한 적은 continue로 넘겨서 공격하지 않음.
        {
            continue;
        }
        else // 위의 EnemyStat Enemy에 EncounteredEnemy의 i번째 목록을 불러왔고 Enemy가 가진 정보(EnemyStat 기반)의 Name을 확인하여 조건에 맞는 메서드를 출력
        {
            if (Enemy.Name == "미니언") 
            {
                EnemyAttack_Minion();
            }
            else if (Enemy.Name == "공허충")
            {
                EnemyAttack_Void();
            }
            else if (Enemy.Name == "대포미니언")
            {
                EnemyAttack_Canon();
            }
        }
    }
    
    Console.WriteLine("");
    Console.WriteLine(" 적의 차례가 끝났습니다.");
    Console.WriteLine(" 아무 키나 누르면 플레이어의 차례로 돌아갑니다.");
    Console.Write(" >> ");
    Console.ReadKey(true);
    PlayerTurn(); // 적의 공격이 끝나면 플레이어의 턴으로 돌아감
}

static void Status()
{
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine(" 상태 보기\n캐릭터의 정보가 표시됩니다.");
    Console.WriteLine();
    Console.WriteLine($"   Lv. {playerStat.Level:D2}");
    Console.WriteLine($" {playerStat.Name} ( {playerStat.PlayerClass} )");
    Console.Write($" 공격력 : {playerStat.AtkValue} ");
    if (playerStat.AtkValue - playerStat.BaseAtkValue > 0)
    {
        Console.Write($"(+{playerStat.AtkValue - playerStat.BaseAtkValue})");
    }
    Console.Write($"\n 방어력 : {playerStat.DefValue} ");
    if (playerStat.DefValue - playerStat.BaseDefValue > 0)
    {
        Console.Write($"(+{playerStat.DefValue - playerStat.BaseDefValue})");
    }
    Console.Write($"\n 체  력 : {playerStat.HpValue} ");
    if (playerStat.HpValue - playerStat.BaseHpValue > 0)
    {
        Console.Write($"(+{playerStat.HpValue - playerStat.BaseHpValue})");
    }
    Console.WriteLine($"\n  Gold  : {playerStat.Gold} G");
    Console.WriteLine();
    Console.WriteLine(" 0 : 나가기");
    Console.WriteLine();
    Console.WriteLine(" 원하시는 행동을 입력해주세요.");
    Console.Write(" >>  ");
    string input = Console.ReadLine();

    if (input == "0")
    {
        MainScene();
    }
    else
    {
        AnyKey();
    }
}

static void UpdatePlayerStats()
{
    int totalAtk = 0;
    int totalDef = 0;
    int totalHp = 0;

    foreach (ItemData item in playerEquippedItems)
    {
        totalAtk += item.ItemAtk;
        totalDef += item.ItemDef;
        totalHp += item.ItemHp;
    }

    playerStat.AtkValue = playerStat.BaseAtkValue + totalAtk;
    playerStat.DefValue = playerStat.BaseDefValue + totalDef;
    playerStat.HpValue = playerStat.BaseHpValue + totalHp;
    playerStat.FullHpValue = playerStat.HpValue = playerStat.BaseHpValue + totalHp;
}

private static void HighlightedColor(string text, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.Write(text);
    Console.ResetColor();
}


static void Inventory()
{
    string ItemEquipped;

    Console.Clear();
    Console.WriteLine();
    Console.WriteLine(" 인벤토리\n 보유 중인 아이템을 관리할 수 있습니다.");
    Console.WriteLine();
    Console.WriteLine(" [아이템 목록]");

    for (int i = 0; i < itemsInDatabase.Count; i++)
    {
        ItemData item = itemsInDatabase[i];
        if (item.IsPlayerOwned != true)
        {
            continue;
        }

        ItemEquipped = item.IsItemEquipped ? "[E] " : "";
        Console.Write($" - {i + 1:D2} ");
        HighlightedColor($"{ItemEquipped}", ConsoleColor.Yellow);
        Console.Write($" {item.ItemName}");
        DisplayAtkOrDef(item);
        Console.WriteLine($" {item.Description} ");

    }
    Console.WriteLine();
    Console.WriteLine(" 1. 장착 관리\n 0. 나가기");
    Console.WriteLine();
    Console.WriteLine(" 원하시는 행동을 입력해주세요.");
    Console.Write(" >> ");
    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            ManagementEquipment();
            break;

        case "0":
            MainScene();
            break;

        default:
            AnyKey();
            break;
    }
}

static void ManagementEquipment()
{
    string ItemEquipped;

    Console.Clear();
    Console.WriteLine();
    Console.WriteLine(" 인벤토리 - 장착 관리\n 보유 중인 아이템을 관리할 수 있습니다.");
    Console.WriteLine();
    Console.WriteLine(" [아이템 목록]");

    for (int i = 0; i < itemsInDatabase.Count; i++)
    {
        ItemData item = itemsInDatabase[i];
        if (item.IsPlayerOwned != true)
        {
            continue;
        }

        ItemEquipped = item.IsItemEquipped ? "[E] " : "";

        Console.Write($" - {i + 1:D2} ");
        HighlightedColor($"{ItemEquipped}", ConsoleColor.Yellow);
        Console.Write($" {item.ItemName}");
        DisplayAtkOrDef(item);
        Console.WriteLine($" {item.Description} ");

    }
    Console.WriteLine();
    Console.WriteLine(" 0. 나가기 \n 아이템 번호. 아이템 장착/해제");
    Console.WriteLine();
    Console.WriteLine(" 원하시는 행동을 입력해주세요.");
    Console.Write(" >> ");
    int input = int.Parse(Console.ReadLine());
    if (input == 0)
    {
        Inventory();
    }
    else if (input > 0 && input <= itemsInDatabase.Count)
    {
        ItemData selectedItem = itemsInDatabase[input - 1]; // item리스트의 번호와 달리 index는 0부터 시작하므로 -1 해준다.
        ItemEquip(selectedItem);
        ManagementEquipment();
    }
    else if (input > 0 && input > itemsInDatabase.Count)
    {
        AnyKey();
    }
}

static void ItemEquip(ItemData item)
{

    item.IsItemEquipped = !item.IsItemEquipped;

    if (item.IsItemEquipped)
    {
        playerEquippedItems.Add(item);
    }
    else
    {
        playerEquippedItems.Remove(item);
    }
    UpdatePlayerStats();
}

static void DisplayAtkOrDef(ItemData item)
{
    if (item.ItemAtk > 0 && item.ItemAtk < 10 && item.ItemDef == 0)
    {
        Console.Write($" |   공격력 + {item.ItemAtk}   |");
    }
    else if (item.ItemAtk >= 10 && item.ItemDef == 0) // 공격력 10 이상일 경우 칸 안맞는거 개선
    {
        Console.Write($" |   공격력 + {item.ItemAtk}  |");
    }
    else if (item.ItemAtk == 0 && item.ItemDef > 0 && item.ItemDef < 10)
    {
        Console.Write($" |   방어력 + {item.ItemDef}   |");
    }
    else if (item.ItemAtk == 0 && item.ItemDef >= 10) // 방어력 10 이상일 경우 칸 안맞는거 개선
    {
        Console.Write($" |   방어력 + {item.ItemDef}  |");
    }
}


static void Store()
{
    Console.Clear();
    Console.WriteLine(" 상점\n 필요한 아이템을 얻을 수 있는 상점입니다.");
    Console.WriteLine();
    Console.WriteLine(" [보유 골드]");
    Console.WriteLine($" {playerStat.Gold} G");
    Console.WriteLine();
    Console.WriteLine(" [아이템 목록]");
    for (int i = 0; i < itemsInDatabase.Count; i++)
    {
        ItemData item = itemsInDatabase[i];

        Console.Write($" - {item.ItemName}");
        DisplayAtkOrDef(item);
        Console.Write($" {item.Description} | ");
        if (!item.IsPlayerOwned)
        {
            Console.Write($"{item.ItemPrice} G \n");
        }
        else
        {
            HighlightedColor("구매완료 \n", ConsoleColor.Yellow);
        }
    }
    Console.WriteLine();
    Console.WriteLine(" 1. 아이템 구매\n 0. 나가기");
    Console.WriteLine();
    Console.WriteLine(" 원하시는 행동을 입력해주세요.");
    Console.Write(" >> ");
    string input = Console.ReadLine();
    switch (input)
    {
        case "1":
            buyItems();
            break;

        case "0":
            MainScene();
            break;

        default:
            AnyKey();
            break;
    }
}

static void buyItems()
{
    Console.Clear();
    Console.WriteLine(" 상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.");
    Console.WriteLine();
    Console.WriteLine(" [보유 골드]");
    Console.WriteLine($" {playerStat.Gold} G");
    Console.WriteLine();
    Console.WriteLine(" [아이템 목록]");
    for (int i = 0; i < itemsInDatabase.Count; i++)
    {
        ItemData item = itemsInDatabase[i];

        Console.Write($" - {i + 1:D2} ");
        Console.Write($"{item.ItemName}");
        DisplayAtkOrDef(item);
        Console.Write($" {item.Description} | ");
        if (!item.IsPlayerOwned)
        {
            Console.Write($"{item.ItemPrice} G \n");
        }
        else
        {
            HighlightedColor("구매완료 \n", ConsoleColor.Yellow);
        }
    }
    Console.WriteLine();
    Console.WriteLine(" 0. 나가기 \n 아이템 번호. 아이템 구매하기");
    Console.WriteLine();
    Console.WriteLine(" 원하시는 행동을 입력해주세요.");
    Console.Write(" >> ");
    int input = int.Parse(Console.ReadLine());

    if (input == 0)
    {
        MainScene();
    }
    else if (input != 0 && input <= itemsInDatabase.Count)
    {
        ItemData selectedShopItem = itemsInDatabase[input - 1]; // 인벤토리와 마찬가지

        if (playerStat.Gold < selectedShopItem.ItemPrice && selectedShopItem.IsPlayerOwned != true)
        {
            Console.Clear();
            Console.WriteLine(" Gold가 부족합니다.\n 잠시 후 아이템 구매창으로 되돌아갑니다.");
            Thread.Sleep(3000);
            buyItems();
        }
        if (playerStat.Gold >= selectedShopItem.ItemPrice && selectedShopItem.IsPlayerOwned != true)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($" 선택한 아이템: {selectedShopItem.ItemName}");
            Console.WriteLine($" 선택한 아이템의 성능");
            Console.WriteLine($" 공격력 : + {selectedShopItem.ItemAtk}");
            Console.WriteLine($" 방어력 : + {selectedShopItem.ItemDef}");
            Console.WriteLine($" 체  력 : + {selectedShopItem.ItemHp}");
            Console.WriteLine(" 정말로 구매하시겠습니까?\n 1. 구매\n 0. 취소");
            Console.Write(" >> ");
            string buyInput = Console.ReadLine();
            switch (buyInput)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine(" 구매가 완료되었습니다.\n 잠시 후 아이템 구매창으로 되돌아갑니다.");
                    playerStat.Gold -= selectedShopItem.ItemPrice;
                    selectedShopItem.IsPlayerOwned = true;
                    Thread.Sleep(3000);
                    buyItems();
                    break;

                case "0":
                    Console.Clear();
                    Console.WriteLine(" 구매를 취소하셨습니다.\n 잠시 후 아이템 구매창으로 되돌아갑니다.");
                    Thread.Sleep(3000);
                    buyItems();
                    break;

                default:
                    AnyKey();
                    break;
            }
        }
        if (selectedShopItem.IsPlayerOwned == true)
        {
            Console.Clear();
            Console.WriteLine(" 이미 구매한 아이템입니다. \n 잠시 후 아이템 구매창으로 되돌아갑니다.");
            Thread.Sleep(3000);
            buyItems();
        }
    }
    else if (input != 0 && input > itemsInDatabase.Count)
    {
        AnyKey();
    }

}

static void AnyKey() // 잘못 입력했을때 초기화면으로 돌아가게 해주는 메서드
{
    Console.Clear();
    Console.WriteLine(" 잘못된 입력입니다.\n 아무키나 누르면 처음 화면으로 돌아갑니다.");
    Console.ReadKey(true);
    MainScene();
}
    }
}