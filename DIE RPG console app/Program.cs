using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DIE_RPG_console_app
{
    class Program
    {
        public const int readTime = 1000;
        //default should be 1000

        public int roundCounter;
        System.Random rnd = new System.Random();
        public int orcsSlain;
        public int testVar;
        public string currentGroupType;

        public void IncreaseVar()
        {
            testVar = +1;
        }

        public class Page
        {
            public int PageNumber;
            public string Text;
            public string IntroTextA;
            public string IntroTextB;
            public string IntroTextC;
            public Page PageA;
            public Page PageB;
            public Page PageC;
            public Page DestA;
            public Page DestB;
            public Page DestC;
            public List<Warrior> CombatEncounter;
            public OffHand ItemPickup;


            public Page(int pagenumber, string text, string introTextA, string introTextB, string introTextC)
            {
                PageNumber = pagenumber;
                Text = text;
                IntroTextA = introTextA;
                IntroTextB = introTextB;
                IntroTextC = introTextC;
            }

            public void EnterRoom(int pagefrom)
            {                

                Console.WriteLine(Text);
                string t = Console.ReadLine();
                switch (t)
                {                  
                        
                        
                }
            }
        }

        public class Weapon
        {
            public string Name;
            public int Dam;
            public int AttackMod;
            public int DefenceMod;

            public Weapon(int dam, int attmod, int defmod, string name)
            {
                Dam = dam;
                AttackMod = attmod;
                DefenceMod = defmod;
                Name = name;
            }

            public string WeaponInfo()
            {
                return ("Name: " + Name + ". Damage: " + Dam);

            }



        }

        public class OffHand
        {
            public string Name;
            public int DefenceBonus;
            public int DefenceMod;
            public string Type;


            public OffHand(int defbonus, int defencemod, string name, string type)
            {
                DefenceBonus = defbonus;
                DefenceMod = defencemod;
                Name = name;
                Type = type;
            }
        }

        public class Item
        {
            //public interface IActiveEffect { };       
        }

        public class Warrior
        {
            public string Name;
            public int Hp;
            public int AttackTrue;
            public int DefenceTrue;
            public int AttackEffective;
            public int DefenceEffective;
            public int Dam;
            public Weapon EquippedWeapon;
            public OffHand EquippedOffHand;
            public List<Item> Inventory;

            public Warrior(int hp, int attack, int defence, string name, Weapon weapon, OffHand offhand)
            {
                Hp = hp;
                AttackTrue = attack;
                DefenceTrue = defence;
                Name = name;
                EquippedWeapon = weapon;
                EquippedOffHand = offhand;

                UpdateStats();
            }
            public void UpdateStats()
            {
                AttackEffective = AttackTrue + EquippedWeapon.AttackMod;
                DefenceEffective = DefenceTrue + EquippedWeapon.DefenceMod + EquippedOffHand.DefenceMod;
                Dam = EquippedWeapon.Dam;
            }

            public void TakeDamage(int dam)
            {
                Hp -= dam;
            }

            public int CalcAttack(System.Random rnd)
            {
                return (rnd.Next(1, AttackEffective + 1));
            }

            public int CalcDefence(System.Random rnd)
            {
                return (rnd.Next(1, DefenceEffective + 1) + EquippedOffHand.DefenceBonus);
            }

            public int CalcDefenceMultibleOpponents(System.Random rnd, int enemies)
            {
                return ((rnd.Next(1, DefenceEffective + 1) / enemies) + EquippedOffHand.DefenceBonus);
            }

            public int CalcDamage(System.Random rnd)
            {
                return (rnd.Next(0, Dam + 1) + Dam);
            }

            public bool Alive()
            {
                if (Hp > 0)
                    return (true);
                else
                    return (false);
            }

            public override string ToString()
            {
                return Name + " HP: " + Hp;
            }
        }

        static void Main(string[] args)
        {
            Program knutnutnutknut = new Program();
            knutnutnutknut.Run();
        }

        public void Run()
        {
            Weapon Axe = new Weapon(4, 0, 0, "axe");
            OffHand none = new OffHand(0, 0, "nothing", "NONE");
            OffHand shield = new OffHand(1, 3, "shield", "SHIELD");
            Warrior player = new Warrior(15, 15, 10, "Fjeldulf", Axe, none);
            Warrior Gundar = new Warrior(10, 5, 10, "Gundar", Axe, shield);

            Warrior orc1 = new Warrior(5, 10, 5, "Orc", Axe, none);
            Page p1 = new Page(1,"You enter Start room", "You come from Shield room", "You come from Orc room", "kys fucking nigger");
            Page p2 = new Page(2,"You enter Shield room", "You come from Start room ", "You come from Orc room", "kys fucking nigger");
            Page p3 = new Page(3, "You enter Orc room", "You come from Start room", "You come from Shield room", "kys fucking nigger");
            Page p4 = new Page(4, "You enter Victory room", "You come from Orc room", "You come from B", "kys fucking nigger");

            List<Warrior> emptyshit = new List<Warrior>();
            List<Warrior> orcs = new List<Warrior>();
            for (int i = 0; i < 3; i++)
            {
                orcs.Add(new Warrior(5, 5, 5, "Orc", Axe, none));
            }

            p1.PageA = p2;
            p1.PageB = p3;
            p1.DestA = p2;
            p1.DestB = p3;
            p1.ItemPickup = none;
            p1.CombatEncounter = emptyshit;

            p2.PageA = p1;
            p2.PageB = p3;
            p2.DestA = p1;
            p2.DestB = p3;
            p2.ItemPickup = shield;
            p2.CombatEncounter = emptyshit;

            p3.PageA = p1;
            p3.PageB = p2;
            p3.PageC = p4;
            p3.DestA = p1;
            p3.DestB = p2;
            p3.DestC = p4;
            p3.ItemPickup = none;
            p3.CombatEncounter = orcs;
            
            p4.PageA = p3;
            p4.PageB = p4;
            p4.DestA = p3;
            p4.ItemPickup = none;
            p4.CombatEncounter = emptyshit;



            // GroupCombatStart(player, orcs);
            // Sleep(2);
            // PrintNewline(player.Name + " died after slaying " + orcsSlain + " orcs");
            // for (int i = 0; 0 < player.Hp; i++)
            // {
            //     Warrior orci = new 
            // }
            //CombatStart(player, orc1);        
            //CombatStart(player, Gundar);


            //string t = Console.ReadLine();  
            ReadPage(p1, p1, player);            
        }



        public void CombatStart(Warrior w1, Warrior w2)
        {
            PrintNewline("The combat begins!");
            roundCounter = 0;
            Sleep(1);
            CombatOverCheck(w1, w2, true);
            

        }

        // The body of combat rounds. Ends in CombatOverCheck()
        public void CombatRound(Warrior w1, Warrior w2)
        {
            roundCounter += 1;
            int attackValueW1 = w1.CalcAttack(rnd);
            int attackValueW2 = w2.CalcAttack(rnd);
            int defenceValueW1 = w1.CalcDefence(rnd);
            int defenceValueW2 = w2.CalcDefence(rnd);
            //PrintNewline(w1.Name + attackValueW1 + defenceValueW1);
            //PrintNewline(w2.Name + attackValueW2 + defenceValueW2);
            if (attackValueW1 > defenceValueW2)
            {
                int currentDam = w1.CalcDamage(rnd);
                w2.TakeDamage(currentDam);
                PrintHitAttack(w1, w2, currentDam);
            }
            else
            {
                PrintParry(w1, w2);
            }

            Sleep(2);
            if (attackValueW2 > defenceValueW1)
            {
                int currentDam = w2.CalcDamage(rnd);
                w1.TakeDamage(currentDam);
                PrintHitAttack(w2, w1, currentDam);
            }
            else
            {
                PrintParry(w2, w1);
            }

            Sleep(2);
            CombatOverCheck(w1, w2, false);

        }

        public void CombatOverCheck(Warrior w1, Warrior w2, bool firstRound)
        {
            PrintNewline(w1.Name + " has " + w1.Hp + " HP left");
            PrintNewline(w2.Name + " has " + w2.Hp + "HP left");
            Sleep(3);
            if (w1.Alive() && w2.Alive())
            {
                if (!firstRound)
                {
                    PrintNewline("Combat carries on");
                    Sleep(2);
                }
                CombatRound(w1, w2);
            }
            else
            {
                if (!w1.Alive() && !w2.Alive())
                {
                    PrintNewline(w1.Name + " and " + w2.Name + " are both dead");
                    Sleep(1.5);
                    PrintNewline("Nobody is Victorious");
                }
                else
                {
                    if (!w1.Alive())
                    {
                        PrintNewline(w1.Name + " has been slain");
                        Sleep(1.5);
                        PrintNewline(w2.Name + " is victorious");
                        Console.ReadLine();
                    }
                    else
                    {
                        PrintNewline(w2.Name + " has been slain");
                        Sleep(1.5);
                        PrintNewline(w1.Name + " is victorious");
                    }
                }                
            }
        }

        public void GroupCombatStart(Warrior w1, List<Warrior> group)
        {
            PrintNewline("The combat begins!");
            roundCounter = 0;
            Sleep(1);
            GroupCombatOverCheck(w1, group, true);
        }

        public void GroupCombatRound(Warrior w1, List<Warrior> group)
        {
            roundCounter += 1;
            int attackValueW1 = w1.CalcAttack(rnd);
            int defenceValueG1 = group[0].CalcDefence(rnd);
            if (attackValueW1 > defenceValueG1)
            {
                int currentDam = w1.CalcDamage(rnd);
                group[0].TakeDamage(currentDam);
                PrintHitAttack(w1, group[0], currentDam);

            }
            else
                PrintParry(w1, group[0]);
            Sleep(2);
            for (int i = 0; i < group.Count; i++)
            {
                if (group[0].CalcAttack(rnd) > w1.CalcDefenceMultibleOpponents(rnd, group.Count))
                {                    
                    int currentDam = group[0].CalcDamage(rnd);
                    w1.TakeDamage(currentDam);
                    PrintHitAttack(group[i], w1, currentDam);
                }
                else
                    PrintParry(group[i], w1);
                Sleep(2);
            }
            //Console.WriteLine("b4 dead members are removed");
            RemoveDeadMembers(group);
            //Console.WriteLine("dead members removed. " + group.Count + " members left");
            GroupCombatOverCheck(w1, group, false);

        }

        public void GroupCombatOverCheck(Warrior w1, List<Warrior> group, bool firstround)
        {
            if (GroupAlive(group))
            {
                currentGroupType = group[0].Name;
            }
            PrintNewline(w1.Name + " has " + w1.Hp + " HP left");
            for (int i = 0; i < group.Count; i++)
            {
                PrintNewline(group[i].Name + " has " + group[i].Hp + " HP left");
            }
            Sleep(group.Count/2 + 2);
            if (w1.Alive() && GroupAlive(group))
            {
                if (!firstround)
                {
                    PrintNewline("Combat carries on");
                }
                GroupCombatRound(w1, group);
            }
            else
            {
                if (!w1.Alive() && !GroupAlive(group))
                {
                    PrintNewline("All combetants have been slain");
                    Sleep(1.5);
                    PrintNewline("Nobody is victorious");
                }
                else
                {
                    if (!w1.Alive())
                    {
                        PrintNewline(w1.Name + " has been slain");
                        Sleep(1.5);
                        PrintNewline(group[0].Name + " and his group is victorious");                        
                    }
                    else
                    {
                        PrintNewline(currentGroupType + " and his group has been slain");
                        Sleep(1.5);
                        PrintNewline(w1.Name + " is victorious");
                    }
                }
            }
        }
        public void PrintNewline(string newText)
        {
            Console.WriteLine(newText);
        }

        public void PrintHitAttack(Warrior attacker, Warrior defender, int currentDam)
        {
            PrintNewline(attacker.Name + " strikes " + defender.Name + " with his " + attacker.EquippedWeapon.Name + " for " + currentDam + " points of damage");
        }

        public void PrintParry(Warrior attacker, Warrior defender)
        {
            if (ShieldWeild(defender.EquippedOffHand))
            {
                PrintNewline(attacker.Name + "'s attack is blocked by " + defender.Name + "'s " + defender.EquippedOffHand.Name);
            }
            else
            {
                PrintNewline(attacker.Name + "'s " + attacker.EquippedWeapon.Name + " is parried by " + defender.Name);
            }
        }

        public bool GroupAlive(List<Warrior> group)
        {
            //Console.WriteLine("groupAlive triggered");
            int livecount = 0;
            for (int i = 0; i < group.Count; i++)
            {
                if (group[i].Alive())
                {
                    livecount += 1;
                }
            }
            if (livecount == 0)
            {
                return false;

            }
            else
                return true;
        }

        public bool ShieldWeild(OffHand offHand)
        {
            if (offHand.Type == "SHIELD")
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        public void RemoveDeadMembers(List<Warrior> group)
        {
            List<int> whomsveToRemove = new List<int>();
            for (int i = group.Count - 1; i >= 0; i--)
            {
                if (!group[i].Alive())
                {
                    orcsSlain += 1;
                    whomsveToRemove.Add(i);
                    PrintNewline(group[i].Name + " has been slain");
                    Sleep(1.5);
                }

            }
            for (int i = 0; i < whomsveToRemove.Count; i++)
            {
                group.RemoveAt(whomsveToRemove[i]);
            }
        }

        public void Sleep(double seconds)
        {
            Thread.Sleep((int)(seconds * readTime));
        }




        //The narrative script
        public void ReadPage(Page CurrentPage, Page PageFrom, Warrior player)
        {
            if (PageFrom == CurrentPage.PageA)
                Console.WriteLine(CurrentPage.IntroTextA);
            if (PageFrom == CurrentPage.PageB)
                Console.WriteLine(CurrentPage.IntroTextB);
            if (PageFrom == CurrentPage.PageC)
                Console.WriteLine(CurrentPage.IntroTextC);
            Sleep(1);
            Console.WriteLine(CurrentPage.Text);
            Sleep(2);
            if (CurrentPage.ItemPickup.Name == "shield")
            {
                player.EquippedOffHand = CurrentPage.ItemPickup;
                PrintNewline("You have picked up a " + CurrentPage.ItemPickup.Name + "!");
                OffHand none = new OffHand(0, 0, "nothing", "NONE");
                CurrentPage.ItemPickup = none;
                Sleep(2);
            }

            if (GroupAlive(CurrentPage.CombatEncounter))
            {
                GroupCombatStart(player, CurrentPage.CombatEncounter);              
            }
            if (player.Alive())
            {
                if (CurrentPage.PageNumber == 4)
                {
                    PrintNewline("You are victorious, well played!");
                    Console.ReadLine();
                }
                else
                {
                    string t = Console.ReadLine();
                    switch (t)
                    {
                        case "a":
                            ReadPage(CurrentPage.DestA, CurrentPage, player);
                            break;
                        case "b":
                            ReadPage(CurrentPage.DestB, CurrentPage, player);
                            break;
                        case "c":
                            ReadPage(CurrentPage.DestC, CurrentPage, player);
                            break;
                        default:
                            ReadPage(CurrentPage, CurrentPage, player);
                            break;


                    }
                }
            }
            else
            {
                PrintNewline("Game Over...");
                Console.ReadLine();
            }
                
            
            
        }

    }
}
