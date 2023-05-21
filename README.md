# codesmells

### Завдання 1. Рефакторинг на рівні окремих операторів

Наступна функція перевіряє, чи немає підозрілих осіб у списку осіб, імена яких були захаркоджені:

```
void checkSecurity(String[] people) {
  boolean found = false;
  for (int i = 0; i < people.Length; i++) {
    if (!found) {
      if (people[i].Equals("Don")) {
        sendAlert();
        found = true;
      }
      if (people[i].Equals("John")) {
        sendAlert();
        found = true;
      }
    }
  }
}
```

Помічені "запахи коду":

```
void checkSecurity(String[] people) {
  boolean found = false;                          // забруднювач коду - мертва змінна
  for (int i = 0; i < people.Length; i++) {       // роздувальщик коду, який робить метод більшим
    if (!found) {
      if (people[i].Equals("Don")) {
        sendAlert();
        found = true;
      }
      if (people[i].Equals("John")) {              // забруднювач коду - дублювання коду
        sendAlert();
        found = true;
      }
    }
  }
}
```

Рефакторинг:

1. Видалення змінної found та заміна на return, оскільки при знаходженні захардкодженої людини викликається sendAlert і цикл завершується;
2. Покращення циклу шляхом використання foreach;
3. Прибрати дублювання коду, виносячи логіку перевірки хардкоду в окрему функцію.

```
void checkSecurityRefactored(string[] people) {
  foreach (string person in people) {
    if (isSuspiciousPerson(person)) {
      sendAlert();
      return;
    }
  }
}

bool isSuspiciousPerson(string person) {
  string[] suspiciousPeople = { "Don", "John" };
  return suspiciousPeople.Contains(person);
}
```

### Завдання 2. Рефакторинг на рівні даних

```
enum TransportType {
  eCar,
  ePlane,
  eSubmarine
};

class Transport {
  public:
    Transport(TransportType type) : m_type (type) {}

    int GetSpeed(int distance, int time) {
    if (time != 0) {
      switch(m_type) {
        case eCar:
            return distance/time;
        case ePlane:
            return distance/(time - getTakeOffTime() - getLandingTime());
        case eSubmarine:
            return distance/(time - getDiveTime() - getAscentTime());
        }
      }
    }
  ...
  private:
    int m_takeOffTime;
    int m_landingTime;
    int m_diveTime;
    int m_ascentTime;
    enum m_type;
};
```

Помічені "запахи коду":

```
num TransportType {
  eCar,
  ePlane,
  eSubmarine
};

class Transport {
  public:
    Transport(TransportType type) : m_type (type) {}

    int GetSpeed(int distance, int time) {
    if (time != 0) {
      switch(m_type) {                                                      // порушник об*єктного дизайну - оператор switch
        case eCar:
            return distance/time;
        case ePlane:
            return distance/(time - getTakeOffTime() - getLandingTime());
        case eSubmarine:
            return distance/(time - getDiveTime() - getAscentTime());
        }
      }
    }
  ...
  private:
    int m_takeOffTime;                                                      // відсутність методів доступу до приватних змінних, також
    int m_landingTime;                                                      // порушники об*єктного дизайну - довгий список параметрів,
    int m_diveTime;                                                         // які потрібно сетити через конструктор
    int m_ascentTime;
    enum m_type;
};
```

Рефакторинг:

1. Наслідування елементів enum TransportType від класу Transport;
2. Зміна логіки публічного методу;
3. Заміна конструкції switch;
4. Заміна кодування типу класом;
5. Написання гетерів для доступу до приватних полів;

```
class Transport {
  public:
    virtual const int GetSpeed(int distance, int time) = 0;
};

class Car : public Transport {
  public:
    override const int GetSpeed(int distance, int time) {
      if (time == 0) {
        return 0;
      }
      return distance / time;
    }
};

class Plane : public Transport {
  public:
    Plane(int takeOffTime, int landingTime)
      : m_takeOffTime(takeOffTime), m_landingTime(landingTime) {}

    override const int GetSpeed(int distance, int time) {
      if (time == 0) {
        return 0;
      }
      time -= GetTakeOffTime() + GetLandingTime();
      return distance / time;
    }

    const int GetTakeOffTime() { return m_takeOffTime; }
    const int GetLandingTime() { return m_landingTime; }

  private:
    int m_takeOffTime;
    int m_landingTime;
};

class Submarine : public Transport {
  public:
    Submarine(int diveTime, int ascentTime)
      : m_diveTime(diveTime), m_ascentTime(ascentTime) {}

    override const int GetSpeed(int distance, int time) {
      if (time == 0) {
        return 0;
      }
      return distance / (time - GetDiveTime() - GetAscentTime());
    }

    const int GetDiveTime() { return m_diveTime; }
    const int GetAscentTime() { return m_ascentTime; }

  private:
    int m_diveTime;
    int m_ascentTime;
};

```
