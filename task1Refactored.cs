interface Alive
{
  int GetAge();
}

class Animal : Alive
{
  public:
    Animal(int age)
    {
      m_age = age;
    }

    int GetAge() { return m_age; }

  private:
    int m_age;
};

class Dog : public Animal
{
  public:
    Dog(int age) : base(age) {}
};

class Cat : public Animal
{
  public:
    Cat(int age) : base(age) {}
};