# codesmells

### Завдання 1. Рішення задач узагальнення

```
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
```

Помічені "запахи коду":

```
class Animal                        // забруднювач коду - клас даних
{
  public:
    Animal() {}
    ...
};

class Dog : public Animal           // роздувальщик - великий клас
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

    int GetAge() { return m_age; }  // забруднювач коду - дублювання коду
    ...
  private:
    int m_age;
};
```

Рефакторинг:

1. Підйом приватного поля m_age
2. Підйом гетера GetAge та конструктора
3. Відокремлення інтерфейсу Alive

```
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
```

### Завдання 2. Рефакторинг на рівні окремих операторів

```
int getSpeed()
{
  int result = 0;
  if (isTransport())
  {
    if(isCar)
    {
      result = getCarSpeed();
    }
    else
    {
      if(isPlane)
      {
        for (int i = 0; i <> m_planes.Length; i++)
        {
          result += getPlaneSpeed(m_planes[i]);
        }
        if (m_planes.Length <> 0)
        {
          result = result / m_planes.Length;
        }
      }
      else
      {
        if(isBoat)
        {
          result = getBoatSpeed();
        }
      }
    }
  }
  else
  {
    result = getManSpeed();
  }
  return result;
}
```

Помічені "запахи коду":

```
int getSpeed()                                            // роздувальщик - довгий метод
{
  int result = 0;                                         // забруднювач коду - непотрібна змінна
  if (isTransport())
  {
    if(isCar)
    {
      result = getCarSpeed();
    }
    else                                                  // роздувальщик коду - непотрібне обгортання в блок
    {
      if(isPlane)
      {
        for (int i = 0; i < m_planes.Length; i++)         // роздувальщик коду - довгий метод (як реалізація циклу, так і сам розрахунок швидкості літаків)
        {
          result += getPlaneSpeed(m_planes[i]);
        }
        if (m_planes.Length > 0)
        {
          result = result / m_planes.Length;
        }
      }
      else                                                // роздувальщик коду - непотрібне обгортання в блок
      {
        if(isBoat)
        {
          result = getBoatSpeed();
        }
      }
    }
  }
  else                                                    // роздувальщик коду - можна перемістити наверх й позбутися від великого блоку
  {
    result = getManSpeed();
  }
  return result;
}
```

Рефакторинг:

1. Позбуваємося від непотрібної змінної result, замість неї використовуємо return всередині кожної умови
2. Переміщуємо перевірку !isTransport() наверх, щоб великий блок перевірки транспортів винести на верхній рівень, а також створюємо окрему булеву змінну isTransport
3. Прибираємо непотрібні блоки else для літаків та човнів
4. Помічаємо, що розрахунок середньої швидкості літака було заімплеменчено з використанням надто великої кількості коду
5. Використовуємо оператор switch замість багатьох іфів задля більшої лаконічності
6. Додаємо коментар для подальшої перевірки правильності розрахунку швидкості літаків

```
int getSpeed()
{
  switch() {
    case !isTransport():
      return getManSpeed();
    case isCar:
      return getCarSpeed();
    case isPlane:
      return getPlanesAverageSpeed();
    case isBoat:
      return getBoatSpeed();
  }
}

int getPlanesAverageSpeed() {
  int result = 0;

  foreach (PlaneType plane in m_planes)
  {
    // @TODO clarify if we really can sum speeds of different planes
    result += getPlaneSpeed(plane);
  }

  return result / m_planes.Length;
}
```
