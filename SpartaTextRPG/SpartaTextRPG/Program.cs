internal class Program
{
    private static Character player;
    //private static Item item1;
    private static Item[] myItems;
    private static Item[] storeItems;
    static int equipAtk = 0;
    static int equipDef = 0;

    static void Main(string[] args)
    {
        GameDataSetting();
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("민열", "전사", 1, 10, 5, 100, 15000);

        // 아이템 정보 세팅 => 상점에서 골드 대신 판매 완료를 하려면 어떻게 할까
        Item item1 = new Item("무쇠갑옷", 0, 5, "무쇠로 만들어져 튼튼한 갑옷입니다.", false, "armor", 600, true);
        Item item2 = new Item("낡은 검 ", 2, 0, "마을에서 쉽게 볼 수 있는 낡은 검입니다.", false, "weapon", 500, true);
        Item item3 = new Item("강철 검 ", 8, 0, "질 좋은 강철로 제련된 강철검입니다.", false, "weapon", 1000, false);
        Item item4 = new Item("판금갑옷", 0, 10, "판금으로 만들어져 가볍고 튼튼합니다.", false, "armor", 1100, false);
        Item item5 = new Item("스파르타의 창", 22, 0, "스파르타에서 제작된 전설의 창입니다.", false, "weawpon", 2000, false);
        Item item6 = new Item("스파르타의 갑옷", 0, 22, "스파르타에서 제작된 전설의 갑옷입니다.", false, "armor", 2000, false);


        myItems = new Item[2] { item1, item2 };
        storeItems = new Item[6] { item1, item2, item3, item4, item5, item6 };



    }

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 3);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
                break;
            case 3:
                DisplayStore();
                break;
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.Atk} ({equipAtk})");
        Console.WriteLine($"방어력 : {player.Def} ({equipDef})");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayInventory()  //인벤토리를 보여주는 메소드, 장착관리를 따로 만들지 않고 인벤토리에서 해결
    {
        Console.Clear();
        // 아이템 개수가 몇개인지 확인
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < myItems.Length; i++)
        {

            Console.Write($"- {i + 1} ");
            Console.Write($"\t{myItems[i].Name}\t|\t");

            if (myItems[i].Atk > 0) Console.Write($"공격력 + {myItems[i].Atk}\t|");
            if (myItems[i].Def > 0) Console.Write($"방어력 + {myItems[i].Def}\t|");



            Console.Write($"\t{myItems[i].Inform}");
            Console.WriteLine();

        }

        Console.WriteLine("0. 나가기");
        Console.WriteLine($"1~{myItems.Length}. 1~{myItems.Length}번 아이템 장착 / 해제");

        int input = CheckValidInput(0, myItems.Length);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
            default:
                if (myItems[input - 1].Name.StartsWith("[E]"))
                {

                    myItems[input - 1].Name = myItems[input - 1].Name.Remove(0, 3);
                    myItems[input - 1].IsEquip = false;


                }
                else
                {
                    myItems[input - 1].Name = myItems[input - 1].Name.Insert(0, "[E]");
                    myItems[input - 1].IsEquip = true;
                }

                // ※ type이 웨폰일때, 아머일때 원래 장착한 웨폰이 있고 내가 웨폰을 고르면 원래있던걸 해제하고 지금 고른걸 장착, 아머도 마찬가지

                EquipItem();
                DisplayInventory();
                break;

        }


    }
    static void DisplayStore()
    {
        Console.Clear();

        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < storeItems.Length; i++)
        {

            Console.Write($"- {i + 1} ");
            Console.Write($"\t{storeItems[i].Name}\t|\t");

            if (storeItems[i].Atk > 0) Console.Write($"공격력 + {storeItems[i].Atk}\t|");
            if (storeItems[i].Def > 0) Console.Write($"방어력 + {storeItems[i].Def}\t|");


            Console.Write($"\t{storeItems[i].Inform}\t|");
            foreach (Item it in myItems)
            {
                if (storeItems[i].Equals(it))
                {
                    storeItems[i].IsBuy = true;
                    break;

                }
                else
                {
                    storeItems[i].IsBuy = false;

                }

            }
            if (storeItems[i].IsBuy == true)
            {
                Console.Write("\t구매완료");

            }
            else
            {
                Console.Write($"\t{storeItems[i].Gold}");

            }
            Console.WriteLine();

        }

        Console.WriteLine("0. 나가기");
        Console.WriteLine($"1~{storeItems.Length}. 1~{storeItems.Length}번 아이템 구매");
        Console.WriteLine($"{storeItems.Length + 1}. 아이템 판매");

        int input = CheckValidInput(0, storeItems.Length + 1);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
            case 7:
                SellItem();
                break;
            default:
                // 내 아이템에 들어있는지 확인 후 없으면 돈 체크(보유 금액이 충분하다면 구매를 완료했습니다. 없으면 금액이 부족합니다.(얼마 부족한지도?)), 있으면 이미 구매한 아이템입니다.
                if (storeItems[input - 1].IsBuy == true)
                {
                    Console.WriteLine("이미 보유한 아이템입니다.");

                }
                else
                {
                    if (player.Gold >= storeItems[input - 1].Gold)
                    {
                        Console.WriteLine("구매를 완료했습니다.");
                        // 재화 감소, 인벤토리에 아이템 추가, 상점에 구매완료 표시
                        player.Gold -= storeItems[input - 1].Gold;

                        myItems = myItems.Append(storeItems[input - 1]).ToArray();



                        storeItems[input - 1].IsBuy = true;

                    }
                    else
                    {
                        Console.WriteLine($"{storeItems[input - 1].Gold - player.Gold} G가 부족합니다.");
                    }
                }

                // ※ 구매 정상적으로 이루어지고 중복 구매도 되지 않는것은 확인, 하지만 구매완료 등 메세지가 뜨지 않음
                Thread.Sleep(1000); // 임시방편
                DisplayStore();
                break;

        }



    }

    static void SortItem()
    {
        // ※ 아이템 정렬 -> items 배열의 item1, item2들을 특정 조건(Atk, Def순서 등)으로 정렬할수 있을까?
    }
    static void EquipItem()
    {
        // 아이템 배열 중 장착여부가 true인 것들의 공격력과 방어력을 player.Atk / player.Def에 더함. 변수 필요
        equipAtk = 0;
        equipDef = 0;
        foreach (Item i in myItems)
        {

            if (i.IsEquip == true)
            {
                equipAtk += i.Atk;
                equipDef += i.Def;
            }

        }
        // player 능력치 초기화 필요
        player.Atk = 10;
        player.Def = 5;
        player.Atk += equipAtk;
        player.Def += equipDef;


    }
    static void SellItem()
    {
        Console.Clear();

        Console.WriteLine("상점 - 아이템 판매");
        Console.WriteLine("상점에 아이템을 판매할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < myItems.Length; i++)
        {

            Console.Write($"- {i + 1} ");
            Console.Write($"\t{myItems[i].Name}\t|\t");

            if (myItems[i].Atk > 0) Console.Write($"공격력 + {myItems[i].Atk}\t|");
            if (myItems[i].Def > 0) Console.Write($"방어력 + {myItems[i].Def}\t|");

            Console.Write($"\t{myItems[i].Inform}");
            Console.Write($"\t{myItems[i].Gold}");
            Console.WriteLine();

        }

        Console.WriteLine("0. 나가기");
        Console.WriteLine($"1~{myItems.Length}. 1~{myItems.Length}번 아이템 판매");

        int input = CheckValidInput(0, myItems.Length);
        switch (input)
        {
            case 0:
                DisplayStore();
                break;
            default:
                if (myItems[input - 1].Name.StartsWith("[E]"))
                {

                    myItems[input - 1].Name = myItems[input - 1].Name.Remove(0, 3);
                    myItems[input - 1].IsEquip = false;



                }

                player.Gold += myItems[input - 1].Gold * 0.85f;
                storeItems[input - 1].IsBuy = false;
                //※ 아이템이 판매되는 과정 추가 (인벤토리(myItems)에서 사라지게 해야하는데..)
                Console.WriteLine("판매완료");
                Thread.Sleep(1000);



                SellItem();
                break;

        }

    }

    static int CheckValidInput(int min, int max)    //사용자로부터 입력받고 입력받은 값이 매개변수 사이의 값이 아니면 다시 입력받음
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}


public class Character  // 캐릭터 정보를 담당하는 클래스
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
    public float Gold { get; set; }

    public Character(string name, string job, int level, int atk, int def, int hp, float gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }
}

public class Item   //아이템 정보를 담당하는 클래스
{
    public string Name { get; set; }
    public int Atk { get; }
    public int Def { get; }
    public string Inform { get; }
    public bool IsEquip { get; set; }
    public string Type { get; }
    public float Gold { get; }
    public bool IsBuy { get; set; }
    public Item(string name, int atk, int def, string inform, bool isEquip, string type, float gold, bool isBuy)
    {
        Name = name;
        Atk = atk;
        Def = def;
        Inform = inform;
        IsEquip = isEquip;
        Type = type;
        Gold = gold;
        IsBuy = isBuy;
    }


}