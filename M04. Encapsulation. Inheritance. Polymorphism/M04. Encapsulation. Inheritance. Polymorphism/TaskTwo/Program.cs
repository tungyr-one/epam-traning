using System;
using System.Collections.Generic;

namespace TaskTwo
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Game
    {
        public int Score { get; set; }

        public void InitGame()
        {
        }

        public void InitField(){ throw new NotImplementedException(); }       

    }

    class Field
    {
        public int Width { get; set; }
        public int Height { get; set; }

        Field()
        {
            this.Width = 100;
            this.Height = 50;
        }

        static public void SetObjectsStartPositions(){throw new NotImplementedException();}
    }

    interface IGameObject
    {
        Position Position { get; set; }
    }

    interface IMoveable : IGameObject
    {
        void Move();
        void ObstacleHit();
    }

    abstract class GameObject : IGameObject
    {
        public virtual Position Position { get; set; }

        public GameObject(Position position)
        {
            this.Position = position;
        }        
    }

    abstract class Moveable : GameObject
    {
        public int MoveSpeed { get; set; }

        public Moveable(Position position, int moveSpeed) : base(position)
        {
            this.MoveSpeed = moveSpeed;
        }
    }

    abstract class Character : Moveable, IGameObject
    {        
        public bool IsAlive { get; set; }
        public int Health { get; set; }
        private const int CharacterDefaultSpeed = 3;
        private const int CharacterDefaultHealth = 20;

        public Character(Position position, bool isAlive)
            : base(position, CharacterDefaultSpeed)
        {
            this.IsAlive = isAlive;
            this.Health = CharacterDefaultHealth;
        }

        public void ObstacleHit() { throw new NotImplementedException(); }
    }

    class Player : Character, IGameObject, IMoveable
    {

        public Player(Position position)
            : base(position, true)
        {
            this.MoveSpeed = 3;
        }

        public void PickUpBonus() { throw new NotImplementedException(); }
        public void Move() { throw new NotImplementedException(); }
    }  


    // monster classes

    abstract class Monster : Character, IGameObject
    {
        public int Damage { get; set; }
        private const int MonsterDefaultHealth = 20;

        public Monster(Position position, bool isAlive, int damageValue)
            : base(position, isAlive)
        {
            this.Damage = damageValue;
            this.Health = MonsterDefaultHealth;

        }

        public abstract void Hunt();
        public abstract void Attack();
    }

    class Wolf: Monster, IMoveable
    {
        private const int damageValue = 1;

        public Wolf(Position position)
            : base(position, true, damageValue)
        {
        }

        public void Move() { throw new NotImplementedException(); }
        public override void Hunt() { throw new NotImplementedException(); }
        public override void Attack() { throw new NotImplementedException(); }
    }

    class Bear: Monster, IMoveable
    {
        private const int damageValue = 2;

        public Bear(Position position)
            : base(position, true, damageValue)
        {
        }

        public void Move() { throw new NotImplementedException(); }
        public override void Hunt() { throw new NotImplementedException(); }
        public override void Attack() { throw new NotImplementedException(); }
    }

    // bonus classes

    abstract class Bonus : GameObject, IGameObject
    {
        public int Award { get; set; }

        public Bonus(Position position, int awardValue) : base(position)
        {
            this.Award = awardValue;
        }
    }

    class Apple : Bonus
    {
        private const int AwardValue = 1;

        public Apple(Position position) : base(position, AwardValue)
        {
        }
    }

    class Cherry : Bonus
    {
        private const int AwardValue = 2;

        public Cherry(Position position) : base(position, AwardValue)
        {
        }
    }

    class Bananas : Bonus
    {
        private const int AwardValue = 3;

        public Bananas(Position position) : base(position, AwardValue)
        {
        }
    }


    //obstacle classes

    abstract class Obstacle : GameObject, IGameObject
    {
        public int Damage { get; set; }

        public Obstacle(Position position, int damageValue) : base(position)
        {
            this.Damage = damageValue;
        }        
    }

    class Stone: Obstacle
    {
        private const int damageValue = 1;

        public Stone(Position position): base(position, damageValue)
        {

        }
    }

    class Tree : Obstacle
    {
        private const int damageValue = 2;

        public Tree(Position position) : base(position, damageValue)
        {

        }
    }

    struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    } 


}
