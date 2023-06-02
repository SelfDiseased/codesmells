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
        for (int i = 0; i < m_planes.Length; i++)
        {
          result += getPlaneSpeed(m_planes[i]);
        }
        if (m_planes.Length > 0)
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