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

            public PlayerStat(string _name, string _playerClass, int _level, int _atkValue, int _defValue, int _hpValue, int _gold)
            {
                Name = _name;
                PlayerClass = _playerClass;
                Level = _level;
                AtkValue = _atkValue;
                DefValue = _defValue;
                HpValue = _hpValue;
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

            public EnemyStat(string _name, int _level, int _hpValue, int _atkValue)
            {
                Name = _name;
                Level = _level;
                HpValue = _hpValue;
                AtkValue = _atkValue;
            }
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

        static void MainScene()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 스파르타 마을에 오신 여러분 환영합니다.\n 이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine(" 1. 상태보기\n 2. 인벤토리\n 3. 상점");
            Console.WriteLine();
            Console.WriteLine(" 원하시는 행동을 입력해주세요.");
            Console.Write(" >> ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Status();
                    break;

                // case "2":
                //     Inventory();
                //     break;

                // case "3":
                //     Store();
                //     break;

                default:
                    AnyKey();
                    break;
            }
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
        }

        private static void HighlightedColor(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
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
                HighlightedColor($"{ItemEquipped}");
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
                HighlightedColor($"{ItemEquipped}");
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
                    HighlightedColor("구매완료 \n");
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
                    HighlightedColor("구매완료 \n");
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