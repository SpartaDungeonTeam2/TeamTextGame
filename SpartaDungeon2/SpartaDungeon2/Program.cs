using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Xml;
using System.Runtime.InteropServices.ComTypes;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.Metadata;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace SpartaDungeon2
{
    internal class Program
    {
        private static PlayerStat player;
        private static List<EnemyStat> enemyList = new List<EnemyStat>();
        public static int startMe = 0;

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
            public bool isAlive;

            public PlayerStat(string _name, string _playerClass, int _level, int _atkValue, int _defValue, int _hpValue, int _gold)
            {
                Name = _name;
                PlayerClass = _playerClass;
                Level = _level;
                AtkValue = _atkValue;
                DefValue = _defValue;
                HpValue = _hpValue;
                Gold = _gold;
                isAlive = true;

                BaseAtkValue = _atkValue;
                BaseDefValue = _defValue;
                BaseHpValue = _hpValue;
            }

            public void PlayerInfo()
            {
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{Level}  {Name} ({PlayerClass})");
                Console.WriteLine($"HP {HpValue}/{BaseHpValue}");
                Console.WriteLine();
            }
        }

        public class EnemyStat // 적 클래스 생성
        {
            public string Name;
            public int Level;
            public int HpValue;
            public int AtkValue;
            public bool isAlive;

            public EnemyStat(string _name, int _level, int _hpValue, int _atkValue)
            {
                Name = _name;
                Level = _level;
                HpValue = _hpValue;
                AtkValue = _atkValue;
                isAlive = true;
            }

            public void MonsterInfo()
            {
                Console.WriteLine($"Lv.{Level} {Name} HP {HpValue}");
            }
        }

        static void PlayerDataSet()
        {
            player = new PlayerStat("Chad", "전사", 1, 10, 5, 100, 1500);
        }

        static void Main()
        {
            PlayerDataSet();
            while (true)
            {
                MainScene();
            }
        }

        static void MainScene()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 스파르타 마을에 오신 여러분 환영합니다.\n 이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine(" 1. 상태 보기\n 2. 전투 시작");
            Console.WriteLine();
            Console.WriteLine(" 원하시는 행동을 입력해주세요.");
            Console.Write(" >> ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Status();
                    break;
                case "2":
                    Battle();
                    break;
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
            Console.WriteLine($"   Lv. {player.Level:D2}");
            Console.WriteLine($" {player.Name} ( {player.PlayerClass} )");
            Console.Write($" 공격력 : {player.AtkValue} ");
            if (player.AtkValue - player.BaseAtkValue > 0)
            {
                Console.Write($"(+{player.AtkValue - player.BaseAtkValue})");
            }
            Console.Write($"\n 방어력 : {player.DefValue} ");
            if (player.DefValue - player.BaseDefValue > 0)
            {
                Console.Write($"(+{player.DefValue - player.BaseDefValue})");
            }
            Console.Write($"\n 체  력 : {player.HpValue} ");
            if (player.HpValue - player.BaseHpValue > 0)
            {
                Console.Write($"(+{player.HpValue - player.BaseHpValue})");
            }
            Console.WriteLine($"\n  Gold  : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine(" 0 : 나가기");
            Console.WriteLine();
            Console.WriteLine(" 원하시는 행동을 입력해주세요.");
            Console.Write(" >>  ");
            Console.ReadKey(true);
        }

        static void Battle()
        {
            // 영선
            InitEnemy();

            Console.WriteLine("1. 공격");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            String input = Console.ReadLine();

            if (input == "1")
            {
                // 4번
                while (isBattleFinished())
                {
                    //2번
                    PlayerPhase();
                    // 3번
                    EnemyPhase();
                }
            }
        }

        // 4번
        public static bool isBattleFinished()
        {
            return true;
        }

        // 플레이어 공격 턴 화면
        public static void PlayerPhase()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" Battle!!\n\n");
            // 몬스터 앞에 숫자 표시
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].HpValue > 0)
                {
                    Console.Write($"{i + 1}. ");
                    enemyList[i].MonsterInfo();
                }
                // 체력이 0이하로 떨어진 몬스터 표시
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{i + 1}. ");
                    Console.WriteLine($"Lv.{enemyList[i].Level} {enemyList[i].Name} Dead");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            PrintPlayerStatus();
            Console.WriteLine("0. 취소\n\n대상을 선택해주세요.");
            Console.Write(" >> ");
            startMe = GetPlayerSelect(0, enemyList.Count);

            switch (startMe)
            {
                // 플레이어 턴 스킵
                case 0:
                    break;
                // 몬스터 선택
                default:
                    // 선택한 몬스터의 체력이 0 이하일 때
                    if (enemyList[startMe - 1].HpValue <= 0)
                    {
                        PlayerPhase();      // 플레이어 공격 턴 화면 표시
                    }
                    else
                    {
                        PlayerPhaseResult(); // 플레이어 전투 결과 화면 표시
                    }
                    break;
            }
        }

        // 플레이어 전투 결과 화면
        public static void PlayerPhaseResult()
        {
            // 플레이어 공격 오차범위 선언 (소숫점 발생 시, 올림 처리)
            float min = player.AtkValue * 0.9f;
            float max = player.AtkValue * 1.1f;
            Random random = new Random();
            int randomAtk = random.Next((int)Math.Ceiling(min), (int)Math.Ceiling(max) + 1);

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"{player.Name} 의 공격!");
            Console.Write($"{enemyList[startMe - 1].Name} 을(를) 맞췄습니다.");
            Console.WriteLine($" [데미지] : {randomAtk}\n");
            Console.WriteLine($"{enemyList[startMe - 1].Name}");
            Console.Write($"HP {enemyList[startMe - 1].HpValue}");
            // 체력이 0 이하인 몬스터 표시
            if ((enemyList[startMe - 1].HpValue -= randomAtk) <= 0)
            {
                Console.WriteLine(" - > Dead");
                enemyList[startMe - 1].isAlive = false;
            }
            // 체력이 0 이상인 몬스터 표시
            else
            {
                Console.WriteLine($" - > {enemyList[startMe - 1].HpValue}");
            }
            Console.WriteLine();
            if (enemyList.Exists(x => x.isAlive == true))
            {
                Console.WriteLine("아무키나 누르면 적의 차례가 시작됩니다.");
                Console.Write(" >> ");
                Console.ReadKey();
            }
            else
            {
                Console.ReadKey(); // 전투 승리 시 정보 표기 안되고 바로 넘어가는 것 방지
                PrintBattleResult();
            }
            
        }

        // 3번
        public static void EnemyPhase()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                EnemyStat enemy = enemyList[i];

                Random AtkDamage = new Random();
                int atkDamage = AtkDamage.Next(enemy.AtkValue - 1, enemy.AtkValue + 2);

                if (enemy.HpValue <= 0)
                {
                    continue;
                }

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("적의 차례입니다.");
                Console.WriteLine();
                Console.WriteLine($"Lv. {enemy.Level} {enemy.Name} 의 공격!");
                Console.WriteLine($"{player.Name} 을(를) 맞췄습니다.     [데미지 : {atkDamage}]");
                Console.WriteLine();
                Console.WriteLine($"Lv. {player.Level} {player.Name}");
                Console.WriteLine($"HP {player.HpValue} -> {player.HpValue - atkDamage}");
                player.HpValue -= atkDamage;
                Console.WriteLine();
                Console.WriteLine("아무키나 누르면 다음으로 넘어갑니다.");
                Console.Write(" >> ");
                Console.ReadKey();
                if (player.HpValue <= 0)
                {
                    break;
                }
            }
            if (player.HpValue <= 0)
            {
                Console.Clear();
                player.HpValue = 0;
                player.isAlive = false;
                PrintBattleResult(false);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("적의 차례가 끝났습니다.");
                Console.WriteLine("아무키나 누르면 플레이어의 차례가 시작됩니다.");
                Console.ReadKey();
            }
            
        }

        // 4번
        public static void PrintBattleResult(bool victory = true)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result|\n");

            if (victory)
            {
                Console.WriteLine("Victory\n");
                Console.WriteLine($"던전에서 몬스터 {enemyList.Count}마리를 잡았습니다.\n");
            }
            else
            {
                Console.WriteLine("You Lose\n");
            }
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP {player.BaseHpValue} -> {player.HpValue}\n");
            Console.WriteLine("아무 키나 누르면 마을로 돌아갑니다.");
            Console.ReadKey();
            player.HpValue = player.BaseHpValue; // Hp 리셋
            MainScene();
        }

        static void AnyKey() // 잘못 입력했을때 초기화면으로 돌아가게 해주는 메서드
        {
            Console.Clear();
            Console.WriteLine(" 잘못된 입력입니다.\n 아무키나 누르면 처음 화면으로 돌아갑니다.");
            Console.ReadKey(true);
        }


        // 영선
        static void InitEnemy()
        {
            enemyList.Clear();
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" Battle!!\n\n");

            // 1~4 사이의 랜덤 숫자 생성
            Random random = new Random();
            int randomMonster = random.Next(1, 5);

            // 적 리스트 초기화 진행
            // enemyList = new List<EnemyStat>();

            // 등장하는 적의 수만큼 반복문 진행
            for (int i = 0; i < randomMonster; i++)
            {
                int enemyId = random.Next(1, 4);

                EnemyStat enemy;

                switch (enemyId)
                {
                    case 1:
                        enemy = new EnemyStat("미니언", 2, 15, 5);
                        break;
                    case 2:
                        enemy = new EnemyStat("공허충", 3, 10, 9);
                        break;
                    case 3:
                        enemy = new EnemyStat("대포미니언", 5, 25, 8);
                        break;
                    default:
                        enemy = new EnemyStat("미니언", 2, 15, 5);
                        break;
                }
                enemyList.Add(enemy);
            }

            PrintEnemyStatus();
            PrintPlayerStatus();
        }

        static void PrintEnemyStatus()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].MonsterInfo();
            }
        }

        static void PrintPlayerStatus()
        {
            player.PlayerInfo();
        }

        // 랜덤으로 생성된 몬스터 선택 메서드 (시작 번호, 끝 번호)
        static int GetPlayerSelect(int start, int end)
        {
            int select = 0;
            bool isNum = false;
            // bool값이 true가 될 때까지
            while (true)
            {
                // int를 bool값으로 변환
                isNum = int.TryParse(Console.ReadLine(), out select);

                // 숫자가 아니거나 시작 번호와 끝 번호 사이에 없는 경우
                if (!isNum || (select < start || select > end))
                {

                    Console.WriteLine("잘못된 입력입니다.");
                }
                else break;
            }
            // 선택한 숫자가 존재하는 경우
            return select;
        }
    }
}