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
