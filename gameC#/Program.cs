using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    // =====================================
    // Part 1: Abstraction + Encapsulation
    // =====================================

    abstract class GameCharacter
    {
        // Encapsulation: إخفاء الاسم
        private string _name;

        // Property: قراءة وتعديل الاسم
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Encapsulation: حماية الصحة
        private int _health;

        // Protected Property: الكلاسات الوارثة تقدر تستخدمها
        protected int Health
        {
            get { return _health; }

            set
            {
                if (value > 100)
                    _health = 100; // الحد الأعلى

                else if (value < 0)
                    _health = 0; // الحد الأدنى

                else
                    _health = value;
            }
        }

        // Constructor: تحديد الاسم والصحة
        public GameCharacter(string name, int health)
        {
            Name = name;
            Health = health;
        }

        // Abstraction: كل شخصية لازم تنفذ هذه الميثود
        public abstract void UseSpecialAbility();

        // Virtual Method: يمكن تعديلها في الكلاسات الوارثة
        public virtual void TakeDamage(int amount)
        {
            if (amount < 0)
                amount = 0;

            Health = Health - amount;
        }

        // Method Overloading: علاج 10 نقاط
        public void Heal()
        {
            Heal(10);
        }

        // Method Overloading: علاج حسب الرقم
        public void Heal(int amount)
        {
            if (amount < 0)
                amount = 0;

            int oldHealth = Health;

            Health = Health + amount;

            Console.WriteLine(Name + " heals " +
                (Health - oldHealth) + " points.");

            ShowHealth();
        }

        // عرض صحة الشخصية
        public void ShowHealth()
        {
            Console.WriteLine(Name + "'s health: " + Health);
        }
    }


    // =====================================
    // Part 2: Inheritance + Overriding
    // Warrior
    // =====================================

    class Warrior : GameCharacter // Inheritance
    {
        // Constructor: استدعاء Constructor الأب
        public Warrior(string name, int health)
            : base(name, health)
        {
        }

        // تنفيذ Abstract Method
        public override void UseSpecialAbility()
        {
            Console.WriteLine("The Warrior attacks with a sword!");
        }

        // Overriding: المحارب يخسر ضرراً أقل بنقطتين
        public override void TakeDamage(int amount)
        {
            int damage = Math.Max(0, amount - 2);

            base.TakeDamage(damage); // تنفيذ ميثود الأب

            Console.WriteLine(Name + " takes " + damage + " damage.");
        }
    }


    // =====================================
    // Part 2: Inheritance + Overriding
    // Wizard
    // =====================================

    class Wizard : GameCharacter // Inheritance
    {
        // Constructor
        public Wizard(string name, int health)
            : base(name, health)
        {
        }

        // تنفيذ Abstract Method
        public override void UseSpecialAbility()
        {
            Console.WriteLine("The Wizard uses magic!");
        }

        // Overriding: الساحر يخسر الضرر كاملاً
        public override void TakeDamage(int amount)
        {
            int damage = Math.Max(0, amount);

            base.TakeDamage(damage);

            Console.WriteLine(Name + " takes " + damage + " damage.");
        }
    }


    // =====================================
    // Part 4: Interface
    // =====================================

    interface ICollectible
    {
        void Collect(); // ميثود لجمع العنصر
    }


    // =====================================
    // Part 4: HealthPotion + Try Catch
    // =====================================

    class HealthPotion : ICollectible
    {
        // Property: اسم الجرعة
        public string PotionName { get; set; } = "Health Potion";

        // تنفيذ ميثود Interface
        public void Collect()
        {
            Console.WriteLine(PotionName + " collected!");
        }

        // استخدام الجرعة لعلاج شخصية
        public void Use(GameCharacter character, string amountText)
        {
            try // محاولة تنفيذ الكود
            {
                // تحويل النص إلى رقم
                int amount = int.Parse(amountText);

                // علاج الشخصية
                character.Heal(amount);
            }
            catch (FormatException) // معالجة النص غير الصحيح
            {
                Console.WriteLine("Invalid input! Please enter a number.");
            }
            catch (OverflowException) // معالجة الرقم الكبير جداً
            {
                Console.WriteLine("Number is out of range.");
            }
        }
    }


    // =====================================
    // Part 5: Main - Run the Game
    // =====================================

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== ADVENTURE GAME =====");
            Console.WriteLine();

            // Object: إنشاء المحارب
            Warrior warrior = new Warrior("Alex", 80);

            // Object: إنشاء الساحر
            Wizard wizard = new Wizard("Luna", 70);

            // List: تخزين الشخصيتين
            List<GameCharacter> characters = new List<GameCharacter>();

            characters.Add(warrior);
            characters.Add(wizard);

            // Loop: المرور على جميع الشخصيات
            foreach (GameCharacter character in characters)
            {
                // عرض اسم ونوع الشخصية
                Console.WriteLine(character.GetType().Name + " " +
                                  character.Name + " enters the game!");

                // Polymorphism: تشغيل قدرة كل شخصية
                character.UseSpecialAbility();

                // تطبيق ضرر 10 نقاط
                character.TakeDamage(10);

                // عرض الصحة
                character.ShowHealth();

                Console.WriteLine();
            }

            // Method Overloading: علاج المحارب 10 نقاط
            warrior.Heal();

            Console.WriteLine();

            // Method Overloading: علاج المحارب 15 نقطة
            warrior.Heal(15);

            Console.WriteLine();

            // إنشاء جرعة العلاج
            HealthPotion potion = new HealthPotion();

            // Interface: جمع الجرعة
            potion.Collect();

            Console.WriteLine();

            // Try: إدخال صحيح
            potion.Use(wizard, "15");

            Console.WriteLine();

            // Catch: إدخال خاطئ
            potion.Use(wizard, "abc");
        }
    }
}

