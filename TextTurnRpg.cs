using System;

interface ICharacter
{
    string Name { get; set; }
    int Health { get; set; }
    int Attack { get; set; }
    bool IsDead { get; set; }

    void TakeDamage(int damage);
}
interface IItem
{
    string Name { get; set; }

    void Use(Warrior warrior);

}
public class HealthPotion : IItem
{
    public string Name { get; set; }
    public int Value { get; private set; }

    public HealthPotion(string name)
    {
        Name = name;
        Value = 30;
    }
    public void Use(Warrior warrior)
    {
        Console.WriteLine
            ($" {warrior.Name} 이 {Name} 을 사용하여 {warrior.Health} 가 +{Value} 회복되었습니다");
    }
}
public class StrengthPotion : IItem
{
    public string Name { get; set; }
    public int Value { get; private set; }

    public StrengthPotion(string name)
    {
        Name = name;
        Value = 5;
    }
    public void Use(Warrior warrior)
    {
        Console.WriteLine
            ($" {warrior.Name} 이 {Name} 을 사용하여 {warrior.Attack} 가 +{Value} 상승하였습니다");
    }
}
public class Warrior : ICharacter
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public bool IsDead { get; set; }

    public Warrior(string name, int hp, int atk, bool isDead)
    {
        Name = name;
        Health = hp;
        Attack = atk;
        IsDead = isDead;
    }

    public void AttackAction() // 공격하는 메서드
    {
        Console.WriteLine($" {Name} 의 공격!!! 공격력 :{Attack} ");
        Console.WriteLine();
        Console.WriteLine($" {Name} 이 검을 휘두릅니다. ");
    }
    public void TakeDamage(int damage) // 데미지를 받는 메서드
    {
        if (!IsDead)
        {
            Health -= damage;
            Console.WriteLine($" {damage} 의 데미지를 입었습니다. 남은 체력: {Health}");            
        }
    }
}
public class Monster : ICharacter
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public bool IsDead { get; set; }

    public Monster(string name, int hp, int atk, bool isDead)
    {
        Name = name;
        Health = hp;
        Attack = atk;
        IsDead = isDead;
    }

    public void TakeDamage(int damage) // 데미지를 받는 메서드
    {
        if (!IsDead)
        {
            Health -= damage;
            Console.WriteLine($" {damage} 의 데미지를 입었습니다. 남은 체력: {Health}");            
        }
    }
    public virtual void AttackAction()
    {

    }
}
public class Goblin : Monster
{
    public Goblin(string name, int hp, int atk, bool isDead) : base(name, hp, atk, isDead)
    {
        Name = name;
        Health = hp;
        Attack = atk;
        IsDead = isDead;
    }
    public override void AttackAction()
    {
        Console.WriteLine($" {Name} 의 공격!!! 공격력 :{Attack} ");
        Console.WriteLine();
        Console.WriteLine($" {Name} 이 방망이를 휘두릅니다. ");
    }
}
public class Dragon : Monster
{
    public Dragon(string name, int hp, int atk, bool isDead) : base(name, hp, atk, isDead)
    {
        Name = name;
        Health = hp;
        Attack = atk;
        IsDead = isDead;
    }
    public override void AttackAction()
    {
        Console.WriteLine($" {Name} 의 공격!!! 공격력 :{Attack} ");
        Console.WriteLine();
        Console.WriteLine($" {Name} 이 불을 내뿜습니다. ");
    }
}

namespace TextTurnRpg
{
    internal class TextTurnRpg
    {
        public static Warrior warrior;
        public static Goblin gob;
        public static Dragon dragon;
        public static HealthPotion hpPotion;
        public static StrengthPotion stPotion;
        
        public static bool IsPlaying;
        public static int turn;
        static void Main()
        {              
            GameSetting();

            Console.WriteLine(" 게임을 시작 합니다. ");
            Console.WriteLine();
            Console.WriteLine($" 플레이어의 체력 :{warrior.Health} , 몬스터의 체력 :{gob.Health} ");
            

            while (IsPlaying)
            {
                Console.WriteLine();
                Console.Write(" 다음 행동을 선택하세요 : ");
                CheckUserInput();                
                turn++;
                CheckComputerChoice();
                if (warrior.Health <= 0 || gob.Health <= 0)
                    break;
            }
            Console.ReadKey();
        }
        
        static void CheckUserInput()
        {
            int input = int.Parse(Console.ReadLine());

            switch(input)
            {
                case 3:
                    // 도망가기
                    break;
                case 2:
                    // 아이템 사용
                    break;
                case 1:
                    AttackText(turn);
                    break;
                default :
                    Console.Write(" 잘못된 입력입니다, 다시 입력해주세요! ");
                    CheckUserInput();
                    break;
            } 
        }
        static int CheckComputerChoice()
        {
            int answer;

            answer = Random.Range(1, 3); // 1~2 랜덤
            return answer;
        }
        static void AttackText(int turn)
        {
            if (turn % 2 == 1)
            {
                Console.WriteLine();
                Console.WriteLine(" ~ Player Trun ~ "); // test
                Console.WriteLine();
                warrior.AttackAction();
                Console.WriteLine();
                gob.TakeDamage(warrior.Attack);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(" ~ Monster turn ~ "); // test 
                Console.WriteLine();
                gob.AttackAction();
                Console.WriteLine();
                warrior.TakeDamage(gob.Attack);                
            }
        }
        static void GameEnd()
        {
            IsPlaying = false;
            // Restart?
        }
        static void CheckDead()
        {
            /*
             * if warrior.Isdead true = gameend
             * if gob.Isdead true = UseReward
             * if dragon.Isdead true = Usereward
             */


            UseReward();
        } // 죽은게 몬스터인지 플레이어인지 판단해서 플레이어면 게임 끝
          // 몬스터면 UseReward 실행
        static void UseReward()
        {

        } // 보상 아이템 중 선택하여 사용 input , swithch

        static void GameSetting()
        {
            IsPlaying = true;
            turn = 1;
            warrior = new Warrior("Leo", 300, 10, false);
            gob = new Goblin("Gob", 100, 5, false);
            dragon = new Dragon("Dragon", 500, 20, false);
            hpPotion = new HealthPotion("빨간 물약");
            stPotion = new StrengthPotion("노란 물약");
        }
    }
}