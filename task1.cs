class Animal
{
  public:
    Animal() {}
    ...
};

class Dog : public Animal
{
  public:
    Dog(int age) : Animal()
    {
      m_age = age;
    }

    int GetAge() { return m_age; }
    ...
  private:
    int m_age;
};

class Cat : public Animal
{
  public:
    Cat (int age) : Animal()
    {
      m_age = age;
    }

    int GetAge() { return m_age; }
    ...
  private:
    int m_age;
};